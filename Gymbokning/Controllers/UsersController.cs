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
        public async Task<IActionResult> Details(int? id)
        {
            throw new NotImplementedException();
        }


        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplicationUser user)
        {
            throw new NotImplementedException();
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
        public async Task<IActionResult> Delete(int? id)
        {
            throw new NotImplementedException();
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            throw new NotImplementedException();
        }

    }
}
