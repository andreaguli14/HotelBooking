using System;
using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Models
{
    public class ExtraService
    {
        [Key]
        public int Id { get; set; }

        public int Booking_Id { get; set; }

        public bool InRoomBreakFast { get; set; } = false;

        public string? InRoomBreakFast_Date { get; set; }

        public double InRoomBreakFast_Price { get; set; } = 0;



        public bool Minibar { get; set; } = false;

        public string? Minibar_Date { get; set; }

        public double Minibar_Price { get; set; } = 0;



        public bool Internet { get; set; } = false;

        public string? Internet_Date { get; set; }

        public double Internet_Price { get; set; } = 0;



        public bool ExtraBed { get; set; } = false;

        public string? ExtraBed_Date { get; set; }

        public double ExtraBed_Price { get; set; } = 0;



        public bool Crib { get; set; } = false;

        public string? Crib_Date { get; set; }

        public double Crib_Price { get; set; } = 0;
    }
}

