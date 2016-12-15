using System.Collections.Generic;
using BLL.DTOModels;

namespace BLL.Interfaces
{
	public interface IService<T> where T:class
	{
		IEnumerable<T> GetAll();
		T GetCurrent(int Id);
		void Upsert(T item);
		void Delete(int Id);
		
	}
}
