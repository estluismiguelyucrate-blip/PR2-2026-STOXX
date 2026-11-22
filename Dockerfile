FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY src/Stoxx.Web/Stoxx.Web.csproj src/Stoxx.Web/
RUN dotnet restore src/Stoxx.Web/Stoxx.Web.csproj
COPY . .
RUN dotnet publish src/Stoxx.Web/Stoxx.Web.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "Stoxx.Web.dll"]
