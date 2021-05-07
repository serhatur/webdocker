FROM mcr.microsoft.com/dotnet/sdk:3.1
WORKDIR /app 
COPY . .
RUN dotnet restore 
RUN dotnet publish  WebDocker.csproj -c Release -o out
WORKDIR out 



COPY /bin/Release/netcoreapp3.1/publish .
ENTRYPOINT ["dotnet", "WebDocker.dll"]