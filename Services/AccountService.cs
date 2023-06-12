using CapstoneWine.Data;
using CapstoneWine.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace CapstoneWine.Services
{
	public interface IAccountService
	{
		public Task<IdentityUser> CreateAccountAsync(string emaill, string password);
		public Task<CustomerModel> CreateCustomerAsync(CustomerModel customerModel);
	}

	public class AccountService : IAccountService
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly IUserStore<IdentityUser> _userStore;
		private readonly ApplicationDbContext _context;
		private readonly IUserEmailStore<IdentityUser> _emailStore;
		private readonly IEmailSender _emailSender;

		public AccountService(
			UserManager<IdentityUser> userManager,
			IUserStore<IdentityUser> userStore,
			ApplicationDbContext context,
			IEmailSender emailSender)
		{
			_userManager = userManager;
			_userManager.Options.SignIn.RequireConfirmedAccount = false;
			_userStore = userStore;
			_emailStore = GetEmailStore();
			_context = context;
			_emailSender = emailSender;

		}

		public async Task<IdentityUser> CreateAccountAsync(
			string email,
			string password)
		{

			var user = CreateUser();

			await _userStore.SetUserNameAsync(user, email, CancellationToken.None);
			await _emailStore.SetEmailAsync(user, email, CancellationToken.None);
			var result = await _userManager.CreateAsync(user, password);

			if (result.Succeeded)
			{
				await _userManager.AddToRoleAsync(user, "Registered");

				// Manually verify new user's email
				var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
				await _userManager.ConfirmEmailAsync(user, code);
			}

            await _context.SaveChangesAsync();

            return user;

		}

		public async Task<CustomerModel> CreateCustomerAsync(CustomerModel customerModel)
		{
			_context.Add(customerModel);

			await _context.SaveChangesAsync();
			return customerModel;

		}

		private IdentityUser CreateUser()
		{
			try
			{
				return Activator.CreateInstance<IdentityUser>();
			}
			catch
			{
				throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
					$"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
					$"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
			}
		}

		private IUserEmailStore<IdentityUser> GetEmailStore()
		{
			if (!_userManager.SupportsUserEmail)
			{
				throw new NotSupportedException("The default UI requires a user store with email support.");
			}
			return (IUserEmailStore<IdentityUser>)_userStore;
		}
		
	}
}
