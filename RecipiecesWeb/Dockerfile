# Create an image for the Recipieces website
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
RUN apt-get update && curl -sL https://deb.nodesource.com/setup_14.x | bash -
RUN apt-get install -y nodejs

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
RUN apt-get update && curl -sL https://deb.nodesource.com/setup_14.x | bash -
RUN apt-get install -y nodejs
WORKDIR /src
COPY ["RecipiecesWeb/RecipiecesWeb.csproj", "RecipiecesWeb/"]
COPY ["RecipeUIClassLib/RecipeUIClassLib.csproj", "RecipeUIClassLib/"]
COPY ["Api.Infrastructure/Api.Infrastructure.csproj", "Api.Infrastructure/"]
COPY ["Api.Domain/Api.Domain.csproj", "Api.Domain/"]
RUN dotnet restore "RecipiecesWeb/RecipiecesWeb.csproj"

COPY ["RecipiecesWeb/", "RecipiecesWeb/"]
COPY ["RecipeUIClassLib/", "RecipeUIClassLib/"]
COPY ["Api.Infrastructure/", "Api.Infrastructure/"]
COPY ["Api.Domain/", "Api.Domain/"]
WORKDIR "/src/RecipiecesWeb"
RUN dotnet build "RecipiecesWeb.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RecipiecesWeb.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RecipiecesWeb.dll"]