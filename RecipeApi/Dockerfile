# Create an image for the Recipe API
FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

COPY RecipeApi.csproj ./RecipeApi/
RUN dotnet restore ./RecipeApi/RecipeApi.csproj

COPY . ./RecipeApi/
WORKDIR /app/RecipeApi
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /app
COPY --from=build /app/RecipeApi/out ./
ENTRYPOINT ["dotnet", "RecipeApi.dll"]