using MediatR;

namespace Hospital.Domain.Events
{
    public record PetDischargedEvent(int PetId) : IRequest, IEvent;
}
