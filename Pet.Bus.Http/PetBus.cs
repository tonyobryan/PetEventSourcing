using Newtonsoft.Json;
using Pet.Bus.Http;
using System.Collections.Concurrent;
using System.Text;

public class PetBus
{
    private static readonly HttpClient httpClient = new();
    private static readonly ConcurrentQueue<InturnalMessage> messages = new();
    private static List<Subscriber> subscribers = new();
    private static readonly object lockObject = new();

    private static void Main(string[] args)
    {
        ConfigureSender();

        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.MapGet("/", () => "Hello PetBus!");
        app.MapPost("/PublishMessage", (InturnalMessage message) =>
        {
            messages.Enqueue(message);
        });

        app.Run("https://localhost:7004");
    }

    public static void PublishMessage(string connectionString, BusMessage message)
    {
        httpClient.PostAsync("https://localhost:7004/PublishMessage", new StringContent(JsonConvert.SerializeObject(new InturnalMessage(connectionString, message)), Encoding.UTF8, "application/json"));
    }

    private static void ConfigureSender()
    {
        string configJson = File.ReadAllText(Environment.CurrentDirectory + "\\SubscriberConfig.json"); //TODO: Local paths / embeded file?
        subscribers = JsonConvert.DeserializeObject<List<Subscriber>>(configJson);

        var timer = new System.Timers.Timer(10000); // TODO: Make this configerable.
        timer.Elapsed += Send;
        timer.Enabled = true;
        timer.AutoReset = true;
        timer.Start();
    }

    private static void Send(object? sender, System.Timers.ElapsedEventArgs e)
    {
        lock (lockObject)
        {
            while (messages.TryDequeue(out var message))
            {
                // TODO: Error handling / Deadletter?
                SendToSubscribers(message); 
            }
        }
    }

    private static void SendToSubscribers(InturnalMessage message)
    {
        Subscriber[] subs = subscribers.Where(x => x.ConnectionString == message.ConnectionString).ToArray();
        foreach (Subscriber s in subs)
        {
            //TODO: Take a beat between messages?
            SendMessage(s.CallbackUri, message);
        }
    }

    private static void SendMessage(Uri callbackUri, BusMessage message)
    {
        if (!string.IsNullOrWhiteSpace(callbackUri?.ToString()))
        {
            var response = httpClient.PostAsync(callbackUri, new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json")).Result;//JsonContent.Create(message));
        }
    }
}