FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NinthAgeCmsToArmyBook/NinthAgeCmsToArmyBook.csproj", "NinthAgeCmsToArmyBook/"]
RUN dotnet restore "NinthAgeCmsToArmyBook/NinthAgeCmsToArmyBook.csproj"
COPY . .
WORKDIR "/src/NinthAgeCmsToArmyBook"
RUN dotnet build "NinthAgeCmsToArmyBook.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NinthAgeCmsToArmyBook.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NinthAgeCmsToArmyBook.dll"]
