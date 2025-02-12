# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/UI/ChatRoomWithBot.UI.MVC/ChatRoomWithBot.UI.MVC.csproj", "src/UI/ChatRoomWithBot.UI.MVC/"]
COPY ["src/Application/ChatRoomWithBot.Application/ChatRoomWithBot.Application.csproj", "src/Application/ChatRoomWithBot.Application/"]
COPY ["src/Domain/ChatRoomWithBot.Domain/ChatRoomWithBot.Domain.csproj", "src/Domain/ChatRoomWithBot.Domain/"]
COPY ["src/Data/ChatRoomWithBot.Data/ChatRoomWithBot.Data.csproj", "src/Data/ChatRoomWithBot.Data/"]
COPY ["src/Infra/ChatRoomWithBot.Infra.HttpRequest/ChatRoomWithBot.Infra.HttpRequest.Infra.HttpRequest.csproj", "src/Infra/ChatRoomWithBot.Infra.HttpRequest/"]
COPY ["src/Services/ChatRoomWithBot.Services.BerechitLogger/ChatRoomWithBot.Services.BerechitLogger.csproj", "src/Services/ChatRoomWithBot.Services.BerechitLogger/"]
COPY ["src/Services/ChatRoomWithBot.Services.RabbitMq/ChatRoomWithBot.Services.RabbitMq.csproj", "src/Services/ChatRoomWithBot.Services.RabbitMq/"]
RUN dotnet restore "./src/UI/ChatRoomWithBot.UI.MVC/ChatRoomWithBot.UI.MVC.csproj"
COPY . .
WORKDIR "/src/src/UI/ChatRoomWithBot.UI.MVC"
RUN dotnet build "./ChatRoomWithBot.UI.MVC.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ChatRoomWithBot.UI.MVC.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatRoomWithBot.UI.MVC.dll"]