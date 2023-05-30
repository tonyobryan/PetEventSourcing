using Newtonsoft.Json;

namespace Pet.Bus.Http
{
    public class BusMessage
    {
        public BusMessage()
        {
        }

        public BusMessage(string from, object content)
        {
            From = from;
            Type = content.GetType().Name;
            JsonMessage = JsonConvert.SerializeObject(content);
        }

        public string From { get; set; }

        public string Type { get; set; }

        public string JsonMessage { get; set; }
    }

    //TODO: Make this inturnal
    public class InturnalMessage : BusMessage
    {
        public InturnalMessage() { }

        public InturnalMessage(string connectionString, BusMessage message)
        {
            ConnectionString= connectionString;
            From = message.From;
            Type = message.Type;
            JsonMessage = message.JsonMessage;
        }

        public string ConnectionString { get; set; }
    }
}
