using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Models
{
	public class ProductViewModel
	{
		//[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }

		[Required(ErrorMessage = "Please enter a product number")]
		[Display(Name = "Number")]
		public int Number { get; set; }

		[Required(ErrorMessage = "Please enter a product name")]
		[Display(Name = "Name of Product")]
		public string Name { get; set; }

		[Required]
		[Display(Name = "Quantity")]
		public int Quantity { get; set; }

		[Required(ErrorMessage = "Please enter a positive price")]
		public decimal Price { get; set; }
		public byte[] Image { get; set; }
		public CategoryViewModel Category { get; set; }
	}
}