

 
using ChatRoomWithBot.Service.WorkerService;
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
            services.AddMassTransit(x =>
            {
               // x.AddConsumer<QueueClientSaved>();
               // x.AddConsumer<QueueSendEmail>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(hostContext.Configuration.GetConnectionString("RabbitMq"));
                    cfg.ConfigureEndpoints(context);
                });
            });



            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionScopedJobFactory();
                var jobKey = new JobKey("ChatRoomWithBot");
                q.AddJob<ConsumerRabbitMq>(opts => opts.WithIdentity(jobKey));

                q.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity("ChatRoomWithBot")
                    .WithCronSchedule("0/15 * * * * ?"));

                services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

            });


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