namespace CapstoneWine.Models.ViewModels
{
	public class CustomerViewModel
	{
		public CustomerModel Customer { get; set; }
		public string? Email { get; set; }
		public SubscriptionsModel? Subscription { get; internal set; }
		public OrdersModel? Order { get; internal set; }
		public WinesModel Wine { get; internal set; }
	}
}
