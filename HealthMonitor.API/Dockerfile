#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# ���������� ����������� ����� .NET 6 SDK ��� ������
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# �������� ����� ������� � ��������������� �����������
COPY ["HealthMonitor.API/HealthMonitor.API.csproj", "HealthMonitor.API/"]
RUN dotnet restore "./HealthMonitor.API/HealthMonitor.API.csproj"
COPY . .
WORKDIR "/src/HealthMonitor.API"
RUN dotnet build "./HealthMonitor.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# �������� ��������� ����� � �������� ����������
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HealthMonitor.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false


# ���������� ����������� ����� .NET 6 Runtime ��� �������
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# ��������� ������� ��� ������� ����������
ENTRYPOINT ["dotnet", "HealthMonitor.API.dll"]