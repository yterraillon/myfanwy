FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ./Directory.Packages.props ./
COPY ./Web/Web.csproj ./Web/
RUN dotnet restore "./Web/Web.csproj"
COPY . .
WORKDIR "/src/Web"
RUN dotnet build "Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM node:alpine AS tailwind
WORKDIR /npm
COPY --from=build ./src/Web/package*.json ./
RUN npm install
COPY --from=build ./src/Web/ .
RUN ./node_modules/tailwindcss/lib/cli.js -i ./Shared/Styles/input.css -o ./wwwroot/css/output.css --minify

FROM build AS publish
WORKDIR /src/Web
COPY --from=tailwind /npm/ .
RUN dotnet publish "Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
ARG VERSION=unknown
LABEL version=$VERSION
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.dll"]