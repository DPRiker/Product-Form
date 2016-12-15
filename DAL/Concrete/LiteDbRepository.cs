using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;
using DAL.Abstract;

namespace DAL.Concrete
{
 public class LiteDbRepository<T> : IRepository<T> where T : class, new()
	{
		private readonly LiteCollection<T> _collection;
		private readonly LiteDatabase _db = new LiteDatabase("Product.Db");

		public LiteDbRepository()
		{
			var collectionName = typeof(T).Name + "s";          // get class type name
			_collection = _db.GetCollection<T>(collectionName);  // get collection
		}

		public IEnumerable<T> GetAll()
		{
			var items = _collection.FindAll();

			return items;
		}

		public T GetCurrent(int id)
		{
			var item = _collection.FindById(id);

			return item;
		}
		public void Create(T item)
		{
			_collection.Insert(item);
		}

		public void Update(T item)
		{
			_collection.Update(item);
		}

		public void Delete(int id)
		{
			_collection.Delete(id);
		}
		
	}
}

