using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HotelBooking.Models;

namespace HotelBooking.Controllers;

public class RoomsController : Controller
{

    private Data.ApplicationDbContext _db;
    public RoomsController(Data.ApplicationDbContext context)
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

