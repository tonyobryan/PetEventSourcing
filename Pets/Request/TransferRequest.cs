using MediatR;

namespace Pets.API.Request
{
    public class TransferRequest : IRequest
    {
        public int PetId { get; set; }
    }
}
