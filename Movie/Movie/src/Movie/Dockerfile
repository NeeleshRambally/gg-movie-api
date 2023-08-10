FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Movie/src/Movie/Movie.csproj", "Movie/src/Movie/"]
RUN dotnet restore "Movie/src/Movie/Movie.csproj"
COPY . .
WORKDIR "/src/Movie/src/Movie"
RUN dotnet build "Movie.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Movie.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Movie.dll"]
