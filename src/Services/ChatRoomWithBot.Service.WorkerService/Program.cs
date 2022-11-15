

 
using ChatRoomWithBot.Domain.Events;
using ChatRoomWithBot.Service.WorkerService;
using ChatRoomWithBot.Service.WorkerService.Settings;
using MassTransit;
using Quartz;
using Serilog;
using Serilog.Events;


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();


try
{
    Log.Information("Starting host");

    var host = Host.CreateDefaultBuilder(args)
        .ConfigureServices((hostContext, services) =>
        {
               
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionScopedJobFactory();
                var jobKey = new JobKey("ChatRoomWithBot");
                q.AddJob<ConsumerRabbitMq>(opts => opts.WithIdentity(jobKey));

                q.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity("ChatRoomWithBot")
                    .WithCronSchedule("* 0/5 * * * ?"));

                
               // services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

            });


            services.AddMassTransit(x =>
            {
                x.AddConsumer<ChatMessageCommandEventConsumer>();

                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.UseHealthCheck(provider);
                    cfg.Host(new Uri("rabbitmq://localhost"), h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ReceiveEndpoint("botBundleQueue", ep =>
                    {
                        ep.ConfigureConsumer<ChatMessageCommandEventConsumer>(provider);
                    });
                }));
            });

            services.AddMassTransitHostedService();


            services.Configure<RabbitMqSettings>(
                hostContext.Configuration.GetSection("RabbitMQ"));


        })
        .UseSerilog()
        .Build();

    await host.RunAsync();

    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");

    return 1;
}
finally
{
    Log.CloseAndFlush();
}