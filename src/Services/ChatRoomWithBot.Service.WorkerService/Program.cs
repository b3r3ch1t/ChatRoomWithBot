using ChatRoomWithBot.Service.WorkerService;
using ChatRoomWithBot.Service.WorkerService.Interface;
using ChatRoomWithBot.Service.WorkerService.Services;
using ChatRoomWithBot.Service.WorkerService.Settings;
using MassTransit;
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
			IConfiguration configuration = hostContext.Configuration;

			var hostName = configuration.GetSection("RabbitMQ:Connection:HostName").Value;
			var receiveEndpoint = configuration.GetValue<string>("RabbitMQ:botCommandQueue");

			services.AddMassTransit(x =>
			{
				x.AddConsumer<ChatMessageCommandEventConsumer>();

				x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
				{
					cfg.Host(new Uri($"rabbitmq://{hostName}") );

					cfg.ReceiveEndpoint(receiveEndpoint, ep =>
					{
						ep.ConfigureConsumer<ChatMessageCommandEventConsumer>(provider);
					});
				}));
			});

			services.AddScoped<IRabbitMqPublish, RabbitMqPublish>();

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
