using Microsoft.AspNetCore.Mvc;
using HotelBooking.Models;
using HotelBooking.Data;
using Microsoft.AspNetCore.Authorization;

namespace HotelBooking.Controllers;


[Authorize(Roles ="Manager")]
public class BookingsController : Controller
{

    private ApplicationDbContext _db;
    public BookingsController(ApplicationDbContext context)
    {
        _db = context;

    }

    public IActionResult Index(string? CF, string? type)
    {

        if (CF != null)
        {
            try
            {
                var user = _db.Customers.FirstOrDefault(x => x.CF.Equals(CF));
                var model = _db.Bookings.Where(x => x.Customer_Id.Equals(user.Id)).ToList();
                return View(model);
            }
            catch (Exception e) { return BadRequest("User Not Found"); }
        }else if(type != null && type != "All")
        {
            try
            {
                var model = _db.Bookings.Where(x => x.Type.Equals(type)).ToList();
                return View(model);
            }
            catch (Exception e) { return BadRequest("User Not Found"); }
        }
        else
        {
            var model = _db.Bookings.ToList();
            return View(model);
        }
       
    }


    public IActionResult AddBooking()
    {
        var model = new BookingPage();
        model.customer = _db.Customers.ToList();
        model.room = _db.Rooms.ToList();

        return View(model);
    }

    [HttpPost]
    public IActionResult AddBooking(BookingPage model)
    {
        _db.Bookings.Add(model.booking);
        model.booking.Date = DateTime.Now.ToString();
        _db.SaveChanges();

        model.extraService.Booking_Id = _db.Bookings.OrderBy(x => x.Id).Last().Id;

        if (model.extraService.InRoomBreakFast) model.extraService.InRoomBreakFast_Date = DateTime.Now.ToString();
        if (model.extraService.Internet) model.extraService.Internet_Date = DateTime.Now.ToString();
        if (model.extraService.ExtraBed) model.extraService.ExtraBed_Date = DateTime.Now.ToString();
        if (model.extraService.Crib) model.extraService.Crib_Date = DateTime.Now.ToString();
        if (model.extraService.Minibar) model.extraService.Minibar_Date = DateTime.Now.ToString();

        _db.ExtraServices.Add(model.extraService);

        var user = _db.Customers.FirstOrDefault(x => x.Id.Equals(model.booking.Customer_Id)) ;
        user.Booking_Id = _db.Bookings.OrderBy(x => x.Id).Last().Id;
        user.Room_Id = model.booking.Room_Id;

        var room =  _db.Rooms.FirstOrDefault(x => x.Id.Equals(model.booking.Room_Id));
        room.Booking_Id = _db.Bookings.OrderBy(x => x.Id).Last().Id;
        room.Customer_Id = model.booking.Customer_Id;
        room.Busy = true;

        _db.SaveChanges();


        return RedirectToAction("Index");
    }

     
    public  IActionResult DeleteBooking(int id)
    {
        try
        {
            var booking = _db.Bookings.FirstOrDefault(x => x.Id.Equals(id));
            var service = _db.ExtraServices.FirstOrDefault(x => x.Booking_Id.Equals(id));
            try
            {
                var user = _db.Customers.FirstOrDefault(x => x.Booking_Id.Equals(id));
                user.Booking_Id = null;
                user.Room_Id = null;
            }
            catch (Exception e) { }
            try
            {
                var room = _db.Rooms.FirstOrDefault(x => x.Booking_Id.Equals(id));
                room.Booking_Id = null;
                room.Customer_Id = null;
                room.Busy = false;
            }
            catch (Exception e) { }

            _db.ExtraServices.Remove(service);
            _db.Bookings.Remove(booking);


            _db.SaveChanges();
        }catch(Exception e) { }
        return RedirectToAction("Index");
    }


    public IActionResult Checkout(int id)
    {

        try
        { 
            var booking = _db.Bookings.FirstOrDefault(x => x.Id.Equals(id));
            booking.Checkout = true;
        }
        catch(Exception e){ }
        try
        {
            var user = _db.Customers.FirstOrDefault(x => x.Booking_Id.Equals(id));
            user.Booking_Id = null;
            user.Room_Id = null;
        }
        catch(Exception e){ }
        try
        {
            var room = _db.Rooms.FirstOrDefault(x => x.Booking_Id.Equals(id));
            room.Booking_Id = null;
            room.Customer_Id = null;
            room.Busy = false;
        }
        catch (Exception e) { }
        _db.SaveChanges();

        return RedirectToAction("PrintPage" ,new{ id= id});
    }

    public IActionResult PrintPage(int id)
    {
        var booking = _db.Bookings.FirstOrDefault(x => x.Id == id);
        var extraservice = _db.ExtraServices.FirstOrDefault(x => x.Booking_Id == id);
        var room = _db.Rooms.FirstOrDefault(x => x.Id == booking.Room_Id);
        var customer = _db.Customers.FirstOrDefault(x => x.Id == booking.Customer_Id);

        var model = new PrintPage();

        model.booking = booking;
        model.extraService = extraservice;
        model.room = room;
        model.customer = customer;

        return View(model);
    }

    public IActionResult EditBooking(int id)
    {
        var booking = _db.Bookings.FirstOrDefault(x => x.Id == id);
        var extraservice = _db.ExtraServices.FirstOrDefault(x => x.Booking_Id == id);
        var rooms = _db.Rooms.ToList();
        var user = _db.Customers.ToList();

        var model = new BookingPage();

        model.booking = booking;
        model.extraService = extraservice;
        model.room = rooms;
        model.customer = user;


        return View(model);
    }

    [HttpPost]
    public IActionResult EditBooking(BookingPage model, int id)
    {
        var booking = _db.Bookings.FirstOrDefault(x => x.Id.Equals(id));

        booking.Customer_Id = model.booking.Customer_Id;
        booking.Room_Id = model.booking.Room_Id;
        booking.Type = model.booking.Type;
        booking.Date_From = model.booking.Date_From;
        booking.Date_To = model.booking.Date_To;
        booking.Deposit = model.booking.Deposit;
        booking.Price = model.booking.Price;


        var extraservice = _db.ExtraServices.FirstOrDefault(x => x.Booking_Id.Equals(id));

        if (extraservice.Internet != model.extraService.Internet && model.extraService.Internet) extraservice.Internet_Date = DateTime.Now.ToString();
        if (extraservice.Minibar != model.extraService.Minibar && model.extraService.Minibar) extraservice.Minibar_Date = DateTime.Now.ToString();
        if (extraservice.InRoomBreakFast != model.extraService.InRoomBreakFast && model.extraService.InRoomBreakFast) extraservice.InRoomBreakFast_Date = DateTime.Now.ToString();
        if (extraservice.ExtraBed != model.extraService.ExtraBed && model.extraService.ExtraBed) extraservice.ExtraBed_Date = DateTime.Now.ToString();
        if (extraservice.Crib != model.extraService.Crib && model.extraService.Crib) extraservice.Crib_Date = DateTime.Now.ToString();

        extraservice.Internet = model.extraService.Internet;
        extraservice.Internet_Price = model.extraService.Internet_Price;
        extraservice.Minibar = model.extraService.Minibar;
        extraservice.Minibar_Price = model.extraService.Minibar_Price;
        extraservice.InRoomBreakFast = model.extraService.InRoomBreakFast;
        extraservice.InRoomBreakFast_Price = model.extraService.InRoomBreakFast_Price;
        extraservice.ExtraBed = model.extraService.ExtraBed;
        extraservice.ExtraBed_Price = model.extraService.ExtraBed_Price;
        extraservice.Crib = model.extraService.Crib;
        extraservice.Crib_Price = model.extraService.Crib_Price;


        _db.SaveChanges();
        return RedirectToAction("Index");
    }


}

