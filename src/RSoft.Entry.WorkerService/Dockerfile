#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RSoft.Entry.WorkerService/RSoft.Entry.WorkerService.csproj", "RSoft.Entry.WorkerService/"]
COPY ["RSoft.Entry.Cross/RSoft.Entry.Cross.csproj", "RSoft.Entry.Cross/"]
COPY ["RSoft.Entry.Infra/RSoft.Entry.Infra.csproj", "RSoft.Entry.Infra/"]
COPY ["RSoft.Entry.Core/RSoft.Entry.Core.csproj", "RSoft.Entry.Core/"]
COPY ["RSoft.Entry.Application/RSoft.Entry.Application.csproj", "RSoft.Entry.Application/"]
COPY ["RSoft.Entry.Contracts/RSoft.Entry.Contracts.csproj", "RSoft.Entry.Contracts/"]
COPY [".nuget/nuget.config", ""]
RUN dotnet restore "RSoft.Entry.WorkerService/RSoft.Entry.WorkerService.csproj"
COPY . .
WORKDIR "/src/RSoft.Entry.WorkerService"
RUN dotnet build "RSoft.Entry.WorkerService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RSoft.Entry.WorkerService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RSoft.Entry.WorkerService.dll"]