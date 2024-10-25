FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 2123

ENV ASPNETCORE_URLS=http://+:2123

USER app

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src

# Install Node.js (version 20.x)
RUN apt-get update && \
    apt-get install -y curl gnupg && \
    curl -fsSL https://deb.nodesource.com/setup_22.x | bash - && \
    apt-get install -y nodejs

COPY ["AutoNaJuz.Web/AutoNaJuz.Web.csproj", "AutoNaJuz.Web/"]
RUN dotnet restore "AutoNaJuz.Web/AutoNaJuz.Web.csproj"
COPY . .
WORKDIR "/src/AutoNaJuz.Web"

RUN dotnet build "AutoNaJuz.Web.csproj" -c $configuration -o /app/build

RUN npm install

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "AutoNaJuz.Web.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AutoNaJuz.Web.dll"]