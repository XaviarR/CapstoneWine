using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CapstoneWine.Models
{
    public class OrderHistoryModel
    {
        [Key]
        public int OrderID { get; set; }

        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
        public string? UserID { get; set; }
        [ForeignKey("UserID")]
        // public UserProfile userprofile { get; set; }
        public int WineID { get; set; }
        [ForeignKey("WineID")]
        public int SubID { get; set; }
        [ForeignKey("SubID")]
        public int Quantity { get; set; }
        public string? DeliveryAdd { get; set; }
        public decimal? DeliveryCharge { get; set; }
        public decimal? TotalCost { get; set; }
        public string? OrderStatus { get; set; }
        public int RewardPoints { get; set; }
        public OrderHistoryModel(OrdersModel ordersModel)
        {
            OrderID = ordersModel.OrderID;
            OrderDate = ordersModel.OrderDate;
        }

        public OrderHistoryModel(WinesModel winesModel)
        {
            WineID = winesModel.WineID;
        }
        public OrderHistoryModel(SubscriptionsModel subscriptions)
        {
            SubID = subscriptions.SubID;
            Quantity = subscriptions.NumOfBottles;
            DeliveryAdd = "Random Place";
            DeliveryCharge = subscriptions.PricePerDel;
            //Total
            RewardPoints = subscriptions.RewardPoints;
            //OrderStatus
        }
        public OrderHistoryModel() { }
    }
}
