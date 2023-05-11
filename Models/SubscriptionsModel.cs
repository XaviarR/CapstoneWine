using System.ComponentModel.DataAnnotations;

namespace CapstoneWine.Models
{
    public class SubscriptionsModel
    {
        [Key]
        public int SubID { get; set; }
        public string? SubName { get; set; }
        public string? Type { get; set; }
        public int Frequency { get; set; }
        public int NumOfBottles { get; set; }
        public int RewardPoints { get; set; }
    }
}