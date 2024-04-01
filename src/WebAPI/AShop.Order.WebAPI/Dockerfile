FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/WebAPI/AShop.Order.WebAPI/AShop.Order.WebAPI.csproj", "src/WebAPI/AShop.Order.WebAPI/"]
COPY ["src/Application/AShop.Order.Application/AShop.Order.Application.csproj", "src/Application/AShop.Order.Application/"]
COPY ["src/Infrastructure/AShop.Order.Infrastructure/AShop.Order.Infrastructure.csproj", "src/Infrastructure/AShop.Order.Infrastructure/"]
COPY ["src/Domain/AShop.Order.Domain/AShop.Order.Domain.csproj", "src/Domain/AShop.Order.Domain/"]
COPY ["src/Commons/AShop.EventBus.Message/AShop.EventBus.Message.csproj", "src/Commons/AShop.EventBus.Message/"]
COPY ["src/Commons/AShop.Common.Logging/AShop.Common.Logging.csproj", "src/Commons/AShop.Common.Logging/"]
RUN dotnet restore "src/WebAPI/AShop.Order.WebAPI/AShop.Order.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/WebAPI/AShop.Order.WebAPI"
RUN dotnet build "AShop.Order.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AShop.Order.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AShop.Order.WebAPI.dll"]
