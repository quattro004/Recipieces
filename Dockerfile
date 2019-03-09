FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln ./
COPY RecipeApi/*.csproj ./RecipeApi/
RUN dotnet restore RecipeApi.sln 

# copy everything else and build app
COPY RecipeApi/. ./RecipeApi/
WORKDIR /app/RecipeApi
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /app
COPY --from=build /app/RecipeApi/out ./
ENTRYPOINT ["dotnet", "RecipeApi.dll"]