FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
RUN apt-get update && apt-get install -y nodejs
COPY ["DigiCV.API/*.csproj", "DigiCV.API/"]
COPY ["DigiCV.Infrastructure/*.csproj", "DigiCV.Infrastructure/"]
COPY ["DigiCV.Persistence/*.csproj", "DigiCV.Persistence/"]
COPY ["DigiCV.Application/*.csproj", "DigiCV.Application/"]
COPY ["DigiCV.Domain/*.csproj", "DigiCV.Domain/"]

RUN dotnet restore "DigiCV.API/DigiCV.API.csproj"
COPY . .
WORKDIR "/src/DigiCV.API"
RUN dotnet build "DigiCV.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "DigiCV.API.csproj" -c Release -o /app

FROM build AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DigiCV.API.dll"]