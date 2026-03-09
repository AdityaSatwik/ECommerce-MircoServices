FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["ECommerce.OrderService/ECommerce.OrderService.csproj", "ECommerce.OrderService/"]
RUN dotnet restore "ECommerce.OrderService/ECommerce.OrderService.csproj"
COPY . .
WORKDIR "/src/ECommerce.OrderService"
RUN dotnet build "ECommerce.OrderService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ECommerce.OrderService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerce.OrderService.dll"]
