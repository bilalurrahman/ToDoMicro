#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Announcement/Announcement.API/Announcement.API.csproj", "Services/Announcement/Announcement.API/"]
COPY ["Services/Announcement/Annoucement.Integration/Annoucement.Infrastructure.csproj", "Services/Announcement/Annoucement.Integration/"]
COPY ["Services/Announcement/Announcement.Application/Announcement.Application.csproj", "Services/Announcement/Announcement.Application/"]
COPY ["../MessagingQueue.Messages/EventsBus.Messages.csproj", "../MessagingQueue.Messages/"]
RUN dotnet restore "Services/Announcement/Announcement.API/Announcement.API.csproj"
COPY . .
WORKDIR "/src/Services/Announcement/Announcement.API"
RUN dotnet build "Announcement.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Announcement.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Announcement.API.dll"]
