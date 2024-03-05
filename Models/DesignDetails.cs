using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cloud22010446_Dut4life.Models
{
	public class DesignDetails
	{
        public int? DesignID { get; set; }
        public string DesignName { get; set; }
        public double Price { get; set; }
        public string Url { get; set; }
        public string color { get; set; }
        public int Quantity { get; set; } // New property for Quantity
    }
}