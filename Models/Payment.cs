using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cloud22010446_Dut4life.Models
{
	public class Payment
	{
		[Key]
		[Display(Name = "Transation no.")]
		public int PayID { get; set; }

		[Display(Name = "Email")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		[Required]
		[StringLength(3, ErrorMessage = "Cannot be longer than 3 characters")]
		public string CVV { get; set; }
		[Required]
		[StringLength(13, ErrorMessage = "Cannot be longer than 13 characters")]
		public string Card { get; set; }

		public double Amount { get; set; }

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