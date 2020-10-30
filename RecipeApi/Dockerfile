# Create an image for the Recipe API
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["RecipeApi/RecipeApi.csproj", "RecipeApi/"]
COPY ["Api.Domain/Api.Domain.csproj", "Api.Domain/"]
COPY ["Api.Infrastructure/Api.Infrastructure.csproj", "Api.Infrastructure/"]
RUN dotnet restore "RecipeApi/RecipeApi.csproj"

COPY ["RecipeApi/", "RecipeApi/"]
COPY ["Api.Infrastructure/", "Api.Infrastructure/"]
COPY ["Api.Domain/", "Api.Domain/"]
WORKDIR "/src/RecipeApi"
RUN dotnet build "RecipeApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RecipeApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RecipeApi.dll"]