# ChatRoomWithBot with .NET CORE 8 | SignalR | RabbitMQ

This is a browser-based chat application built with .NET CORE 8. It supports real-time communication in chat rooms and allows users to retrieve stock quotes using specific commands. The system integrates with Entra ID to fetch user groups, which function as chat rooms. Messages are processed using RabbitMQ, where a message is published and subsequently consumed to trigger a RestAPI request. The solution follows Clean Architecture principles and Domain-Driven Design (DDD).

## Key Features

- **Chat Room**: Users can engage in real-time conversations within browser-based chat rooms.

- **User Authentication**: Authentication is managed using ASP.NET Identity Core and integrated with Entra ID.

- **Group-Based Chat Rooms**: The system retrieves groups from Entra ID, with each group representing a chat room.

- **Stock Commands**: Users can retrieve stock quotes by entering commands in the format `/stock=stock_code`. Examples: `/stock=AAPL.US`, `/stock=MSFT.US`.

- **Message Broker**: RabbitMQ is used for message brokering between chat rooms and the stock quote API (https://stooq.com).

- **Worker Service**: A background worker service processes commands and returns the data to the chat room.

- **Message Processing with RabbitMQ**: Messages are sent to RabbitMQ, processed asynchronously, and trigger a REST API request.

- **Message History**: Displays the last 50 messages in each chat room, ordered by timestamp.

- **Robust Logging and Monitoring**: Includes Serilog and Sentry for logging and error tracking.

- **Resilient API Communication**: Implements Polly for retry policies and error handling.

## Technologies Used

- .NET Core 8.0

- Entity Framework Core 8.0

- ASP.NET Identity Core

- SignalR

- RabbitMQ with MassTransit

- MediatR

- FluentValidation

- Serilog

- Sentry

- Polly

- RestSharp

- AutoMapper

- CsvHelper

- Bogus

- Moq

- Docker

## Architecture

- **Clean Architecture** with a strong separation of concerns.

- **Domain-Driven Design (DDD)**: Leveraging domain events and business logic encapsulation.

- **CQRS Pattern**: Implements immediate consistency for queries and commands.

- **Microservices and Event-Driven Architecture**: RabbitMQ facilitates communication between different services.

- **Secure Authentication**: Integrated with Entra ID for enterprise-level authentication and authorization.

- **DevOps and CI/CD**: Configured for Azure DevOps with automated deployments.

- **Containerized Deployment**: Runs using Docker and Docker Compose for seamless scalability.

## Project Structure

| Project                                 | Description                                                             |
| --------------------------------------- | ----------------------------------------------------------------------- |
| ChatRoomWithBot.UI.MVC                  | Web Application                                                         |
| ChatRoomWithBot.Application             | Task coordination and delegation to domain objects and repositories     |
| ChatRoomWithBot.Domain                  | Business logic and domain processes                                     |
| ChatRoomWithBot.Service.Identity        | User authentication with ASP.NET Identity Core and Entra ID integration |
| ChatRoomWithBot.Services.BerechitLogger | Logging services (Serilog, Sentry)                                      |
| ChatRoomWithBot.Services.RabbitMq       | RabbitMQ message publishing and consuming                               |
| ChatRoomWithBot.Data                    | Repositories and database connection (MS SQL Server)                    |
| ChatRoomWithBot.Service.WorkerService   | Background worker service for RabbitMQ message processing               |
| ChatRoomWithBot.Data.Test               | Unit tests for Data Layer                                               |
| ChatRoomWithBot.Domain.Test             | Unit tests for Domain Layer                                             |

## Running the Project

You can run the project using Docker or directly in Visual Studio or Visual Code:

- **Docker**:
  
  ```
  docker-compose up -d
  ```

- **Visual Studio/Visual Code**:
  Configure the solution for multiple startup projects and select:
  
  - ChatRoomWithBot.UI.MVC
  
  - ChatRoomWithBot.Service.WorkerService
