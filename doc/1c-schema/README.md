### Структура регистров сведений 1С

Версия DaJet Agent 6.3.3 поддерживает 4 пары регистров сведений,
которые могут использоваться в качестве входящих и исходящих очередей.

В будущем планируется перейти к одной (эталонной) паре регистров.

Сервис DaJet Agent умеет автоматически распознавать с какой версией
входящей или исходящей очереди (регистром сведений) он работает в данный момент.
Это распознавание выполняется по метаданным 1С и не требует остановки службы.
Другими словами версию регистра сведений можно менять в любое время - служба автоматически
адаптируется "на лету".

**Исходящие очереди**

|Версия 1|Версия 10|Версия 11|Версия 12|
|:---:|:---:|:---:|:---:|
|![](https://github.com/zhichkin/dajet-agent/blob/main/doc/images/OutgoingQueue1.png)|![](https://github.com/zhichkin/dajet-agent/blob/main/doc/images/OutgoingQueue10.png)|![](https://github.com/zhichkin/dajet-agent/blob/main/doc/images/OutgoingQueue11.png)|![](https://github.com/zhichkin/dajet-agent/blob/main/doc/images/OutgoingQueue12.png)|

**Входящие очереди**
<table>
  <tr>
    <th>Версия 1</th>
    <th>Версия 10</th>
    <th>Версия 11</th>
    <th>Версия 12</th>
  </tr>
  <tr>
    <td valign="top"><img src="https://github.com/zhichkin/dajet-agent/blob/main/doc/images/IncomingQueue1.png"/></td>
    <td valign="top"><img src="https://github.com/zhichkin/dajet-agent/blob/main/doc/images/IncomingQueue10.png"/></td>
    <td valign="top"><img src="https://github.com/zhichkin/dajet-agent/blob/main/doc/images/IncomingQueue11.png"/></td>
    <td valign="top"><img src="https://github.com/zhichkin/dajet-agent/blob/main/doc/images/IncomingQueue12.png"/></td>
  </tr>
</table>