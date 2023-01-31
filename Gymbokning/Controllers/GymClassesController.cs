using AutoMapper;
using Booking.Core.Entities;
using Booking.Core.Repositories;
using Booking.Core.ViewModels;
using Booking.Data.Data;
using Booking.Web.Extensions;
using Booking.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Diagnostics;
using System.Security.Claims;


namespace Booking.Web.Controllers
{
    public class GymClassesController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GymClassesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            //_context = context;
            _userManager = userManager;
            _uow= unitOfWork;
            _mapper = mapper;

        }

        // GET: GymClasses
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var gymClasses=await _uow.GymClassRepository.GetWithAttendingAsync();
            var res= _mapper.Map<IEnumerable<GymClassViewModel>>(gymClasses);

            //var userId = _userManager.GetUserId(User);

            //var model = (await _uow.GymClassRepository.GetWithAttendingAsync())
            //    .Select(c=>new GymClassViewModel
            //    {
            //        Id = c.Id,
            //        Name = c.Name,
            //        StartTime= c.StartTime,
            //        Duration= c.Duration,
            //        Attending = c.AttendingMembers.Any(a=>a.ApplicationUserId==userId)
            //    }
            //    ).ToList();

            return View(res);
        }

        // GET: GymClasses/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            return View(await _uow.GymClassRepository.GetWithAttendingAsync((int)id!));
        }


        // GET: GymClasses/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return Request.IsAjax() ? PartialView("CreateGymClassPartial") : View();
        }

        // POST: GymClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(GymClass gymClass)
        {
            if (ModelState.IsValid)
            {
                _uow.GymClassRepository.Add(gymClass);
                await _uow.CompleteAsync();
                return Request.IsAjax() ? 
                    PartialView("GymClassPartial", gymClass) : 
                    RedirectToAction(nameof(Index));
            }
            if (Request.IsAjax())
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return PartialView("CreateGymClassPartial", gymClass);
            }
            return View(gymClass);
        }


        // GET: GymClasses/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            return View(await _uow.GymClassRepository.GetAsync((int)id!));
        }


        // POST: GymClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
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
                    _uow.GymClassRepository.Update(gymClass);
                    await _uow.CompleteAsync();
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            return View(await _uow.GymClassRepository.GetAsync((int)id!));
        }

        // POST: GymClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gymClass = await _uow.GymClassRepository.GetAsync(id);
            if (gymClass != null)
            {
                _uow.GymClassRepository.Remove(gymClass);
            }

            await _uow.CompleteAsync();
            return RedirectToAction(nameof(Index));
        }


        [Authorize]
        public async Task<IActionResult> BookingToggle(int? id)
        {
            if (id == null) return NotFound();

            //get the gymclass with attendees
            var gymClass = await _uow.GymClassRepository.GetWithAttendingAsync((int)id!);

            if (gymClass == null)
            {
                return NotFound();
            }

            //get user id from user manager
            var userId = _userManager.GetUserId(User);
            if (userId == null) { return BadRequest(); }


            var attending = await _uow.ApplicationUserGymClassRepository.FindAsync(userId,(int)id!);

            if(attending==null)
            {
                var applicationUserGymClass = new ApplicationUserGymClass
                {
                    ApplicationUserId = userId,
                    GymClassId = id.GetValueOrDefault(),
                };

                _uow.ApplicationUserGymClassRepository.Add(applicationUserGymClass);
                await _uow.CompleteAsync();

            }
            else
            {
                _uow.ApplicationUserGymClassRepository.Remove(attending);
                await _uow.CompleteAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool GymClassExists(int id)
        {
            return (_uow.GymClassRepository.Exists(id));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
