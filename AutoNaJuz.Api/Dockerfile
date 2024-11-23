FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 3123

ENV ASPNETCORE_URLS=http://+:3123

USER app

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Debug
WORKDIR src

COPY ["AutoNaJuz.Api/AutoNaJuz.Api.csproj", "AutoNaJuz.Api/"]
RUN dotnet restore "AutoNaJuz.Api/AutoNaJuz.Api.csproj"
COPY . .
WORKDIR "/src/AutoNaJuz.Api"

# **Perform Replacement in app.js**
# UGLY, parametrize it (npm script and config?)
RUN ls -la
RUN sed -i "s|const API_URL = '.*';|const API_URL = 'https://api.dev.michalo.works/api';|" ./wwwroot//js/app.js


RUN dotnet build "AutoNaJuz.Api.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "AutoNaJuz.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "AutoNaJuz.Api.dll"]
