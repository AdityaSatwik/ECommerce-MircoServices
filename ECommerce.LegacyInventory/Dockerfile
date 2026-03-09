FROM mono:latest AS build
WORKDIR /app

# Copy csproj and restore
COPY ["ECommerce.LegacyInventory/ECommerce.LegacyInventory.csproj", "ECommerce.LegacyInventory/"]

# Mono's msbuild can restore PackageReferences directly
RUN msbuild "ECommerce.LegacyInventory/ECommerce.LegacyInventory.csproj" /t:restore

# Copy everything else and build
COPY . .
WORKDIR /app/ECommerce.LegacyInventory
RUN msbuild "ECommerce.LegacyInventory.csproj" /p:Configuration=Release /p:OutputPath=/app/build

FROM mono:latest AS runtime
WORKDIR /app
COPY --from=build /app/build .
EXPOSE 9000
CMD ["mono", "ECommerce.LegacyInventory.exe"]
