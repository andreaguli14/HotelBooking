using System;
using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        public int Number { get; set; }

        public string Description { get; set; }

        public int? Customer_Id { get; set; }

        public int? Booking_Id { get; set; }

        public bool? Busy { get; set; } = false;

        public string Type { get; set; } 
    }
}

