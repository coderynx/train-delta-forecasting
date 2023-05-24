FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Railsense.Cli/Railsense.Cli.csproj", "Railsense.Cli/"]
RUN dotnet restore "Railsense.Cli/Railsense.Cli.csproj"
COPY . .
WORKDIR "/src/Railsense.Cli"
RUN dotnet build "Railsense.Cli.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Railsense.Cli.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Railsense.Cli.dll"]
