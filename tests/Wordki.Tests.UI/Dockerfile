FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src

# https://github.com/wv-gis/mudak-wrm-public/issues/2
ENV APT_KEY_DONT_WARN_ON_DANGEROUS_USAGE=DontWarn

# Install chrome
RUN wget -q -O - https://dl-ssl.google.com/linux/linux_signing_key.pub | apt-key add - &&\
	sh -c 'echo "deb [arch=amd64] http://dl.google.com/linux/chrome/deb/ stable main" >> /etc/apt/sources.list.d/google.list' &&\
	apt-get update &&\
    apt-get install -yq google-chrome-stable
	
RUN apt-cache policy google-chrome-stable

COPY . .

RUN dotnet restore Wordki.Tests.UI.csproj

ENTRYPOINT dotnet test --test-adapter-path:. --logger:"junit;LogFilepath=TESTS-ui-e2e.xml"
