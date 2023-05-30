using MediatR;
using Rescue.API.Events;

namespace Rescue.API.Request
{
    public class AdoptionRequest : IRequest
    {
        public AdoptionRequest(PetAdoptedEvent adoptedEvent)
        {
            Id = adoptedEvent.Id;
            PetId = adoptedEvent.PetId;
            Name = adoptedEvent.Name;
            Breed = adoptedEvent.Breed;
            Age = adoptedEvent.Age;
        }

        public int Id { get; }
        public int PetId { get; }
        public string Name { get; }
        public string Breed { get; }
        public int Age { get; }
    }
}
