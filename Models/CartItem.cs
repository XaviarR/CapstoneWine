namespace CapstoneWine.Models
{
        public class CartItem
        {
                public int ProductId { get; set; }
                public string ProductName { get; set; }
                public int Quantity { get; set; }
                public decimal Price { get; set; }
                public decimal Total
                {
                        get { return Quantity * Price; }
                }
                public string Image { get; set; }

                public CartItem()
                {
                }

                public CartItem(WinesModel wines)
                {
                        ProductId = wines.WineID;
                        ProductName = wines.WineName;
                        Price = wines.Price;
                        Quantity = 1;
                        Image = wines.Image;
                }

        }
}
