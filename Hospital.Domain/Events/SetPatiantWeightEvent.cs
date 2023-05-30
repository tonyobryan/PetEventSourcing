using MediatR;

namespace Hospital.Domain.Events
{
    public record SetPatiantWeightEvent(int PetId, int Weight) : IRequest, IEvent;
}
