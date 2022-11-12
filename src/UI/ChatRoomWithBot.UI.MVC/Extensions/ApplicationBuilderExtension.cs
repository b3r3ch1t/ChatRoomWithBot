using ChatRoomWithBot.Data;

namespace ChatRoomWithBot.UI.MVC.Extensions
{
    public static  class ApplicationBuilderExtension
    {
       public static  void SeedData(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var serviceProvider = scopedServices.ServiceProvider;

            var dataSeeder = serviceProvider.GetRequiredService<DataSeeder>();

            dataSeeder.Seed();
        }
    }
}
