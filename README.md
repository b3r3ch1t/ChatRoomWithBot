# ChatRoomWithBot with .NET CORE 6
This system is a browser-based chat application using .NET CORE 6.

This application allow several users to talk in a chatroom and also to get stock quotes from an API using a specific command.

The users are stored in MS SQL Server using Microsoft Identity and Entiframework CORE as ORM. 

Only authentication users can send messages in chat rooms. 

The system allows to setup the number of roow as how many as need just adding it at ChatRoom table. 

In the rooms allows to send commands that use the message broker (RabbitMQ) that call the API https://stooq.com.

There are a Worker Service running from chat(https://learn.microsoft.com/en-us/dotnet/core/extensions/workers) that process the commands. 

The Workers Services receive the CSV file and send a message back into the chatroom using a message broker (RabbitMQ); 

The chats show the last 50 messages ordered by timestamp. 


## Features
  - Chat Room for talking with other users using browser. 
  - .NET Identity Core for users authentication
  - Implements Chat Bots
  - Message broker
  
## Architecture:

- Full architecture with responsibility separation concerns, SOLID and Clean Code
- Domain Driven Design (Layers and Domain Model Pattern)
- Domain Events
- Domain Validations
- CQRS (Imediate Consistency)
- Repository
- Unity Tests
  


## Technologies implemented:

- ASP.NET 6.0
- ASP.NET Identity Core
- Entity Framework Core 6.0
- .NET Core Native DI
- AutoMapper
- FluentValidator
- MediatR
- SignalR
- Serilog
- Sentry
- MassTransit
- CsvHelper
- Bogus
- Moq
- Docker
