using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HotelBooking.Models;

namespace HotelBooking.Controllers;

public class CustomersController : Controller
{

    private Data.ApplicationDbContext _db;
    public CustomersController(Data.ApplicationDbContext context)
    {
        _db = context;

    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }


}

