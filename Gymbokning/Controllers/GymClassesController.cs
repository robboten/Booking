using Booking.Core.Entities;
using Booking.Data.Data;
using Booking.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Booking.Web.Controllers
{
    public class GymClassesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GymClassesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        // GET: GymClasses
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                Console.WriteLine(User.Identity.Name);
            }

            return _context.GymClasses != null ?
                          View(await _context.GymClasses.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.GymClasses'  is null.");
        }

        // GET: GymClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GymClasses == null)
            {
                return NotFound();
            }

            var gymClass = await _context.GymClasses
                .Include(c => c.AttendingMembers).ThenInclude(m=>m.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gymClass == null)
            {
                return NotFound();
            }

            return View(gymClass);
        }

        // GET: GymClasses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GymClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GymClass gymClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gymClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gymClass);
        }

        // GET: GymClasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GymClasses == null)
            {
                return NotFound();
            }

            var gymClass = await _context.GymClasses.FindAsync(id);
            if (gymClass == null)
            {
                return NotFound();
            }
            return View(gymClass);
        }

        // POST: GymClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GymClass gymClass)
        {
            if (id != gymClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gymClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GymClassExists(gymClass.Id))
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
            return View(gymClass);
        }

        // GET: GymClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GymClasses == null)
            {
                return NotFound();
            }

            var gymClass = await _context.GymClasses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gymClass == null)
            {
                return NotFound();
            }

            return View(gymClass);
        }

        // POST: GymClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GymClasses == null)
            {
                return Problem("Entity set 'ApplicationDbContext.GymClasses'  is null.");
            }
            var gymClass = await _context.GymClasses.FindAsync(id);
            if (gymClass != null)
            {
                _context.GymClasses.Remove(gymClass);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GymClassExists(int id)
        {
            return (_context.GymClasses?.Any(e => e.Id == id)).GetValueOrDefault();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> BookingToggle(int? id)
        {
            if (id == null) return NotFound();

            var user = await _userManager.GetUserAsync(User);
            var userId = _userManager.GetUserId(User);


            if (User.Identity.IsAuthenticated)
            {
                Console.WriteLine(User.Identity.Name);
            }


            if (userId != null) Console.WriteLine(userId);


            var gymClass = await _context.GymClasses
                .Include(c => c.AttendingMembers).Where(c=>c.Id==id)
                .FirstOrDefaultAsync();
            if (gymClass == null)
            {
                return NotFound();
            }

            var gymClassAttendees = gymClass.AttendingMembers;

            var isAttending = gymClassAttendees.Any(a => a.ApplicationUser.Id == userId);
            if (isAttending) {
                var applicationUserGymClass = gymClassAttendees.Where(a => a.GymClassId == id).FirstOrDefault();

                _context.Remove(applicationUserGymClass);
                await _context.SaveChangesAsync();
            }
            else { 
            var applicationUserGymClass = new ApplicationUserGymClass
            {
                ApplicationUserId = userId,
                GymClassId = id.GetValueOrDefault(),
            };

            _context.Add(applicationUserGymClass);
            await _context.SaveChangesAsync();
            } 
            //return View(nameof(Index),await _context.GymClasses.ToListAsync());
            return RedirectToAction(nameof(Index));
        }
    }
}
