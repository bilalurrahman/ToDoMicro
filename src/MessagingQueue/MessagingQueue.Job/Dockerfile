#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MessagingQueue/MessagingQueue.Job/EventBus.Job.csproj", "MessagingQueue/MessagingQueue.Job/"]
COPY ["Services/Shared/Shared/SharedKernal.csproj", "Services/Shared/Shared/"]
COPY ["Services/Shared/SharedKernal.Core/SharedKernal.Core.csproj", "Services/Shared/SharedKernal.Core/"]
RUN dotnet restore "MessagingQueue/MessagingQueue.Job/EventBus.Job.csproj"
COPY . .
WORKDIR "/src/MessagingQueue/MessagingQueue.Job"
RUN dotnet build "EventBus.Job.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EventBus.Job.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EventBus.Job.dll"]
