using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using ToDoListApp.Models;

namespace ToDoListApp.Controllers
{
    public class HomeController : Controller
    {
      [HttpGet]
      public ActionResult Index()
      {
        return View();
      }
    }
}
