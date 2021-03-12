# РИБ 1С на RabbitMQ
 
<details>
<summary>Утилита генерации файлов настроек</summary>

Скачать утилиту для генерации файлов настроек можно в [разделе релизов](https://github.com/zhichkin/dajet-agent/releases/).

Поддерживается работа с базами данных 1С на Microsoft SQL Server и PostgreSQL.

Файлы настроек должны быть расположены в корневом каталоге установки агента обмена данными **DaJet.Agent.Service.exe**.

Утилита генерирует два файла:
- producer-settings.json
- consumer-settings.json

Оба эти файла нужны для настройки параметров работы агента обмена данными.

Файл **producer-settings.json** нужен для настройки агента в роли экспортёра
данных из базы данных 1С в очереди RabbitMQ.

Файл **consumer-settings.json** нужен для настройки агента в роли импортёра
данных из очередей RabbitMQ в базу данных 1С.

![Помощь по использованию](https://github.com/zhichkin/dajet-agent/blob/main/doc/dajet-agent-help.png)

Пример использования для Microsoft SQL Server:

![Пример использования для Microsoft SQL Server](https://github.com/zhichkin/dajet-agent/blob/main/doc/dajet-agent-ms-usage.png)

Пример использования для PostgreSQL:

![Пример использования для PostgreSQL](https://github.com/zhichkin/dajet-agent/blob/main/doc/dajet-agent-pg-usage.png)

**Примечание:** в случае необходимости указать порт для **PostgreSQL** адрес сервера можно указать, например, так: **127.0.0.1:5432**

</details>