namespace Booking.Web.Extensions
{
    public static class RequestExtensions
    {
        public static bool IsAjax(this HttpRequest request)
        {
            if (request== null)
            {
                return false;
            }
            return request.Headers["X-Requested-With"]=="XMLHttpRequest";
        }
    }
}
