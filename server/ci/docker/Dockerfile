FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /server
COPY . .
RUN dotnet restore "./src/Api/Api.csproj"
RUN dotnet build "./src/Api/Api.csproj" -c Release -o /app

##########################################################
# Build tests project
##########################################################


FROM build AS tests
WORKDIR /server

ENTRYPOINT [ "dotnet", "test", "--test-adapter-path:.", "--logger:junit"]


##########################################################
# Publish application
##########################################################

FROM build AS pre-release

RUN dotnet publish "./src/Api/Api.csproj" -c Release -o /app

##########################################################
# Run app
##########################################################

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS local

WORKDIR /app

COPY --from=pre-release /app .
ENTRYPOINT dotnet Api.dll


##########################################################
# Run app
##########################################################

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS release

WORKDIR /app

ENV ASPNETCORE_URLS=$ASPNETCORE_URLS

COPY --from=pre-release /app .
ENTRYPOINT dotnet Api.dll