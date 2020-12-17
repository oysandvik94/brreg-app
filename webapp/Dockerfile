FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env

ENV ASPNETCORE_URLS=http://+:80

# install NodeJS 13.x
# see https://github.com/nodesource/distributions/blob/master/README.md#deb
RUN apt-get update -yq 
RUN apt-get install curl gnupg -yq 
RUN curl -sL https://deb.nodesource.com/setup_15.x | bash -
RUN apt-get install -y nodejs

COPY . /app
WORKDIR /app
RUN ["dotnet", "restore"]
RUN ["dotnet", "build"]

EXPOSE 80
RUN chmod +x ./entrypoint.sh
CMD /bin/bash ./entrypoint.sh