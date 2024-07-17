FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TriatlonREST/TriatlonREST.csproj", "TriatlonREST/"]
RUN dotnet restore "TriatlonREST/TriatlonREST.csproj"
COPY . .
WORKDIR "/src/TriatlonREST"
RUN dotnet build "TriatlonREST.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TriatlonREST.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TriatlonREST.dll"]
