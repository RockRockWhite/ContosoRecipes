FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ContosoRecipes/ContosoRecipes.csproj", "ContosoRecipes/"]
RUN dotnet restore "ContosoRecipes/ContosoRecipes.csproj"
COPY . .
WORKDIR "/src/ContosoRecipes"
RUN dotnet build "ContosoRecipes.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContosoRecipes.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContosoRecipes.dll"]
