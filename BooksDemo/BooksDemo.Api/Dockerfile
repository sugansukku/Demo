#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BooksDemo.Api/BooksDemo.Api.csproj", "BooksDemo.Api/"]
COPY ["BooksDemo.Repository/BooksDemo.Repository.csproj", "BooksDemo.Repository/"]
COPY ["BooksDemo.Models/BooksDemo.Models.csproj", "BooksDemo.Models/"]
COPY ["BooksDemo.Data/BooksDemo.Data.csproj", "BooksDemo.Data/"]
COPY ["BooksDemo.Service/BooksDemo.Service.csproj", "BooksDemo.Service/"]
COPY ["BooksDemo.Logging/BooksDemo.Logging.csproj", "BooksDemo.Logging/"]
RUN dotnet restore "BooksDemo.Api/BooksDemo.Api.csproj"
COPY . .
WORKDIR "/src/BooksDemo.Api"
RUN dotnet build "BooksDemo.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BooksDemo.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BooksDemo.Api.dll"]