using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CapstoneWine.Models
{
    public class OrdersModel
    {
        [Key]
        public int OrderID { get; set; }

        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        // public UserProfile userprofile { get; set; }

        public int WineID { get; set; }
        [ForeignKey("WineID")]
        public WinesModel? wine { get; set; }

        public int SubID { get; set; }
        [ForeignKey("SubID")]
        public SubscriptionsModel? subscription { get; set; }

        public int Quantity { get; set; }
        public string? DeliveryAdd { get; set; }
        public decimal? DeliveryCharge { get; set; }
        public decimal? TotalCost { get; set; }
        public string? OrderStatus { get; set; }
        public int RewardPoints { get; set; }
    }
}