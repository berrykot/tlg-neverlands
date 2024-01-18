FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["tlg_neverlands/tlg_neverlands.csproj", "tlg_neverlands/"]
RUN dotnet restore "tlg_neverlands/tlg_neverlands.csproj"
COPY . .
WORKDIR "/src/tlg_neverlands"
RUN dotnet build "tlg_neverlands.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "tlg_neverlands.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "tlg_neverlands.dll"]
