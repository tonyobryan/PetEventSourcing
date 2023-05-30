using MediatR;
using Pet.Bus.Http;
using Pets.API.Events;
using Pets.API.Infastructure;
using Pets.API.Request;

namespace Pets.API.Handlers
{
    public class AdoptionHandler : IRequestHandler<AdoptionRequest>
    {
        private readonly PetDbContext dbContext;

        public AdoptionHandler(PetDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Handle(AdoptionRequest request, CancellationToken cancellationToken)
        {
            var pet = dbContext.Pets.FirstOrDefault(x => x.Id == request.PetId);
            if (pet == default)
            {
                //TODO: throw? return a status code?
                return;
            }

            pet.FlaggedForAdoption = true;
            await dbContext.SaveChangesAsync();

            PetBus.PublishMessage("PetAdopted.Connection", new BusMessage("Pets.API", new PetAdoptedEvent(pet)));
        }
    }
}
