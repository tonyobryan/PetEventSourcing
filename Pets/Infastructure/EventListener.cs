using Newtonsoft.Json;
using Pet.Bus;
using Pets.API.Events;

namespace Pets.API.Infastructure
{
    public class EventListener
    {
        public void Listen()
        {
            //PetAdoptedEvent.Handler.Register(e =>
            //{
            //    var message = new BusMessage("https://localhost:7001/PetAdoptedEvent", "Pet.Api", nameof(PetAdoptedEvent), JsonConvert.SerializeObject(e));
            //    var json = JsonConvert.SerializeObject(message);

            //    using var sw = File.AppendText(Constants.EventLogFullPath);
            //    sw.WriteLine(json);
            //});

            //PetTransferedEvent.Handler.Register(e =>
            //{
            //    //TODO: Refactor Send Logic
            //    //TODO: Dont use port magic numbers
            //    var message = new BusMessage("https://localhost:7002/PetTransferedEvent", "Pet.Api", nameof(PetTransferedEvent), JsonConvert.SerializeObject(e));
            //    var json = JsonConvert.SerializeObject(message);

            //    using var sw = File.AppendText(Constants.EventLogFullPath);
            //    sw.WriteLine(json);
            //});
        }
    }
}
