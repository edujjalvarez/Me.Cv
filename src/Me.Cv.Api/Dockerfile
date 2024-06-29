#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Me.Cv.Api/Me.Cv.Api.csproj", "src/Me.Cv.Api/"]
COPY ["src/Me.Cv.Application/Me.Cv.Application.csproj", "src/Me.Cv.Application/"]
COPY ["src/Me.Cv.Common/Me.Cv.Common.csproj", "src/Me.Cv.Common/"]
COPY ["src/Me.Cv.Domain/Me.Cv.Domain.csproj", "src/Me.Cv.Domain/"]
COPY ["src/Me.Cv.Infrastructure/Me.Cv.Infrastructure.csproj", "src/Me.Cv.Infrastructure/"]
RUN dotnet restore "./src/Me.Cv.Api/Me.Cv.Api.csproj"
COPY . .
WORKDIR "/src/src/Me.Cv.Api"
RUN dotnet build "./Me.Cv.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Me.Cv.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Me.Cv.Api.dll"]