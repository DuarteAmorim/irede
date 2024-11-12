## See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
## This stage is used when running from VS in fast mode (Default for Debug configuration)
#FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
#USER app
#WORKDIR /app
##EXPOSE 8080
##EXPOSE 8081
#
#
## This stage is used to build the service project
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
#COPY ["src/irede.api/irede.api.csproj", "irede.api/"]
#RUN dotnet restore "./src/irede.api/irede.api.csproj"
#COPY . .
#WORKDIR "/src/irede.api"
#RUN dotnet build "./src/irede.api.csproj" -c $BUILD_CONFIGURATION -o /app/build
#
## This stage is used to publish the service project to be copied to the final stage
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "./src/irede.api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#
## This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "irede.api.dll"]

#----------------------------------

# Etapa base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia os arquivos .csproj dos projetos
COPY ["src/irede.api/irede.api.csproj", "irede.api/"]
COPY ["src/irede.application/irede.application.csproj", "irede.application/"]
COPY ["src/irede.core/irede.core.csproj", "irede.core/"]
COPY ["src/irede.infra/irede.infra.csproj", "irede.infra/"]
COPY ["src/irede.shared/irede.shared.csproj", "irede.shared/"]

# Restaura as dependências
RUN dotnet restore "irede.api/irede.api.csproj"

# Copia todo o código-fonte dos projetos
COPY src/ .

# Define o diretório de trabalho para o projeto principal
WORKDIR "/src/irede.api"

# Build da aplicação
RUN dotnet build "irede.api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Etapa de publicação
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "irede.api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Etapa final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "irede.api.dll"]


