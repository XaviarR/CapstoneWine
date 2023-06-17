namespace CapstoneWine.Models.ViewModels
{
	public class CustomerViewModel
	{
		public CustomerModel Customer { get; set; }
		public string? Email { get; set; }
		public SubscriptionsModel? Subscription { get; internal set; }
		public OrdersModel[] Orders { get; internal set; }
		public List<WinesModel> Wines { get; internal set; }
	}
}
