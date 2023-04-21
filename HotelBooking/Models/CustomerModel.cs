using System;
using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Models
{
    public class Customer
    {
        [Key]
        public string Id { get; set; }
    }
}

