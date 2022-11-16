# ChatRoomWithBot with .NET CORE 6 | SignalR | RabbitMQ
This system is a browser-based chat application using .NET CORE 6.

This application allows several users to talk in a chatroom and also to get stock quotes from an API using a specific command.

The users are stored in MS SQL Server using Microsoft Identity and Entiframework CORE as ORM. 

Only authentication users can send messages in chat rooms. 

The system allows to setup the number of rooms as how many as need just adding it at ChatRoom table. 

In the rooms allows to send commands that use the message broker (RabbitMQ) that call the API https://stooq.com.

There are a Worker Service running from chat(https://learn.microsoft.com/en-us/dotnet/core/extensions/workers) that process the commands. 

The Workers Services receive the CSV file and send a message back into the chatroom using a message broker (RabbitMQ); 

The chats show the last 50 messages ordered by timestamp. 

*This system is not a real project, just a prove of concept; 


## Features
  - Chat Room for talking with other users using browser. 
  - .NET Identity Core for users authentication
  - Implements Chat Bots
  - Message broker
  
## Architecture:

- Full architecture with responsibility separation concerns
- SOLID and Clean Code
- Clean Architecture
- Domain Driven Design (Layers and Domain Model Pattern)
- Domain Events
- Domain Validations
- CQRS (Imediate Consistency)
- Repository
- Unity Tests
  


## Technologies implemented:

- ASP.NET 6.0 - (https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- ASP.NET Identity Core - (https://learn.microsoft.com/pt-br/aspnet/core/security/authentication/?view=aspnetcore-6.0)
- Entity Framework Core 6.0 - (https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-6.0/plan)
- .NET Core Native DI - (https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-6.0)
- AutoMapper - (https://automapper.org/)
- FluentValidator - (https://fluentvalidation.net/) 
- MediatR - (https://github.com/jbogard/MediatR)
- SignalR - (https://dotnet.microsoft.com/en-us/apps/aspnet/signalr)
- Serilog - (https://serilog.net/) 
- Sentry - (https://sentry.io/)
- MassTransit - (https://masstransit-project.com/)
- CsvHelper - (https://github.com/JoshClose/CsvHelper)
- Bogus - (https://github.com/bchavez/Bogus)
- Moq - (https://github.com/moq/moq4)
- Docker - (https://www.docker.com/)


## Projects Structure:

| Project | Description |
| ------ | ------ |
| ChatRoomWithBot.UI.MVC | Web Application  |
| ChatRoomWithBot.Application | Only coordinates tasks and delegates work to collaborations of domain objects and repositories  |
| ChatRoomWithBot.Domain | Is the heart of the business. It is based on a set of ideas, knowledge and business processes.  |
| ChatRoomWithBot.Service.Identity | Responsible for Authentication using ASP.NET Identity Core  |
| ChatRoomWithBot.Services.BerechitLogger | Implements Logs   |
| ChatRoomWithBot.Services.RabbitMq | Publish messages to RabbitMQ and Consumer answers messages from RabbitMQ   |
| ChatRoomWithBot.Data | Contains all repositories and connction with MS SQL Server   |
| ChatRoomWithBot.Service.WorkerService | Worker Service to process messages in RabbitMQ   |
| ChatRoomWithBot.Data.Test | Unity tests in Data Layer   |
| ChatRoomWithBot.Domain.Test | Unity tests in Domain Layer   |


*This project doesn't intend to have a ideal rate of code coverage. The tests in this project just show some techniques and tools that can be replicated in this project or in a real project.


### Settings Files

  - [ChatRoomWithBot.UI.MVC/appsettings.json] File with `ConnectionStrings` and `RabbitMQ Server credentials`.
  - [ChatRoomWithBot.Service.WorkerService/appsettings.json] File with credentials and settings to `RabbitMQ Consumer` for sending and receiving bot commands.



## Configuration

*Is not necessary to create initial users and chat rooms. The system is configured to seed the database with initial faker users and chat rooms. 


```
public void Seed()
 {

  _context.Database.EnsureCreated();

  SeedUsers();

  SeedChatRooms();
 }
```


| User | Password |
| ------ | ------ |
| user1@teste.com | Test12345678 |
| user2@teste.com | Test12345678 | 


| Chat Room  | 
| ------ |
| Room 1 |
| Room 2 | 
| Room 3 |


You can run this project using Docker or directly in Visual Studio or Visual Code :

  - For Docker
  
  Execute this command in the root folder
  
  ```sh
  $ docker-compose up -d 
  ```
  
- For Visual Studio or Visual Code
  
  Configure the solution for multiple startup projects and select this projects :
    - ChatRoomWithBot.UI.MVC
    - ChatRoomWithBot.Service.WorkerService