FROM mcr.microsoft.com/dotnet/sdk:3.1 as build
WORKDIR /app 
COPY . .
RUN dotnet restore 
RUN dotnet publish  WebDocker.csproj -c Release -o out
WORKDIR out 
ENV ASPNETCORE_URLS="http://*:4500"
ENTRYPOINT ["dotnet", "WebDocker.dll"]