#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PrimeKNative.csproj", "."]
RUN dotnet restore "./PrimeKNative.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "PrimeKNative.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PrimeKNative.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PrimeKNative.dll"]