using Booking.Core.Entities;
using Booking.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public IGymClassRepository GymClassRepository { get; private set; }
        public IApplicationUserGymClassRepository ApplicationUserGymClassRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            GymClassRepository = new GymClassRepository(db);
            ApplicationUserGymClassRepository = new ApplicationUserGymClassRepository(db);
        }
        public async Task CompleteAsync()
        {
            await _db.SaveChangesAsync();
        }


    }
}
