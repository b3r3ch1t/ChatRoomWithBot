using System.Text.Json;
using ChatRoomWithBot.Domain;
using ChatRoomWithBot.Domain.Interfaces;
using ChatRoomWithBot.Domain.OperationResults;
using Polly;
using StackExchange.Redis;

namespace ChatRoomWithBot.Infra.Cache
{
	public class CacheRepository : ICacheRepository
	{
		private readonly IDatabase _database;
		private readonly IBerechitLogger _logFramework;
		private readonly ConnectionMultiplexer _connection;
		public CacheRepository(IBerechitLogger logFramework)
		{
			_logFramework = logFramework;

			var hostname = SharedSettings.Current.Redis.HostName;
			var port = SharedSettings.Current.Redis.Port;
			var password = SharedSettings.Current.Redis.Password;


			var config = new ConfigurationOptions
			{
				EndPoints = { $"{hostname }:{port}" },  
				Password = password,  
				AbortOnConnectFail = false
			};

			_connection = ConnectionMultiplexer.Connect(config);
			_database = _connection.GetDatabase();

		}

		public async Task<OperationResult<T>> GetAsync<T>(string key)
		{
			try
			{
				var serializedData = await _database.StringGetAsync(key);
				if (!serializedData.HasValue)
					return OperationResult<T>.CreateFailureResult("The value isn't in the cache", default);

				var resultObject = JsonSerializer.Deserialize<T>(serializedData.ToString());
				if (resultObject == null)
					return OperationResult<T>.CreateFailureResult("The value isn't in the cache", default);

				return OperationResult<T>.CreateSuccessResult(resultObject);
			}
			catch (Exception e)
			{
				_logFramework.Error($"Error getting the key '{key}' from the cache");
				_logFramework.Error(e);
				return OperationResult<T>.CreateFailureResult(e.Message, default);
			}
		}
		public async Task<OperationResult<bool>> SetAsync<T>(string key, T value, int cacheDurationInMinutes)
		{
			var retryPolicy = Policy
				.Handle<RedisConnectionException>()
				.Or<TimeoutException>()
				.WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
					(exception, timeSpan, retryCount, context) =>
					{
						_logFramework.Warning($"Retry {retryCount} encountered an exception: {exception.Message}. Waiting {timeSpan} before next retry.");
					});

			var fallbackPolicy = Policy<bool>
				.Handle<Exception>()
				.FallbackAsync(false, (exception, context) =>
				{
					_logFramework.Error($"Executing fallback due to: {exception}");
					return Task.CompletedTask;
				});

			try
			{
				var result = await fallbackPolicy.WrapAsync(retryPolicy).ExecuteAsync(async () =>
				{
					var json = JsonSerializer.Serialize(value);
					var expiry = TimeSpan.FromMinutes(cacheDurationInMinutes);
					await _database.StringSetAsync(key, json, expiry);

					_logFramework.Information($"Created cache key={key} with expiration={expiry.TotalMinutes} minutes, date={DateTime.Now}");
					return true;
				});

				return result ? OperationResult<bool>.CreateSuccessResult(true) : OperationResult<bool>.CreateFailureResult("Failed to set cache", false);
			}
			catch (Exception e)
			{
				_logFramework.Error(e);
				return OperationResult<bool>.CreateFailureResult(e.Message, false);
			}
		}
		public async Task<OperationResult<bool>> RemoveAsync(string key)
		{
			try
			{
				await _database.KeyDeleteAsync(key);
				return OperationResult<bool>.CreateSuccessResult(true);
			}
			catch (Exception e)
			{
				_logFramework.Error(e);
				return OperationResult<bool>.CreateFailureResult(e.Message, false);
			}
		}
		public void Dispose()
		{
			_connection?.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}