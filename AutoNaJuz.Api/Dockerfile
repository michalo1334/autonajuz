# AutoNaJuzApi/Dockerfile

# AutoNaJuzApi/Dockerfile

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 3123

ENV ASPNETCORE_URLS=http://+:3123

USER app

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Declare build arguments
ARG configuration=Release
ARG DEV_API_URL
ARG COMMIT_HASH
ARG VERSION_MAJOR
ARG VERSION_MINOR
ARG VERSION_PATCH

# Pass build arguments as environment variables
ENV DOTNET_BUILD_CONFIGURATION=$configuration

# Copy and restore as distinct layers
COPY ["AutoNaJuz.Api/AutoNaJuz.Api.csproj", "AutoNaJuz.Api/"]
RUN dotnet restore "AutoNaJuz.Api/AutoNaJuz.Api.csproj"

# Copy the remaining source code
COPY . .
WORKDIR "/src/AutoNaJuz.Api"

# Perform Replacement in app.js using the build argument
RUN sed -i "s|const API_URL = '.*';|const API_URL = '${DEV_API_URL}';|" ./wwwroot/js/app.js

RUN sed -i "s|const COMMIT_HASH = '.*';|const COMMIT_HASH = '${COMMIT_HASH}';|" ./wwwroot/js/app.js

RUN sed -i "s|const V_MAJOR = '.*';|const V_MAJOR = '${VERSION_MAJOR}';|" ./wwwroot/js/app.js
RUN sed -i "s|const V_MINOR = '.*';|const V_MINOR = '${VERSION_MINOR}';|" ./wwwroot/js/app.js
RUN sed -i "s|const V_PATCH = '.*';|const V_PATCH = '${VERSION_PATCH}';|" ./wwwroot/js/app.js

# Build the project
RUN dotnet build "AutoNaJuz.Api.csproj" -c ${configuration} -o /app/build

FROM build AS publish
# Use the same build argument for publishing
RUN dotnet publish "AutoNaJuz.Api.csproj" -c ${configuration} -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "AutoNaJuz.Api.dll"]
