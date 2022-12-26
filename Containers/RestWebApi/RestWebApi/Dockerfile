#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
ARG PROJECTXL_PACKAGES_USERNAME
ARG PROJECTXL_PACKAGES_PASSWORD
ARG SHARED_PACKAGES_USERNAME
ARG SHARED_PACKAGES_PASSWORD
COPY NuGet.Config NuGet.Config
COPY ["RestWebApi.BLL/RestWebApi.BLL.csproj", "RestWebApi.BLL/"]
COPY ["RestWebApi.DAL/RestWebApi.DAL.csproj", "RestWebApi.DAL/"]
COPY ["RestWebApi/RestWebApi.csproj", "RestWebApi/"]
RUN dotnet nuget update source ProjectXL.Packages --username $PROJECTXL_PACKAGES_USERNAME --password $PROJECTXL_PACKAGES_PASSWORD --store-password-in-clear-text \
	&& dotnet nuget update source Shared.Packages --username $SHARED_PACKAGES_USERNAME --password $SHARED_PACKAGES_PASSWORD --store-password-in-clear-text
RUN dotnet restore "RestWebApi/RestWebApi.csproj"

COPY . .
WORKDIR "/src/RestWebApi"
RUN dotnet build "RestWebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RestWebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RestWebApi.dll"]