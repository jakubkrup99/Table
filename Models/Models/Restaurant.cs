namespace Table.DataAccess.Models
{
    public class Restaurant
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public TimeOnly OpeningHour { get; set; }

        public TimeOnly ClosingHour { get; set; }

    }
}
