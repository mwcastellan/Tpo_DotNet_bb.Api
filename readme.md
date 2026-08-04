DOCKER:
1) 
# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "Tpo_DotNet_bb.Api.dll"]
2) 
docker build -t tpo-dotnet-api .
3)
docker run -p 8080:8080 tpo-dotnet-api Local Testing
docker run -p 8080:80 tpo-dotnet-api -- LOCAL TESTING
--> http://localhost:8080/swagger todos los router y controllers.
