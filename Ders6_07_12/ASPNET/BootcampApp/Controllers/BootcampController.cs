
using BootcampApp.Models;
using Microsoft.AspNetCore.Mvc;


namespace BootcampApp.Controllers;

public class BootcampController: Controller
{

  
    public IActionResult List(){

        var kurslar=new List<Bootcamp>(){
            new Bootcamp(){Id=1,Title="Full Stack Developer",Image="image.jpg", Description="Html,css java scripot aspnet dersleri"},
            new Bootcamp(){Id=2,Title="Game BootCam",Image="image2.jpg",Description="Unity ile oyun geliştirme"},

            new Bootcamp(){Id=3,Title="Sql Bootcamp",Image="image3.jpg",Description="sql server kullanmak"},
             new Bootcamp(){Id=4,Title=" Bootcamp",Image="image4.jpg",Description="sql server kullanmak"},
        };
        return View(kurslar);
    }
}