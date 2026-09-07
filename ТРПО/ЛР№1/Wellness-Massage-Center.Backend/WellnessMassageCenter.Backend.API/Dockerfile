# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080


# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WellnessMassageCenter.Backend.API/WellnessMassageCenter.Backend.API.csproj", "WellnessMassageCenter.Backend.API/"]
COPY ["WellnessMassageCenter.Backend.DAL/WellnessMassageCenter.Backend.DAL.csproj", "WellnessMassageCenter.Backend.DAL/"]
COPY ["WellnessMassageCenter.Backend.DTO/WellnessMassageCenter.Backend.DTO.csproj", "WellnessMassageCenter.Backend.DTO/"]
COPY ["WellnessMassageCenter.Backend.Services/WellnessMassageCenter.Backend.Services.csproj", "WellnessMassageCenter.Backend.Services/"]

RUN dotnet restore "./WellnessMassageCenter.Backend.API/WellnessMassageCenter.Backend.API.csproj"
COPY . .
WORKDIR "/src/WellnessMassageCenter.Backend.API"
RUN dotnet build "./WellnessMassageCenter.Backend.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WellnessMassageCenter.Backend.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WellnessMassageCenter.Backend.API.dll"]