#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RestClient/RestClient.csproj", "RestClient/"]
RUN dotnet restore "RestClient/RestClient.csproj"

COPY . .
WORKDIR "/src/RestClient"
RUN dotnet build "RestClient.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RestClient.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RestClient.dll"]