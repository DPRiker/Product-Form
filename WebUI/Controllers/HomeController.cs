using AutoMapper;
using BLL.DTOModels;
using BLL.Interfaces;
using BLL.Realization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Web.Mvc;
using WebUI.Models;
using System.Net;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace WebUI.Controllers
{
	public class HomeController : Controller
	{
		private readonly IService<ProductDTO> _db;

		public HomeController(IService<ProductDTO> repo)
		{
			_db = repo;
		}
		public ActionResult Index()
		{
			var products = _db.GetAll();
			var productsDto = new List<ProductViewModel>();


			foreach (var item in products)
			{
				var current = new ProductViewModel
				{
					Name = item.Name,
					Number = item.Number,
					Image = item.Image,
					Price = item.Price,
					Quantity = item.Quantity,
					Id = item.Id,
					Category = new CategoryViewModel
					{
						Id = item.Category.Id,
						Name = item.Category.Name
					}
				};
				productsDto.Add(current);
			};


			return View(productsDto);			
		}
		[HttpGet]
		public ActionResult Create()
		{
			var categories = new List<CategoryViewModel> { new CategoryViewModel { Id = 1, Name = "Books" }, new CategoryViewModel { Id = 2, Name = "Cars" } };
			var catagoriesList = new SelectList(categories, "Id", "Name");
			ViewBag.CategoriesList = catagoriesList;

			return View("Edit", new ProductViewModel());
		}
		[HttpGet]
		public ActionResult Edit(int Id)
		{
			var categories = new List<CategoryViewModel> { new CategoryViewModel { Id = 1, Name = "Books" }, new CategoryViewModel { Id = 2, Name = "Cars" } };
			var catagoriesList = new SelectList(categories, "Id", "Name");
			ViewBag.CategoriesList = catagoriesList;

			var currentProduct = _db.GetCurrent(Id);
			var current = new ProductViewModel
			{
				Name = currentProduct.Name,
				Number = currentProduct.Number,
				Image = currentProduct.Image,
				Price = currentProduct.Price,
				Quantity = currentProduct.Quantity,
				Id = currentProduct.Id,
				Category = new CategoryViewModel
				{
					Id = currentProduct.Category.Id,
					Name = currentProduct.Category.Name
				}

			};

			return View(current);
		}

		[HttpPost]
		public ActionResult Edit(ProductViewModel product, HttpPostedFileBase uploadImage)
		{
			

			try
			{
				if (ModelState.IsValid && uploadImage != null)
				{
					byte[] imageData = null;

					using (var binaryReader = new BinaryReader(uploadImage.InputStream))
					{
						imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
					}

					product.Image = imageData;
				}

				var currentProduct = new ProductDTO
				{
					
					Name = product.Name,
					Number = product.Number,
					Image = product.Image,
					Price = product.Price,
					Quantity = product.Quantity,
					Id = product.Id,
					Category = new CategoryDTO {
						Id = product.Category.Id,
						Name = product.Category.Name
					}

				};
				_db.Upsert(currentProduct);

				return Redirect("/Home/Index");

			}
			catch (ValidationException ex)
			{
				ModelState.AddModelError(ex.Source, ex.Message);
			}
			return View(product);
		}

		public ActionResult MailSend(int Id)
		{
			var currentProduct = _db.GetCurrent(Id);
			var current = new ProductViewModel
			{
				Name = currentProduct.Name,
				Number = currentProduct.Number,
				Image = currentProduct.Image,
				Price = currentProduct.Price,
				Quantity = currentProduct.Quantity,
				Id = currentProduct.Id,
				Category = new CategoryViewModel
				{
					Id = currentProduct.Category.Id,
					Name = currentProduct.Category.Name
				}

			}; 
			  MailAddress from = new MailAddress("rik613@i.ua", "Tom");
			// to whom
			MailAddress to = new MailAddress("dmitriy.piestov@gmail.com");
			// object
			MailMessage m = new MailMessage(from, to);
			// Theme
			m.Subject = "Тест";
			// Text
			m.Body = $"<h2>Product Page</h2>" +
								$"<p>Product Name: {currentProduct.Name}</p>" +
								$"<p>Product Number: {currentProduct.Number}</p>" +
								$"<p>Product Quantity: {currentProduct.Quantity}</p>" +
								$"<p>Product Price: {currentProduct.Price}</p>";							
			//  html
			m.IsBodyHtml = true;
			// smtp-server and port
			SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
			// логин и пароль
			smtp.Credentials = new NetworkCredential("dmitriy.piestov@gmail.com", "99564123qwerty");
			smtp.EnableSsl = true;
			smtp.Send(m);

			return Redirect("/Home/Index");
		}
	}
}