using System;
using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Models
{
    public class Booking
    {
        [Key]
        public string Id { get; set; }
    }
}

