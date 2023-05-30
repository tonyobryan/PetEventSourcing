using MediatR;
using Pet.Bus.Http;
using Pets.API.Events;
using Pets.API.Infastructure;
using Pets.API.Request;

namespace Pets.API.Handlers
{
    public class TransferHandler : IRequestHandler<TransferRequest>
    {
        private readonly PetDbContext dbContext;

        public TransferHandler(PetDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Handle(TransferRequest request, CancellationToken cancellationToken)
        {
            var pet = dbContext.Pets.FirstOrDefault(x => x.Id == request.PetId);
            if (pet == default)
            {
                // TODO: Throw or return status
                return;
            }

            pet.TransferedToHospital = true;
            await dbContext.SaveChangesAsync();

            PetBus.PublishMessage("PetTransfered.Connection", new BusMessage("Pets.API", new PetTransferedEvent(pet)));
        }
    }
}
