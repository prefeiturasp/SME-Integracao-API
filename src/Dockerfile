FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
RUN ls -la
RUN pwd
RUN echo "estou no passo 1"
COPY SME.Pedagogico.WebAPI/*.csproj ./SME.Pedagogico.WebAPI/
COPY SME.Pedagogico.Interface/*.csproj ./SME.Pedagogico.Interface/
COPY SME.Pedagogico.IoC/*.csproj ./SME.Pedagogico.IoC/
COPY SME.Pedagogico.Service/*.csproj ./SME.Pedagogico.Service/
COPY SME.Pedagogico.Repository/*.csproj ./SME.Pedagogico.Repository/
RUN dotnet restore SME.Pedagogico.WebAPI/SME.Pedagogico.WebAPI.csproj
COPY . .
RUN ls -la
WORKDIR ./SME.Pedagogico.WebAPI
RUN ls -la
RUN dotnet build SME.Pedagogico.WebAPI.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish SME.Pedagogico.WebAPI.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SME.Pedagogico.WebAPI.dll"]