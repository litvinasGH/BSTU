ARG DOTNET_VERSION=8.0


FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build

ARG PROJECT_NAME

WORKDIR /src

COPY . .

RUN dotnet restore "${PROJECT_NAME}/${PROJECT_NAME}.csproj" \
    --runtime linux-x64

RUN dotnet publish "${PROJECT_NAME}/${PROJECT_NAME}.csproj" \
    --configuration Release \
    --runtime linux-x64 \
    --self-contained true \
    --no-restore \
    -p:PublishSingleFile=true \
    -p:IncludeNativeLibrariesForSelfExtract=true \
    -p:PublishTrimmed=false \
    -p:UseAppHost=true \
    -o /app/publish


FROM mcr.microsoft.com/dotnet/runtime-deps:${DOTNET_VERSION} AS final

ARG PROJECT_NAME

WORKDIR /app

COPY --from=build /app/publish/ ./

ENV ASPNETCORE_URLS=http://+:8080
ENV APP_NAME=${PROJECT_NAME}

EXPOSE 8080

ENTRYPOINT ["sh", "-c", "exec ./$APP_NAME"]