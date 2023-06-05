using System.ComponentModel.DataAnnotations;
namespace CapstoneWine.Models
{
    public class WinesModel
    {
        [Key]
        public int WineID { get; set; }
        [Required(ErrorMessage = "The Wine Name field is required.")]
        public string? WineName { get; set; }

		public string? Image { get; set; }

        [Required(ErrorMessage = "The Price field is required.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "The Blurb field is required.")]
        public string? Blurb { get; set; }

        [Required(ErrorMessage = "The Quantity field is required.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "The Type field is required.")]
        public string? Type { get; set; }

        [Required(ErrorMessage = "The Category field is required.")]
        public string? Category { get; set; }
    }
}