#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/ChatRoomWithBot.Service.WorkerService/ChatRoomWithBot.Service.WorkerService.csproj", "src/Services/ChatRoomWithBot.Service.WorkerService/"]
COPY ["src/Domain/ChatRoomWithBot.Domain/ChatRoomWithBot.Domain.csproj", "src/Domain/ChatRoomWithBot.Domain/"]
RUN dotnet restore "./src/Services/ChatRoomWithBot.Service.WorkerService/ChatRoomWithBot.Service.WorkerService.csproj"
COPY . .
WORKDIR "/src/src/Services/ChatRoomWithBot.Service.WorkerService"
RUN dotnet build "./ChatRoomWithBot.Service.WorkerService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ChatRoomWithBot.Service.WorkerService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatRoomWithBot.Service.WorkerService.dll"]