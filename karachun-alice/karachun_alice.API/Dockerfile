#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["karachun_alice.API/karachun_alice.API.API/karachun_alice.API.API.csproj", "karachun_alice.API/karachun_alice.API.API/"]
COPY ["karachun_alice.API/karachun_alice.API.BI/karachun_alice.API.BI.csproj", "karachun_alice.API/karachun_alice.API.BI/"]
COPY ["karachun_alice.API/karachun_alice.API.Data/karachun_alice.API.Data.csproj", "karachun_alice.API/karachun_alice.API.Data/"]
COPY ["karachun_alice.API/karachun_alice.API.General/karachun_alice.API.General.csproj", "karachun_alice.API/karachun_alice.API.General/"]
RUN dotnet restore "karachun_alice.API/karachun_alice.API.API/karachun_alice.API.API.csproj"
COPY . .
WORKDIR "/src/karachun_alice.API/karachun_alice.API.API"
RUN dotnet build "karachun_alice.API.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "karachun_alice.API.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "karachun_alice.API.API.dll"]