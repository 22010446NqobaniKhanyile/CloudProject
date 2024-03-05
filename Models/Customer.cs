using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cloud22010446_Dut4life.Models
{
	public class Customer
	{
		[Key]
		public int CustomerId { get; set; }
		[Display(Name = "First Name")]
		public string FirstName { get; set; }

		[Display(Name = "Last Name")]
		public string LastName { get; set; }
		[Display(Name = "Email")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Display(Name = "Phone Number")]
		public string Contact { get; set; }
		public string Address { get; set; }

	}
}