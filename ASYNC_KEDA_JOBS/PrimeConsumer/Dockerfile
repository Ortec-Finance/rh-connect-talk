#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PrimeConsumer.csproj", "."]
RUN dotnet restore "./PrimeConsumer.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "PrimeConsumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PrimeConsumer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://*:9000
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PrimeConsumer.dll"]