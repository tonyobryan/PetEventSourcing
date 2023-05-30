using MediatR;

namespace Hospital.Domain.Events
{
    public record SetPatiantBloodTypeEvent(int PetId, string BloodType) : IRequest, IEvent;
}
