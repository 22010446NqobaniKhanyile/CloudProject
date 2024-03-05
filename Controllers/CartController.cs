using Cloud22010446_Dut4life.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cloud22010446_Dut4life.Controllers
{
    public class CartController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        Designs _mdal = new Designs();
        DataTable dt;


        //browse products categorized efficiently
        public ActionResult Index()
        {
            string mycmd = "select * from Designs";
            dt = new DataTable();

            dt = _mdal.SelactAll(mycmd);


            List<Designs> list = new List<Designs>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Designs DesgObT = new Designs();
                DesgObT.DesignID = Convert.ToInt32(dt.Rows[i]["DesignID"]);
                DesgObT.DesignName = dt.Rows[i]["DesignName"].ToString();
                DesgObT.Price = Convert.ToDouble(dt.Rows[i]["Price"]);
                DesgObT.Url = dt.Rows[i]["Url"].ToString();
                DesgObT.Url = dt.Rows[i]["Url"].ToString();
                list.Add(DesgObT);
            }
            return View(list);

        }

      




        //action to add item to cart
        [HttpPost]
        public ActionResult Add(Designs DesignObj)
        {
            if (Session["cart"] == null)
            {
                List<Designs> SecObj = new List<Designs>();
                SecObj.Add(DesignObj);
                Session["cart"] = SecObj;
                ViewBag.cart = SecObj.Count();
                Session["count"] = DesignObj.Quantity; // Set the count based on quantity
            }
            else
            {
                List<Designs> SecObj = (List<Designs>)Session["cart"];
                // Check if the item already exists in the cart
                bool itemExists = false;
                foreach (var item in SecObj)
                {
                    if (item.DesignID == DesignObj.DesignID)
                    {
                        itemExists = true;
                        // If the item exists, increase its quantity
                        item.Quantity += DesignObj.Quantity;
                        Session["count"] = Convert.ToInt32(Session["count"]) + DesignObj.Quantity; // Increase the count by the item's quantity

                        break;
                    }
                }
                if (!itemExists)
                {
                    SecObj.Add(DesignObj);
                    Session["count"] = Convert.ToInt32(Session["count"]) + DesignObj.Quantity; // Increase the count by the item's quantity
                }
                Session["cart"] = SecObj;
                ViewBag.cart = SecObj.Count();
            }
            return RedirectToAction("CustomerHomePage", "Home");
        }


        //action to add item to cart log out
        [HttpPost]
        public ActionResult ExternalAdd(Designs DesignObj)

        {
            if (Session["cart"] == null)
            {
                List<Designs> SecObj = new List<Designs>();
                SecObj.Add(DesignObj);
                Session["cart"] = SecObj;
                ViewBag.cart = SecObj.Count();
                Session["count"] = DesignObj.Quantity; // Set the count based on quantity
            }
            else
            {
                List<Designs> SecObj = (List<Designs>)Session["cart"];
                // Check if the item already exists in the cart
                bool itemExists = false;
                foreach (var item in SecObj)
                {
                    if (item.DesignID == DesignObj.DesignID)
                    {
                        itemExists = true;
                        // If the item exists, increase its quantity
                        item.Quantity += DesignObj.Quantity;
                        Session["count"] = Convert.ToInt32(Session["count"]) + DesignObj.Quantity; // Increase the count by the item's quantity

                        break;
                    }
                }
                if (!itemExists)
                {
                    SecObj.Add(DesignObj);
                    Session["count"] = Convert.ToInt32(Session["count"]) + DesignObj.Quantity; // Increase the count by the item's quantity
                }
                Session["cart"] = SecObj;
                ViewBag.cart = SecObj.Count();
            }
            return RedirectToAction("login", "Account");
        }

        //view cart 
        public ActionResult Checkout()
        {
            if (Session["cart"] == null)
            {
                return RedirectToAction("Index", "Cart");

            }
            return View((List<Designs>)Session["cart"]);

        }
        //retrieve total amount
        public PartialViewResult Total()

        {
            
            return PartialView((List<Designs>)Session["cart"]);

        }

        public ActionResult MyOrder(Order orderObject)

        {
            // Retrieve the list of items from the session
            List<Designs> items = Session["cart"] as List<Designs>;

            // Check if the list is not empty
            if (items != null && items.Any())
            {
                var username = System.Web.HttpContext.Current.User.Identity.Name; ;

                foreach (var item in items)
                {
                    // Create a new instance of Orders for each item

                    orderObject. Email = username;
                     orderObject.DesignName = item.DesignName;
                    orderObject.Quantity = item.Quantity;
                    orderObject.Price = item.Price*item.Quantity;
                    orderObject.Url = item.Url;
                    

                    // Add the item to the database context
                    db.Orders.Add(orderObject);
                    // Save changes to the database (outside the loop)
                    db.SaveChanges();
                }

                

                // Optionally, redirect to another action or view
                return RedirectToAction("Index", "Orders");
            }

            // Handle the case where the list is empty
            return RedirectToAction("Index", "Cart");
        }



        //Delete items
        public ActionResult Remove(Designs DegOb)
        {
            List<Designs> cartItems = (List<Designs>)Session["cart"];

            // Find the index of the item to remove
            int indexToRemove = cartItems.FindIndex(x => x.DesignID == DegOb.DesignID);

            // Check if the item is found
            if (indexToRemove != -1)
            {
                // Remove the item from the list
                cartItems.RemoveAt(indexToRemove);

                // Update the session and count
                Session["cart"] = cartItems;
                Session["count"] = Convert.ToInt32(Session["count"]) - DegOb.Quantity;
            }

            return RedirectToAction("Index", "Cart");
        }





    }
}
