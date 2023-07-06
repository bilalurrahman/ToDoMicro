#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Tasks/Tasks.API/Tasks.API.csproj", "Services/Tasks/Tasks.API/"]
COPY ["../Tasks.Application/Tasks.Application.csproj", "../Tasks.Application/"]
COPY ["../Tasks.Domain/Tasks.Domain.csproj", "../Tasks.Domain/"]
COPY ["../Tasks.Infrastructure/Tasks.Infrastructure.csproj", "../Tasks.Infrastructure/"]
RUN dotnet restore "Services/Tasks/Tasks.API/Tasks.API.csproj"
COPY . .
WORKDIR "/src/Services/Tasks/Tasks.API"
RUN dotnet build "Tasks.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tasks.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tasks.API.dll"]
