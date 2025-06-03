FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.sln ./
COPY FreelanceApp.Api/FreelanceApp.Api.csproj ./FreelanceApp.Api/

COPY . .

RUN dotnet restore FreelanceApp.sln
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80

ENTRYPOINT ["dotnet", "FreelanceApp.Api.dll"]