# РИБ 1С на RabbitMQ

[Общее описание обмена данными на RabbitMQ](https://github.com/zhichkin/dajet-agent/blob/main/doc/%D0%9E%D0%BF%D0%B8%D1%81%D0%B0%D0%BD%D0%B8%D0%B5%20%D0%BE%D0%B1%D0%BC%D0%B5%D0%BD%D0%B0%20%D0%B4%D0%B0%D0%BD%D0%BD%D1%8B%D0%BC%D0%B8%20RabbitMQ.pdf)

![Общая схема обмена данными на RabbitMQ](https://github.com/zhichkin/dajet-agent/blob/main/doc/%D0%A1%D1%85%D0%B5%D0%BC%D0%B0%20%D0%BE%D0%B1%D0%BC%D0%B5%D0%BD%D0%B0%20%D0%B4%D0%B0%D0%BD%D0%BD%D1%8B%D0%BC%D0%B8%20RabbitMQ.png)

Для работы утилиты генерации файлов настроек и агентов обмена данными требуется установка [.NET Core 3.1](https://dotnet.microsoft.com/download).

<details>
<summary>Утилита для генерации файлов настроек и создания очередей RabbitMQ по плану обмена 1С</summary>

[Скачать утилиту](https://github.com/zhichkin/dajet-agent/releases/).

Поддерживается работа с базами данных 1С на Microsoft SQL Server и PostgreSQL.

<details>
<summary>Генерация файлов настроек</summary>

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
<summary>Создание очередей RabbitMQ по плану обмена 1С</summary>

Для автоматического создания очередей RabbitMQ используется параметр утилиты **--rmq**, в котором указывается имя плана обмена 1С,
для которого создаются очереди. Эта процедура выполняется для центральной базы данных РИБ, где хранятся сведения обо всех узлах обмена.

Для узлов плана обмена, которые помечены на удаление в 1С, операция не выполняется.

**Пример использования:**

![Пример автоматического создания очередей RabbitMQ](https://github.com/zhichkin/dajet-agent/blob/main/doc/dajet-agent-rmq-usage.png)

В данном примере план обмена называется "Тестовый". Имя плана обмена указывается так, как оно задано в конфигураторе 1С.

![Имя плана обмена](https://github.com/zhichkin/dajet-agent/blob/main/doc/exchange-plan-name.png)

Утилита попросит ввести данные для подключения к серверу RabbitMQ.

По окончанию своей работы покажет результат или сообщит об ошибке.

</details>

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

[Скачать агента обмена данными](https://github.com/zhichkin/dajet-agent/releases/).

Агент обмена данными логирует свою работу в файле **dajet-agent.log**,
который автоматически создаётся в корневом каталоге исполняемого файла агента в случае его отсутствия.

**Агент обмена данными устанавливается отдельно для каждого узла обмена данными (базы данных СУБД).**

1. Распаковать архив в каталог установки. Исполняемый файл называется **DaJet.Agent.Service.exe**.

<details>
<summary>2. Настроить файл appsettings.json.</summary>

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
</details>

<details>
<summary>3. Настроить файл producer-settings.json для роли экспортёра данных.</summary>

<details>
<summary>Описание файла producer-settings.json</summary>

**Внимание:** файл настроек producer-settings.json рекомендуется создавать автоматически
при помощи утилиты генерации файлов настроек (см. соответствующий раздел инструкции).
После этого отредактировать его вручную.

- **CriticalErrorDelay** - интервал ожидания доступности сервера СУБД или RabbitMQ в секундах.
- **MessageBrokerSettings** - секция для настройки подключения к серверу RabbitMQ.
  - **HostName** - сетевой адрес сервера.
  - **PortNumber** - порт сервера.
  - **UserName** - имя пользователя для подключения к серверу.
  - **Password** - пароль пользователя для подключения к серверу.
  - **ConfirmationTimeout** - таймаут ожидания подтверждения сервером RabbitMQ получения сообщения (publisher confirm), указывается в секундах.
- **DatabaseSettings** - секция для настройки подключения к серверу СУБД.
  - **DatabaseProvider** - тип севера СУБД (0 - Microsoft SQL Server, 1 - PostgreSQL).
  - **ConnectionString** - строка подключения к базе данных СУБД.
  - **MessagesPerTransaction** - количество исходящих сообщений, обрабатываемых за одну транзакцию СУБД.
  - **DatabaseQueryingPeriodicity** - интервал ожидания новых сообщений в узле обмена в секундах.
  - **UseNotifications** - включение/выключение ONLINE режима (событийного обмена) для Microsoft SQL Server.
  - **NotificationQueueName** - имя очереди SQL Server Service Broker для оповещения агента обмена о появлении новых сообщений в справочнике "ИсходящаяОчередьRabbitMQ".
  - **WaitForNotificationTimeout** - таймаут ожидания уведомления о новых сообщениях в узле обмена в секундах (реализовано только для SQL Server).
Используется для реализации обмена данными в режиме online.
  - **DatabaseQueue** - секция для описания таблицы справочника "ИсходящаяОчередьRabbitMQ".
    - **TableName** - имя таблицы СУБД.
    - **ObjectName** - имя объекта метаданных 1С.
    - **Fields** - секция для описания полей таблицы СУБД справочника "ИсходящаяОчередьRabbitMQ".
      - **Name** - имя поля таблицы СУБД.
      - **Property** - имя реквизита объекта метаданных 1С.

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
    "UseNotifications": true,
    "NotificationQueueName": "dajet-agent-export-queue",
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
</details>

<details>
<summary>4. Настроить файл consumer-settings.json для роли импортёра данных.</summary>

<details>
<summary>Описание файла consumer-settings.json</summary>

**Внимание:** файл настроек consumer-settings.json рекомендуется создавать автоматически
при помощи утилиты генерации файлов настроек (см. соответствующий раздел инструкции).
После этого отредактировать его вручную.

- **ThisNode** - код этого узла обмена (получателя), который обслуживает данный агент обмена данными.
- **SenderNodes** - коды узлов-отправителей, разделённых запятой, от которых данный узел принимает сообщения.
- **CriticalErrorDelay** - интервал ожидания доступности сервера СУБД или RabbitMQ в секундах.
- **MessageBrokerSettings** - секция для настройки подключения к серверу RabbitMQ.
  - **HostName** - сетевой адрес сервера.
  - **PortNumber** - порт сервера.
  - **UserName** - имя пользователя для подключения к серверу.
  - **Password** - пароль пользователя для подключения к серверу.
  - **ConsumerPrefetchCount** - количество сообщений, которое буферизуется на клиенте для получения.
Данный параметр влияет на скорость загрузки сообщений, но также может влиять увеличение расходования ресурсов, например, оперативной памяти клиентом.
Настраивается в зависимости от условий эксплуатации. Подробнее можно посмотреть в [документации RabbitMQ](https://www.rabbitmq.com/consumer-prefetch.html).
- **DatabaseSettings** - секция для настройки подключения к серверу СУБД.
  - **DatabaseProvider** - тип севера СУБД (0 - Microsoft SQL Server, 1 - PostgreSQL).
  - **ConnectionString** - строка подключения к базе данных СУБД.
  - **DatabaseQueue** - секция для описания таблицы справочника "ВходящаяОчередьRabbitMQ".
    - **TableName** - имя таблицы СУБД.
    - **ObjectName** - имя объекта метаданных 1С.
    - **Fields** - секция для описания полей таблицы СУБД справочника "ВходящаяОчередьRabbitMQ".
      - **Name** - имя поля таблицы СУБД.
      - **Property** - имя реквизита объекта метаданных 1С.

</details>
<details>
<summary>Пример файла consumer-settings.json</summary>

```JSON
{
  "ThisNode": "MAIN",
  "SenderNodes": [ "N001", "N002", "N003" ],
  "CriticalErrorDelay": 300,
  "MessageBrokerSettings": {
    "HostName": "localhost",
    "PortNumber": 5672,
    "UserName": "guest",
    "Password": "guest",
    "ConsumerPrefetchCount": 1
  },
  "DatabaseSettings": {
    "DatabaseProvider": 0,
    "ConnectionString": "Data Source=SERVER_ADDRESS;Initial Catalog=DATABASE_NAME;Integrated Security=True",
    "DatabaseQueue": {
      "TableName": "_Reference164",
      "ObjectName": "Справочник.ВходящаяОчередьRabbitMQ",
      "Fields": [
        {
          "Name": "_Fld165",
          "Property": "ДатаВремя"
        },
        {
          "Name": "_Fld166",
          "Property": "Отправитель"
        },
        {
          "Name": "_Fld167",
          "Property": "ТипОперации"
        },
        {
          "Name": "_Fld168",
          "Property": "ТипСообщения"
        },
        {
          "Name": "_Fld169",
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
</details>

<details>
<summary>5. Установка и запуск агента как службы Windows.</summary>

- Выполнить от имени администратора системы команду:
```SQL
sc create "DaJet Agent Service" binPath="D:\dajet-agent\DaJet.Agent.Service.exe"
```
В данном случае установочный архив распакован в каталог D:\dajet-agent.

- Перейти в системную консоль управления сервисами Windows.
- При необходимости заменить пользователя Windows, от имени которого будет работать сервис.
- При необходимости установить тип запуска Automatic, чтобы сервис стартовал при запуске/перезагрузке системы.
- Запустить сервис "DaJet Agent Service".

Агент может работать в среде Linux, в том числе как демон Linux.

</details>

<details>
<summary>6. Создание очередей RabbitMQ.</summary>

Нужные очереди RabbitMQ необходимо создать до начала обмена данными.

Для каждой пары узлов, участвующих в обмене данными, настраивается две очереди на сервере RabbitMQ.
Две очереди необходимы только в том случае, если обмен между этими двумя узлами двусторонний.
В противном случае достаточно только одной очереди (обмен односторонний).

Шаблон именования очередей имеет следующий вид:
```JSON
РИБ.<код узла отправителя>.<код узла получателя>
```
Например, очереди для обмена между тремя узлами РИБ могут выглядеть так:
![Пример настройки очередей RabbitMQ](https://github.com/zhichkin/dajet-agent/blob/main/doc/RMQ-monitor.png)

Удаление очередей можно выполнять в любое время — это не влияет на работу адаптера RabbitMQ.
При этом следует учитывать, что очередь сообщений на стороне узла-отправителя для соответствующего узла-получателя будет 
увеличиваться. То есть следует такой узел исключить из обмена, например, пометить на удаление в 1С.

</details>

<details>
<summary>7. Проверка работы обмена данными</summary>

1. В каждом узле обмена данными должен быть одноимённый план обмена.
2. В составе каждого из этих планов обмена должен присутствовать идентичный объект метаданных, например, справочник "Номенклатура".
3. В конфигурации узла, который выполняет экспорт (выгрузку) данных,
должен работать тот или иной механизм регистрации объектов, например БСП 1С,
который обеспечивает заполнение набора узлов свойства "Получатели" параметров обмена объекта 1С.
Заполнение получателей должно выполняться до срабатывания подписок на события "ПриЗаписи" подсистемы "ОбменДаннымиRabbitMQ",
например, в подписках на события "ПередЗаписью".
В случае разрешения автоматической регистрации изменений для объекта метаданных 1С в плане обмена этот набор узлов заполняется автоматически.
4. Убедиться, что подсистема 1С "ОбменДаннымиRabbitMQ" включена.
5. Убедиться, что все нужные очереди на сервере RabbitMQ созданы.
6. Убедиться, что сервисы агентов обмена данными работают.
7. Изменить объект 1С (создать новый, изменить или удалить существующий) в узле-отправителе данных.
8. Убедиться, что объект попал в справочник "ИсходящаяОчередьRabbitMQ" узла-отправителя.
9. Убедиться, что объект попал в соответствующую очередь RabbitMQ.
10. Убедиться, что сообщение из RabbitMQ удалено агентом-получателем.
11. Убедиться, что сообщение появилось в справочнике "ВходящаяОчередьRabbitMQ" узла-получателя.
12. Убедиться, что регламентное задание "ОбменДаннымиRabbitMQ" узла-получателя обработало
входящее сообщение - соответствующий объект конфигурации был создан, обновлён или удалён.

**Примечание:** пункты 7-12 могут отработать очень быстро.
При удачном стечении обстоятельств после выполнения пункта 7 можно сразу переходить к пункту 11 или 12.

Если что-то пошло не так, то, в первую очередь, следует посмотреть логи агентов - файл **dajet-agent.log**,
который расположен в корневом каталоге установки агента.

</details>

</details>

<details>
<summary>Настройка режима ONLINE (событийного обмена данными) для Microsoft SQL Server</summary>

Работа данной опции основана на использовании функциональности **SQL Server Service Broker**.

Для настройки потребуется выполнить следующие действия:
- Выполнить SQL-скрипт **setup-service-broker.sql** из каталога **sql\ms** в базе данных узла обмена 1С.
- Выполнить SQL-скрипт **export-queue-trigger.sql** из каталога **sql\ms** в базе данных узла обмена 1С.

В этих скриптах необходимо заполнить/заменить следующее:
- указать актуальное название базы данных узла обмена 1С;
- имя таблицы справочника 1С "ИсходящаяОчередьRabbitMQ" для создания триггера
(её название есть в файле настроек producer-settings.json);
- имя очереди Service Broker для оповещения агента о новых исходящих сообщениях.

Данный режим работы настраивается в файле **producer-settings.json**. См. следующие настройки:
- **UseNotifications** - включение/выключение ONLINE режима
- **NotificationQueueName** - имя очереди Service Broker для оповещений
- **WaitForNotificationTimeout** - время ожидания появления новых сообщений в секундах

</details>