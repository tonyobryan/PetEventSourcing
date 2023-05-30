using MediatR;

namespace Hospital.Domain.Events
{
    public record PetAdmittedEvent(int PetId) : IRequest, IEvent;
}
