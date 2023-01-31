using Booking.Core.Entities;

namespace Booking.Core.Repositories
{
    public interface IApplicationUserGymClassRepository
    {
        Task<ApplicationUserGymClass?> FindAsync(string userId, int gymClassId);
        void Add(ApplicationUserGymClass booking);
        void Remove(ApplicationUserGymClass attending);
    }
}