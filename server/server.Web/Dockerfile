FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["server.Web/server.Web.csproj", "server.Web/"]
COPY ["server.Infrastructure/server.Infrastructure.csproj", "server.Infrastructure/"]
COPY ["server.Core/server.Core.csproj", "server.Core/"]
COPY ["server.Operations/server.Operations.csproj", "server.Operations/"]
RUN dotnet restore "server.Web/server.Web.csproj"
COPY . .
WORKDIR "/src/server.Web"
RUN dotnet build "server.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "server.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "server.Web.dll"]
