# РИБ 1С на RabbitMQ

[Общее описание обмена данными на RabbitMQ](https://github.com/zhichkin/dajet-agent/blob/main/doc/%D0%9E%D0%BF%D0%B8%D1%81%D0%B0%D0%BD%D0%B8%D0%B5%20%D0%BE%D0%B1%D0%BC%D0%B5%D0%BD%D0%B0%20%D0%B4%D0%B0%D0%BD%D0%BD%D1%8B%D0%BC%D0%B8%20RabbitMQ.pdf)

![Общая схема обмена данными на RabbitMQ](https://github.com/zhichkin/dajet-agent/blob/main/doc/%D0%A1%D1%85%D0%B5%D0%BC%D0%B0%20%D0%BE%D0%B1%D0%BC%D0%B5%D0%BD%D0%B0%20%D0%B4%D0%B0%D0%BD%D0%BD%D1%8B%D0%BC%D0%B8%20RabbitMQ.png)
 
<details>
<summary>Утилита для генерации файлов настроек</summary>

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

<details>
<summary>Установка и настройка подсистемы 1С "ОбменДаннымиRabbitMQ"</summary>

[Скачать подсистему "ОбменДаннымиRabbitMQ"](https://github.com/zhichkin/dajet-agent/blob/main/1c/%D0%9E%D0%B1%D0%BC%D0%B5%D0%BD%D0%94%D0%B0%D0%BD%D0%BD%D1%8B%D0%BC%D0%B8RabbitMQ.cf).

1. Обновить целевую конфигурацию 1С при помощи сравнения и объединения. Появится новая подсистема 1С "ОбменДаннымиRabbitMQ".
2. Сохранить конфигурацию 1С и перейти в режим пользователя для настройки подсистемы.
3. Заполнить константу **ИмяПланаОбменаRabbitMQ**, указав имя плана обмена так как это указано в конфигурации 1С, который будет использован для обмена данными RabbitMQ.
![Имя плана обмена](https://github.com/zhichkin/dajet-agent/blob/main/doc/exchange-plan-name.png)
4. Включить константу **ИспользоватьОбменДаннымиRabbitMQ** для активации использования подсистемы "ОбменДаннымиRabbitMQ".
5. Настроить и запланировать запуск регламентного задания "ОбменДаннымиRabbitMQ", которое выполняет загрузку объектов 1С из очереди входящих сообщений, справочник "ВходящаяОчередьRabbitMQ".
6. Включить константу **ИспользоватьРабочийРежимRabbitMQ** для активации рабочего режима использования подсистемы "ОбменДаннымиRabbitMQ".

Подсистема "ОбменДаннымиRabbitMQ" может работать в двух режимах "рабочий" и "тестовый".

В тестовом режиме (используется по умолчанию) регистрация изменений осуществляется одновременно и в плане обмена,
указанном в настройке "ИмяПланаОбменаRabbitMQ", и в очереди исходящих сообщений, справочник ""ИсходящаяОчередьRabbitMQ".

В рабочем режиме регистрация выполняется только в очереди исходящих сообщений, справочник ""ИсходящаяОчередьRabbitMQ".
Регистрация в плане обмена не выполняется, что обеспечивается выполнением следующего кода 1С:
```1C
Процедура ОчиститьСписокПолучателей(Источник, МассивПолучателей)
	
	Если Не ИспользоватьРабочийРежимRabbitMQ() Тогда
		Возврат;
	КонецЕсли;
	
	Для Каждого Получатель Из МассивПолучателей Цикл
		Источник.ОбменДанными.Получатели.Удалить(Получатель);
	КонецЦикла;
	
КонецПроцедуры
```

Сериализация и десериализация объектов 1С выполняется при помощи встроенного объекта платформы "СериализаторXDTO" в формате JSON.

Пример JSON 1С:
```JSON
{
  "#type" : "jcfg:CatalogObject.Номенклатура",
  "#value" :
  {
    "IsFolder":false,
    "Ref":"8f2ad5ce-8347-11eb-9c98-408d5c93cc8e",
    "DeletionMark":false,
    "Parent":"00000000-0000-0000-0000-000000000000",
    "Code":"00000001",
    "Description":"Тестовая номенклатура"
  }
}
```

</details>

<details>
<summary>Установка и настройка агента обмена данными</summary>

Скачать агента обмена данными можно в [разделе релизов](https://github.com/zhichkin/dajet-agent/releases/).

1. Распаковать архив в каталог установки. Исполняемый файл называется **DaJet.Agent.Service.exe**.
2. Настроить файл **appsettings.json**.
<details>
<summary>Описание файла appsettings.json</summary>

- **LogSize** - размер файла лога в байтах, по умолчанию равен 64 Kb, по достижению этого размера пересоздаётся.
- **UseProducer** - использовать агента в качестве экспортёра данных из базы данных в очереди RabbitMQ.
- **UseConsumer** - использовать агента в качестве импортёра данных из очередей RabbitMQ в базу данных.
- **ShutdownTimeout** - стандартная настройка хоста .NET Core. Таймаут принудительного завершения работы сервисов хоста.
Нужен для того чтобы, например, можно было увеличить отсрочку остановки сервиса хоста для освобождения занятых им ресурсов и т.п.

</details>
<details>
<summary>Пример файла appsettings.json</summary>

```JSON
{
  "LogSize": 65536,
  "UseProducer": true,
  "UseConsumer": true,
  "HostOptions": {
    "ShutdownTimeout": "00:00:30"
  }
}
```

</details>

3. Настроить файл **producer-settings.json** для роли экспортёра данных.
<details>
<summary>Описание файла producer-settings.json</summary>

- **CriticalErrorDelay** - интервал ожидания доступности сервера СУБД или RabbitMQ в секундах.
- **MessageBrokerSettings** - секция для настройки подключения к серверу RabbitMQ.

-- **HostName** - сетевой адрес сервера.
-- **PortNumber** - порт сервера.
-- **UserName** - имя пользователя для подключения к серверу.
-- **Password** - пароль пользователя для подключения к серверу.
-- **ConfirmationTimeout** - таймаут ожидания подтверждения сервером RabbitMQ получения сообщения (publisher confirm), указывается в секундах.

- **DatabaseSettings** - секция для настройки подключения к серверу СУБД.

-- **DatabaseProvider** - тип севера СУБД (0 - Microsoft SQL Server, 1 - PostgreSQL).
-- **ConnectionString** - строка подключения к базе данных СУБД.
-- **MessagesPerTransaction** - количество исходящих сообщений, обрабатываемых за одну транзакцию СУБД.
-- **DatabaseQueryingPeriodicity** - интервал ожидания новых сообщений в узле обмена в секундах.
-- **WaitForNotificationTimeout** - таймаут ожидания уведомления о новых сообщениях в узле обмена в секундах (реализовано только для SQL Server).
Используется для реализации обмена данными в режиме online.
-- **DatabaseQueue** - секция для описания таблицы справочника "ИсходящаяОчередьRabbitMQ".

--- **TableName** - имя таблицы СУБД.
--- **ObjectName** - имя объекта метаданных 1С.
--- **Fields** - секция для описания полей таблицы СУБД справочника "ИсходящаяОчередьRabbitMQ".

---- **Name** - имя поля таблицы СУБД.
---- **Property** - имя реквизита объекта метаданных 1С.

</details>
<details>
<summary>Пример файла producer-settings.json</summary>

```JSON
{
  "CriticalErrorDelay": 300,
  "MessageBrokerSettings": {
    "HostName": "localhost",
    "PortNumber": 5672,
    "UserName": "guest",
    "Password": "guest",
    "ConfirmationTimeout": 1
  },
  "DatabaseSettings": {
    "DatabaseProvider": 0,
    "ConnectionString": "Data Source=SERVER_ADDRESS;Initial Catalog=DATABASE_NAME;Integrated Security=True",
    "MessagesPerTransaction": 1000,
    "DatabaseQueryingPeriodicity": 60,
    "WaitForNotificationTimeout": 180,
    "DatabaseQueue": {
      "TableName": "_Reference157",
      "ObjectName": "Справочник.ИсходящаяОчередьRabbitMQ",
      "Fields": [
        {
          "Name": "_Fld158",
          "Property": "ДатаВремя"
        },
        {
          "Name": "_Fld159",
          "Property": "Отправитель"
        },
        {
          "Name": "_Fld160",
          "Property": "Получатели"
        },
        {
          "Name": "_Fld161",
          "Property": "ТипОперации"
        },
        {
          "Name": "_Fld162",
          "Property": "ТипСообщения"
        },
        {
          "Name": "_Fld163",
          "Property": "ТелоСообщения"
        },
        {
          "Name": "_Code",
          "Property": "Код"
        },
        {
          "Name": "_IDRRef",
          "Property": "Ссылка"
        },
        {
          "Name": "_Marked",
          "Property": "ПометкаУдаления"
        },
        {
          "Name": "_PredefinedID",
          "Property": "Предопределённый"
        },
        {
          "Name": "_Version",
          "Property": "ВерсияДанных"
        }
      ]
    }
  }
}
```

</details>

4. Настроить файл **consumer-settings.json** для роли импортёра данных.

</details>