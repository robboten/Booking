namespace Booking.Core.Repositories
{
    public interface IUnitOfWork
    {
        IApplicationUserGymClassRepository ApplicationUserGymClassRepository { get; }
        IGymClassRepository GymClassRepository { get; }

        Task CompleteAsync();
    }
}