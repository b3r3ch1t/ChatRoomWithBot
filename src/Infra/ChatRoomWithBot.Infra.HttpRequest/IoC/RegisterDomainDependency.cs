using ChatRoomWithBot.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ChatRoomWithBot.Infra.HttpRequest.Infra.HttpRequest.IoC
{
	public static class RegisterHttpRequestDependency
	{
		public static IServiceCollection RegisterHttpRequestDependencies(
			this IServiceCollection services )
		{  

			services.AddScoped<IHttpRequests, HttpRequests>();

			return services;
		}
	}
}
