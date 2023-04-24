using System;
using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        public string CF { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string City { get; set; }

        public string? Province { get; set; }

        public string Mail { get; set; }

        public double? Phone { get; set; }

        public int? Room_Id { get; set; }
         
        public int? Booking_Id { get; set; }



    }
}

