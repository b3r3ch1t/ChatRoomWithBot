namespace ChatRoomWithBot.Domain
{
	public class SharedSettings
	{
		public static SharedSettings Current;

		public SharedSettings()
		{
			Current = this;
		}


		public RabbitMqSettings RabbitMqSettings { get; set; }
		public MySqlSettings MySqlSettings { get; set; }

		public MongoDblSettings MongoDblSettings { get; set; }

		public RedisSettings RedisSettings { get; set; }
		public string CurrencyRateKey { get; set; } = string.Empty;
		public string CurrencyRateUrl { get; set; } = string.Empty;
		public OtelExporter OtelExporter { get; set; } 
	}

	 
	public class OtelExporter
	{
		public string HostName { get; set; }
		public int Port { get; set; }

		public string OtelExporterEndpoint
		{
			get
			{
				var printableStr = $"{HostName}:{Port}";
				Console.WriteLine($"Using OtelExporter connection: {printableStr}");
				return $"http://{HostName}:{Port}";
			}
		}
	}

	public class RedisSettings
	{
		public string HostName { get; set; }
		public int Port { get; set; }
		public string Password { get; set; }
		public string ConnectionString
		{

			get
			{

				var printableStr = $"{HostName}:{Port}";
				Console.WriteLine($"Using Redis connection: {printableStr}");
				return $"{HostName}:{Port},abortConnect=false";
			}

		}
	}


	public class MySqlSettings
	{

		public string Hostname { get; set; }
		public int Port { get; set; }
		public string Database { get; set; }
		public string User { get; set; }
		public string Password { get; set; }


		public string ConnectionString
		{
			get
			{

				var printableStr = $"{Hostname}:{Port}";
				Console.WriteLine($"Using MySQL connection: {printableStr}");
				return $"Server={Hostname};Port={Port};Database={Database};User={User};Password={Password};";
			}
		}

	}


	public class MongoDblSettings
	{
		public string Hostname { get; set; }
		public int Port { get; set; }
		public string Database { get; set; }
		 

		public string ConnectionString
		{

			get
			{

				var printableStr = $"{Hostname}:{8080}";
				Console.WriteLine($"Using MongoDb connection: {printableStr}");
				return $"mongodb://{Hostname}:{Port}";
			}
		}


	}

	public class RabbitMqSettings
	{

		public string Hostname { get; set; }
		public int Port { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public ushort PrefetchCount { get; set; }


		public string ConnectionString => $"amqp://{Username}:{Password}@{Hostname}:{Port}/";

	}
}
