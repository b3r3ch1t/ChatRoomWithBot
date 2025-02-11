using System.Text.Json;
using ChatRoomWithBot.Domain;
using ChatRoomWithBot.Domain.Bus;
using ChatRoomWithBot.Domain.Events;
using ChatRoomWithBot.Domain.Interfaces;
using ChatRoomWithBot.Services.RabbitMq.Settings;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Options;

namespace ChatRoomWithBot.Services.RabbitMq.Handler
{
	internal class BotMessageNotificationHandler : IRequestHandler<ChatMessageCommandEvent, CommandResponse>
	{

	 
		private readonly IBus _bus;
		public BotMessageNotificationHandler(  IBerechitLogger berechitLogger, IBus bus)
		{
			_bus = bus; 
		}

		public async Task<CommandResponse> Handle(ChatMessageCommandEvent notification, CancellationToken cancellationToken)
		{
			try
			{
				 
				var uri = new Uri($"rabbitmq://{SharedSettings.Current.RabbitMq.Host}/BotCommandQueue");
				var endPoint = await _bus.GetSendEndpoint(uri);
				await endPoint.Send(notification);
				return CommandResponse.Ok();
			}
			catch (Exception e)
			{
				return CommandResponse.Fail(e);
			}


		}
	}
}
