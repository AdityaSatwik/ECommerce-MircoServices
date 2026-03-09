FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["ECommerce.CatalogService/ECommerce.CatalogService.csproj", "ECommerce.CatalogService/"]
RUN dotnet restore "ECommerce.CatalogService/ECommerce.CatalogService.csproj"
COPY . .
WORKDIR "/src/ECommerce.CatalogService"
RUN dotnet build "ECommerce.CatalogService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ECommerce.CatalogService.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerce.CatalogService.dll"]
