FROM mcr.microsoft.com/dotnet/sdk:3.1 as build
WORKDIR /app 
COPY *.csproj . 
RUN dotnet restore 
COPY . . 
RUN dotnet publish  WebDocker.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/sdk:3.1
WORKDIR /app 
COPY --from=build /app/out .
ENV ASPNETCORE_URLS="http://*:4500"
ENV ASPNETCORE_ENVIRONMENT="Development"
ENTRYPOINT ["dotnet", "WebDocker.dll"]