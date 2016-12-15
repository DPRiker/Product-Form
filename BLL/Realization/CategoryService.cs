using AutoMapper;
using BLL.DTOModels;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Abstract;
using DAL.Models;
using System.Collections.Generic;

namespace BLL.Realization
{
	public class CategoryService : IService<CategoryDTO>
	{
		private readonly IRepository<Category> _db;

		public CategoryService(IRepository<Category> repo)
		{
			_db = repo;
		}
		public void Delete(int Id)
		{
			if (Id == 0)
			{
				throw new ValidationException("Category id not set", "");
			}

			_db.Delete(Id);
		}

		public IEnumerable<CategoryDTO> GetAll()
		{
			Mapper.Initialize(cfg => cfg.CreateMap<IEnumerable<Category>, List<CategoryDTO>>());
			var categories = Mapper.Map<IEnumerable<Category>, List<CategoryDTO>>(_db.GetAll());

			return categories;
		}

		public CategoryDTO GetCurrent(int Id)
		{
			Mapper.Initialize(cfg => cfg.CreateMap<Category, CategoryDTO>());
			var currentCategory = Mapper.Map<Category, CategoryDTO>(_db.GetCurrent(Id));

			return currentCategory;
		}

		public void Upsert(CategoryDTO category)
		{
			if (category.Id <= 0)
			{

				Mapper.Initialize(cfg => cfg.CreateMap<CategoryDTO, Category>());
				var currentCategory = Mapper.Map<CategoryDTO, Category>(category);

				_db.Create(currentCategory);
			}
			else
			{
				var newCategory = new Category
				{
					Id = category.Id,
					Name = category.Name,
				};

				_db.Update(newCategory);

			}
		}
	}
}
