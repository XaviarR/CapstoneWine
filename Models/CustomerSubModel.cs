using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CapstoneWine.Models
{
	public class CustomerSubModel
	{
		[Key]
		public int? CustomerSubID { get; set; }
		public int SubID { get; set; }
		public int CustomerID { get; set; }
		public DateTime	StartDate { get; set; } = DateTime.Now;
	}
}
