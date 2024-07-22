FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["chuck-swapi/chuck-swapi.csproj", "chuck-swapi/"]
RUN dotnet restore "chuck-swapi/chuck-swapi.csproj"

COPY . .
WORKDIR "/src/chuck-swapi"
RUN dotnet build "chuck-swapi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "chuck-swapi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT [ "dotnet", "chuck-swapi.dll" ]