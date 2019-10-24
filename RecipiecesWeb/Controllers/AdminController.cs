using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipiecesWeb.Data;
using RecipiecesWeb.Models;

namespace RecipiecesWeb.Controllers
{
    [Authorize(AuthenticationSchemes = "Identity.Application", Policy = "IsAdmin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
 
        public AdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
 
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.Select(user => 
                new AdminViewModel {
                    Email = user.Email,
                    IsAdmin = user.IsAdmin,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    EmailConfirmed = user.EmailConfirmed
                }).ToListAsync());
        }
 
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
 
            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Email == id);
            if (user == null)
            {
                return NotFound();
            }
 
            return View(new AdminViewModel
            {
                Email = user.Email,
                IsAdmin = user.IsAdmin,
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailConfirmed = user.EmailConfirmed
            });
        }
 
        public IActionResult Create()
        {
            return View();
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
         [Bind("Email,IsAdmin,FirstName,LastName")] AdminViewModel adminViewModel)
        {
            if (ModelState.IsValid)
            {
                await _userManager.CreateAsync(new ApplicationUser
                {
                    Email = adminViewModel.Email,
                    IsAdmin = adminViewModel.IsAdmin,
                    FirstName = adminViewModel.FirstName,
                    LastName = adminViewModel.LastName,
                    UserName = adminViewModel.Email
                });
                return RedirectToAction(nameof(Index));
            }
            return View(adminViewModel);
        }
 
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
 
            var user = await _userManager.FindByEmailAsync(id);
            if (user == null)
            {
                return NotFound();
            }
 
            return View(new AdminViewModel
            {
                Email = user.Email,
                IsAdmin = user.IsAdmin,
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailConfirmed = user.EmailConfirmed
            });
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Email,IsAdmin,FirstName,LastName,EmailConfirmed")] AdminViewModel adminViewModel)
        {
            if (id != adminViewModel.Email)
            {
                return NotFound();
            }
 
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByEmailAsync(id);
                    user.IsAdmin = adminViewModel.IsAdmin;
                    user.FirstName = adminViewModel.FirstName;
                    user.LastName = adminViewModel.LastName;
                    user.EmailConfirmed = adminViewModel.EmailConfirmed;
 
                    await _userManager.UpdateAsync(user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminViewModelExists(adminViewModel.Email))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(adminViewModel);
        }
 
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
 
            var user = await _userManager.FindByEmailAsync(id);
            if (user == null)
            {
                return NotFound();
            }
 
            return View(new AdminViewModel
            {
                Email = user.Email,
                IsAdmin = user.IsAdmin,
                FirstName = user.FirstName,
                LastName = user.LastName
            });
        }
 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByEmailAsync(id);
            await _userManager.DeleteAsync(user);
            return RedirectToAction(nameof(Index));
        }
 
        private bool AdminViewModelExists(string id)
        {
            return _context.Users.Any(e => e.Email == id);
        }
    }
}