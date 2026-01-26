# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia apenas os csproj para cache de restore
COPY ["src/Contratacao.Api/Contratacao.Api.csproj", "src/Contratacao.Api/"]
COPY ["src/Contratacao.Infra.CrossCuting/Contratacao.Infra.CrossCuting.csproj", "src/Contratacao.Infra.CrossCuting/"]
COPY ["src/Contratacao.Infra.Data/Contratacao.Infra.Data.csproj", "src/Contratacao.Infra.Data/"]
COPY ["src/Contratacao.Domain/Contratacao.Domain.csproj", "src/Contratacao.Domain/"]
COPY ["src/Contratacao.Application/Contratacao.Application.csproj", "src/Contratacao.Application/"]

# Restaura pacotes
RUN dotnet restore "src/Contratacao.Api/Contratacao.Api.csproj"

# Copia todo o código
COPY . .

# Publica especificando **o caminho completo do csproj**
RUN dotnet publish "src/Contratacao.Api/Contratacao.Api.csproj" -c Debug -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Variáveis de ambiente para Development
ENV ASPNETCORE_ENVIRONMENT=Development
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_USE_POLLING_FILE_WATCHER=1
ENV ASPNETCORE_URLS=http://+:4091

# Copia os binários
COPY --from=build /app/publish .

EXPOSE 4091

ENTRYPOINT ["dotnet", "Contratacao.Api.dll"]
