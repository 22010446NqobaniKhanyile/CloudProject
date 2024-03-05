using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Cloud22010446_Dut4life.Models;

namespace Cloud22010446_Dut4life.Controllers
{
    public class PaymentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Payments
        public ActionResult Index()
        {
            return View(db.Payments.ToList());
        }

        // GET: Payments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // GET: Payments/Create
        public ActionResult Pay(Payment payment)
        {
            // Retrieve the list of items from the session
            List<Designs> items = Session["cart"] as List<Designs>;
            int sum = 0;


            foreach (var item in items)
            {
                sum = Convert.ToInt32(item.Price * item.Quantity) + sum;
            }
            payment.Amount = sum;

            return View(payment);
        }

        // POST: Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Pay([Bind(Include = "PayID,Email,CVV,Card,Amount,Date")] Payment payment, Order orderObject)
        {
            // Retrieve the list of items from the session
            List<Designs> items = Session["cart"] as List<Designs>;

            

            // Check if the list is not empty
            if (items != null && items.Any())
            {
                var username = System.Web.HttpContext.Current.User.Identity.Name; ;
                payment.Email = username;
                foreach (var item in items)
                {
                    // Create a new instance of Orders for each item

                    orderObject.Email = username;
                    orderObject.DesignName = item.DesignName;
                    orderObject.Quantity = item.Quantity;
                    orderObject.Price = item.Price * item.Quantity;
                    orderObject.Url = item.Url;


                    // Add the item to the database context
                    db.Orders.Add(orderObject);
                    // Save changes to the database (outside the loop)
                    db.SaveChanges();
                }



               
            }
            

            if (ModelState.IsValid)
            {


                int sum = 0;

                
                foreach (var item in items)
                {
                    sum = Convert.ToInt32(item.Price * item.Quantity) + sum;
                }
                payment.Amount = sum;
                db.Payments.Add(payment);
                db.SaveChanges();
                MailModel _objModelMail = new MailModel();

                MailMessage mail = new MailMessage();

                _objModelMail.To = payment.Email;

                mail.To.Add(_objModelMail.To);
                _objModelMail.From = "taxindunstry@gmail.com";
                mail.From = new MailAddress(_objModelMail.From);
                mail.Subject = "Payment confirmation";
                string Body = " This serves as payment confirmation from Dut Branded staff." + " Date issued: " + payment.Date + " Amount paid: R" + payment.Amount;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("taxindunstry@gmail.com", "zlsmnhuyuvuyzcja "); // Enter seders User name and password  
                smtp.EnableSsl = true;
                smtp.Send(mail);



                return RedirectToAction("Index","Orders");
            }

            return View(payment);
        }

        // GET: Payments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PayID,Email,CVV,Card,Amount,Date")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(payment);
        }

        // GET: Payments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment payment = db.Payments.Find(id);
            db.Payments.Remove(payment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
