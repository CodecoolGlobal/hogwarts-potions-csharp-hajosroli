FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5076
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HogwartsPotions.csproj", "./"]
RUN dotnet restore "HogwartsPotions.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "HogwartsPotions.csproj" -c Release -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "HogwartsPotions.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HogwartsPotions.dll"]

