#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Localization/Localization.Grpc/Localization.Grpc.csproj", "Services/Localization/Localization.Grpc/"]
COPY ["Services/Localization/Localization.Integration/Localization.Integration.csproj", "Services/Localization/Localization.Integration/"]
COPY ["Services/Localization/Localization.Core/Localization.Application.csproj", "Services/Localization/Localization.Core/"]
COPY ["Services/Localization/Localization.Domain/Localization.Domain.csproj", "Services/Localization/Localization.Domain/"]
RUN dotnet restore "Services/Localization/Localization.Grpc/Localization.Grpc.csproj"
COPY . .
WORKDIR "/src/Services/Localization/Localization.Grpc"
RUN dotnet build "Localization.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Localization.Grpc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Localization.Grpc.dll"]
