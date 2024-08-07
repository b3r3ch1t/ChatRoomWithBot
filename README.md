
# ChatRoomWithBot with .NET CORE 8 | SignalR | RabbitMQ
This is a browser-based chat application built with .NET CORE 8. It supports real-time communication in chat rooms and allows users to retrieve stock quotes using specific commands.


## Release Notes

**Version Upgrade**:
- Updated from ASP.NET Core 6 to ASP.NET Core 8.
- Updated from Entity Framework Core 6 to Entity Framework Core 8.

**Fixes**:
- General bug fixes and performance improvements.


## Key Features
- **Chat Room**: Users can engage in conversations within browser-based chat rooms.
- **User Authentication**: Authentication is managed using ASP.NET Identity Core and Entity Framework Core with MS SQL Server.
- **Stock Commands**: Users can retrieve stock quotes by entering commands in the format `/stock=stock_code`. Examples: `/stock=AAPL.US`, `/stock=MSFT.US`.
- **Message Broker**: RabbitMQ is used for message brokering between chat rooms and the stock quote API (https://stooq.com).
- **Worker Service**: A background worker service processes commands and returns the data to the chat room.
- **Message History**: Displays the last 50 messages in each chat room, ordered by timestamp.

## Technologies Used
- ASP.NET Core 8.0
- Entity Framework Core 8.0
- ASP.NET Identity Core
- SignalR
- RabbitMQ with MassTransit
- AutoMapper
- FluentValidator
- MediatR
- Serilog
- Sentry
- CsvHelper
- Bogus
- Moq
- Docker

## Architecture
- **Clean Architecture** with separation of concerns.
- **Domain Driven Design (DDD)**: Including domain events and validations.
- **CQRS Pattern**: Immediate consistency.
- **Unit Testing**: Example tests using Moq and other testing tools.

## Project Structure
| Project | Description |
| ------ | ------ |
| ChatRoomWithBot.UI.MVC | Web Application |
| ChatRoomWithBot.Application | Task coordination and delegation to domain objects and repositories |
| ChatRoomWithBot.Domain | Business logic and domain processes |
| ChatRoomWithBot.Service.Identity | User authentication with ASP.NET Identity Core |
| ChatRoomWithBot.Services.BerechitLogger | Logging services |
| ChatRoomWithBot.Services.RabbitMq | RabbitMQ message publishing and consuming |
| ChatRoomWithBot.Data | Repositories and database connection (MS SQL Server) |
| ChatRoomWithBot.Service.WorkerService | Background worker service for RabbitMQ |
| ChatRoomWithBot.Data.Test | Unit tests for Data Layer |
| ChatRoomWithBot.Domain.Test | Unit tests for Domain Layer |

## Configuration
No need to manually create initial users and chat rooms—the system is pre-configured to seed the database with sample users and chat rooms using the `Seed()` method.

| User | Password |
| ------ | ------ |
| user1@teste.com | Test12345678 |
| user2@teste.com | Test12345678 |

| Chat Room  | 
| ------ |
| Room 1 |
| Room 2 |
| Room 3 |

## Running the Project

You can run the project using Docker or directly in Visual Studio or Visual Code:

- **Docker**:
  ```sh
  docker-compose up -d
  ```

- **Visual Studio/Visual Code**:
  Configure the solution for multiple startup projects and select:
  - ChatRoomWithBot.UI.MVC
  - ChatRoomWithBot.Service.WorkerService
