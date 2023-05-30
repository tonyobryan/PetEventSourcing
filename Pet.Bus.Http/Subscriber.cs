namespace Pet.Bus.Http
{
    public class Subscriber
    {
        public string Name { get; set; }
        public string ConnectionString { get; set; }
        public Uri CallbackUri { get; set; }
    }
}
