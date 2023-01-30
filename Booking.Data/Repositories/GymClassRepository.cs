using Booking.Core.Entities;
using Booking.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Data.Repositories
{
    public class GymClassRepository : IGymClassRepository
    {
        private readonly ApplicationDbContext _db;

        public GymClassRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<List<GymClass>> GetAsync()
        {
            return await _db.GymClasses.ToListAsync();
        }

        public async Task<GymClass?> GetAsync(int id)
        {
            ArgumentNullException.ThrowIfNull(id,nameof(id));

            return await _db.GymClasses.FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<IEnumerable<GymClass>> GetWithAttendingAsync()
        {
            return await _db.GymClasses.Include(c => c.AttendingMembers).ThenInclude(m => m.ApplicationUser).ToListAsync();
        }

        public async Task<GymClass?> GetWithAttendingAsync(int id)
        {
            ArgumentNullException.ThrowIfNull(id, nameof(id));
            return await _db.GymClasses.Include(c => c.AttendingMembers).ThenInclude(m => m.ApplicationUser).FirstOrDefaultAsync(m => m.Id == id);
        }

        public void Add(GymClass gymClass)
        {
            _db.Add(gymClass);
        }
        public void Remove(GymClass gymClass)
        {
            _db.Remove(gymClass);
        }
        public void Update(GymClass gymClass)
        {
            _db.Update(gymClass);
        }
        public bool Exists(int id)
        {
            return _db.GymClasses.Any(e => e.Id == id);
        }
    }
}
