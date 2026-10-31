# Руководство по ручной сборке под Linux (single-file) и запуску в Docker

Тебе нужно **вручную** опубликовать каждый проект в виде **одного самодостаточного исполняемого файла** для `linux-x64`, положить бинарники в папку `prebuilt/` и запустить `docker compose up --build`.

## 1) Подготовить структуру папок

В корне `WS01` создай папки:

```bash
mkdir -p prebuilt/ws01-01 prebuilt/ws01-02 prebuilt/sticky-gateway prebuilt/roundrobin-gateway
```

## 2) Опубликовать проекты под linux-x64 (single file)

Выполняй из корня `WS01/`.

### WS01_01
```bash
dotnet publish WS01_01/WS01_01.csproj \
  -c Release \
  -r linux-x64 \
  --self-contained true \
  -p:PublishSingleFile=true \
  -p:IncludeNativeLibrariesForSelfExtract=true \
  -p:PublishTrimmed=false \
  -o ./prebuilt/ws01-01
```

### WS01_02
```bash
dotnet publish WS01_02/WS01_02.csproj \
  -c Release \
  -r linux-x64 \
  --self-contained true \
  -p:PublishSingleFile=true \
  -p:IncludeNativeLibrariesForSelfExtract=true \
  -p:PublishTrimmed=false \
  -o ./prebuilt/ws01-02
```

### StickyGateway
```bash
dotnet publish StickyGateway/StickyGateway.csproj \
  -c Release \
  -r linux-x64 \
  --self-contained true \
  -p:PublishSingleFile=true \
  -p:IncludeNativeLibrariesForSelfExtract=true \
  -p:PublishTrimmed=false \
  -o ./prebuilt/sticky-gateway
```

### RoundRobinGateway
```bash
dotnet publish RoundRobinGateway/RoundRobinGateway.csproj \
  -c Release \
  -r linux-x64 \
  --self-contained true \
  -p:PublishSingleFile=true \
  -p:IncludeNativeLibrariesForSelfExtract=true \
  -p:PublishTrimmed=false \
  -o ./prebuilt/roundrobin-gateway
```

## 3) Скопировать index.html в wwwroot

В опубликованных папках должны быть `wwwroot/` с `index.html`. Его можно просто скопировать.

```bash
cp index.html prebuilt/ws01-01/wwwroot/
cp index.html prebuilt/ws01-02/wwwroot/
cp index.html prebuilt/sticky-gateway/wwwroot/
cp index.html prebuilt/roundrobin-gateway/wwwroot/
```

Если `wwwroot` ещё не созданы, создай:

```bash
mkdir -p prebuilt/ws01-01/wwwroot prebuilt/ws01-02/wwwroot prebuilt/sticky-gateway/wwwroot prebuilt/roundrobin-gateway/wwwroot
cp index.html prebuilt/ws01-01/wwwroot/
cp index.html prebuilt/ws01-02/wwwroot/
cp index.html prebuilt/sticky-gateway/wwwroot/
cp index.html prebuilt/roundrobin-gateway/wwwroot/
```

## 4) Сделать исполняемый файл исполняемым (опционально)

Иногда нужно дать права на запуск:

```bash
chmod +x prebuilt/ws01-01/WS01_01 prebuilt/ws01-02/WS01_02 prebuilt/sticky-gateway/StickyGateway prebuilt/roundrobin-gateway/RoundRobinGateway
```

## 5) Собрать и запустить в Docker

После того, как все 4 проекта опубликованы и `index.html` скопирован:

```bash
docker compose up --build
```

Порты:
- StickyGateway (WS01-01): http://localhost:5100
- RoundRobinGateway (WS01-02): http://localhost:5300
