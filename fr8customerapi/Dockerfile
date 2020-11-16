#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["fr8customerapi/fr8customerapi.csproj", "fr8customerapi/"]
COPY ["fr8model/fr8model.csproj", "fr8model/"]
RUN dotnet restore "fr8customerapi/fr8customerapi.csproj"
COPY . .
WORKDIR "/src/fr8customerapi"
RUN dotnet build "fr8customerapi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "fr8customerapi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "fr8customerapi.dll"]