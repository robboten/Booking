using Booking.Core.Entities;

namespace Booking.Data.Repositories
{
    public interface IApplicationUserGymClassRepository
    {
        Task<ApplicationUserGymClass?> FindAsync(string userId, int gymClassId);
        void Add(ApplicationUserGymClass booking);
        void Remove(ApplicationUserGymClass attending);
    }
}