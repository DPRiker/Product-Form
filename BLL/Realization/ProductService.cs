using AutoMapper;
using BLL.DTOModels;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Abstract;
using DAL.Models;
using System;
using System.Collections.Generic;

namespace BLL.Realization
{
	public class ProductService : IService<ProductDTO>
	{
		private readonly IRepository<Product> _db;

		public ProductService(IRepository<Product> repo)
		{
			_db = repo;
		}
		public void Delete(int Id)
		{
			if (Id == 0)
			{
				throw new ValidationException("Product id not set", "");
			}
			
			_db.Delete(Id);
		}

		public IEnumerable<ProductDTO> GetAll()
		{
			//Mapper.Initialize(cfg => cfg.CreateMap<IEnumerable<Product>, List<ProductDTO>>());
			//var products = Mapper.Map<IEnumerable<Product>, List<ProductDTO>>(_db.GetAll());
			var products = _db.GetAll();
			var productsDto = new List<ProductDTO>();


			foreach (var item in products)
			{
				var current = new ProductDTO
				{
					Name = item.Name,
					Number = item.Number,
					Image = item.Image,
					Price = item.Price,
					Quantity = item.Quantity,
					Id = item.Id,
					Category = new CategoryDTO
					{
						Id = item.Category.Id,
						Name = item.Category.Name
					}
				};
				productsDto.Add(current);
			};


			return productsDto;
		}

		public ProductDTO GetCurrent(int Id)
		{

			var currentProduct = _db.GetCurrent(Id);
			var current = new ProductDTO
			{
				Name = currentProduct.Name,
				Number = currentProduct.Number,
				Image = currentProduct.Image,
				Price = currentProduct.Price,
				Quantity = currentProduct.Quantity,
				Id = currentProduct.Id,
				Category = new CategoryDTO
				{
					Id = currentProduct.Category.Id,
					Name = currentProduct.Category.Name
				}
				
			};


			return current;
		}

		public void Upsert(ProductDTO product)
		{
			if (product.Id <= 0)
			{
				var currentProduct = new Product
				{
					Name = product.Name,
					Number = product.Number,
					Image = product.Image,
					Price = product.Price,
					Quantity = product.Quantity,
					Id = product.Id,
					Category = new Category
					{
						Id = product.Category.Id,
						Name = product.Category.Name
					}
				};

				_db.Create(currentProduct);
				
				
			}
			else
			{
				Mapper.Initialize(cfg => cfg.CreateMap<CategoryDTO, Category>());
				var currentCategory = Mapper.Map<CategoryDTO, Category>(product.Category);

				var newProduct = new Product
				{
					Id = product.Id,
					Name = product.Name,
					Number = product.Number,
					Quantity = product.Quantity,
					Price = product.Price,
					Category = currentCategory,
					Image = product.Image

				};

				_db.Update(newProduct);

			}
		}
	}
}
