using Booking.Core.Entities;
using Booking.Core.Repositories;
using Booking.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Data.Repositories
{
    internal class ApplicationUserGymClassRepository : IApplicationUserGymClassRepository
    {
        private readonly ApplicationDbContext _db;

        public ApplicationUserGymClassRepository(ApplicationDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<ApplicationUserGymClass?> FindAsync(string userId, int gymClassId)
        {
            return await _db.ApplicationUserGymClass.FindAsync(userId, gymClassId);
        }

        public void Add(ApplicationUserGymClass booking)
        {
            ArgumentNullException.ThrowIfNull(booking, nameof(booking));
            _db.ApplicationUserGymClass.Add(booking);
        }

        public void Remove(ApplicationUserGymClass attending)
        {
            ArgumentNullException.ThrowIfNull(attending, nameof(attending));
            _db.Remove(attending);
        }
    }
}
