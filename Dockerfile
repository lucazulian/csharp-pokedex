FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base

ENV ASPNETCORE_URLS=https://+:5001;http://+:5000
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/CsharpPokedex.Api/CsharpPokedex.Api.csproj", "CsharpPokedex.Api/"]
RUN dotnet restore "CsharpPokedex.Api/CsharpPokedex.Api.csproj"
COPY ["src/CsharpPokedex.Api/", "CsharpPokedex.Api/"]
WORKDIR "CsharpPokedex.Api"
RUN dotnet build "CsharpPokedex.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CsharpPokedex.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CsharpPokedex.Api.dll"]
