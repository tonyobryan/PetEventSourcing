using Dapper;
using Microsoft.Data.SqlClient;

const string dbConnection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Pets;Integrated Security=true;";
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello Pets!");
app.MapGet("/Pets", async () =>
{
    //TODO paging
    using var conn = new SqlConnection(dbConnection);
    var pets = (await conn.QueryAsync("SELECT * FROM Pets")).ToList();
    return Results.Ok(pets);

});
app.MapGet("/Pets/{petId}", async (int petId) =>
{
    using var conn = new SqlConnection(dbConnection);
    var pet = (await conn.QueryAsync("SELECT * FROM Pets WHERE Id = @PetId", new { PetId = petId })).ToList();
    return Results.Ok(pet);
});

app.Run("https://localhost:7003");
