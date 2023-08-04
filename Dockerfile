FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

COPY FirstToDoList/*.csproj ./FirstToDoList/

WORKDIR /app/FirstToDoList
RUN dotnet restore

WORKDIR /app
COPY FirstToDoList FirstToDoList

WORKDIR /app/FirstToDoList
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/FirstToDoList/out ./
ENV ASPNETCORE_ENVIRONMENT Development
EXPOSE 80
ENTRYPOINT ["dotnet", "FirstToDoListBlazor.dll"]
