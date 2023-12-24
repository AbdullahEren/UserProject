FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY Solution.sln ./
COPY UserProject/*.csproj ./UserProject/
COPY Repositories/*.csproj ./Repositories/
COPY Entities/*.csproj ./Entities/
COPY Services/*.csproj ./Services/

RUN dotnet restore
COPY . .
WORKDIR /src/UserProject
RUN dotnet build -c Release -o /app

WORKDIR /src/Repositories
RUN dotnet build -c Release -o /app

WORKDIR /src/Entities
RUN dotnet build -c Release -o /app

WORKDIR /src/Services
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "UserProject.dll"]