#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8888
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["powerplant-coding-challenge/powerplant-coding-challenge.csproj", "powerplant-coding-challenge/"]
RUN dotnet restore "./powerplant-coding-challenge/./powerplant-coding-challenge.csproj"
COPY . .
WORKDIR "/src/powerplant-coding-challenge"
RUN dotnet build "./powerplant-coding-challenge.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./powerplant-coding-challenge.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "powerplant-coding-challenge.dll"]