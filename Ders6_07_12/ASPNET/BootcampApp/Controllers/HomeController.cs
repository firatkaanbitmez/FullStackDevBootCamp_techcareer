using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BootcampApp.Models;

namespace BootcampApp.Controllers;

public class HomeController : Controller
{

    public IActionResult Index()
    {
          var kurs=new Bootcamp();
        kurs.Id=1;
        kurs.Title="Full Stack Bootcamp";
        kurs.Image="image.jpg";
        kurs.Description="Html css javascipt aspnet dersleri";
        return View(kurs);
    }
   

  
}
