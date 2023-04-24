using System;
using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Models
{
    public class PrintPage
    {
        public Booking booking { get; set; }

        public ExtraService extraService { get; set; }

        public Customer customer { get; set; }

        public Room room { get; set; }

    }
}

