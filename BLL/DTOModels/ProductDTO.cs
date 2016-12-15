using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOModels
{
	public class ProductDTO
	{
		public int Id { get; set; }
		public int Number { get; set; }
		public string Name { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public byte[] Image { get; set; }
		public CategoryDTO Category { get; set; }
		public int CategoryId { get; set; }
	}
}
