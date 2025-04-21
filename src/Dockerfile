FROM mcr.microsoft.com/dotnet/aspnet:9.0  AS base
RUN apt-get update && apt-get install -y libgdiplus
WORKDIR /app
EXPOSE 80
COPY CmsApp/bin/Release/publish .
COPY CmsApp.db .
ENTRYPOINT ["dotnet", "CmsApp.dll"]
