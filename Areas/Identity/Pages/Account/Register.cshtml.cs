// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using CapstoneWine.Controllers;
using CapstoneWine.Data;
using CapstoneWine.Models;
using CapstoneWine.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace CapstoneWine.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {

		private readonly IAccountService _accountService;
		private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly ApplicationDbContext _context;
		
		public RegisterModel(
			IAccountService accountService,
			SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            ApplicationDbContext context)
        {
			_accountService = accountService;
			_logger = logger;
			_signInManager = signInManager;
            _context = context;

		}

		/// <summary>
		///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
		///     directly from your code. This API may change or be removed in future releases.
		/// </summary>
		[BindProperty]
        public InputModel Input { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string StreetAddress { get; set; }
        public string Suburb { get; set; }
        public string City { get; set; } 
        public string PostCode { get; set; }

		/// <summary>
		///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
		///     directly from your code. This API may change or be removed in future releases.
		/// </summary>
		public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

			[Required]
			[Display(Name = "First Name")]
			public string FirstName { get; set; }

			[Required]
			[Display(Name = "Last Name")]
			public string LastName { get; set; }

			//Additional fields TBC
			//[Required]
			[Display(Name = "Street Address")]
			public string StreetAddress { get; set; }

			//[Required]
			[Display(Name = "Suburb")]
			public string Suburb { get; set; }

			//[Required]
			[Display(Name = "City")]
			public string City { get; set; }

			//[Required]
			[Display(Name = "PostCode")]
			public string PostCode { get; set; }

			/// <summary>
			///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
			///     directly from your code. This API may change or be removed in future releases.
			/// </summary>
			[Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
			{

                var user = _accountService.CreateAccountAsync(Input.Email, Input.Password);

                var customerModel = new CustomerModel();
                customerModel.FirstName = Input.FirstName;
                customerModel.LastName = Input.LastName;
                customerModel.StreetAddress = Input.StreetAddress;
                customerModel.Suburb = Input.Suburb;
                customerModel.City = Input.City;
                customerModel.PostCode = Input.PostCode;
                customerModel.IdentityKey = user.Result.Id;

                await _accountService.CreateCustomerAsync(customerModel);

				return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });

			}

			// If we got this far, something failed, redisplay form
			return Page();
        }
    }
}
