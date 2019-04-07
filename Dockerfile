# Create an image for the Recipieces website
FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

COPY RecipiecesWeb/RecipiecesWeb.csproj ./RecipiecesWeb/
COPY RecipeUIClassLib/RecipeUIClassLib.csproj ./RecipeUIClassLib/
RUN dotnet restore ./RecipeUIClassLib/RecipeUIClassLib.csproj 
RUN dotnet restore ./RecipiecesWeb/RecipiecesWeb.csproj 

COPY ./RecipiecesWeb/ ./RecipiecesWeb/
COPY ./RecipeUIClassLib/ ./RecipeUIClassLib/
WORKDIR /app/RecipiecesWeb
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /app
COPY --from=build /app/RecipiecesWeb/out ./
ENTRYPOINT ["dotnet", "RecipiecesWeb.dll"]