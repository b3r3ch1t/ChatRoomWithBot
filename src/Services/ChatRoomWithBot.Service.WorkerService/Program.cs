

 
using ChatRoomWithBot.Service.WorkerService;
using ChatRoomWithBot.Service.WorkerService.Settings;
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
                    .WithCronSchedule("0/25 * * * * ?"));


                services.Configure<RabbitMqSettings>(
                    hostContext.Configuration.GetSection("RabbitMQ"));

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