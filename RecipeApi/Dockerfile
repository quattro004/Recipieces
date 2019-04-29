# Create an image for the Recipe API
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS publish
WORKDIR /src
COPY . .
RUN dotnet restore /ignoreprojectextensions:.dcproj
#WORKDIR /src/RecipeApi
RUN dotnet publish RecipeApi.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app ./
ENTRYPOINT ["dotnet", "RecipeApi.dll"]