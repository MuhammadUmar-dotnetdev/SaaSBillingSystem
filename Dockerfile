FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["SaaSBillingSystem.API/SaaSBillingSystem.API.csproj", "SaaSBillingSystem.API/"]
COPY ["SaaSBillingSystem.Application/SaaSBillingSystem.Application.csproj", "SaaSBillingSystem.Application/"]
COPY ["SaaSBillingSystem.Domain/SaaSBillingSystem.Domain.csproj", "SaaSBillingSystem.Domain/"]
COPY ["SaaSBillingSystem.Infrastructure/SaaSBillingSystem.Infrastructure.csproj", "SaaSBillingSystem.Infrastructure/"]
COPY ["SaaSBillingSystem.Shared/SaaSBillingSystem.Shared.csproj", "SaaSBillingSystem.Shared/"]

RUN dotnet restore "SaaSBillingSystem.API/SaaSBillingSystem.API.csproj"

COPY . .

WORKDIR "/src/SaaSBillingSystem.API"

RUN dotnet publish \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "SaaSBillingSystem.API.dll"]