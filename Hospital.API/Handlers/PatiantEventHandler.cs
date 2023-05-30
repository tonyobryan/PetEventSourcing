using Hospital.Domain.Events;
using Hospital.Infrastructure;
using MediatR;

namespace Hospital.API.Handlers
{
    public class PatiantEventHandler : 
        IRequestHandler<SetPatiantWeightEvent>,
        IRequestHandler<SetPatiantBloodTypeEvent>,
        IRequestHandler<PetTransferedEvent>,
        IRequestHandler<PetAdmittedEvent>,
        IRequestHandler<PetDischargedEvent>
    {
        private readonly PatientStore patientStore;

        public PatiantEventHandler(PatientStore patientStore)
        {
            this.patientStore = patientStore;
        }

        public async Task Handle(SetPatiantWeightEvent request, CancellationToken cancellationToken)
        {
            await Handle(request.PetId, request, cancellationToken);
        }

        public async Task Handle(SetPatiantBloodTypeEvent request, CancellationToken cancellationToken)
        {
            await Handle(request.PetId, request, cancellationToken);
        }

        public async Task Handle(PetTransferedEvent request, CancellationToken cancellationToken)
        {
            await Handle(request.PetId, request, cancellationToken);
        }

        public async Task Handle(PetAdmittedEvent request, CancellationToken cancellationToken)
        {
            await Handle(request.PetId, request, cancellationToken);
        }

        public async Task Handle(PetDischargedEvent request, CancellationToken cancellationToken)
        {
            await Handle(request.PetId, request, cancellationToken);
        }

        private async Task Handle<T>(int patiantId, T patientEvent, CancellationToken cancellationToken)
        {
            if (patientEvent == null)
            {
                // TODO: Retrun an error / log
                return;
            }
            await patientStore.SaveAsync<T>(patiantId.ToString(), patientEvent, cancellationToken);
        }

    }
}
