#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Auth.WebAPI/Auth.WebAPI.csproj", "Auth.WebAPI/"]
COPY ["Auth.Application/Auth.Application.csproj", "Auth.Application/"]
COPY ["Auth.Domain/Auth.Domain.csproj", "Auth.Domain/"]
COPY ["Auth.Services/Auth.Services.csproj", "Auth.Services/"]
COPY ["Auth.Infra.Data/Auth.Infra.Data.csproj", "Auth.Infra.Data/"]
COPY ["Auth.Infra.Ioc/Auth.Infra.Ioc.csproj", "Auth.Infra.Ioc/"]
RUN dotnet restore "Auth.WebAPI/Auth.WebAPI.csproj"
COPY . .
WORKDIR "/src/Auth.WebAPI"
RUN dotnet build "Auth.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Auth.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Auth.WebAPI.dll"]