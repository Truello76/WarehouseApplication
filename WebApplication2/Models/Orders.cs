using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        [Required(ErrorMessage = "Proszę podać adres dostawy")]
        public string Address { get; set; }
        public decimal PriceTotal { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Required(ErrorMessage = "Proszę podać datę zamówienia")]
        public DateTime OrderDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ShipmentDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DeliveryDate { get; set; }
        public string PaymentStatus { get; set; }
        public string DeliveryStatus { get; set; }
        public string Note { get; set; }
        public ICollection<OrdersItems> ItemListId { get; set; }
        public Clients Client { get; set; }
    }

    public class OrdersItems
    {
        public int Id { get; set; }
        public int ItemsCount { get; set; }
        public Orders IDOrders { get; set; }
        public Items IDItems { get; set; }
    }

    public class OrdersCreateModel
    {
        public Orders Orders { get; set; }
        public SelectList ItemList { get; set; }
        [Required(ErrorMessage = "Co najmniej jeden rodzaj przedmiotu jest wymagany")]
        public List<string> ItemListConfirm { get; set; }
        public SelectList ClientList { get; set; }
        [Required(ErrorMessage = "Klient jest wymagany")]
        public string ClientConfirm { get; set; }
        [Required(ErrorMessage = "Co najmniej jeden przedmiot danego rodzaju jest wymagany")]
        public List<int> ItemCountList { get; set; }
    }

    public class Reports
    {
        public List<int> Stats { get; set; }
        public decimal IncomeTotal { get; set; }
        public string Filter { get; set; }
    }
}
