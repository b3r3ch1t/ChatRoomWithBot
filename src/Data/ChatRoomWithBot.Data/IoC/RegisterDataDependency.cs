using ChatRoomWithBot.Data.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore; 
using ChatRoomWithBot.Data.Repository;
using ChatRoomWithBot.Domain;
using ChatRoomWithBot.Domain.Interfaces.Repositories;

namespace ChatRoomWithBot.Data.IoC
{
	public static class RegisterDataDependency
	{
		public static IServiceCollection RegisterDataDependencies(
			this IServiceCollection services )
		{


 
			
			services.AddScoped<IChatMessageRepository, ChatMessageRepository>();

			var connection = SharedSettings.Current.SQLServer.ConnectionString;

			services.AddDbContext<ChatRoomWithBotContext>(options =>
				options.UseSqlServer(connection));



			return services;
		}
	}
}
