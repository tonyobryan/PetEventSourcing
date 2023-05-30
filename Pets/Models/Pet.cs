namespace Pets.API.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool FlaggedForAdoption { get; set; } = false;
        public bool TransferedToHospital { get; set; } = false;
    }
}
