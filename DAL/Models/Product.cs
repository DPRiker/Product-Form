namespace DAL.Models
{
	public class Product
	{
		public int Id { get; set; }
		public int Number { get; set; }
		public string Name { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public byte[] Image { get; set; }
		public Category Category { get; set; }
		//public int CategoryId { get; set; }
	}
}
