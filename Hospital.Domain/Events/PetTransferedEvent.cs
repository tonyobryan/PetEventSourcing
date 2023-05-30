using MediatR;

namespace Hospital.Domain.Events
{
    public record PetTransferedEvent(int PetId, string Name, string Breed, int Age) : IRequest, IEvent;
}
