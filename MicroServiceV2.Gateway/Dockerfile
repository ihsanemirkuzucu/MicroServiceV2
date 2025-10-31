# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MicroServiceV2.Gateway/MicroServiceV2.Gateway.csproj", "MicroServiceV2.Gateway/"]
COPY ["MicroServiceV2.Shared/MicroServiceV2.Shared.csproj", "MicroServiceV2.Shared/"]
RUN dotnet restore "./MicroServiceV2.Gateway/MicroServiceV2.Gateway.csproj"
COPY . .
WORKDIR "/src/MicroServiceV2.Gateway"
RUN dotnet build "./MicroServiceV2.Gateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MicroServiceV2.Gateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroServiceV2.Gateway.dll"]