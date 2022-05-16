### Установка и настройка DaJet Agent

Агент обмена данными логирует свою работу в файле **dajet-agent.log**,
который автоматически создаётся в корневом каталоге исполняемого файла агента **DaJet.Agent.Service.exe** в случае его отсутствия.

Агент обмена данными устанавливается отдельно для каждого узла обмена данными (базы данных СУБД).

- [Скачать дистрибутив](https://github.com/zhichkin/dajet-agent/releases/), например, **dajet-agent-svc-x64-6.3.3**.
- Распаковать архив в каталог установки. Исполняемый файл называется **DaJet.Agent.Service.exe**.
- Настроить файл **appsettings.json** (см. ниже).
- Настроить файл [**producer-settings.json**](https://github.com/zhichkin/dajet-agent/blob/main/src/dajet-agent/producer-settings.json).
- Настроить файл [**consumer-settings.json**](https://github.com/zhichkin/dajet-agent/blob/main/src/dajet-agent/consumer-settings.json).

Описание файла **appsettings.json**.

- **LogSize** - размер файла лога в байтах, по умолчанию равен 1 Mb, по достижению этого размера пересоздаётся.
- **UseProducer** - использовать агента в качестве экспортёра данных из базы данных в очереди RabbitMQ.
- **UseConsumer** - использовать агента в качестве импортёра данных из очередей RabbitMQ в базу данных.
- **ShutdownTimeout** - стандартная настройка хоста .NET Core. Таймаут принудительного завершения работы сервисов хоста.
- **ExchangePlans** - массив планов обмена 1С для настройки и автоматического создания очередей RabbitMQ.
Нужен для того чтобы, например, можно было увеличить отсрочку остановки сервиса хоста для освобождения занятых им ресурсов и т.п.

Пример файла **appsettings.json**:
```json
{
  "LogSize": 1048576,
  "UseProducer": true, // producer-settings.json
  "UseConsumer": true, // consumer-settings.json
  "HostOptions": {
    "ShutdownTimeout": "00:00:30"
  },
  "ExchangePlans": [ "ПланОбмена.МойПланОбменаДанными" ]
}
```

**Установка и запуск агента как службы Windows**

- Выполнить от имени администратора системы команду:
```SQL
sc create "DaJet Agent Service" binPath="D:\dajet-agent\DaJet.Agent.Service.exe"
```
В данном случае установочный архив распакован в каталог D:\dajet-agent.

- Перейти в системную консоль управления сервисами Windows.
- При необходимости заменить пользователя Windows, от имени которого будет работать сервис.
- При необходимости установить тип запуска Automatic, чтобы сервис стартовал при запуске/перезагрузке системы.
- Запустить сервис "DaJet Agent Service".
- Проверить запуск и работу службы в логе службы **dajet-agent.log**.

## Важно!

Публикация сообщений осуществляется в двух режимах: агрегатор и диспетчер (дистрибьютор).

[Подробнее см. в описании версии 4.1.0](https://github.com/zhichkin/dajet-agent/releases/tag/svc-4.1)