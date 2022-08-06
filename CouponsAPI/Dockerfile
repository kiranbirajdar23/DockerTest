FROM mcr.microsoft.com/dotnet/aspnet AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk AS build
WORKDIR /app
COPY *.csproj ./
ADD CA_Bundle.crt ./

COPY ./CA_Bundle.crt /usr/local/share/ca-certificates/CA_Bundle.crt
RUN chmod 644 /usr/local/share/ca-certificates/CA_Bundle.crt && update-ca-certificates

RUN dotnet restore

COPY . .
RUN dotnet build -c Release

FROM build AS publish
RUN dotnet publish -c Release -o /publish

FROM base as final
WORKDIR /app
COPY --from=publish /publish .
ENTRYPOINT ["dotnet","CouponsAPI.dll"]