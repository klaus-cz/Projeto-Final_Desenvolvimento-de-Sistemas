FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY src/ChecklistProductivity.Domain/ChecklistProductivity.Domain.csproj src/ChecklistProductivity.Domain/
COPY src/ChecklistProductivity.Application/ChecklistProductivity.Application.csproj src/ChecklistProductivity.Application/
COPY src/ChecklistProductivity.Infrastructure/ChecklistProductivity.Infrastructure.csproj src/ChecklistProductivity.Infrastructure/
COPY src/ChecklistProductivity.Api/ChecklistProductivity.Api.csproj src/ChecklistProductivity.Api/

RUN dotnet restore src/ChecklistProductivity.Api/ChecklistProductivity.Api.csproj

COPY . .
RUN dotnet publish src/ChecklistProductivity.Api/ChecklistProductivity.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "ChecklistProductivity.Api.dll"]
