#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Pomodoros/Pomodoros.API/Pomodoros.API.csproj", "Services/Pomodoros/Pomodoros.API/"]
COPY ["Services/Pomodoros/Pomodoros.Infrastructure/Pomodoros.Infrastructure.csproj", "Services/Pomodoros/Pomodoros.Infrastructure/"]
COPY ["Services/Pomodoros/Pomodoros.Application/Pomodoros.Application.csproj", "Services/Pomodoros/Pomodoros.Application/"]
COPY ["Services/Pomodoros/Pomodoros.Domain/Pomodoros.Domain.csproj", "Services/Pomodoros/Pomodoros.Domain/"]
RUN dotnet restore "Services/Pomodoros/Pomodoros.API/Pomodoros.API.csproj"
COPY . .
WORKDIR "/src/Services/Pomodoros/Pomodoros.API"
RUN dotnet build "Pomodoros.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pomodoros.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pomodoros.API.dll"]
