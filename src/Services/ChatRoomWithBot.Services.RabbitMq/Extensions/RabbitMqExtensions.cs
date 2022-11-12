using ChatRoomWithBot.Domain.Interfaces;
using ChatRoomWithBot.Services.RabbitMq.Manager;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChatRoomWithBot.Services.RabbitMq.Extensions
{
    public static class RabbitMqExtensions
    {
        private static IRabbitMqManager _rabbitMqReceiver;
        private static  IError _error;


        public static IApplicationBuilder UseRabbitListener(this IApplicationBuilder app)
        {

            using var scopedServices = app.ApplicationServices.CreateScope();

            var serviceProvider = scopedServices.ServiceProvider;

            _rabbitMqReceiver = serviceProvider.GetRequiredService<IRabbitMqManager>();
            _error = serviceProvider.GetRequiredService<IError>();

            var lifetime = app.ApplicationServices.GetService<IApplicationLifetime>();

            lifetime.ApplicationStarted.Register(OnStarted);

            lifetime.ApplicationStopping.Register(OnStopping);

            return app;
        }

        private static void OnStarted()
        {
            try
            {
                _rabbitMqReceiver.Register();
            }
            catch (Exception e)
            {
                _error.Error(e);
                throw;
            }

        }

        private static void OnStopping()
        {
            _rabbitMqReceiver.DeRegister();
        }
    }
}
