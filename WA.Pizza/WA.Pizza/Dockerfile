#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WA.Pizza/WA.Pizza.Api.csproj", "WA.Pizza/"]
COPY ["WA.Pizza.Infrastructure/WA.Pizza.Infrastructure.csproj", "WA.Pizza.Infrastructure/"]
COPY ["WA.Pizza.Core/WA.Pizza.Core.csproj", "WA.Pizza.Core/"]
RUN dotnet restore "WA.Pizza/WA.Pizza.Api.csproj"
COPY . .
WORKDIR "/src/WA.Pizza"
RUN dotnet build "WA.Pizza.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WA.Pizza.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WA.Pizza.Api.dll"]
