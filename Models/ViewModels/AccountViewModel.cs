namespace CapstoneWine.Models.ViewModels
{
    public class AccountViewModel
    {
        public CustomerModel Customer { get; set; }
        public List<OrdersModel> Orders { get; set; }
    } 
}
