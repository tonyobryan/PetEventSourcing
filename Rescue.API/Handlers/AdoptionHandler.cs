using MediatR;
using Rescue.API.Infastructure;
using Rescue.API.Models;
using Rescue.API.Request;

namespace Rescue.API.Handlers
{
    public class AdoptionHandler : IRequestHandler<AdoptionRequest>
    {
        private readonly RescueDbContext dbContext;

        public AdoptionHandler(RescueDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Handle(AdoptionRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                // TODO: Retrun an error / log
                return;
            }

            var model = new RescuedAnimal
            {
                Id = request.Id,
                PetId = request.PetId,
            };

            dbContext.RescuedAnimals.Add(model);
            await dbContext.SaveChangesAsync();
        }
    }
}
