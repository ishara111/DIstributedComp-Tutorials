namespace WebClient.Models
{
    public partial class Booking
    {
        public int Id { get; set; }
        public string name { get; set; }
        public Nullable<System.DateTime> startDate { get; set; }
        public Nullable<System.DateTime> endDate { get; set; }
        public Nullable<int> centreId { get; set; }
    }
}
