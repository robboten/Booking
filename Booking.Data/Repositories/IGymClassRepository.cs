using Booking.Core.Entities;

namespace Booking.Data.Repositories
{
    public interface IGymClassRepository
    {
        Task<List<GymClass>> GetAsync();
        Task<GymClass?> GetAsync(int id);
        Task<IEnumerable<GymClass>> GetWithAttendingAsync();
        Task<GymClass?> GetWithAttendingAsync(int id);
        void Add(GymClass gymClass);
        void Remove(GymClass gymClass);
        bool Exists(int id);
        void Update(GymClass gymClass);
    }

}