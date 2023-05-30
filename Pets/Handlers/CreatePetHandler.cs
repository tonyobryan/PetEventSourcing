using MediatR;
using Pets.API.Infastructure;
using Pets.API.Request;

namespace Pets.API.Handlers
{
    public class CreatePetHandler : IRequestHandler<CreatePetRequest>
    {
        private readonly PetDbContext dbContext;

        public CreatePetHandler(PetDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Handle(CreatePetRequest request, CancellationToken cancellationToken)
        {
            // TODO: Validate input, fluent validation?

            await dbContext.AddAsync(new Models.Pet
            {
                Age = request.Age,
                Breed = request.Breed,
                DateOfBirth = request.DateOfBirth,
                Name = request.Name,
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
