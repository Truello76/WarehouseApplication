using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Items
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage = "Podaj nieujemną liczbę przedmiotów")]
        public int ItemCount { get; set; }
        [Required(ErrorMessage = "Podaj nieujemną cenę")]
        [Range(0.0, Double.PositiveInfinity, ErrorMessage = "Cena musi być nieujemna")]
        [RegularExpression("^[0-9]+\x2E[0-9]{2}$", ErrorMessage = "Błędna wartość")]
        public decimal Price { get; set; }
        public string ShelfCode { get; set; }
        public bool Fragile { get; set; }
    }
}
