namespace Booking.Data.Repositories
{
    public interface IUnitOfWork
    {
        IApplicationUserGymClassRepository ApplicationUserGymClassRepository { get; }
        IGymClassRepository GymClassRepository { get; }

        Task CompleteAsync();
    }
}