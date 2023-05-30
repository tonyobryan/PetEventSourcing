namespace Pets.API.Events
{
    public class PetTransferedEvent
    {
        public PetTransferedEvent(Models.Pet petModel)
        {
            this.PetId = petModel.Id;
            this.Name = petModel.Name;
            this.Breed = petModel.Breed;
            this.Age = petModel.Age;
        }

        public int PetId { get; }
        public string Name { get; }
        public string Breed { get; }
        public int Age { get; }
    }
}
