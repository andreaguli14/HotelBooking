using Microsoft.AspNetCore.Mvc;
using HotelBooking.Models;
using HotelBooking.Data;
using Microsoft.AspNetCore.Authorization;

namespace HotelBooking.Controllers;

[Authorize(Roles = "Manager")]
public class CustomersController : Controller
{
   
    private ApplicationDbContext _db;
    public CustomersController(ApplicationDbContext context)
    {
        _db = context;

    }

    public IActionResult Index()
    {
        var model = _db.Customers.ToList();
        return View(model);
    }

    public IActionResult AddCustomer() {

        return View();
    }

    [HttpPost]
    public IActionResult AddCustomer(Customer model)
    {
        _db.Customers.Add(model);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult DeleteCustomer(int id)
    {
        var user = _db.Customers.FirstOrDefault(x => x.Id.Equals(id));
        _db.Customers.Remove(user);
        if (user.Room_Id != null)
        {
            var room = _db.Rooms.FirstOrDefault(x => x.Customer_Id.Equals(id));
            room.Customer_Id = null;
        }
        _db.SaveChanges();

        return RedirectToAction("Index");

    }


    public IActionResult EditCustomer(int id)
    {
        var model = _db.Customers.FirstOrDefault(x => x.Id == id);
        return View(model);
    }

    [HttpPost]
    public IActionResult EditCustomer(Customer model, int id)
    {
        var user = _db.Customers.FirstOrDefault(x => x.Id.Equals(id));

        user.CF = model.CF;
        user.Name = model.Name;
        user.Surname = model.Surname;
        user.City = model.City;
        user.Province = model.Province;
        user.Mail = model.Mail;
        user.Phone = model.Phone;

        _db.SaveChanges();
        return RedirectToAction("Index");
    }

}

