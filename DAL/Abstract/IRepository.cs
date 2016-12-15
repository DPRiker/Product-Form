using System.Collections.Generic;

namespace DAL.Abstract
{
	public interface IRepository<T> where T : class
	{
		IEnumerable<T> GetAll();
		T GetCurrent(int Id);
		void Create(T item);
		void Update(T item);
		void Delete(int Id);
	}
}
