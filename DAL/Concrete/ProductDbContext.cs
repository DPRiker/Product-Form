using System.Data.Entity;

namespace DAL.Concrete
{
	public class ProductDbContext<T> : DbContext where T :class
	{
		public DbSet<T> Items { get; set; }

		public ProductDbContext() : base("DefaultConnection")
		{
		}
	}
}
