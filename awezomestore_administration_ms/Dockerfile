#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["awezomestore_administration_ms/awezomestore_administration_ms.csproj", "awezomestore_administration_ms/"]
RUN dotnet restore "awezomestore_administration_ms/awezomestore_administration_ms.csproj"
COPY . .
WORKDIR "/src/awezomestore_administration_ms"
RUN dotnet build "awezomestore_administration_ms.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "awezomestore_administration_ms.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "awezomestore_administration_ms.dll"]
