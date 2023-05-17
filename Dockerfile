FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NinthAgeCmsToArmyBook.Api/NinthAgeCmsToArmyBook.Api.csproj", "NinthAgeCmsToArmyBook.Api/"]
RUN dotnet restore "NinthAgeCmsToArmyBook.Api/NinthAgeCmsToArmyBook.Api.csproj"
COPY . .
WORKDIR "/src/NinthAgeCmsToArmyBook.Api"
RUN dotnet build "NinthAgeCmsToArmyBook.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NinthAgeCmsToArmyBook.Api.csproj" -c Release -o /app/publish

FROM base AS final

COPY setup-tex-live.sh .
RUN chmod +x ./setup-tex-live.sh
RUN ./setup-tex-live.sh

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NinthAgeCmsToArmyBook.Api.dll"]
