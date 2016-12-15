using DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace DAL.Concrete
{
	public class EFRepository<T> : IRepository<T> where T : class
	{
		private readonly ProductDbContext<T> _db = new ProductDbContext<T>(); 
	
		public void Create(T item)
		{
			_db.Items.Add(item);
			_db.SaveChanges();
		}

		public void Delete(int Id)
		{
			var item = _db.Items.Find(Id);
			if (item != null)
				_db.Items.Remove(item);
			_db.SaveChanges();
		}

		public IEnumerable<T> GetAll()
		{
			return _db.Items;
		}

		public T GetCurrent(int Id)
		{
			return _db.Items.Find(Id);
		}

		public void Update(T item)
		{
			_db.Entry(item).State = EntityState.Modified;
			_db.SaveChanges();
		}
	}
}
