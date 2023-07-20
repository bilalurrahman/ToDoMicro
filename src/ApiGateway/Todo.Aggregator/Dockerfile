#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ApiGateway/Todo.Aggregator/Todo.Aggregator.API.csproj", "ApiGateway/Todo.Aggregator/"]
COPY ["Services/Shared/Shared/SharedKernal.csproj", "Services/Shared/Shared/"]
COPY ["Services/Shared/SharedKernal.Core/SharedKernal.Core.csproj", "Services/Shared/SharedKernal.Core/"]
COPY ["ApiGateway/TodoAggregator/Todo.Aggregator.Infrastructure/Todo.Aggregator.Infrastructure.csproj", "ApiGateway/TodoAggregator/Todo.Aggregator.Infrastructure/"]
COPY ["../Todo.Aggregator.Application/Todo.Aggregator.Application.csproj", "../Todo.Aggregator.Application/"]
RUN dotnet restore "ApiGateway/Todo.Aggregator/Todo.Aggregator.API.csproj"
COPY . .
WORKDIR "/src/ApiGateway/Todo.Aggregator"
RUN dotnet build "Todo.Aggregator.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Todo.Aggregator.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Todo.Aggregator.API.dll"]
