using Cloud22010446_Dut4life.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cloud22010446_Dut4life.Controllers
{
	public class HomeController : Controller
	{
		private ApplicationDbContext db = new ApplicationDbContext();
		Designs _mdal = new Designs();
		DataTable dt;
		// Display products
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
				DesgObT.Description = dt.Rows[i]["Description"].ToString(); // Populate Description field
				list.Add(DesgObT);
			}
			return View(list);

		}
		public ActionResult CustomerHomePage()
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
				DesgObT.Description = dt.Rows[i]["Description"].ToString(); // Populate Description field
				list.Add(DesgObT);
			}
			return View(list);
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