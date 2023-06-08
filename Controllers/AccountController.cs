using CapstoneWine.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System.Data;

namespace CapstoneWine.Controllers
{
    //[Authorize(Roles = "Registered")]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(ApplicationDbContext context, SignInManager<IdentityUser> signinManager, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _signInManager = signinManager;
            _userManager = userManager;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            //TODO: Review loading statement to load all orders of curent customer
            var applicationDbContext = _context.Orders.Include(o => o.subscription).Include(o => o.wine);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Customer/Details/
        public async Task<IActionResult> Customer(int? id)
        {
            //var identityKey = User.Identity.Id;
           //var identityKey = _userManager.GetUserId(User);
            return null;
         
        }
        /*
        public IActionResult Index()
        {
            return View();
        }
        */
    }
}
