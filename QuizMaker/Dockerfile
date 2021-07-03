#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#copy base image for aspnet.core
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

#copy necessary build tools to build our project in the container
FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY *.sln .
COPY QuizMaker/*.csproj ./QuizMaker/
COPY QuizMaker.Abstractions/*.csproj ./QuizMaker.Abstractions/
COPY QuizMaker.DataAccess/*.csproj ./QuizMaker.DataAccess/
COPY QuizMaker.Services/*.csproj ./QuizMaker.Services/

RUN dotnet restore

#copy necessary proejct files to container
COPY QuizMaker/. ./QuizMaker/
COPY QuizMaker.Abstractions/. ./QuizMaker.Abstractions/
COPY QuizMaker.DataAccess/. ./QuizMaker.DataAccess/
COPY QuizMaker.Services/. ./QuizMaker.Services/


WORKDIR "/src/QuizMaker"

#publish project
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QuizMaker.dll"]