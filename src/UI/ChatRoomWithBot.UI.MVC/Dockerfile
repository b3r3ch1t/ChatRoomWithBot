#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/UI/ChatRoomWithBot.UI.MVC/ChatRoomWithBot.UI.MVC.csproj", "src/UI/ChatRoomWithBot.UI.MVC/"]
COPY ["src/Application/ChatRoomWithBot.Application/ChatRoomWithBot.Application.csproj", "src/Application/ChatRoomWithBot.Application/"]
COPY ["src/Data/ChatRoomWithBot.Data/ChatRoomWithBot.Data.csproj", "src/Data/ChatRoomWithBot.Data/"]
COPY ["src/Domain/ChatRoomWithBot.Domain/ChatRoomWithBot.Domain.csproj", "src/Domain/ChatRoomWithBot.Domain/"]
COPY ["src/Services/ChatRoomWithBot.Service.Identity/ChatRoomWithBot.Service.Identity.csproj", "src/Services/ChatRoomWithBot.Service.Identity/"]
COPY ["src/Services/ChatRoomWithBot.Services.BerechitLogger/ChatRoomWithBot.Services.BerechitLogger.csproj", "src/Services/ChatRoomWithBot.Services.BerechitLogger/"]
COPY ["src/Services/ChatRoomWithBot.Services.RabbitMq/ChatRoomWithBot.Services.RabbitMq.csproj", "src/Services/ChatRoomWithBot.Services.RabbitMq/"]
RUN dotnet restore "src/UI/ChatRoomWithBot.UI.MVC/ChatRoomWithBot.UI.MVC.csproj"
COPY . .
WORKDIR "/src/src/UI/ChatRoomWithBot.UI.MVC"
RUN dotnet build "ChatRoomWithBot.UI.MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChatRoomWithBot.UI.MVC.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatRoomWithBot.UI.MVC.dll"]