using MediatR;

namespace Pets.API.Request
{
    public class AdoptionRequest : IRequest
    {
        public int PetId { get; set; }
    }
}
