using MediatR;

namespace Pets.API.Request
{
    public class CreatePetRequest : IRequest
    {
        public string Name { get; set; }
        public string Breed { get; set; }
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
