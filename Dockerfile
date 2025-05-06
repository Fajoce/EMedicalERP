# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copiar la solución y todos los archivos de proyecto que referencia
COPY EMedicalERP.sln ./

COPY Application.API/Application.API.csproj ./Application.API/
COPY Infraestructure.API/Infraestructure.API.csproj ./Infraestructure.API/
COPY Domain.API/Domain.API.csproj ./Domain.API/
COPY EMedicalERP.API/EMedicalERP.API.csproj ./EMedicalERP.API/
COPY Persistencia.API/Persistencia.API.csproj ./Persistencia.API/
COPY UnitTests.API/UnitTests.API.csproj ./UnitTests.API/

# Restaurar dependencias
RUN dotnet restore EMedicalERP.sln

# Copiar el código fuente completo de cada proyecto
COPY Application.API/ ./Application.API/
COPY Infraestructure.API/ ./Infraestructure.API/
COPY Domain.API/ ./Domain.API/
COPY EMedicalERP.API/ ./EMedicalERP.API/
COPY Persistencia.API/ ./Persistencia.API/
COPY UnitTests.API/ ./UnitTests.API/

# Publicar la API principal
WORKDIR /app/EMedicalERP.API
RUN dotnet publish -c Release -o /app/publish

# Imagen final
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "EMedicalERP.API.dll"]