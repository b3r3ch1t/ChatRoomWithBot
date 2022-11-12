using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChatRoomWithBot.Services.RabbitMq.Extensions
{
    public  static  class RabbitMqExtensions
    {
        private static RabbitMqManager RabbitMqReceiver { get; set; }



        public static IApplicationBuilder UseRabbitListener(this IApplicationBuilder app)
        {
            RabbitMqReceiver = app.ApplicationServices.GetService<RabbitMqManager>();

            var lifetime = app.ApplicationServices.GetService<IApplicationLifetime>();

            lifetime.ApplicationStarted.Register(OnStarted);

            lifetime.ApplicationStopping.Register(OnStopping);

            return app;
        }

        private static void OnStarted()
        {
            RabbitMqReceiver.Register();
        }

        private static void OnStopping()
        {
            RabbitMqReceiver.DeRegister();
        }
    }
}
