# Create an image for the Album API
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
ENV ASPNETCORE_URLS http://+:80
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS publish
WORKDIR /src
COPY . ./AlbumApi
RUN dotnet restore AlbumApi/AlbumApi.csproj /ignoreprojectextensions:.dcproj
RUN dotnet publish AlbumApi/AlbumApi.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app ./
ENTRYPOINT ["dotnet", "AlbumApi.dll"]