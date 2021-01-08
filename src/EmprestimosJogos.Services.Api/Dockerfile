#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /src
COPY ["src/EmprestimosJogos.Services.Api/EmprestimosJogos.Services.Api.csproj", "src/EmprestimosJogos.Services.Api/"]
COPY ["src/EmprestimosJogos.Domain/EmprestimosJogos.Domain.csproj", "src/EmprestimosJogos.Domain/"]
COPY ["src/EmprestimosJogos.Infra.CrossCutting.Helpers/EmprestimosJogos.Infra.CrossCutting.Helpers.csproj", "src/EmprestimosJogos.Infra.CrossCutting.Helpers/"]
COPY ["src/EmprestimosJogos.Infra.CrossCutting.CompressionManager/EmprestimosJogos.Infra.CrossCutting.CompressionManager.csproj", "src/EmprestimosJogos.Infra.CrossCutting.CompressionManager/"]
COPY ["src/EmprestimosJogos.Domain.Core/EmprestimosJogos.Domain.Core.csproj", "src/EmprestimosJogos.Domain.Core/"]
COPY ["src/EmprestimosJogos.Application/EmprestimosJogos.Application.csproj", "src/EmprestimosJogos.Application/"]
COPY ["src/EmprestimosJogos.Infra.CrossCutting.Auth/EmprestimosJogos.Infra.CrossCutting.Auth.csproj", "src/EmprestimosJogos.Infra.CrossCutting.Auth/"]
COPY ["src/EmprestimosJogos.Infra.CrossCutting.ExceptionHandler/EmprestimosJogos.Infra.CrossCutting.ExceptionHandler.csproj", "src/EmprestimosJogos.Infra.CrossCutting.ExceptionHandler/"]
COPY ["src/EmprestimosJogos.Infra.Data/EmprestimosJogos.Infra.Data.csproj", "src/EmprestimosJogos.Infra.Data/"]
COPY ["src/EmprestimosJogos.Infra.CrossCutting.Identity/EmprestimosJogos.Infra.CrossCutting.Identity.csproj", "src/EmprestimosJogos.Infra.CrossCutting.Identity/"]
COPY ["src/EmprestimosJogos.Infra.CrossCutting.Swagger/EmprestimosJogos.Infra.CrossCutting.Swagger.csproj", "src/EmprestimosJogos.Infra.CrossCutting.Swagger/"]
COPY ["src/EmprestimosJogos.Infra.CrossCutting.IoC/EmprestimosJogos.Infra.CrossCutting.IoC.csproj", "src/EmprestimosJogos.Infra.CrossCutting.IoC/"]
COPY ["src/EmprestimosJogos.Infra.CrossCutting.BuscaCEP/EmprestimosJogos.Infra.CrossCutting.BuscaCEP.csproj", "src/EmprestimosJogos.Infra.CrossCutting.BuscaCEP/"]
COPY ["src/EmprestimosJogos.Infra.CrossCutting.WebRequest/EmprestimosJogos.Infra.CrossCutting.WebRequest.csproj", "src/EmprestimosJogos.Infra.CrossCutting.WebRequest/"]
COPY ["src/EmprestimosJogos.Infra.CrossCutting.PDFGenerator/EmprestimosJogos.Infra.CrossCutting.PDFGenerator.csproj", "src/EmprestimosJogos.Infra.CrossCutting.PDFGenerator/"]
COPY ["src/EmprestimosJogos.Infra.CrossCutting.SignalR/EmprestimosJogos.Infra.CrossCutting.SignalR.csproj", "src/EmprestimosJogos.Infra.CrossCutting.SignalR/"]
COPY ["src/EmprestimosJogos.Infra.CrossCutting.AzureStorage/EmprestimosJogos.Infra.CrossCutting.AzureStorage.csproj", "src/EmprestimosJogos.Infra.CrossCutting.AzureStorage/"]
COPY ["src/EmprestimosJogos.Infra.CrossCutting.SendGrid/EmprestimosJogos.Infra.CrossCutting.SendGrid.csproj", "src/EmprestimosJogos.Infra.CrossCutting.SendGrid/"]
RUN dotnet restore "src/EmprestimosJogos.Services.Api/EmprestimosJogos.Services.Api.csproj"
COPY . .
WORKDIR "/src/src/EmprestimosJogos.Services.Api"
RUN dotnet build "EmprestimosJogos.Services.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EmprestimosJogos.Services.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmprestimosJogos.Services.Api.dll"]
