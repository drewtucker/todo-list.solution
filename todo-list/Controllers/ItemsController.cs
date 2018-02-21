using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ToDoListApp.Models;

namespace ToDoListApp.Controllers
{
    public class ItemsController : Controller
    {

        [HttpGet("/")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet("/items/new")]
        public ActionResult CreateForm()
        {
            return View();
        }

        [HttpPost("/items/delete")]
        public ActionResult DeleteAll()
        {
          Item.DeleteAll();
          return View();
        }

        // [HttpGet("/categories/{categoryId}/items/new")]
        // public ActionResult CreateForm(int categoryId)
        // {
        //   Dictionary<string, object> model = new Dictionary<string, object>();
        //   Category category = Category.Find(categoryId);
        //   return View(category);
        // }
        //
        // [HttpGet("/categories/{categoryId}/items/{itemId}")]
        // public ActionResult Details(int categoryId, int itemId)
        // {
        //   Item item = Item.Find(itemId);
        //   Dictionary<string, object> model = new Dictionary<string, object>();
        //   Category category = Category.Find(categoryId);
        //   model.Add("item", item);
        //   model.Add("category", category);
        //   return View(item);
        // }
    }
}
