using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab23.Models;

namespace Lab23.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Chris_CoffeeEntities ORM = new Chris_CoffeeEntities();
            ViewBag.Users = ORM.users.ToList();
            return View();
        }

        public ActionResult UserMenu()
        {
            Chris_CoffeeEntities ORM = new Chris_CoffeeEntities();
            ViewBag.Items = ORM.items.ToList();
            return View();
        }

        public ActionResult NewItem()
        {
            return View();
        }

        public ActionResult SaveNewItem(item newItem)
        {
            Chris_CoffeeEntities ORM = new Chris_CoffeeEntities();//need this in every action that interacts with the database!

            //to do: validation!! never skip!

            ORM.items.Add(newItem); // add new item to database

            ORM.SaveChanges(); // sync with the database / save changes to database

            return RedirectToAction("Index");
        }

        public ActionResult NewUser()
        {
            return View();
        }

        public ActionResult Registration(user newUser)
        {
            Chris_CoffeeEntities ORM = new Chris_CoffeeEntities();
            ORM.users.Add(newUser);
            ORM.SaveChanges();
            return RedirectToAction("UserMenu");
        }

        public ActionResult DeleteItem(int itemid)
        {
            Chris_CoffeeEntities ORM = new Chris_CoffeeEntities();
            // for loop to find the id
            // "Find()" is a method used to find objects using the primary key
            item ItemToDelete = ORM.items.Find(itemid);
            // remove
            ORM.items.Remove(ItemToDelete);
            // save changes
            ORM.SaveChanges(); // to do: use try and catch here

            return RedirectToAction("Index");
        }

        public ActionResult ItemDetails(int itemid)
        {
            //this shows the old data
            Chris_CoffeeEntities ORM = new Chris_CoffeeEntities();

            //find the item 
            item ItemToEdit = ORM.items.Find(itemid);


            //send it back to the view
            ViewBag.ItemToEdit = ItemToEdit;
            return View();
        }

        public ActionResult SaveChanges(item UpdatedItem)
        {
            Chris_CoffeeEntities ORM = new Chris_CoffeeEntities();
            //find the old record
            item OldRecord = ORM.items.Find(UpdatedItem.itemid);

            // to do: check for null

            OldRecord.name = UpdatedItem.name;
            OldRecord.description = UpdatedItem.description;
            OldRecord.quantity = UpdatedItem.quantity;
            OldRecord.price = UpdatedItem.price;

            ORM.Entry(OldRecord).State = System.Data.Entity.EntityState.Modified;

            ORM.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}