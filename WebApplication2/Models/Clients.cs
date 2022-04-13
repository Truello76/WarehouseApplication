using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Clients
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Podaj imię klienta")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Podaj nazwisko klienta")]
        public string LastName { get; set; }
        public string Firm { get; set; }
        [Required(ErrorMessage = "Podaj e-mail klienta")]
        [RegularExpression("^[\\S]+[@][\\S]+[.][\\S]+$", ErrorMessage = "Niepoprawny format e-mail")]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
