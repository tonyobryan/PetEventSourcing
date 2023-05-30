using Rescue.API.Models;

namespace Rescue.API.Events
{
    public class PetAdoptedEvent
    {
        public int Id { get; set; }
        public int PetId { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public int Age { get; set; }
        //public RescuedAnimal RescuedAnimal { get; set; }
    }
}
