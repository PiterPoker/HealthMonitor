# HealthMonitor REST API

Этот проект представляет собой REST API для управления сущностями пациентов. API поддерживает операции CRUD (создание, чтение, обновление, удаление) и использует базу данных `healthdb`. Проект упакован в Docker Compose для удобного развертывания.

## Установка

1. Клонируйте репозиторий:
    ```bash
    git clone https://github.com/PiterPoker/healthmonitor.git
    cd healthmonitor
    ```

2. Запустите Docker Compose:
    ```bash
    docker-compose up --build
    ```

## Использование

После запуска Docker Compose, API будет доступен по адресу `http://localhost:5200`.