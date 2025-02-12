namespace ChatRoomWithBot.Domain
{
    public class SharedSettings
    {
        public static SharedSettings Current;

        public SharedSettings()
        {
            Current = this;
        }


        public AzureAd AzureAd { get; set; }

        public string Sentry { get; set; }

        public RabbitMq RabbitMq { get; set; }

        public SQLServer SQLServer { get; set; } 
    }


    public class AzureAd
    {
        public string Instance { get; set; } = string.Empty;
        public string Domain { get; set; } = string.Empty;
        public string TenantId { get; set; } = string.Empty;
        public string ClientId { get; set; } = string.Empty;
        public string CallbackPath { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
        public string PostLogoutRedirectUri { get; set; } = string.Empty;

    }

    public class RabbitMq
    {
        public string Host { get; set; } = string.Empty;
        public int  Port { get; set; } = 0;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Queue { get; set; } = string.Empty;
    }

    public class SQLServer
    {
        public string Hostname { get; set; } = string.Empty;
        public int Port { get; set; } = 0;
        public string Database { get; set; } = string.Empty;
        public string User { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string TrustServerCertificate { get; set; } = string.Empty;


        public string ConnectionString
        {
            get
            {
                var trustServerCertificate = false;
                try
                {
                    trustServerCertificate = Convert.ToBoolean(TrustServerCertificate);
                }
                catch (Exception e)
                {
                    trustServerCertificate = false;
                }

                var printableStr = $"{Hostname}:{Port}";
                Console.WriteLine($"Using SQL server connection: {printableStr}");
                return $"Server={Hostname},{Port};Database={Database};User Id={User};Password={Password};TrustServerCertificate={trustServerCertificate}";

            }
        }
    }
     
}