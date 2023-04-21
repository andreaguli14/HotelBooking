using System;
using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Models
{
    public class Room
    {
        [Key]
        public string Id { get; set; }
    }
}

