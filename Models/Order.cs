using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cloud22010446_Dut4life.Models
{
	public class Order
	{
		[Key]
		public int OrderID { get; set; }
		public string DesignName { get; set; }
		public double Price { get; set; }
		public string Url { get; set; }
		[EmailAddress]
		public string Email { get; set; }
		public int Quantity { get; set; }
		private DateTime _returnDate = DateTime.MinValue;
		[Display(Name = "Date issued")]
		public DateTime Date
		{
			get
			{
				return (_returnDate == DateTime.MinValue) ? DateTime.Now : _returnDate;
			}
			set { _returnDate = value; }
		}

	}
}