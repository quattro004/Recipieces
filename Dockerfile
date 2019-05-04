# Create an image for the Recipieces website
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
WORKDIR /app
ENV ASPNETCORE_URLS http://+:80
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS publish
WORKDIR /src
COPY ./RecipiecesWeb ./RecipiecesWeb
COPY ./RecipeUIClassLib ./RecipeUIClassLib
RUN dotnet restore ./RecipeUIClassLib/RecipeUIClassLib.csproj /ignoreprojectextensions:.dcproj
RUN dotnet restore ./RecipiecesWeb/RecipiecesWeb.csproj /ignoreprojectextensions:.dcproj 
RUN dotnet publish ./RecipiecesWeb/RecipiecesWeb.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app ./
ENTRYPOINT ["dotnet", "RecipiecesWeb.dll"]