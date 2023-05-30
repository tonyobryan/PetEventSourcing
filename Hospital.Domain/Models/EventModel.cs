namespace Hospital.Domain.Models
{
    public class EventModel
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ObjectId { get; set; }
        public string TypeName { get; set; }
        public string Json { get; set; }
    }
}
