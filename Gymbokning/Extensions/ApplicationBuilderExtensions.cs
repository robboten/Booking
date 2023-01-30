namespace Booking.Web.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task SeedDataAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {

                var services = scope.ServiceProvider;
                //var db = services.GetRequiredService<ApplicationDbContext>();
                //db.Database.EnsureDeleted();
                //db.Database.Migrate();


                try
                {
                    //await SeedData.InitAsync(services);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }

        }
    }
}
