FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["ECommerce.Web/ECommerce.Web.csproj", "ECommerce.Web/"]
RUN dotnet restore "ECommerce.Web/ECommerce.Web.csproj"
COPY . .
WORKDIR "/src/ECommerce.Web"
RUN dotnet build "ECommerce.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ECommerce.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerce.Web.dll"]
