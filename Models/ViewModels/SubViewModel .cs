using System.ComponentModel.DataAnnotations;
using System;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;


namespace CapstoneWine.Models.ViewModels
{
	public class SubViewModel
	{
		public List<SubItem> SubItems { get; set; }
		public decimal GrandTotal { get; set; }
		public string NumOfItems { get; internal set; }
	}
}
