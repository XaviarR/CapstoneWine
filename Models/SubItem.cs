namespace CapstoneWine.Models
{
	public class SubItem
	{
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public string? Type { get; set; }
		public int Frequency { get; set; }
		public int NumOfBottles { get; set; }
		public int RewardPoints { get; set; }

		public decimal BottlePrice { get; set; }

		public decimal PricePerDel { get; set; }

		public decimal Shipping
		{
			get { return 0; }
		}
		public decimal Total
		{
			get { return NumOfBottles * BottlePrice; }
		}

		public SubItem()
		{
		}

		public SubItem(SubscriptionsModel subscriptions)
		{
			ProductId = subscriptions.SubID;
			ProductName = subscriptions.SubName;
			Type = subscriptions.Type;
			Frequency = subscriptions.Frequency;
			NumOfBottles = subscriptions.NumOfBottles;
			RewardPoints = subscriptions.RewardPoints;
			BottlePrice = subscriptions.BtlPrice;
			PricePerDel = subscriptions.PricePerDel;

		}
	}
}
