# AutoNaJuzWeb/Dockerfile

# AutoNaJuzWeb/Dockerfile

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 2123

ENV ASPNETCORE_URLS=http://+:2123

USER app

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Declare build arguments
ARG configuration=Debug
ARG DEV_API_URL

# Pass build arguments as environment variables
ENV DOTNET_BUILD_CONFIGURATION=$configuration

# Install Node.js (version 22.x)
RUN apt-get update && \
    apt-get install -y curl gnupg && \
    curl -fsSL https://deb.nodesource.com/setup_22.x | bash - && \
    apt-get install -y nodejs

# Copy and restore as distinct layers
COPY ["AutoNaJuz.Web/AutoNaJuz.Web.csproj", "AutoNaJuz.Web/"]
RUN dotnet restore "AutoNaJuz.Web/AutoNaJuz.Web.csproj"

# Copy the remaining source code
COPY . .
WORKDIR "/src/AutoNaJuz.Web"

# Perform Replacement in apiUrls.js using the build argument
RUN sed -i "s|const API_URL = '.*';|const API_URL = '${DEV_API_URL}';|" wwwroot/apiUrls.js

RUN sed -i "s|const COMMIT_HASH = '.*';|const COMMIT_HASH = '${COMMIT_HASH}';|" ./wwwroot/apiUrls.js

# Build the project
RUN dotnet build "AutoNaJuz.Web.csproj" -c ${configuration} -o /app/build

# Install npm dependencies
RUN npm install

FROM build AS publish
# Use the same build argument for publishing
RUN dotnet publish "AutoNaJuz.Web.csproj" -c ${configuration} -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "AutoNaJuz.Web.dll"]
