FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

COPY FirstToDoList/*.csproj ./FirstToDoList/
# COPY Tracker.Order.Web.Tests/*.csproj ./Tracker.Order.Web.Tests/

WORKDIR /app/FirstToDoList
RUN dotnet restore

# WORKDIR /app/Tracker.Order.Web.Tests
# RUN dotnet restore

WORKDIR /app
# first "FirstToDoList" is source and the next is destination (EM)
COPY FirstToDoList FirstToDoList
# COPY Tracker.Order.Web.Tests Tracker.Order.Web.Tests

# WORKDIR /app/Tracker.Order.Web.Tests
# RUN dotnet test

WORKDIR /app/FirstToDoList
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/FirstToDoList/out ./
ENV ASPNETCORE_ENVIRONMENT Development
EXPOSE 80
ENTRYPOINT ["dotnet", "FirstToDoListBlazor.dll"]
