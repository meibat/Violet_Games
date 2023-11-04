FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

#Copy the project
COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

#Build runtime image
FROM mcr.microsoft.com/dotnet/sdk:5.0
WORKDIR /app
EXPOSE 80
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "VioletGames.dll" ]