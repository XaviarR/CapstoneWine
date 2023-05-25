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
		}
	}
}
