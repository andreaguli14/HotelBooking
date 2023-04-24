using System;
using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Models
{
    public class BookingPage
    {
        public Booking booking { get; set; }

        public ExtraService extraService { get; set; }

        public List<Customer> customer { get; set; }

        public List<Room> room { get; set; }

    }
}

