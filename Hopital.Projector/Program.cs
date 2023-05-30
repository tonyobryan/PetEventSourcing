using Dapper;
using Hospital.Domain.Infastructure;
using Hospital.Domain.Models;
using Hospital.Infrastructure;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, Hospital Projector!");

// TODO: Should really use DI...
// TODO: And maybe update patiants via rest api?
const string eventConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HospitalEvents;Integrated Security=true;";
const string hospitalConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Hospital;Integrated Security=true;";

var optionsBuilder = new DbContextOptionsBuilder<EventStoreDbContext>();
optionsBuilder.UseSqlServer(eventConnectionString);
var eventStore = new PatientStore(new EventStoreDbContext(optionsBuilder.Options));

// TODO: Make this configerable.
var timer = new System.Timers.Timer(10000); 
timer.Elapsed += Timer_Elapsed;
timer.Enabled = true;
timer.AutoReset = true;
timer.Start();

Console.ReadLine();

void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
{
    Console.WriteLine("Timer elapsed!");
    IEnumerable<Patient> patients = new List<Patient>();
    try
    {
        patients = RehidratePatients(GetEventObjectIds());
    }
    catch(SqlException ex)
    {
        Console.WriteLine(ex.Message);
    }
    
    using var conn = new SqlConnection(hospitalConnectionString);

    foreach (var patient in patients)
    {
        Console.WriteLine($"Updating Patent {patient.PetId}");
        conn.Execute(
            @"DELETE FROM Patients WHERE PetId = @PetId
            INSERT INTO Patients (PetId, Name, Breed, Age, Weight, Bloodtype, PatientStatus) VALUES
            (@PetId, @Name, @Breed, @Age, @Weight, @BloodType, @PatientStatus)", 
            new 
            { 
                patient.PetId,
                patient.Name,
                patient.Breed,
                patient.Age,
                patient.Weight,
                patient.BloodType,
                patient.PatientStatus
            });
    }
}

List<string> GetEventObjectIds()
{
    // TODO: Filter out this list to only updated work.
    using var conn = new SqlConnection(eventConnectionString);
    return conn.Query<string>("SELECT DISTINCT ObjectId FROM Events").ToList();
}

IEnumerable<Patient> RehidratePatients(List<string> objectIds)
{
    foreach (var objectId in objectIds)
    {
        yield return eventStore.Load(objectId);
    }
}