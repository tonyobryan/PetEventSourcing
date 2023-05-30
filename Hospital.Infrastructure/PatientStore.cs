using Hospital.Domain.Events;
using Hospital.Domain.Infastructure;
using Hospital.Domain.Models;
using Newtonsoft.Json;
using System;

namespace Hospital.Infrastructure
{
    public class PatientStore
    {
        private readonly EventStoreDbContext dbContext;

        public PatientStore(EventStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task SaveAsync<T>(string objectId, T eventItem, CancellationToken cancellationToken)
        {
            // TODO: inbound valudation
            // TODO: Should be going to cosmos, but save to SQL server for now

            dbContext.Events.Add(new EventModel
            {
                TypeName = typeof(T)?.AssemblyQualifiedName ?? "unknown",
                ObjectId = objectId,
                Json = JsonConvert.SerializeObject(eventItem),
            });
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public Patient Load(string objectId)
        { 
            if(objectId ==  null)
            {
                throw new ArgumentNullException(nameof(objectId));     
            }

            var events = dbContext.Events
                .Where(x => x.ObjectId == objectId)
                .ToList();

            var result = new Patient();

            foreach(EventModel e in events)
            {
                var type = Type.GetType(e.TypeName);
                if(type == null)
                {
                    //TODO: Log error
                    continue;
                }

                if (JsonConvert.DeserializeObject(e.Json, type) is not IEvent data)
                {
                    //TODO: Log error
                    continue;
                }

                result.ApplyStateFromEvent(data);
            }

            return result;
        }


    }
}