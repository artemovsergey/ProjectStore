FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ProjectStore.API/ProjectStore.API.csproj", "ProjectStore.API/"]
COPY ["ProjectStore.Application/ProjectStore.Application.csproj", "ProjectStore.Application/"]
COPY ["ProjectStore.Domen/ProjectStore.Domen.csproj", "ProjectStore.Domen/"]
COPY ["ProjectStore.Infrastructure/ProjectStore.Infrastructure.csproj", "ProjectStore.Infrastructure/"]
RUN dotnet restore "ProjectStore.API/ProjectStore.API.csproj"
COPY . .
WORKDIR "/src/ProjectStore.API"
RUN dotnet build "ProjectStore.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Тестиррование
#FROM build AS tests
#RUN dotnet test "ProjectStore.API/ProjectStore.API.csproj"

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ProjectStore.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

COPY ./certificate/localhost.key /app/certificate/localhost.key
COPY ./certificate/localhost.crt /app/certificate/localhost.crt

#COPY ./ProjectStore.API/appsettings.Production.json /app/appsettings.Production.json
#COPY ./ProjectStore.API/appsettings.json /app/appsettings.json

ENTRYPOINT ["dotnet", "ProjectStore.API.dll"]
