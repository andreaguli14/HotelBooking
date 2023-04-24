using Microsoft.AspNetCore.Mvc;
using HotelBooking.Models;
using HotelBooking.Data;
using Microsoft.AspNetCore.Authorization;


namespace HotelBooking.Controllers;

[Authorize(Roles = "Manager")]
public class RoomsController : Controller
{

    private ApplicationDbContext _db;
    public RoomsController(ApplicationDbContext context)
    {
        _db = context;

    }

    public IActionResult Index()
    {
        var model = _db.Rooms.ToList();

        return View(model);
    }

    public IActionResult AddRoom()
    {
       
        return View();
    }

    [HttpPost]
    public IActionResult AddRoom(Room model)
    {
        _db.Rooms.Add(model);
        _db.SaveChanges();

        return RedirectToAction("Index");
    }


    public IActionResult DeleteRoom(int id)
    {
        var room = _db.Rooms.FirstOrDefault(x => x.Id.Equals(id));
        _db.Rooms.Remove(room);
        if(room.Customer_Id != null) {
            var user = _db.Customers.FirstOrDefault(x => x.Room_Id.Equals(id));
            user.Room_Id = null;
        }
        _db.SaveChanges();

        return RedirectToAction("Index");
        
    }

    public IActionResult EditRoom(int id)
    {
        var model = _db.Rooms.FirstOrDefault(x => x.Id == id);
        return View(model);
    }

    [HttpPost]
    public IActionResult EditRoom(Room model, int id)
    {
        var room = _db.Rooms.FirstOrDefault(x => x.Id.Equals(id));
        room.Number = model.Number;
        room.Description = model.Description;
        room.Type = model.Type;

        _db.SaveChanges();
        return RedirectToAction("Index");
    }



}

