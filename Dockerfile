FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PortManagementSystemAPI/PortManagementSystemAPI.csproj", "PortManagementSystemAPI/"]
RUN dotnet restore "PortManagementSystemAPI/PortManagementSystemAPI.csproj"
COPY . .
WORKDIR "/src/PortManagementSystemAPI"
RUN dotnet build "PortManagementSystemAPI.csproj" -c Release -o /app/build
RUN dotnet publish "PortManagementSystemAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PortManagementSystemAPI.dll"]
