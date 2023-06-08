namespace CapstoneWine.Models.ViewModels
{
        public class CartViewModel
        {
		internal string NumOfItems;

		public List<CartItem> CartItems { get; set; }
                public decimal GrandTotal { get; set; }
        }
}
