#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Authentication/Authentication.API/Authentication.API.csproj", "Services/Authentication/Authentication.API/"]
COPY ["Services/Authentication/Authentication.Infrastructure/Authentication.Infrastructure.csproj", "Services/Authentication/Authentication.Infrastructure/"]
COPY ["Services/Authentication/Authentication.Application/Authentication.Application.csproj", "Services/Authentication/Authentication.Application/"]
COPY ["Services/Authentication/Authentication.Common/Authentication.Common.csproj", "Services/Authentication/Authentication.Common/"]
COPY ["Services/Authentication/Authentication.Domain/Authentication.Domain.csproj", "Services/Authentication/Authentication.Domain/"]
RUN dotnet restore "Services/Authentication/Authentication.API/Authentication.API.csproj"
COPY . .
WORKDIR "/src/Services/Authentication/Authentication.API"
RUN dotnet build "Authentication.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Authentication.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Authentication.API.dll"]