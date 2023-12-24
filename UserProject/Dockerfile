#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
EXPOSE 80

COPY *.sln .
COPY UserProject/*.csproj ./UserProject/
COPY Entities/*.csproj ./Entities/
COPY Repositories/*.csproj ./Repositories/
COPY Services/*.csproj ./Services/

RUN dotnet restore UserProject/UserProject.csproj
RUN dotnet restore Entities/Entities.csproj
RUN dotnet restore Repositories/Repositories.csproj
RUN dotnet restore Services/Services.csproj

COPY UserProject/. ./UserProject/
COPY Entities/. ./Entities/
COPY Repositories/. ./Repositories/
COPY Services/. ./Services/

WORKDIR /app/UserProject
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app

COPY --from=build /app/UserProject/out ./
ENTRYPOINT ["dotnet", "UserProject.dll"]
