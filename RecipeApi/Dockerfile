# Create an image for the Recipe API
FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

COPY RecipeApi.csproj ./RecipeApi/
RUN dotnet restore ./RecipeApi/RecipeApi.csproj

COPY . ./RecipeApi/
WORKDIR /app/RecipeApi
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime

# update system
RUN apt-get update -y && apt-get upgrade -y

# custom env vars - override as reqd.
ENV DOMAINS='localhost 127.0.0.1'

# dotnet specific env vars
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:80
# ENV ASPNETCORE_URLS=http://+:80;https://+:443

# dotnet kestrel env vars
# ENV Kestrel:Certificates:Default:Path=/etc/ssl/private/cert.pfx
# ENV Kestrel:Certificates:Default:Password=changeit
# ENV Kestrel:Certificates:Default:AllowInvalid=true
# ENV Kestrel:EndPointDefaults:Protocols=Http1AndHttp2

# copy certificate authority certs from local file system
# ENV CA_KEY=./rootCA-key.pem
# ENV CA_CERT=./rootCA.pem

# default ca cert location (mkcert)
# COPY ${CA_KEY} /root/.local/share/mkcert/
# COPY ${CA_CERT} /root/.local/share/mkcert/

# install CA and SSL cert
# RUN apt-get install libnss3-tools -y
# RUN curl -L https://github.com/FiloSottile/mkcert/releases/download/v1.3.0/mkcert-v1.3.0-linux-amd64 > /usr/local/bin/mkcert
# RUN chmod +x /usr/local/bin/mkcert
# RUN mkcert -install
# RUN mkcert -p12-file /etc/ssl/private/cert.pfx -pkcs12 $DOMAINS

WORKDIR /app
EXPOSE 80
# EXPOSE 443

COPY --from=build /app/RecipeApi/out ./
ENTRYPOINT ["dotnet", "RecipeApi.dll"]