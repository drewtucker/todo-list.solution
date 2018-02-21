using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ToDoListApp.Models;
using System;

namespace ToDoListApp.Controllers
{
    public class ItemsController : Controller
    {

        [HttpGet("/items")]
        public ActionResult Index()
        {
          List<Item> allItems = Item.GetAll();
          return View(allItems);
        }

        [HttpGet("/items/new")]
        public ActionResult CreateForm()
        {
            return View();
        }

        [HttpPost("/items")]
        public ActionResult Create()
        {
          Item newItem = new Item (Request.Form["new-item"], Int32.Parse(Request.Form["category-id"]));
          newItem.Save();
          List<Item> alItems = Item.GetAll();
          return RedirectToAction("Index");
        }


        [HttpGet("/items/{id}")]
        public ActionResult Details(int id)
        {
          Item item = Item.Find(id);
          return View(item);
        }

        [HttpGet("/items/{id}/update")]
        public ActionResult UpdateForm(int id)
        {
          Item thisItem = Item.Find(id);
          thisItem.Edit(Request.Form["newname"]);
          return RedirectToAction("Index");
        }

        [HttpPost("/items/{id}/update")]
        public ActionResult Update(int id)
        {
          Item thisItem = Item.Find(id);
          thisItem.Edit(Request.Form["newname"]);
          return RedirectToAction("Index");
        }

        [HttpPost("/items/delete")]
        public ActionResult DeleteAll()
        {
          Item.DeleteAll();
          return View();
        }
    }
}
