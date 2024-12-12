using System.ComponentModel.DataAnnotations;

namespace Table.DataAccess.Models
{
    public class Restaurant
    {
        public int Id { get; set; }

        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Display(Name = "Adres")]
        public string Address { get; set; }

        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Display(Name = "Numer telefonu")]
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        [Display(Name = "Zdjęcie")]
        public string ImageUrl { get; set; }

        [Display(Name = "Godzina otwarcia")]
        public TimeOnly OpeningHour { get; set; }
        [Display(Name = "Godzina zamknięcia")]
        public TimeOnly ClosingHour { get; set; }

    }
}
