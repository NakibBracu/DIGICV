FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
RUN apt-get update && apt-get install -y nodejs
COPY ["DigiCV.Worker/*.csproj", "DigiCV.Worker/"]
COPY ["DigiCV.Infrastructure/*.csproj", "DigiCV.Infrastructure/"]
COPY ["DigiCV.Persistence/*.csproj", "DigiCV.Persistence/"]
COPY ["DigiCV.Application/*.csproj", "DigiCV.Application/"]
COPY ["DigiCV.Domain/*.csproj", "DigiCV.Domain/"]

RUN dotnet restore "DigiCV.Worker/DigiCV.Worker.csproj"
COPY . .
WORKDIR "/src/DigiCV.Worker"
RUN dotnet build "DigiCV.Worker.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "DigiCV.Worker.csproj" -c Release -o /app

FROM build AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DigiCV.Worker.dll"]