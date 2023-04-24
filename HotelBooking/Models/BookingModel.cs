using System;
using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        public int Customer_Id { get; set; }

        public int Room_Id { get; set; }

        public string Date { get; set; }

        public string Date_From { get; set; }

        public string Date_To { get; set; }

        public double Deposit { get; set; }

        public double Price { get; set; }

        public string Type { get; set; }

        public int ExtraService_Id { get; set; }

        public bool Checkout { get; set; } = false;


    }
}

