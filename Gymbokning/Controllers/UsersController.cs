using Booking.Core.Entities;
using Booking.Data.Data;
using Booking.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Booking.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;

        }
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.GetUsersInRoleAsync("Member");

            return users != null ?
                          View(users) :
                          Problem("Users with Member Role is null.");
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var model = await _userManager.FindByIdAsync(id);
            return View(model);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            throw new NotImplementedException();
        }


        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ApplicationUser user)
        {
            throw new NotImplementedException();
        }


        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            var model = await _userManager.FindByIdAsync(id);
            return View(model);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var member = await _userManager.FindByIdAsync(id);
            if (member != null)
            {
                await _userManager.DeleteAsync(member);
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
