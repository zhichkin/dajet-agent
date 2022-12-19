using ProtoBuf;
using DaJet.ProtoBuf;
using System;
using System.Collections.Generic;

namespace erp_model
{
namespace Документ
{

[ProtoContract] public sealed class АктОбУничтожении
{
	[ProtoContract] public sealed class ТоварыЗапись
	{
		[ProtoMember(1)] public string ИдентификаторМДЛП { get; set; }
		[ProtoMember(2)] public bool МДЛП { get; set; }
		[ProtoMember(3)] public double Остаток { get; set; }
		[ProtoMember(4)] public Entity Партия { get; set; }
		[ProtoMember(5)] public Entity Товар { get; set; }
	}
	[ProtoMember(1)] public Entity АдресХранения { get; set; }
	[ProtoMember(2)] public DateTime ДатаОснования { get; set; }
	[ProtoMember(3)] public Entity ДокументОснование { get; set; }
	[ProtoMember(4)] public string Комментарий { get; set; }
	[ProtoMember(5)] public Entity КонтрагентПоУничтожениюЛП { get; set; }
	[ProtoMember(6)] public string НомерОснования { get; set; }
	[ProtoMember(7)] public Entity Отдел { get; set; }
	[ProtoMember(8)] public Entity ПричинаСписания { get; set; }
	[ProtoMember(9)] public Entity СпособУничтожения { get; set; }
	[ProtoMember(10)] public DateTime Дата { get; set; }
	[ProtoMember(11)] public string Номер { get; set; }
	[ProtoMember(12)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(13)] public bool Проведен { get; set; }
	[ProtoMember(14)] public List<АктОбУничтожении.ТоварыЗапись> Товары { get; set; } = new List<АктОбУничтожении.ТоварыЗапись>();
}

[ProtoContract] public sealed class АннулированиеЗаказовПоставщику
{
	[ProtoContract] public sealed class ТоварыЗапись
	{
		[ProtoMember(1)] public DateTime ДатаДоставки { get; set; }
		[ProtoMember(2)] public string КлючСвязи { get; set; }
		[ProtoMember(3)] public double Количество { get; set; }
		[ProtoMember(4)] public Entity Поставщик { get; set; }
		[ProtoMember(5)] public DateTime СрокГодности { get; set; }
		[ProtoMember(6)] public Entity Товар { get; set; }
		[ProtoMember(7)] public double ЦенаЗакупки { get; set; }
	}
	[ProtoMember(1)] public Entity Склад { get; set; }
	[ProtoMember(2)] public DateTime Дата { get; set; }
	[ProtoMember(3)] public string Номер { get; set; }
	[ProtoMember(4)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(5)] public bool Проведен { get; set; }
	[ProtoMember(6)] public List<АннулированиеЗаказовПоставщику.ТоварыЗапись> Товары { get; set; } = new List<АннулированиеЗаказовПоставщику.ТоварыЗапись>();
}

[ProtoContract] public sealed class АссортиментныйПлан
{
	[ProtoContract] public sealed class МатрицыПоАдресамХраненияЗапись
	{
		[ProtoMember(1)] public Entity АдресХранения { get; set; }
		[ProtoMember(2)] public DateTime ДатаНачала { get; set; }
		[ProtoMember(3)] public DateTime ДатаОкончания { get; set; }
		[ProtoMember(4)] public Entity ТоварнаяМатрица { get; set; }
	}
	[ProtoMember(1)] public Entity Ответственный { get; set; }
	[ProtoMember(2)] public DateTime Дата { get; set; }
	[ProtoMember(3)] public string Номер { get; set; }
	[ProtoMember(4)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(5)] public bool Проведен { get; set; }
	[ProtoMember(6)] public List<АссортиментныйПлан.МатрицыПоАдресамХраненияЗапись> МатрицыПоАдресамХранения { get; set; } = new List<АссортиментныйПлан.МатрицыПоАдресамХраненияЗапись>();
}

[ProtoContract] public sealed class АссортиментныйФакт
{
	[ProtoMember(1)] public Entity АдресХранения { get; set; }
	[ProtoMember(2)] public Union ДокументОснование { get; set; }
	[ProtoMember(3)] public Entity Ответственный { get; set; }
	[ProtoMember(4)] public DateTime Дата { get; set; }
	[ProtoMember(5)] public string Номер { get; set; }
	[ProtoMember(6)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(7)] public bool Проведен { get; set; }
}

[ProtoContract] public sealed class ВводНачальныхОстатков
{
	[ProtoContract] public sealed class ОстаткиЗапись
	{
		[ProtoMember(1)] public double Остаток { get; set; }
		[ProtoMember(2)] public Entity Отдел { get; set; }
		[ProtoMember(3)] public Entity Партия { get; set; }
		[ProtoMember(4)] public Entity Товар { get; set; }
	}
	[ProtoContract] public sealed class ЗаказыЗапись
	{
		[ProtoMember(1)] public Entity Заказ { get; set; }
		[ProtoMember(2)] public double Количество { get; set; }
		[ProtoMember(3)] public Entity МестоЗаказа { get; set; }
		[ProtoMember(4)] public Entity Склад { get; set; }
	}
	[ProtoContract] public sealed class ЗаказыДляВозвратовЗапись
	{
		[ProtoMember(1)] public string authcode { get; set; }
		[ProtoMember(2)] public string RRN { get; set; }
		[ProtoMember(3)] public DateTime ДатаПродажи { get; set; }
		[ProtoMember(4)] public Entity Заказ { get; set; }
		[ProtoMember(5)] public Entity ККМ { get; set; }
		[ProtoMember(6)] public string КПК { get; set; }
		[ProtoMember(7)] public string НомерКарты { get; set; }
		[ProtoMember(8)] public string НомерСмены { get; set; }
		[ProtoMember(9)] public double НомерЧека { get; set; }
	}
	[ProtoMember(1)] public Entity АдресХранения { get; set; }
	[ProtoMember(2)] public DateTime Дата { get; set; }
	[ProtoMember(3)] public string Номер { get; set; }
	[ProtoMember(4)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(5)] public bool Проведен { get; set; }
	[ProtoMember(6)] public List<ВводНачальныхОстатков.ОстаткиЗапись> Остатки { get; set; } = new List<ВводНачальныхОстатков.ОстаткиЗапись>();
	[ProtoMember(7)] public List<ВводНачальныхОстатков.ЗаказыЗапись> Заказы { get; set; } = new List<ВводНачальныхОстатков.ЗаказыЗапись>();
	[ProtoMember(8)] public List<ВводНачальныхОстатков.ЗаказыДляВозвратовЗапись> ЗаказыДляВозвратов { get; set; } = new List<ВводНачальныхОстатков.ЗаказыДляВозвратовЗапись>();
}

[ProtoContract] public sealed class ВводОстатковДенежныхСредств
{
	[ProtoMember(1)] public Entity Касса { get; set; }
	[ProtoMember(2)] public string Комментарий { get; set; }
	[ProtoMember(3)] public Entity Ответственный { get; set; }
	[ProtoMember(4)] public double Сумма { get; set; }
	[ProtoMember(5)] public DateTime Дата { get; set; }
	[ProtoMember(6)] public string Номер { get; set; }
	[ProtoMember(7)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(8)] public bool Проведен { get; set; }
}

[ProtoContract] public sealed class ВнесениеДенежныхСредствВКассуККМ
{
	[ProtoMember(1)] public Entity КассаККМ { get; set; }
	[ProtoMember(2)] public string Комментарий { get; set; }
	[ProtoMember(3)] public Entity Организация { get; set; }
	[ProtoMember(4)] public Entity Ответственный { get; set; }
	[ProtoMember(5)] public double СуммаДокумента { get; set; }
	[ProtoMember(6)] public DateTime Дата { get; set; }
	[ProtoMember(7)] public string Номер { get; set; }
	[ProtoMember(8)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(9)] public bool Проведен { get; set; }
}

[ProtoContract] public sealed class ВозвратОтПокупателя
{
	[ProtoContract] public sealed class ВозвратыЗапись
	{
		[ProtoMember(1)] public double Всего { get; set; }
		[ProtoMember(2)] public Entity ЗСЯ { get; set; }
		[ProtoMember(3)] public double Количество { get; set; }
		[ProtoMember(4)] public Entity Отдел { get; set; }
		[ProtoMember(5)] public Entity Партия { get; set; }
		[ProtoMember(6)] public double Скидка { get; set; }
		[ProtoMember(7)] public double Сумма { get; set; }
		[ProtoMember(8)] public Entity ТипКомплектацииЗаказа { get; set; }
		[ProtoMember(9)] public Entity Товар { get; set; }
		[ProtoMember(10)] public double Цена { get; set; }
	}
	[ProtoContract] public sealed class НеХватилоЗапись
	{
		[ProtoMember(1)] public double КоличествоНеХватило { get; set; }
		[ProtoMember(2)] public Entity Партия { get; set; }
		[ProtoMember(3)] public Entity ТипСборки { get; set; }
		[ProtoMember(4)] public Entity Товар { get; set; }
	}
	[ProtoContract] public sealed class СписаниеПакетовЗапись
	{
		[ProtoMember(1)] public Entity ЗСЯ { get; set; }
		[ProtoMember(2)] public double Количество { get; set; }
		[ProtoMember(3)] public Entity Отдел { get; set; }
	}
	[ProtoMember(1)] public Union ДокументОснование { get; set; }
	[ProtoMember(2)] public Entity КодОперации { get; set; }
	[ProtoMember(3)] public double ОрдернаяСхема { get; set; }
	[ProtoMember(4)] public Entity Отдел { get; set; }
	[ProtoMember(5)] public bool СписаниеНеНайдено { get; set; }
	[ProtoMember(6)] public double СуммаДокумента { get; set; }
	[ProtoMember(7)] public Entity Фирма { get; set; }
	[ProtoMember(8)] public DateTime Дата { get; set; }
	[ProtoMember(9)] public string Номер { get; set; }
	[ProtoMember(10)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(11)] public bool Проведен { get; set; }
	[ProtoMember(12)] public Entity ПричинаВозврата { get; set; }
	[ProtoMember(13)] public List<ВозвратОтПокупателя.ВозвратыЗапись> Возвраты { get; set; } = new List<ВозвратОтПокупателя.ВозвратыЗапись>();
	[ProtoMember(14)] public List<ВозвратОтПокупателя.НеХватилоЗапись> НеХватило { get; set; } = new List<ВозвратОтПокупателя.НеХватилоЗапись>();
	[ProtoMember(15)] public List<ВозвратОтПокупателя.СписаниеПакетовЗапись> СписаниеПакетов { get; set; } = new List<ВозвратОтПокупателя.СписаниеПакетовЗапись>();
}

[ProtoContract] public sealed class ВозвратТоваровОтПокупателя
{
	[ProtoContract] public sealed class ТоварыЗапись
	{
		[ProtoMember(1)] public double Всего { get; set; }
		[ProtoMember(2)] public Entity Документ { get; set; }
		[ProtoMember(3)] public double Количество { get; set; }
		[ProtoMember(4)] public Entity Партия { get; set; }
		[ProtoMember(5)] public Entity СтавкаНДС { get; set; }
		[ProtoMember(6)] public double Сумма { get; set; }
		[ProtoMember(7)] public double СуммаНДС { get; set; }
		[ProtoMember(8)] public Entity Товар { get; set; }
		[ProtoMember(9)] public double Цена { get; set; }
		[ProtoMember(10)] public bool МДЛП { get; set; }
	}
	[ProtoMember(1)] public Union ДокументОснование { get; set; }
	[ProtoMember(2)] public Entity Клиент { get; set; }
	[ProtoMember(3)] public Entity Организация { get; set; }
	[ProtoMember(4)] public Entity Склад { get; set; }
	[ProtoMember(5)] public double СуммаДокумента { get; set; }
	[ProtoMember(6)] public double СуммаНДС { get; set; }
	[ProtoMember(7)] public DateTime Дата { get; set; }
	[ProtoMember(8)] public string Номер { get; set; }
	[ProtoMember(9)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(10)] public bool Проведен { get; set; }
	[ProtoMember(11)] public Entity ЗаказКлиента { get; set; }
	[ProtoMember(12)] public string Комментарий { get; set; }
	[ProtoMember(13)] public Entity Договор { get; set; }
	[ProtoMember(14)] public string НомерПоДаннымПартнера { get; set; }
	[ProtoMember(15)] public DateTime ДатаПоДаннымПартнера { get; set; }
	[ProtoMember(16)] public Entity Ответственный { get; set; }
	[ProtoMember(17)] public DateTime ДатаКорСчетФактуры { get; set; }
	[ProtoMember(18)] public string НомерКорСчетФактуры { get; set; }
	[ProtoMember(19)] public bool УпрощенныйОбратныйАкцепт { get; set; }
	[ProtoMember(20)] public List<ВозвратТоваровОтПокупателя.ТоварыЗапись> Товары { get; set; } = new List<ВозвратТоваровОтПокупателя.ТоварыЗапись>();
}

[ProtoContract] public sealed class Выезд
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public string АдресДоставки { get; set; }
	[ProtoMember(3)] public string АдресДоставкиЗначенияПолей { get; set; }
	[ProtoMember(4)] public double ВесЗаказа { get; set; }
	[ProtoMember(5)] public DateTime ДатаДоставки { get; set; }
	[ProtoMember(6)] public Union ДокументОснование { get; set; }
	[ProtoMember(7)] public Entity ЗонаДоставки { get; set; }
	[ProtoMember(8)] public string ИдентификаторКлиента { get; set; }
	[ProtoMember(9)] public Entity ИнтервалДоставки { get; set; }
	[ProtoMember(10)] public string НомерТелефонаПолучателя { get; set; }
	[ProtoMember(11)] public double ОбъемЗаказа { get; set; }
	[ProtoMember(12)] public Entity Оператор { get; set; }
	[ProtoMember(13)] public DateTime ПозвонитьКлиентуЗа { get; set; }
	[ProtoMember(14)] public string ПолучательЗаказа { get; set; }
	[ProtoMember(15)] public string ПричинаВыезда { get; set; }
	[ProtoMember(16)] public Entity ПунктСамовывоза { get; set; }
	[ProtoMember(17)] public Entity Регион { get; set; }
	[ProtoMember(18)] public Entity Склад { get; set; }
	[ProtoMember(19)] public DateTime Дата { get; set; }
	[ProtoMember(20)] public string Номер { get; set; }
	[ProtoMember(21)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(22)] public bool Проведен { get; set; }
	[ProtoMember(23)] public Entity ТипВыезда { get; set; }
	[ProtoMember(24)] public Entity ЗаказКлиента { get; set; }
}

[ProtoContract] public sealed class ВыемкаДенежныхСредствИзКассыККМ
{
	[ProtoMember(1)] public Entity КассаККМ { get; set; }
	[ProtoMember(2)] public string Комментарий { get; set; }
	[ProtoMember(3)] public Entity Магазин { get; set; }
	[ProtoMember(4)] public Entity Организация { get; set; }
	[ProtoMember(5)] public Entity Ответственный { get; set; }
	[ProtoMember(6)] public double СуммаДокумента { get; set; }
	[ProtoMember(7)] public DateTime Дата { get; set; }
	[ProtoMember(8)] public string Номер { get; set; }
	[ProtoMember(9)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(10)] public bool Проведен { get; set; }
}

[ProtoContract] public sealed class ГрафикРаботыКурьера
{
	[ProtoContract] public sealed class ГрафикЗапись
	{
		[ProtoMember(1)] public DateTime ВремяВыхода { get; set; }
		[ProtoMember(2)] public DateTime День { get; set; }
		[ProtoMember(3)] public bool Заявка { get; set; }
		[ProtoMember(4)] public Entity ЗонаДоставки { get; set; }
		[ProtoMember(5)] public Entity Интервал { get; set; }
		[ProtoMember(6)] public double Количество { get; set; }
		[ProtoMember(7)] public Entity ТипДня { get; set; }
	}
	[ProtoMember(1)] public Entity Должность { get; set; }
	[ProtoMember(2)] public DateTime Месяц { get; set; }
	[ProtoMember(3)] public Entity Регион { get; set; }
	[ProtoMember(4)] public Entity Сотрудник { get; set; }
	[ProtoMember(5)] public DateTime Дата { get; set; }
	[ProtoMember(6)] public string Номер { get; set; }
	[ProtoMember(7)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(8)] public bool Проведен { get; set; }
	[ProtoMember(9)] public Entity ПрофильПередвижения { get; set; }
	[ProtoMember(10)] public Entity ФормаРасчета { get; set; }
	[ProtoMember(11)] public List<ГрафикРаботыКурьера.ГрафикЗапись> График { get; set; } = new List<ГрафикРаботыКурьера.ГрафикЗапись>();
}

[ProtoContract] public sealed class ДелениеТовара
{
	[ProtoContract] public sealed class ТоварыЗапись
	{
		[ProtoMember(1)] public double Количество { get; set; }
		[ProtoMember(2)] public double Коэффициент { get; set; }
		[ProtoMember(3)] public Entity НоваяПартия { get; set; }
		[ProtoMember(4)] public double НовоеКоличество { get; set; }
		[ProtoMember(5)] public Entity НовыйТовар { get; set; }
		[ProtoMember(6)] public Entity Партия { get; set; }
		[ProtoMember(7)] public Entity Товар { get; set; }
	}
	[ProtoMember(1)] public Entity Ответственный { get; set; }
	[ProtoMember(2)] public Entity Отдел { get; set; }
	[ProtoMember(3)] public DateTime Дата { get; set; }
	[ProtoMember(4)] public string Номер { get; set; }
	[ProtoMember(5)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(6)] public bool Проведен { get; set; }
	[ProtoMember(7)] public List<ДелениеТовара.ТоварыЗапись> Товары { get; set; } = new List<ДелениеТовара.ТоварыЗапись>();
}

[ProtoContract] public sealed class ЗаданиеНаДоставку
{
	[ProtoContract] public sealed class ЗаказыЗапись
	{
		[ProtoMember(1)] public Entity ЗаказКлиента { get; set; }
		[ProtoMember(2)] public bool Отсканировано { get; set; }
		[ProtoMember(3)] public Entity Упаковка { get; set; }
		[ProtoMember(4)] public Entity ЧекККМ { get; set; }
	}
	[ProtoContract] public sealed class ПеремещенияЗапись
	{
		[ProtoMember(1)] public bool Отсканировано { get; set; }
		[ProtoMember(2)] public Entity ПеремещениеЗаказовКлиентов { get; set; }
	}
	[ProtoMember(1)] public DateTime ВремяВыходаПоГрафику { get; set; }
	[ProtoMember(2)] public double КоличествоЗаказов { get; set; }
	[ProtoMember(3)] public double КоличествоМест { get; set; }
	[ProtoMember(4)] public string Комментарий { get; set; }
	[ProtoMember(5)] public Entity Курьер { get; set; }
	[ProtoMember(6)] public Entity МестоХранения { get; set; }
	[ProtoMember(7)] public Entity Ответственный { get; set; }
	[ProtoMember(8)] public Entity Подразделение { get; set; }
	[ProtoMember(9)] public bool Предварительный { get; set; }
	[ProtoMember(10)] public DateTime СобратьДо { get; set; }
	[ProtoMember(11)] public DateTime Дата { get; set; }
	[ProtoMember(12)] public string Номер { get; set; }
	[ProtoMember(13)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(14)] public bool Проведен { get; set; }
	[ProtoMember(15)] public List<ЗаданиеНаДоставку.ЗаказыЗапись> Заказы { get; set; } = new List<ЗаданиеНаДоставку.ЗаказыЗапись>();
	[ProtoMember(16)] public List<ЗаданиеНаДоставку.ПеремещенияЗапись> Перемещения { get; set; } = new List<ЗаданиеНаДоставку.ПеремещенияЗапись>();
}

[ProtoContract] public sealed class Заказ
{
	[ProtoContract] public sealed class ПодЗаказЗапись
	{
		[ProtoMember(1)] public DateTime ДатаПоставки { get; set; }
		[ProtoMember(2)] public string КлючСвязи { get; set; }
		[ProtoMember(3)] public double Количество { get; set; }
		[ProtoMember(4)] public double КоличествоПеренесено { get; set; }
		[ProtoMember(5)] public string Комментарий { get; set; }
		[ProtoMember(6)] public string Наименование { get; set; }
		[ProtoMember(7)] public string Поставщик { get; set; }
		[ProtoMember(8)] public string Производитель { get; set; }
		[ProtoMember(9)] public DateTime СрокГодности { get; set; }
		[ProtoMember(10)] public Entity СтатусОбработки { get; set; }
		[ProtoMember(11)] public Entity Товар { get; set; }
		[ProtoMember(12)] public double Цена { get; set; }
	}
	[ProtoContract] public sealed class ТоварЗапись
	{
		[ProtoMember(1)] public double Всего { get; set; }
		[ProtoMember(2)] public bool ВставленИзНехватило { get; set; }
		[ProtoMember(3)] public DateTime ДатаПлан { get; set; }
		[ProtoMember(4)] public string КлючСвязи { get; set; }
		[ProtoMember(5)] public string КлючСвязиПартия { get; set; }
		[ProtoMember(6)] public double Количество { get; set; }
		[ProtoMember(7)] public double КоличествоПриОформлении { get; set; }
		[ProtoMember(8)] public Entity Партия { get; set; }
		[ProtoMember(9)] public double Скидка { get; set; }
		[ProtoMember(10)] public Entity Склад { get; set; }
		[ProtoMember(11)] public double Сумма { get; set; }
		[ProtoMember(12)] public Entity Товар { get; set; }
		[ProtoMember(13)] public double Цена { get; set; }
	}
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public Entity АвторПробития { get; set; }
	[ProtoMember(3)] public string АдресДоставки { get; set; }
	[ProtoMember(4)] public double ВесЗаказа { get; set; }
	[ProtoMember(5)] public double ВремяДо { get; set; }
	[ProtoMember(6)] public DateTime ВремяПробития { get; set; }
	[ProtoMember(7)] public double ВремяС { get; set; }
	[ProtoMember(8)] public double Всего { get; set; }
	[ProtoMember(9)] public DateTime ДатаДоставки { get; set; }
	[ProtoMember(10)] public Entity ДокументОснование { get; set; }
	[ProtoMember(11)] public Entity ЗонаДоставки { get; set; }
	[ProtoMember(12)] public Entity ИнтервалДоставки { get; set; }
	[ProtoMember(13)] public Entity ККМ { get; set; }
	[ProtoMember(14)] public Entity Клиент { get; set; }
	[ProtoMember(15)] public string КомментарийКлиента { get; set; }
	[ProtoMember(16)] public double МестПростых { get; set; }
	[ProtoMember(17)] public double МестХолодных { get; set; }
	[ProtoMember(18)] public string НомерЗаказаСайта { get; set; }
	[ProtoMember(19)] public double ОбъемЗаказа { get; set; }
	[ProtoMember(20)] public Entity Оператор { get; set; }
	[ProtoMember(21)] public Entity Отдел { get; set; }
	[ProtoMember(22)] public bool ОтмененДоСборки { get; set; }
	[ProtoMember(23)] public bool ОтправленоСМС { get; set; }
	[ProtoMember(24)] public DateTime ПозвонитьКлиентуЗа { get; set; }
	[ProtoMember(25)] public string Покупатель { get; set; }
	[ProtoMember(26)] public bool Пробит { get; set; }
	[ProtoMember(27)] public Entity Регион { get; set; }
	[ProtoMember(28)] public string Сайт { get; set; }
	[ProtoMember(29)] public Entity Склад { get; set; }
	[ProtoMember(30)] public bool Собран { get; set; }
	[ProtoMember(31)] public Entity СпособДоставки { get; set; }
	[ProtoMember(32)] public DateTime СрокХранения { get; set; }
	[ProtoMember(33)] public double СтатусЗаказа { get; set; }
	[ProtoMember(34)] public Entity СтатусыОбработкиЗаказа { get; set; }
	[ProtoMember(35)] public double СуммаДокумента { get; set; }
	[ProtoMember(36)] public double СуммаДоставки { get; set; }
	[ProtoMember(37)] public string Телефон { get; set; }
	[ProtoMember(38)] public Entity ТипДоставки { get; set; }
	[ProtoMember(39)] public Entity ТипКомплектацииЗаказа { get; set; }
	[ProtoMember(40)] public Entity ТипОплаты { get; set; }
	[ProtoMember(41)] public Entity ТипЦены { get; set; }
	[ProtoMember(42)] public Entity ТочкаСамовывоза { get; set; }
	[ProtoMember(43)] public Entity Фирма { get; set; }
	[ProtoMember(44)] public Entity ФормаОплаты { get; set; }
	[ProtoMember(45)] public DateTime Дата { get; set; }
	[ProtoMember(46)] public string Номер { get; set; }
	[ProtoMember(47)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(48)] public bool Проведен { get; set; }
	[ProtoMember(49)] public List<Заказ.ПодЗаказЗапись> ПодЗаказ { get; set; } = new List<Заказ.ПодЗаказЗапись>();
	[ProtoMember(50)] public List<Заказ.ТоварЗапись> Товар { get; set; } = new List<Заказ.ТоварЗапись>();
}

[ProtoContract] public sealed class ЗаказКлиента
{
	[ProtoContract] public sealed class ВложенияГрузоместаЗапись
	{
		[ProtoMember(1)] public Entity ЗаказКлиента { get; set; }
	}
	[ProtoContract] public sealed class КонтактнаяИнформацияЗапись
	{
		[ProtoMember(1)] public string АдресЭП { get; set; }
		[ProtoMember(2)] public Entity Вид { get; set; }
		[ProtoMember(3)] public Entity ВидДляСписка { get; set; }
		[ProtoMember(4)] public string Город { get; set; }
		[ProtoMember(5)] public string ДоменноеИмяСервера { get; set; }
		[ProtoMember(6)] public string ЗначенияПолей { get; set; }
		[ProtoMember(7)] public string НомерТелефона { get; set; }
		[ProtoMember(8)] public string НомерТелефонаБезКодов { get; set; }
		[ProtoMember(9)] public string Представление { get; set; }
		[ProtoMember(10)] public string Регион { get; set; }
		[ProtoMember(11)] public string Страна { get; set; }
		[ProtoMember(12)] public Entity Тип { get; set; }
		[ProtoMember(13)] public string Значение { get; set; }
	}
	[ProtoContract] public sealed class СпособыОплатыЗапись
	{
		[ProtoMember(1)] public DateTime ДатаПлатежа { get; set; }
		[ProtoMember(2)] public string НомерТранзакции { get; set; }
		[ProtoMember(3)] public double СуммаПлатежа { get; set; }
		[ProtoMember(4)] public Entity ФормаОплаты { get; set; }
		[ProtoMember(5)] public Entity БанкЭквайер { get; set; }
		[ProtoMember(6)] public Entity СпособОнлайнОплаты { get; set; }
	}
	[ProtoContract] public sealed class ТоварыЗапись
	{
		[ProtoMember(1)] public Entity ВариантРезервирования { get; set; }
		[ProtoMember(2)] public string ИдентификаторСтроки { get; set; }
		[ProtoMember(3)] public double Количество { get; set; }
		[ProtoMember(4)] public double КоличествоСобрано { get; set; }
		[ProtoMember(5)] public string Комментарий { get; set; }
		[ProtoMember(6)] public Entity Номенклатура { get; set; }
		[ProtoMember(7)] public bool Отменено { get; set; }
		[ProtoMember(8)] public Entity Партия { get; set; }
		[ProtoMember(9)] public bool Подарок { get; set; }
		[ProtoMember(10)] public string ПромоКод { get; set; }
		[ProtoMember(11)] public double ПроцентСкидки { get; set; }
		[ProtoMember(12)] public Union Размещение { get; set; }
		[ProtoMember(13)] public double Сумма { get; set; }
		[ProtoMember(14)] public double СуммаСкидки { get; set; }
		[ProtoMember(15)] public DateTime ТребуемыйСрокГодности { get; set; }
		[ProtoMember(16)] public bool ТребуетсяСертификат { get; set; }
		[ProtoMember(17)] public double Цена { get; set; }
		[ProtoMember(18)] public Entity ПричинаОтмены { get; set; }
	}
	[ProtoContract] public sealed class ТоварыПодЗаказЗапись
	{
		[ProtoMember(1)] public DateTime ДатаПоставки { get; set; }
		[ProtoMember(2)] public string ИдентификаторСтрокиТоваров { get; set; }
		[ProtoMember(3)] public string КодТовараПоставщика { get; set; }
		[ProtoMember(4)] public string НаименованиеТовара { get; set; }
		[ProtoMember(5)] public string Производитель { get; set; }
		[ProtoMember(6)] public DateTime СрокГодности { get; set; }
	}
	[ProtoContract] public sealed class УпаковкиЗапись
	{
		[ProtoMember(1)] public Entity Упаковка { get; set; }
	}
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public string АдресДоставки { get; set; }
	[ProtoMember(3)] public string АдресДоставкиЗначенияПолей { get; set; }
	[ProtoMember(4)] public double ВесЗаказа { get; set; }
	[ProtoMember(5)] public Entity ВидЗаказаКлиента { get; set; }
	[ProtoMember(6)] public DateTime ВремяВыдачиЗаказа { get; set; }
	[ProtoMember(7)] public DateTime ДатаДоставки { get; set; }
	[ProtoMember(8)] public Entity ЗонаДоставки { get; set; }
	[ProtoMember(9)] public string ИдентификаторАдресаДоставки { get; set; }
	[ProtoMember(10)] public Entity ИнтервалДоставки { get; set; }
	[ProtoMember(11)] public Entity Клиент { get; set; }
	[ProtoMember(12)] public string КомментарийДляСборки { get; set; }
	[ProtoMember(13)] public string КомментарийКлиента { get; set; }
	[ProtoMember(14)] public string НомерНаСайте { get; set; }
	[ProtoMember(15)] public string НомерСокращенный { get; set; }
	[ProtoMember(16)] public double ОбъемЗаказа { get; set; }
	[ProtoMember(17)] public Entity Оператор { get; set; }
	[ProtoMember(18)] public bool Отменен { get; set; }
	[ProtoMember(19)] public DateTime ПозвонитьКлиентуЗа { get; set; }
	[ProtoMember(20)] public double ПроцентСкидки { get; set; }
	[ProtoMember(21)] public Entity ПунктСамовывоза { get; set; }
	[ProtoMember(22)] public Entity Регион { get; set; }
	[ProtoMember(23)] public Entity Сайт { get; set; }
	[ProtoMember(24)] public double СдачаС { get; set; }
	[ProtoMember(25)] public Entity Склад { get; set; }
	[ProtoMember(26)] public Entity СпособДоставки { get; set; }
	[ProtoMember(27)] public DateTime СрокОплаты { get; set; }
	[ProtoMember(28)] public double СуммаДокумента { get; set; }
	[ProtoMember(29)] public double СуммаДоставки { get; set; }
	[ProtoMember(30)] public double СуммаОплатыЗаВес { get; set; }
	[ProtoMember(31)] public double СуммаОплатыЗаОбъем { get; set; }
	[ProtoMember(32)] public double СуммаОплатыЗаСтоимостьЗаказа { get; set; }
	[ProtoMember(33)] public double СуммаСкидки { get; set; }
	[ProtoMember(34)] public bool ТребуетсяКвитанция { get; set; }
	[ProtoMember(35)] public bool ТребуетсяТоварныйЧек { get; set; }
	[ProtoMember(36)] public Entity ФормаОплаты { get; set; }
	[ProtoMember(37)] public DateTime Дата { get; set; }
	[ProtoMember(38)] public string Номер { get; set; }
	[ProtoMember(39)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(40)] public bool Проведен { get; set; }
	[ProtoMember(41)] public string НомерНаСкладе { get; set; }
	[ProtoMember(42)] public bool СрочнаяДоставка { get; set; }
	//[ProtoMember(43)] public Entity Клиент { get; set; }
	[ProtoMember(44)] public bool УчаствуетВПрограммеMindbox { get; set; }
	[ProtoMember(45)] public bool НеПересчитыватьСуммуДоставки { get; set; }
	[ProtoMember(46)] public string ПолучательЗаказа { get; set; }
	[ProtoMember(47)] public Entity СпособОтправкиСертификатов { get; set; }
	[ProtoMember(48)] public string ИдентификаторWMS { get; set; }
	[ProtoMember(49)] public Entity ПричинаОтмены { get; set; }
	[ProtoMember(50)] public bool ОтправлятьЗаказПровайдеруЭР { get; set; }
	[ProtoMember(51)] public Entity Договор { get; set; }
	[ProtoMember(52)] public double СуммаСкидкиЗаДоставку { get; set; }
	[ProtoMember(53)] public List<ЗаказКлиента.ВложенияГрузоместаЗапись> ВложенияГрузоместа { get; set; } = new List<ЗаказКлиента.ВложенияГрузоместаЗапись>();
	[ProtoMember(54)] public List<ЗаказКлиента.КонтактнаяИнформацияЗапись> КонтактнаяИнформация { get; set; } = new List<ЗаказКлиента.КонтактнаяИнформацияЗапись>();
	[ProtoMember(55)] public List<ЗаказКлиента.СпособыОплатыЗапись> СпособыОплаты { get; set; } = new List<ЗаказКлиента.СпособыОплатыЗапись>();
	[ProtoMember(56)] public List<ЗаказКлиента.ТоварыЗапись> Товары { get; set; } = new List<ЗаказКлиента.ТоварыЗапись>();
	[ProtoMember(57)] public List<ЗаказКлиента.ТоварыПодЗаказЗапись> ТоварыПодЗаказ { get; set; } = new List<ЗаказКлиента.ТоварыПодЗаказЗапись>();
	[ProtoMember(58)] public List<ЗаказКлиента.УпаковкиЗапись> Упаковки { get; set; } = new List<ЗаказКлиента.УпаковкиЗапись>();
}

[ProtoContract] public sealed class ЗаказПоставщику
{
	[ProtoContract] public sealed class ЗаказыЗапись
	{
		[ProtoMember(1)] public Entity Заказ { get; set; }
		[ProtoMember(2)] public string КлючСвязи { get; set; }
		[ProtoMember(3)] public double Количество { get; set; }
		[ProtoMember(4)] public Entity Товар { get; set; }
	}
	[ProtoContract] public sealed class ТоварыЗапись
	{
		[ProtoMember(1)] public string КодТовараПоставщика { get; set; }
		[ProtoMember(2)] public double Количество { get; set; }
		[ProtoMember(3)] public double КоличествоОтказ { get; set; }
		[ProtoMember(4)] public double КоэфДеления { get; set; }
		[ProtoMember(5)] public Entity Склад { get; set; }
		[ProtoMember(6)] public DateTime СрокГодности { get; set; }
		[ProtoMember(7)] public double Сумма { get; set; }
		[ProtoMember(8)] public Entity Товар { get; set; }
		[ProtoMember(9)] public double Цена { get; set; }
	}
	[ProtoMember(1)] public DateTime ДатаДоставки { get; set; }
	[ProtoMember(2)] public Entity Договор { get; set; }
	[ProtoMember(3)] public string ИмяФайла { get; set; }
	[ProtoMember(4)] public Entity Клиент { get; set; }
	[ProtoMember(5)] public bool КодПрайсаПоставщика { get; set; }
	[ProtoMember(6)] public string Комментарий { get; set; }
	[ProtoMember(7)] public Entity КонтрагентПрайса { get; set; }
	[ProtoMember(8)] public string НомерДокПоставщика { get; set; }
	[ProtoMember(9)] public bool ПодЗаказ { get; set; }
	[ProtoMember(10)] public bool Статус { get; set; }
	[ProtoMember(11)] public double СуммаДокумента { get; set; }
	[ProtoMember(12)] public double ТипПрайса { get; set; }
	[ProtoMember(13)] public DateTime Дата { get; set; }
	[ProtoMember(14)] public string Номер { get; set; }
	[ProtoMember(15)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(16)] public bool Проведен { get; set; }
	[ProtoMember(17)] public List<ЗаказПоставщику.ЗаказыЗапись> Заказы { get; set; } = new List<ЗаказПоставщику.ЗаказыЗапись>();
	[ProtoMember(18)] public List<ЗаказПоставщику.ТоварыЗапись> Товары { get; set; } = new List<ЗаказПоставщику.ТоварыЗапись>();
}

[ProtoContract] public sealed class ЗакрытиеМаршрутногоЛиста
{
	[ProtoContract] public sealed class ЗаказыЗапись
	{
		[ProtoMember(1)] public Entity ЗаказКлиента { get; set; }
		[ProtoMember(2)] public bool Сдан { get; set; }
	}
	[ProtoContract] public sealed class ВыездыЗапись
	{
		[ProtoMember(1)] public Entity Выезд { get; set; }
		[ProtoMember(2)] public bool Сдан { get; set; }
	}
	[ProtoMember(1)] public Entity АдресХранения { get; set; }
	[ProtoMember(2)] public string Комментарий { get; set; }
	[ProtoMember(3)] public Entity Курьер { get; set; }
	[ProtoMember(4)] public Entity МаршрутныйЛист { get; set; }
	[ProtoMember(5)] public DateTime Дата { get; set; }
	[ProtoMember(6)] public string Номер { get; set; }
	[ProtoMember(7)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(8)] public bool Проведен { get; set; }
	[ProtoMember(9)] public Entity Ответственный { get; set; }
	[ProtoMember(10)] public List<ЗакрытиеМаршрутногоЛиста.ЗаказыЗапись> Заказы { get; set; } = new List<ЗакрытиеМаршрутногоЛиста.ЗаказыЗапись>();
	[ProtoMember(11)] public List<ЗакрытиеМаршрутногоЛиста.ВыездыЗапись> Выезды { get; set; } = new List<ЗакрытиеМаршрутногоЛиста.ВыездыЗапись>();
}

[ProtoContract] public sealed class ЗакрытиеСменыКурьера
{
	[ProtoContract] public sealed class ВложенияЗапись
	{
		[ProtoMember(1)] public double Количество { get; set; }
		[ProtoMember(2)] public double КоличествоФакт { get; set; }
		[ProtoMember(3)] public double СуммаВложения { get; set; }
		[ProtoMember(4)] public Entity Товар { get; set; }
		[ProtoMember(5)] public double ЦенаВложения { get; set; }
	}
	[ProtoContract] public sealed class ЗаказыЗапись
	{
		[ProtoMember(1)] public Entity ЗаказКлиента { get; set; }
		[ProtoMember(2)] public bool Отсканировано { get; set; }
		[ProtoMember(3)] public Entity ОтчетОДоставке { get; set; }
		[ProtoMember(4)] public double СуммаНаличные { get; set; }
		[ProtoMember(5)] public Entity ЧекККМ { get; set; }
	}
	[ProtoContract] public sealed class НачисленияЗапись
	{
		[ProtoMember(1)] public double Сумма { get; set; }
	}
	[ProtoMember(1)] public Entity ЗаданиеНаДоставку { get; set; }
	[ProtoMember(2)] public string Комментарий { get; set; }
	[ProtoMember(3)] public Entity Курьер { get; set; }
	[ProtoMember(4)] public Entity МестоХранения { get; set; }
	[ProtoMember(5)] public Entity Ответственный { get; set; }
	[ProtoMember(6)] public Entity Подразделение { get; set; }
	[ProtoMember(7)] public DateTime Дата { get; set; }
	[ProtoMember(8)] public string Номер { get; set; }
	[ProtoMember(9)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(10)] public bool Проведен { get; set; }
	[ProtoMember(11)] public List<ЗакрытиеСменыКурьера.ВложенияЗапись> Вложения { get; set; } = new List<ЗакрытиеСменыКурьера.ВложенияЗапись>();
	[ProtoMember(12)] public List<ЗакрытиеСменыКурьера.ЗаказыЗапись> Заказы { get; set; } = new List<ЗакрытиеСменыКурьера.ЗаказыЗапись>();
	[ProtoMember(13)] public List<ЗакрытиеСменыКурьера.НачисленияЗапись> Начисления { get; set; } = new List<ЗакрытиеСменыКурьера.НачисленияЗапись>();
}

[ProtoContract] public sealed class ЗаявкаНаДефектуру
{
	[ProtoContract] public sealed class ТоварыЗапись
	{
		[ProtoMember(1)] public double Количество { get; set; }
		[ProtoMember(2)] public Entity Товар { get; set; }
	}
	[ProtoMember(1)] public Entity Склад { get; set; }
	[ProtoMember(2)] public Entity ТипЗаявки { get; set; }
	[ProtoMember(3)] public DateTime Дата { get; set; }
	[ProtoMember(4)] public string Номер { get; set; }
	[ProtoMember(5)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(6)] public bool Проведен { get; set; }
	[ProtoMember(7)] public List<ЗаявкаНаДефектуру.ТоварыЗапись> Товары { get; set; } = new List<ЗаявкаНаДефектуру.ТоварыЗапись>();
}

[ProtoContract] public sealed class ЗаявкаНаРасценку
{
	[ProtoContract] public sealed class ЦеныЗапись
	{
		[ProtoMember(1)] public string ИмяПравилаРассчитавшегоЦену { get; set; }
		[ProtoMember(2)] public double Количество { get; set; }
		[ProtoMember(3)] public string Комментарий { get; set; }
		[ProtoMember(4)] public double НаценкаМаксимальная { get; set; }
		[ProtoMember(5)] public double НаценкаМинимальная { get; set; }
		[ProtoMember(6)] public double НаценкаРекомендованная { get; set; }
		[ProtoMember(7)] public Entity Номенклатура { get; set; }
		[ProtoMember(8)] public double ПДЦ { get; set; }
		[ProtoMember(9)] public Entity Поставщик { get; set; }
		[ProtoMember(10)] public bool ПризнакИСГ { get; set; }
		[ProtoMember(11)] public double Себестоимость { get; set; }
		[ProtoMember(12)] public DateTime СрокГодности { get; set; }
		[ProtoMember(13)] public double СтавкаНДС { get; set; }
		[ProtoMember(14)] public bool ТребуетСогласования { get; set; }
		[ProtoMember(15)] public Entity Характеристика { get; set; }
		[ProtoMember(16)] public double Цена { get; set; }
		[ProtoMember(17)] public double ЦенаЗакупкиБезНДС { get; set; }
		[ProtoMember(18)] public double ЦенаЗачеркнутая { get; set; }
		[ProtoMember(19)] public double ЦенаМинимальная { get; set; }
		[ProtoMember(20)] public double ЦенаМобильногоПриложения { get; set; }
		[ProtoMember(21)] public double ЦенаПоставки { get; set; }
		[ProtoMember(22)] public double ЦенаТекущая { get; set; }
		[ProtoMember(23)] public double ЦЗИ { get; set; }
	}
	[ProtoContract] public sealed class ЭтапыФормированияЗапись
	{
		[ProtoMember(1)] public string НаименованиеЭтапа { get; set; }
		[ProtoMember(2)] public Entity ТипАлгоритма { get; set; }
		[ProtoMember(3)] public double ЭтапПравила { get; set; }
	}
	[ProtoMember(1)] public Entity Аптека { get; set; }
	[ProtoMember(2)] public double ВремяФормирования { get; set; }
	[ProtoMember(3)] public bool Выгружен { get; set; }
	[ProtoMember(4)] public DateTime ДатаАктивации { get; set; }
	[ProtoMember(5)] public Entity ДокументОснование { get; set; }
	[ProtoMember(6)] public double КоличествоОшибок { get; set; }
	[ProtoMember(7)] public double КоличествоПозиций { get; set; }
	[ProtoMember(8)] public string Комментарий { get; set; }
	[ProtoMember(9)] public Entity Пользователь { get; set; }
	[ProtoMember(10)] public Entity Поставщик { get; set; }
	[ProtoMember(11)] public bool РазрешитьЗапись { get; set; }
	[ProtoMember(12)] public Entity Регион { get; set; }
	[ProtoMember(13)] public Entity Сайт { get; set; }
	[ProtoMember(14)] public Entity Статус { get; set; }
	[ProtoMember(15)] public Entity ТипОперации { get; set; }
	[ProtoMember(16)] public DateTime Дата { get; set; }
	[ProtoMember(17)] public string Номер { get; set; }
	[ProtoMember(18)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(19)] public bool Проведен { get; set; }
	[ProtoMember(20)] public bool ЛогАрхивирован { get; set; }
	[ProtoMember(21)] public List<ЗаявкаНаРасценку.ЦеныЗапись> Цены { get; set; } = new List<ЗаявкаНаРасценку.ЦеныЗапись>();
	[ProtoMember(22)] public List<ЗаявкаНаРасценку.ЭтапыФормированияЗапись> ЭтапыФормирования { get; set; } = new List<ЗаявкаНаРасценку.ЭтапыФормированияЗапись>();
}

[ProtoContract] public sealed class ИмпортДанныхКонтрагентов
{
	[ProtoMember(1)] public bool ДанныеПоЗакупкам { get; set; }
	[ProtoMember(2)] public bool ДанныеПоОстаткам { get; set; }
	[ProtoMember(3)] public bool ДанныеПоПродажам { get; set; }
	[ProtoMember(4)] public DateTime ДатаНачала { get; set; }
	[ProtoMember(5)] public DateTime ДатаНачалаЗакупок { get; set; }
	[ProtoMember(6)] public DateTime ДатаНачалаПродаж { get; set; }
	[ProtoMember(7)] public DateTime ДатаОкончания { get; set; }
	[ProtoMember(8)] public DateTime ДатаОкончанияЗакупок { get; set; }
	[ProtoMember(9)] public DateTime ДатаОкончанияПродаж { get; set; }
	[ProtoMember(10)] public Entity Контрагент { get; set; }
	[ProtoMember(11)] public Entity СкладКонтрагента { get; set; }
	[ProtoMember(12)] public DateTime Дата { get; set; }
	[ProtoMember(13)] public string Номер { get; set; }
	[ProtoMember(14)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(15)] public bool Проведен { get; set; }
}

[ProtoContract] public sealed class Инвентаризация
{
	[ProtoContract] public sealed class ИтогЗапись
	{
		[ProtoMember(1)] public bool ДобавленноВручную { get; set; }
		[ProtoMember(2)] public double ЗакупочнаяЦена { get; set; }
		[ProtoMember(3)] public Entity Партия { get; set; }
		[ProtoMember(4)] public double ПоБазе { get; set; }
		[ProtoMember(5)] public double РозничнаяЦена { get; set; }
		[ProtoMember(6)] public Entity Товар { get; set; }
		[ProtoMember(7)] public double Факт { get; set; }
		[ProtoMember(8)] public Entity Ячейка { get; set; }
		[ProtoMember(9)] public double ЗакупочнаяЦенаБезНДС { get; set; }
		[ProtoMember(10)] public double СуммаУчет { get; set; }
		[ProtoMember(11)] public double СуммаФакт { get; set; }
	}
	[ProtoContract] public sealed class Лог_Инвентаризация_СканированиеЗапись
	{
		[ProtoMember(1)] public double Количество { get; set; }
		[ProtoMember(2)] public DateTime Период { get; set; }
		[ProtoMember(3)] public Entity Склад { get; set; }
		[ProtoMember(4)] public Entity Сотрудник { get; set; }
		[ProtoMember(5)] public Entity Товар { get; set; }
	}
	[ProtoContract] public sealed class ПриходПоТоварамЗапись
	{
		[ProtoMember(1)] public double Количество { get; set; }
		[ProtoMember(2)] public Entity Партия { get; set; }
		[ProtoMember(3)] public double РеестроваяЦена { get; set; }
		[ProtoMember(4)] public Entity СтавкаНДС { get; set; }
		[ProtoMember(5)] public Entity Товар { get; set; }
		[ProtoMember(6)] public double Цена { get; set; }
		[ProtoMember(7)] public double ЦенаПроизводителя { get; set; }
		[ProtoMember(8)] public Entity МестоЗСЯ { get; set; }
	}
	[ProtoContract] public sealed class СписаниеПоПартиямЗапись
	{
		[ProtoMember(1)] public double Количество { get; set; }
		[ProtoMember(2)] public Entity Партия { get; set; }
		[ProtoMember(3)] public Entity Товар { get; set; }
		[ProtoMember(4)] public Entity МестоЗСЯ { get; set; }
		[ProtoMember(5)] public double СебестоимостьБезНДС { get; set; }
	}
	[ProtoContract] public sealed class МОЛЗапись
	{
		[ProtoMember(1)] public string Должность { get; set; }
		[ProtoMember(2)] public string ФИО { get; set; }
	}
	[ProtoContract] public sealed class ЧленыКомиссииЗапись
	{
		[ProtoMember(1)] public string Должность { get; set; }
		[ProtoMember(2)] public string ФИО { get; set; }
	}
	[ProtoMember(1)] public Entity АдресХранения { get; set; }
	[ProtoMember(2)] public double АлгоритмЗаполненияФактическихОстатков { get; set; }
	[ProtoMember(3)] public Entity Вид { get; set; }
	[ProtoMember(4)] public double ГлубинаПоискаЦен { get; set; }
	[ProtoMember(5)] public DateTime ДатаНачала { get; set; }
	[ProtoMember(6)] public DateTime ДатаОкончания { get; set; }
	[ProtoMember(7)] public double ЗапретитьВводКоличестваРукамиНаТСДЕслиОстаткМеньшеЧем { get; set; }
	[ProtoMember(8)] public bool ИнвентаризацияЗСЯ { get; set; }
	[ProtoMember(9)] public Entity Исполнитель { get; set; }
	[ProtoMember(10)] public double ИстекающийСрокГодности { get; set; }
	[ProtoMember(11)] public string Комментарий { get; set; }
	[ProtoMember(12)] public double НомерПросчета { get; set; }
	[ProtoMember(13)] public Entity ОтборПересчета { get; set; }
	[ProtoMember(14)] public Entity Ответственный { get; set; }
	[ProtoMember(15)] public bool ПолнаяИнвентаризация { get; set; }
	[ProtoMember(16)] public string ПредседательКомиссии { get; set; }
	[ProtoMember(17)] public Entity Склад { get; set; }
	[ProtoMember(18)] public bool СозданноВЦО { get; set; }
	[ProtoMember(19)] public Entity Статус { get; set; }
	[ProtoMember(20)] public bool УчитыватьВсеОтскнированныеТовары { get; set; }
	[ProtoMember(21)] public DateTime Дата { get; set; }
	[ProtoMember(22)] public string Номер { get; set; }
	[ProtoMember(23)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(24)] public bool Проведен { get; set; }
	[ProtoMember(25)] public double АлгоритмЗагрузкиФактическихОстатковИзФайла { get; set; }
	[ProtoMember(26)] public string ДолжностьПредседателяКомиссии { get; set; }
	[ProtoMember(27)] public Entity СпособИнвентаризации { get; set; }
	[ProtoMember(28)] public Entity ТекущийРезультатПересчета { get; set; }
	[ProtoMember(29)] public bool ЦеныОприходованияПересчитаны { get; set; }
	[ProtoMember(30)] public List<Инвентаризация.ИтогЗапись> Итог { get; set; } = new List<Инвентаризация.ИтогЗапись>();
	[ProtoMember(31)] public List<Инвентаризация.Лог_Инвентаризация_СканированиеЗапись> Лог_Инвентаризация_Сканирование { get; set; } = new List<Инвентаризация.Лог_Инвентаризация_СканированиеЗапись>();
	[ProtoMember(32)] public List<Инвентаризация.ПриходПоТоварамЗапись> ПриходПоТоварам { get; set; } = new List<Инвентаризация.ПриходПоТоварамЗапись>();
	[ProtoMember(33)] public List<Инвентаризация.СписаниеПоПартиямЗапись> СписаниеПоПартиям { get; set; } = new List<Инвентаризация.СписаниеПоПартиямЗапись>();
	[ProtoMember(34)] public List<Инвентаризация.МОЛЗапись> МОЛ { get; set; } = new List<Инвентаризация.МОЛЗапись>();
	[ProtoMember(35)] public List<Инвентаризация.ЧленыКомиссииЗапись> ЧленыКомиссии { get; set; } = new List<Инвентаризация.ЧленыКомиссииЗапись>();
}

[ProtoContract] public sealed class КассоваяСмена
{
	[ProtoMember(1)] public Entity АдресХранения { get; set; }
	[ProtoMember(2)] public DateTime ДатаПервогоНепереданногоФД { get; set; }
	[ProtoMember(3)] public DateTime ДатаСменыККТ { get; set; }
	[ProtoMember(4)] public string КассаККМ { get; set; }
	[ProtoMember(5)] public double КоличествоНепереданныхФД { get; set; }
	[ProtoMember(6)] public double КоличествоФД { get; set; }
	[ProtoMember(7)] public double КоличествоЧеков { get; set; }
	[ProtoMember(8)] public string Комментарий { get; set; }
	[ProtoMember(9)] public DateTime НачалоКассовойСмены { get; set; }
	[ProtoMember(10)] public bool НеобходимаСтрочнаяЗаменаФН { get; set; }
	[ProtoMember(11)] public double НомерСменыККТ { get; set; }
	[ProtoMember(12)] public DateTime ОкончаниеКассовойСмены { get; set; }
	[ProtoMember(13)] public Entity Организация { get; set; }
	[ProtoMember(14)] public bool ПамятьФНПереполнена { get; set; }
	[ProtoMember(15)] public bool ПревышеноВремяОжиданияОтветаОФД { get; set; }
	[ProtoMember(16)] public bool РесурсФНИсчерпан { get; set; }
	[ProtoMember(17)] public Entity Статус { get; set; }
	[ProtoMember(18)] public double СуммаВозвратов { get; set; }
	[ProtoMember(19)] public double СуммаВозвратовНДС { get; set; }
	[ProtoMember(20)] public double СуммаДокумента { get; set; }
	[ProtoMember(21)] public double СуммаНДС { get; set; }
	[ProtoMember(22)] public Entity ФДОЗакрытииСмены { get; set; }
	[ProtoMember(23)] public DateTime Дата { get; set; }
	[ProtoMember(24)] public string Номер { get; set; }
	[ProtoMember(25)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(26)] public bool Проведен { get; set; }
	[ProtoMember(27)] public Entity ОблачнаяККМ { get; set; }
}

[ProtoContract] public sealed class КомплектацияЗаказов
{
	[ProtoContract] public sealed class КомплектацияЗапись
	{
		[ProtoMember(1)] public Entity Заказ { get; set; }
		[ProtoMember(2)] public Entity ЗСЯ { get; set; }
		[ProtoMember(3)] public double Количество { get; set; }
		[ProtoMember(4)] public double КоличествоНеХватило { get; set; }
		[ProtoMember(5)] public Entity Партия { get; set; }
		[ProtoMember(6)] public Entity Товар { get; set; }
	}
	[ProtoContract] public sealed class ПрогрессКомплектацииЗапись
	{
		[ProtoMember(1)] public Entity Заказ { get; set; }
		[ProtoMember(2)] public Entity ЗСЯ { get; set; }
		[ProtoMember(3)] public double Количество { get; set; }
		[ProtoMember(4)] public double КоличествоНеХватило { get; set; }
		[ProtoMember(5)] public Entity Партия { get; set; }
		[ProtoMember(6)] public DateTime Период { get; set; }
		[ProtoMember(7)] public Entity Товар { get; set; }
	}
	[ProtoMember(1)] public DateTime ДатаНачалаКомплектации { get; set; }
	[ProtoMember(2)] public Entity ДокументОснование { get; set; }
	[ProtoMember(3)] public bool КомплектацияЗавершена { get; set; }
	[ProtoMember(4)] public Entity Склад { get; set; }
	[ProtoMember(5)] public bool СобранНаКассе { get; set; }
	[ProtoMember(6)] public Entity Сотрудник { get; set; }
	[ProtoMember(7)] public bool Холод { get; set; }
	[ProtoMember(8)] public DateTime Дата { get; set; }
	[ProtoMember(9)] public string Номер { get; set; }
	[ProtoMember(10)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(11)] public bool Проведен { get; set; }
	[ProtoMember(12)] public List<КомплектацияЗаказов.КомплектацияЗапись> Комплектация { get; set; } = new List<КомплектацияЗаказов.КомплектацияЗапись>();
	[ProtoMember(13)] public List<КомплектацияЗаказов.ПрогрессКомплектацииЗапись> ПрогрессКомплектации { get; set; } = new List<КомплектацияЗаказов.ПрогрессКомплектацииЗапись>();
}

[ProtoContract] public sealed class КорректировкаПределовНагрузкиПоЗонам
{
	[ProtoContract] public sealed class ИнтервалыЗапись
	{
		[ProtoMember(1)] public DateTime ДатаДоставки { get; set; }
		[ProtoMember(2)] public DateTime ДатаОперации { get; set; }
		[ProtoMember(3)] public double ДлинаИнтервала { get; set; }
		[ProtoMember(4)] public Entity ЗонаДоставки { get; set; }
		[ProtoMember(5)] public double КоличествоПлан { get; set; }
		[ProtoMember(6)] public double КоличествоФакт { get; set; }
		[ProtoMember(7)] public DateTime НачалоИнтервала { get; set; }
	}
	[ProtoMember(1)] public string Комментарий { get; set; }
	[ProtoMember(2)] public Entity МестоХранения { get; set; }
	[ProtoMember(3)] public Entity Ответственный { get; set; }
	[ProtoMember(4)] public Entity Подразделение { get; set; }
	[ProtoMember(5)] public DateTime Дата { get; set; }
	[ProtoMember(6)] public string Номер { get; set; }
	[ProtoMember(7)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(8)] public bool Проведен { get; set; }
	[ProtoMember(9)] public List<КорректировкаПределовНагрузкиПоЗонам.ИнтервалыЗапись> Интервалы { get; set; } = new List<КорректировкаПределовНагрузкиПоЗонам.ИнтервалыЗапись>();
}

[ProtoContract] public sealed class КорректировкаРегистров
{
	[ProtoContract] public sealed class ТаблицаРегистровЗапись
	{
		[ProtoMember(1)] public string Имя { get; set; }
	}
	[ProtoMember(1)] public string Комментарий { get; set; }
	[ProtoMember(2)] public bool НеРегистрироватьКОбмену { get; set; }
	[ProtoMember(3)] public Entity Ответственный { get; set; }
	[ProtoMember(4)] public DateTime Дата { get; set; }
	[ProtoMember(5)] public string Номер { get; set; }
	[ProtoMember(6)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(7)] public List<КорректировкаРегистров.ТаблицаРегистровЗапись> ТаблицаРегистров { get; set; } = new List<КорректировкаРегистров.ТаблицаРегистровЗапись>();
}

[ProtoContract] public sealed class Маршрут
{
	[ProtoContract] public sealed class ВозвратыЗапись
	{
		[ProtoMember(1)] public Entity АдресХранения { get; set; }
		[ProtoMember(2)] public Entity Заказ { get; set; }
	}
	[ProtoContract] public sealed class ЗаказыЗапись
	{
		[ProtoMember(1)] public double Всего { get; set; }
		[ProtoMember(2)] public double Скидка { get; set; }
		[ProtoMember(3)] public double Сумма { get; set; }
		[ProtoMember(4)] public Union ДокДоставка { get; set; }
	}
	[ProtoContract] public sealed class КоробкиСборкиЗапись
	{
		[ProtoMember(1)] public Entity АдресХранения { get; set; }
		[ProtoMember(2)] public Entity Коробка { get; set; }
	}
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public Entity АвторПМ { get; set; }
	[ProtoMember(3)] public Entity АдресХранения { get; set; }
	[ProtoMember(4)] public double ВесМаршрута { get; set; }
	[ProtoMember(5)] public string Волна { get; set; }
	[ProtoMember(6)] public DateTime ДатаДоставки { get; set; }
	[ProtoMember(7)] public DateTime ДатаОтработки { get; set; }
	[ProtoMember(8)] public double КоличествоДокДоставки { get; set; }
	[ProtoMember(9)] public Entity КонтрагентИП { get; set; }
	[ProtoMember(10)] public Entity Курьер { get; set; }
	[ProtoMember(11)] public double ОбъемМаршрута { get; set; }
	[ProtoMember(12)] public Entity РегионРаботы { get; set; }
	[ProtoMember(13)] public Entity СтатусМаршрута { get; set; }
	[ProtoMember(14)] public string СтрокаЗоны { get; set; }
	[ProtoMember(15)] public string СтрокаИнтервалыДоставки { get; set; }
	[ProtoMember(16)] public Entity ТипДокументаЗагрузки { get; set; }
	[ProtoMember(17)] public bool Холодильник { get; set; }
	[ProtoMember(18)] public DateTime Дата { get; set; }
	[ProtoMember(19)] public string Номер { get; set; }
	[ProtoMember(20)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(21)] public bool Проведен { get; set; }
	[ProtoMember(22)] public List<Маршрут.ВозвратыЗапись> Возвраты { get; set; } = new List<Маршрут.ВозвратыЗапись>();
	[ProtoMember(23)] public List<Маршрут.ЗаказыЗапись> Заказы { get; set; } = new List<Маршрут.ЗаказыЗапись>();
	[ProtoMember(24)] public List<Маршрут.КоробкиСборкиЗапись> КоробкиСборки { get; set; } = new List<Маршрут.КоробкиСборкиЗапись>();
}

[ProtoContract] public sealed class МаршрутНаАптеку
{
	[ProtoContract] public sealed class ВозвратыЗапись
	{
		[ProtoMember(1)] public Entity Заказ { get; set; }
		[ProtoMember(2)] public Entity МестоЗаказа { get; set; }
	}
	[ProtoContract] public sealed class КоробкиСборкиЗапись
	{
		[ProtoMember(1)] public Entity Коробка { get; set; }
	}
	[ProtoContract] public sealed class СоставЗапись
	{
		[ProtoMember(1)] public Entity Заказ { get; set; }
		[ProtoMember(2)] public Entity МестоЗаказа { get; set; }
	}
	[ProtoMember(1)] public Entity АдресХранения { get; set; }
	[ProtoMember(2)] public Union ДокументОснование { get; set; }
	[ProtoMember(3)] public string НомерДокументаОснования { get; set; }
	[ProtoMember(4)] public Entity ТипДокументаЗагрузки { get; set; }
	[ProtoMember(5)] public DateTime Дата { get; set; }
	[ProtoMember(6)] public string Номер { get; set; }
	[ProtoMember(7)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(8)] public bool Проведен { get; set; }
	[ProtoMember(9)] public List<МаршрутНаАптеку.ВозвратыЗапись> Возвраты { get; set; } = new List<МаршрутНаАптеку.ВозвратыЗапись>();
	[ProtoMember(10)] public List<МаршрутНаАптеку.КоробкиСборкиЗапись> КоробкиСборки { get; set; } = new List<МаршрутНаАптеку.КоробкиСборкиЗапись>();
	[ProtoMember(11)] public List<МаршрутНаАптеку.СоставЗапись> Состав { get; set; } = new List<МаршрутНаАптеку.СоставЗапись>();
}

[ProtoContract] public sealed class МаршрутныйЛист
{
	[ProtoContract] public sealed class ЗаказыЗапись
	{
		[ProtoMember(1)] public Entity ЗаказКлиента { get; set; }
		[ProtoMember(2)] public DateTime ПлановоеВремяПрибытия { get; set; }
		[ProtoMember(3)] public double Порядок { get; set; }
		[ProtoMember(4)] public Entity ПунктСамовывоза { get; set; }
	}
	[ProtoContract] public sealed class ВыездыЗапись
	{
		[ProtoMember(1)] public Entity Выезд { get; set; }
		[ProtoMember(2)] public DateTime ПлановоеВремяПрибытия { get; set; }
		[ProtoMember(3)] public double Порядок { get; set; }
		[ProtoMember(4)] public Entity ПунктСамовывоза { get; set; }
	}
	[ProtoMember(1)] public Entity АдресХранения { get; set; }
	[ProtoMember(2)] public DateTime ВремяВыхода { get; set; }
	[ProtoMember(3)] public DateTime ДатаДоставки { get; set; }
	[ProtoMember(4)] public string Комментарий { get; set; }
	[ProtoMember(5)] public Union Курьер { get; set; }
	[ProtoMember(6)] public DateTime Дата { get; set; }
	[ProtoMember(7)] public string Номер { get; set; }
	[ProtoMember(8)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(9)] public bool Проведен { get; set; }
	[ProtoMember(10)] public double КоличествоДокументов { get; set; }
	[ProtoMember(11)] public bool МаксимальныйПриоритет { get; set; }
	[ProtoMember(12)] public Entity Ответственный { get; set; }
	[ProtoMember(13)] public double ПлановоеВремя { get; set; }
	[ProtoMember(14)] public double ПлановыйПробег { get; set; }
	[ProtoMember(15)] public DateTime ВремяСтарта { get; set; }
	[ProtoMember(16)] public bool ДоставкаПеревозчиком { get; set; }
	[ProtoMember(17)] public string ТелефонКурьераПеревозчика { get; set; }
	[ProtoMember(18)] public string ФИОКурьераПеревозчика { get; set; }
	[ProtoMember(19)] public bool ВозвратНаСклад { get; set; }
	[ProtoMember(20)] public List<МаршрутныйЛист.ЗаказыЗапись> Заказы { get; set; } = new List<МаршрутныйЛист.ЗаказыЗапись>();
	[ProtoMember(21)] public List<МаршрутныйЛист.ВыездыЗапись> Выезды { get; set; } = new List<МаршрутныйЛист.ВыездыЗапись>();
}

[ProtoContract] public sealed class НачисленияКурьерам
{
	[ProtoContract] public sealed class НачисленияЗапись
	{
		[ProtoMember(1)] public Union ДокументНачисления { get; set; }
		[ProtoMember(2)] public double Сумма { get; set; }
	}
	[ProtoMember(1)] public string Комментарий { get; set; }
	[ProtoMember(2)] public Entity Курьер { get; set; }
	[ProtoMember(3)] public Entity МестоХранения { get; set; }
	[ProtoMember(4)] public Entity Ответственный { get; set; }
	[ProtoMember(5)] public DateTime ПериодРасчета { get; set; }
	[ProtoMember(6)] public DateTime ПериодРасчетаПо { get; set; }
	[ProtoMember(7)] public DateTime ПериодРасчетаС { get; set; }
	[ProtoMember(8)] public Entity Подразделение { get; set; }
	[ProtoMember(9)] public DateTime Дата { get; set; }
	[ProtoMember(10)] public string Номер { get; set; }
	[ProtoMember(11)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(12)] public bool Проведен { get; set; }
	[ProtoMember(13)] public List<НачисленияКурьерам.НачисленияЗапись> Начисления { get; set; } = new List<НачисленияКурьерам.НачисленияЗапись>();
}

[ProtoContract] public sealed class ОприходованиеПоИнвентаризации
{
	[ProtoContract] public sealed class ТоварыЗапись
	{
		[ProtoMember(1)] public DateTime ГоденДо { get; set; }
		[ProtoMember(2)] public double Количество { get; set; }
		[ProtoMember(3)] public Entity Партия { get; set; }
		[ProtoMember(4)] public double РеестроваяЦена { get; set; }
		[ProtoMember(5)] public string Серия { get; set; }
		[ProtoMember(6)] public Entity СтавкаНДС { get; set; }
		[ProtoMember(7)] public Entity Товар { get; set; }
		[ProtoMember(8)] public double Цена { get; set; }
		[ProtoMember(9)] public double ЦенаПроизводителя { get; set; }
		[ProtoMember(10)] public double Сумма { get; set; }
		[ProtoMember(11)] public double СуммаБезНДС { get; set; }
		[ProtoMember(12)] public double ЦенаБезНДС { get; set; }
	}
	[ProtoMember(1)] public Entity ДокументОснование { get; set; }
	[ProtoMember(2)] public string Комментарий { get; set; }
	[ProtoMember(3)] public Entity Склад { get; set; }
	[ProtoMember(4)] public Entity Статус { get; set; }
	[ProtoMember(5)] public DateTime Дата { get; set; }
	[ProtoMember(6)] public string Номер { get; set; }
	[ProtoMember(7)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(8)] public bool Проведен { get; set; }
	[ProtoMember(9)] public List<ОприходованиеПоИнвентаризации.ТоварыЗапись> Товары { get; set; } = new List<ОприходованиеПоИнвентаризации.ТоварыЗапись>();
}

[ProtoContract] public sealed class ОтчетОДоставке
{
	[ProtoContract] public sealed class ВложенияЗапись
	{
		[ProtoMember(1)] public Entity ВидОперации { get; set; }
		[ProtoMember(2)] public double Количество { get; set; }
		[ProtoMember(3)] public double СуммаВложения { get; set; }
		[ProtoMember(4)] public Entity Товар { get; set; }
		[ProtoMember(5)] public double ЦенаВложения { get; set; }
	}
	[ProtoContract] public sealed class НачисленияЗапись
	{
		[ProtoMember(1)] public double Сумма { get; set; }
	}
	[ProtoMember(1)] public Entity ВидОперации { get; set; }
	[ProtoMember(2)] public Entity ЗаданиеНаДоставку { get; set; }
	[ProtoMember(3)] public Entity ЗаказКлиента { get; set; }
	[ProtoMember(4)] public string Комментарий { get; set; }
	[ProtoMember(5)] public Entity Курьер { get; set; }
	[ProtoMember(6)] public Entity МестоХранения { get; set; }
	[ProtoMember(7)] public Entity Ответственный { get; set; }
	[ProtoMember(8)] public Entity Подразделение { get; set; }
	[ProtoMember(9)] public string ПричинаОтказа { get; set; }
	[ProtoMember(10)] public double СуммаНачисленийКурьеру { get; set; }
	[ProtoMember(11)] public double СуммаПолученоОтПокупателя { get; set; }
	[ProtoMember(12)] public double СуммаСтоимостьЗаказа { get; set; }
	[ProtoMember(13)] public double ФактДолгота { get; set; }
	[ProtoMember(14)] public double ФактШирота { get; set; }
	[ProtoMember(15)] public DateTime Дата { get; set; }
	[ProtoMember(16)] public string Номер { get; set; }
	[ProtoMember(17)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(18)] public bool Проведен { get; set; }
	[ProtoMember(19)] public Entity ПричинаВозврата { get; set; }
	[ProtoMember(20)] public List<ОтчетОДоставке.ВложенияЗапись> Вложения { get; set; } = new List<ОтчетОДоставке.ВложенияЗапись>();
	[ProtoMember(21)] public List<ОтчетОДоставке.НачисленияЗапись> Начисления { get; set; } = new List<ОтчетОДоставке.НачисленияЗапись>();
}

[ProtoContract] public sealed class ОтчетОДоставкеЗаказаКлиента
{
	[ProtoMember(1)] public Entity АдресХранения { get; set; }
	[ProtoMember(2)] public Entity ВидОперации { get; set; }
	[ProtoMember(3)] public Union ЗаказКлиента { get; set; }
	[ProtoMember(4)] public string Комментарий { get; set; }
	[ProtoMember(5)] public Entity Курьер { get; set; }
	[ProtoMember(6)] public Entity МаршрутныйЛист { get; set; }
	[ProtoMember(7)] public double Сумма { get; set; }
	[ProtoMember(8)] public DateTime Дата { get; set; }
	[ProtoMember(9)] public string Номер { get; set; }
	[ProtoMember(10)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(11)] public bool Проведен { get; set; }
	[ProtoMember(12)] public Entity ПричинаВозврата { get; set; }
	[ProtoMember(13)] public double ФактДолгота { get; set; }
	[ProtoMember(14)] public double ФактШирота { get; set; }
	[ProtoMember(15)] public Entity Ответственный { get; set; }
	[ProtoMember(16)] public Entity ФормаОплаты { get; set; }
}

[ProtoContract] public sealed class ОтчетОДоставкеПеревозчика
{
	[ProtoContract] public sealed class ЗаказыЗапись
	{
		[ProtoMember(1)] public Entity ВидОперации { get; set; }
		[ProtoMember(2)] public Entity ЗаказКлиента { get; set; }
		[ProtoMember(3)] public double Сумма { get; set; }
	}
	[ProtoMember(1)] public Entity АдресХранения { get; set; }
	[ProtoMember(2)] public Entity Договор { get; set; }
	[ProtoMember(3)] public string Комментарий { get; set; }
	[ProtoMember(4)] public DateTime НачалоПериода { get; set; }
	[ProtoMember(5)] public DateTime ОкончаниеПериода { get; set; }
	[ProtoMember(6)] public Entity Организация { get; set; }
	[ProtoMember(7)] public Entity Перевозчик { get; set; }
	[ProtoMember(8)] public double СуммаДокумента { get; set; }
	[ProtoMember(9)] public DateTime Дата { get; set; }
	[ProtoMember(10)] public string Номер { get; set; }
	[ProtoMember(11)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(12)] public bool Проведен { get; set; }
	[ProtoMember(13)] public List<ОтчетОДоставкеПеревозчика.ЗаказыЗапись> Заказы { get; set; } = new List<ОтчетОДоставкеПеревозчика.ЗаказыЗапись>();
}

[ProtoContract] public sealed class ОтчетОРозничныхПродажах
{
	[ProtoContract] public sealed class ВозвратыЗапись
	{
		[ProtoMember(1)] public double Всего { get; set; }
		[ProtoMember(2)] public double Дисконт { get; set; }
		[ProtoMember(3)] public Union Документ { get; set; }
		[ProtoMember(4)] public double Количество { get; set; }
		[ProtoMember(5)] public bool ОплатаПоКарте { get; set; }
		[ProtoMember(6)] public Entity Отдел { get; set; }
		[ProtoMember(7)] public Entity Партия { get; set; }
		[ProtoMember(8)] public double Себестоимость { get; set; }
		[ProtoMember(9)] public double Скидка { get; set; }
		[ProtoMember(10)] public Entity СтавкаНДС { get; set; }
		[ProtoMember(11)] public double Сумма { get; set; }
		[ProtoMember(12)] public double СуммаНДС { get; set; }
		[ProtoMember(13)] public Entity ТипЦены { get; set; }
		[ProtoMember(14)] public Entity Товар { get; set; }
		[ProtoMember(15)] public double Цена { get; set; }
	}
	[ProtoContract] public sealed class РеализацияЗапись
	{
		[ProtoMember(1)] public double Всего { get; set; }
		[ProtoMember(2)] public double Дисконт { get; set; }
		[ProtoMember(3)] public Union Документ { get; set; }
		[ProtoMember(4)] public double Количество { get; set; }
		[ProtoMember(5)] public bool ОплатаПоКарте { get; set; }
		[ProtoMember(6)] public Entity Отдел { get; set; }
		[ProtoMember(7)] public Entity Партия { get; set; }
		[ProtoMember(8)] public double Себестоимость { get; set; }
		[ProtoMember(9)] public double Скидка { get; set; }
		[ProtoMember(10)] public Entity СтавкаНДС { get; set; }
		[ProtoMember(11)] public double Сумма { get; set; }
		[ProtoMember(12)] public double СуммаНДС { get; set; }
		[ProtoMember(13)] public Entity Товар { get; set; }
		[ProtoMember(14)] public double Цена { get; set; }
	}
	[ProtoContract] public sealed class СуммыРеализацииПоКурьерамЗапись
	{
		[ProtoMember(1)] public Entity Курьер { get; set; }
		[ProtoMember(2)] public double Сумма { get; set; }
	}
	[ProtoMember(1)] public Entity КассоваяСмена { get; set; }
	[ProtoMember(2)] public Entity ККМ { get; set; }
	[ProtoMember(3)] public Entity Организация { get; set; }
	[ProtoMember(4)] public Entity Ответственный { get; set; }
	[ProtoMember(5)] public Entity Отдел { get; set; }
	[ProtoMember(6)] public double СуммаВозвратов { get; set; }
	[ProtoMember(7)] public double СуммаДокумента { get; set; }
	[ProtoMember(8)] public DateTime Дата { get; set; }
	[ProtoMember(9)] public string Номер { get; set; }
	[ProtoMember(10)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(11)] public bool Проведен { get; set; }
	[ProtoMember(12)] public List<ОтчетОРозничныхПродажах.ВозвратыЗапись> Возвраты { get; set; } = new List<ОтчетОРозничныхПродажах.ВозвратыЗапись>();
	[ProtoMember(13)] public List<ОтчетОРозничныхПродажах.РеализацияЗапись> Реализация { get; set; } = new List<ОтчетОРозничныхПродажах.РеализацияЗапись>();
	[ProtoMember(14)] public List<ОтчетОРозничныхПродажах.СуммыРеализацииПоКурьерамЗапись> СуммыРеализацииПоКурьерам { get; set; } = new List<ОтчетОРозничныхПродажах.СуммыРеализацииПоКурьерамЗапись>();
}

[ProtoContract] public sealed class ПереброскаТоваров
{
	[ProtoContract] public sealed class ТоварыЗапись
	{
		[ProtoMember(1)] public double Количество { get; set; }
		[ProtoMember(2)] public Entity ПартияИсточник { get; set; }
		[ProtoMember(3)] public Entity ПартияПриемник { get; set; }
		[ProtoMember(4)] public Entity ТоварИсточник { get; set; }
		[ProtoMember(5)] public Entity ТоварПриемник { get; set; }
	}
	[ProtoMember(1)] public string Комментарий { get; set; }
	[ProtoMember(2)] public Entity Склад { get; set; }
	[ProtoMember(3)] public DateTime Дата { get; set; }
	[ProtoMember(4)] public string Номер { get; set; }
	[ProtoMember(5)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(6)] public bool Проведен { get; set; }
	[ProtoMember(7)] public List<ПереброскаТоваров.ТоварыЗапись> Товары { get; set; } = new List<ПереброскаТоваров.ТоварыЗапись>();
}

[ProtoContract] public sealed class ПередачаЗаказовПеревозчику
{
	[ProtoContract] public sealed class ЗаказыЗапись
	{
		[ProtoMember(1)] public Entity ЗаказКлиента { get; set; }
		[ProtoMember(2)] public double Сумма { get; set; }
	}
	[ProtoMember(1)] public Entity АдресХранения { get; set; }
	[ProtoMember(2)] public Entity Договор { get; set; }
	[ProtoMember(3)] public Entity ДокументОснование { get; set; }
	[ProtoMember(4)] public string Комментарий { get; set; }
	[ProtoMember(5)] public Entity Организация { get; set; }
	[ProtoMember(6)] public Entity Перевозчик { get; set; }
	[ProtoMember(7)] public double СуммаДокумента { get; set; }
	[ProtoMember(8)] public string ТелефонКурьераПеревозчика { get; set; }
	[ProtoMember(9)] public string ФИОКурьераПеревозчика { get; set; }
	[ProtoMember(10)] public DateTime Дата { get; set; }
	[ProtoMember(11)] public string Номер { get; set; }
	[ProtoMember(12)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(13)] public bool Проведен { get; set; }
	[ProtoMember(14)] public List<ПередачаЗаказовПеревозчику.ЗаказыЗапись> Заказы { get; set; } = new List<ПередачаЗаказовПеревозчику.ЗаказыЗапись>();
}

[ProtoContract] public sealed class Перемещение
{
	[ProtoContract] public sealed class ТоварыЗапись
	{
		[ProtoMember(1)] public double Брак { get; set; }
		[ProtoMember(2)] public bool БракованнаяСерия { get; set; }
		[ProtoMember(3)] public double Количество { get; set; }
		[ProtoMember(4)] public double КоличествоПлан { get; set; }
		[ProtoMember(5)] public double Лишний { get; set; }
		[ProtoMember(6)] public double Недовоз { get; set; }
		[ProtoMember(7)] public Entity Партия { get; set; }
		[ProtoMember(8)] public double Перевоз { get; set; }
		[ProtoMember(9)] public DateTime СрокГодности { get; set; }
		[ProtoMember(10)] public double Сумма { get; set; }
		[ProtoMember(11)] public Entity Товар { get; set; }
		[ProtoMember(12)] public double Цена { get; set; }
		[ProtoMember(13)] public Entity Претензия { get; set; }
	}
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public Entity АвторПечати { get; set; }
	[ProtoMember(3)] public bool БезУчетаДефектуры { get; set; }
	[ProtoMember(4)] public bool ВведенАвтоматически { get; set; }
	[ProtoMember(5)] public bool ВводЗавершен { get; set; }
	[ProtoMember(6)] public double Вес { get; set; }
	[ProtoMember(7)] public string ВремяПечати { get; set; }
	[ProtoMember(8)] public Union ДокументОснование { get; set; }
	[ProtoMember(9)] public string Комментарий { get; set; }
	[ProtoMember(10)] public double МестПростых { get; set; }
	[ProtoMember(11)] public double МестХолодных { get; set; }
	[ProtoMember(12)] public bool ОрдернаяСхема { get; set; }
	[ProtoMember(13)] public Entity Ответственный { get; set; }
	[ProtoMember(14)] public bool ПеремещениеВРобота { get; set; }
	[ProtoMember(15)] public Entity Получатель { get; set; }
	[ProtoMember(16)] public Entity Поставщик { get; set; }
	[ProtoMember(17)] public bool РазрешенаПриемка { get; set; }
	[ProtoMember(18)] public bool РазрешитьПродажу { get; set; }
	[ProtoMember(19)] public bool СборкаЗавершена { get; set; }
	[ProtoMember(20)] public bool СборкаНаТСД { get; set; }
	[ProtoMember(21)] public DateTime Дата { get; set; }
	[ProtoMember(22)] public string Номер { get; set; }
	[ProtoMember(23)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(24)] public bool Проведен { get; set; }
	[ProtoMember(25)] public Entity Статус { get; set; }
	[ProtoMember(26)] public double СуммаДокумента { get; set; }
	[ProtoMember(27)] public bool Служебное { get; set; }
	[ProtoMember(28)] public bool ВГО { get; set; }
	[ProtoMember(29)] public bool СозданыРТУПТУ { get; set; }
	[ProtoMember(30)] public List<Перемещение.ТоварыЗапись> Товары { get; set; } = new List<Перемещение.ТоварыЗапись>();
}

[ProtoContract] public sealed class ПеремещениеЗаказовКлиентов
{
	[ProtoContract] public sealed class СканированиеЯчеекЗапись
	{
		[ProtoMember(1)] public bool СканированиеЗавершено { get; set; }
		[ProtoMember(2)] public Entity Ячейка { get; set; }
	}
	[ProtoContract] public sealed class ТоварыЗапись
	{
		[ProtoMember(1)] public Entity ВариантРезервирования { get; set; }
		[ProtoMember(2)] public Entity ЗаказКлиента { get; set; }
		[ProtoMember(3)] public double Количество { get; set; }
		[ProtoMember(4)] public double КоличествоКСканированию { get; set; }
		[ProtoMember(5)] public Entity Номенклатура { get; set; }
		[ProtoMember(6)] public Entity Партия { get; set; }
		[ProtoMember(7)] public Entity Склад { get; set; }
		[ProtoMember(8)] public Entity УпаковкаЗаказаКлиента { get; set; }
		[ProtoMember(9)] public Entity Ячейка { get; set; }
		[ProtoMember(10)] public string ШтрихкодУпаковки { get; set; }
	}
	[ProtoContract] public sealed class УпаковкиЗапись
	{
		[ProtoMember(1)] public bool Отсканировано { get; set; }
		[ProtoMember(2)] public Entity УпаковкаЗаказаКлиента { get; set; }
		[ProtoMember(3)] public Entity ЯчейкаПолучатель { get; set; }
		[ProtoMember(4)] public string ШтрихкодУпаковки { get; set; }
	}
	[ProtoMember(1)] public Entity АдресХранения { get; set; }
	[ProtoMember(2)] public Entity ВидОперации { get; set; }
	[ProtoMember(3)] public DateTime Дата { get; set; }
	[ProtoMember(4)] public string Номер { get; set; }
	[ProtoMember(5)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(6)] public bool Проведен { get; set; }
	[ProtoMember(7)] public List<ПеремещениеЗаказовКлиентов.СканированиеЯчеекЗапись> СканированиеЯчеек { get; set; } = new List<ПеремещениеЗаказовКлиентов.СканированиеЯчеекЗапись>();
	[ProtoMember(8)] public List<ПеремещениеЗаказовКлиентов.ТоварыЗапись> Товары { get; set; } = new List<ПеремещениеЗаказовКлиентов.ТоварыЗапись>();
	[ProtoMember(9)] public List<ПеремещениеЗаказовКлиентов.УпаковкиЗапись> Упаковки { get; set; } = new List<ПеремещениеЗаказовКлиентов.УпаковкиЗапись>();
}

[ProtoContract] public sealed class ПересортПоПартиям
{
	[ProtoContract] public sealed class ТоварыЗапись
	{
		[ProtoMember(1)] public double Количество { get; set; }
		[ProtoMember(2)] public Entity Номенклатура { get; set; }
		[ProtoMember(3)] public Entity ПартияИсточник { get; set; }
		[ProtoMember(4)] public Entity ПартияПолучатель { get; set; }
	}
	[ProtoMember(1)] public Entity Автор { get; set; }
	[ProtoMember(2)] public Entity Аптека { get; set; }
	[ProtoMember(3)] public string Комментарий { get; set; }
	[ProtoMember(4)] public Entity Склад { get; set; }
	[ProtoMember(5)] public DateTime Дата { get; set; }
	[ProtoMember(6)] public string Номер { get; set; }
	[ProtoMember(7)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(8)] public bool Проведен { get; set; }
	[ProtoMember(9)] public List<ПересортПоПартиям.ТоварыЗапись> Товары { get; set; } = new List<ПересортПоПартиям.ТоварыЗапись>();
}

[ProtoContract] public sealed class Претензия
{
	[ProtoContract] public sealed class ТоварыЗапись
	{
		[ProtoMember(1)] public double Количество { get; set; }
		[ProtoMember(2)] public string НомерИДатаСчетФактуры { get; set; }
		[ProtoMember(3)] public Entity Партия { get; set; }
		[ProtoMember(4)] public Union Поставщик { get; set; }
		[ProtoMember(5)] public string Причина { get; set; }
		[ProtoMember(6)] public double Сумма { get; set; }
		[ProtoMember(7)] public Entity Товар { get; set; }
		[ProtoMember(8)] public double Цена { get; set; }
		[ProtoMember(9)] public double ИдентификаторСтроки { get; set; }
		[ProtoMember(10)] public double КоличествоПоНакладной { get; set; }
		[ProtoMember(11)] public Entity Претензия { get; set; }
	}
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public Union ДокументОснование { get; set; }
	[ProtoMember(3)] public Entity Отдел { get; set; }
	[ProtoMember(4)] public bool Отправлена { get; set; }
	[ProtoMember(5)] public DateTime Дата { get; set; }
	[ProtoMember(6)] public string Номер { get; set; }
	[ProtoMember(7)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(8)] public bool Проведен { get; set; }
	[ProtoMember(9)] public string Комментарий { get; set; }
	[ProtoMember(10)] public DateTime ДатаПереводаВРаботу { get; set; }
	[ProtoMember(11)] public Entity Ответственный { get; set; }
	[ProtoMember(12)] public List<Претензия.ТоварыЗапись> Товары { get; set; } = new List<Претензия.ТоварыЗапись>();
}

[ProtoContract] public sealed class ПриемкаМаршрута
{
	[ProtoContract] public sealed class СоставЗапись
	{
		[ProtoMember(1)] public Entity Заказ { get; set; }
		[ProtoMember(2)] public Entity МестоЗаказа { get; set; }
	}
	[ProtoMember(1)] public Entity ДокументОснование { get; set; }
	[ProtoMember(2)] public Entity Ответственный { get; set; }
	[ProtoMember(3)] public DateTime Дата { get; set; }
	[ProtoMember(4)] public string Номер { get; set; }
	[ProtoMember(5)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(6)] public bool Проведен { get; set; }
	[ProtoMember(7)] public List<ПриемкаМаршрута.СоставЗапись> Состав { get; set; } = new List<ПриемкаМаршрута.СоставЗапись>();
}

[ProtoContract] public sealed class ПриемныйОрдер
{
	[ProtoContract] public sealed class ПринятыйТоварЗапись
	{
		[ProtoMember(1)] public double Количество { get; set; }
		[ProtoMember(2)] public Entity Партия { get; set; }
		[ProtoMember(3)] public Entity Товар { get; set; }
		[ProtoMember(4)] public string ИдентификаторМДЛП { get; set; }
		[ProtoMember(5)] public double ИдентификаторСтрокиНакладной { get; set; }
		[ProtoMember(6)] public bool ПодтвержденоПоставщикомМДЛП { get; set; }
	}
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public Union ДокументОснование { get; set; }
	[ProtoMember(3)] public Entity Отдел { get; set; }
	[ProtoMember(4)] public bool РазрешитьПродажу { get; set; }
	[ProtoMember(5)] public double Статус { get; set; }
	[ProtoMember(6)] public DateTime Дата { get; set; }
	[ProtoMember(7)] public string Номер { get; set; }
	[ProtoMember(8)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(9)] public bool Проведен { get; set; }
	[ProtoMember(10)] public string ИдентификаторWMS { get; set; }
	[ProtoMember(11)] public string НомерWMS { get; set; }
	[ProtoMember(12)] public List<ПриемныйОрдер.ПринятыйТоварЗапись> ПринятыйТовар { get; set; } = new List<ПриемныйОрдер.ПринятыйТоварЗапись>();
}

[ProtoContract] public sealed class Приходная
{
	[ProtoContract] public sealed class ТоварыЗапись
	{
		[ProtoMember(1)] public double Брак { get; set; }
		[ProtoMember(2)] public bool БракованнаяСерия { get; set; }
		[ProtoMember(3)] public string Выдан { get; set; }
		[ProtoMember(4)] public DateTime ГоденДо { get; set; }
		[ProtoMember(5)] public DateTime ДатаРеализацииПроизводителем { get; set; }
		[ProtoMember(6)] public bool ДеленнаяПозиция { get; set; }
		[ProtoMember(7)] public double Имп { get; set; }
		[ProtoMember(8)] public string КлючСвязиПартия { get; set; }
		[ProtoMember(9)] public double КодСтроки { get; set; }
		[ProtoMember(10)] public double Количество { get; set; }
		[ProtoMember(11)] public double Лишний { get; set; }
		[ProtoMember(12)] public double НаценкаПоставщика { get; set; }
		[ProtoMember(13)] public double НаценкаПроизводителя { get; set; }
		[ProtoMember(14)] public double НаценкаРеестра { get; set; }
		[ProtoMember(15)] public double Недовоз { get; set; }
		[ProtoMember(16)] public Entity НомерГТД { get; set; }
		[ProtoMember(17)] public double НомерЗаказаПоставщику { get; set; }
		[ProtoMember(18)] public Entity Партия { get; set; }
		[ProtoMember(19)] public double Перевоз { get; set; }
		[ProtoMember(20)] public Entity Производитель { get; set; }
		[ProtoMember(21)] public double РеестроваяЦена { get; set; }
		[ProtoMember(22)] public string Серия { get; set; }
		[ProtoMember(23)] public string Сертификат { get; set; }
		[ProtoMember(24)] public DateTime СертификатДо { get; set; }
		[ProtoMember(25)] public Entity Склад { get; set; }
		[ProtoMember(26)] public Entity СтавкаНДС { get; set; }
		[ProtoMember(27)] public double Сумма { get; set; }
		[ProtoMember(28)] public double СуммаНДС { get; set; }
		[ProtoMember(29)] public Entity Товар { get; set; }
		[ProtoMember(30)] public double Цена { get; set; }
		[ProtoMember(31)] public double ЦенаПроизводителя { get; set; }
		[ProtoMember(32)] public string ШК_Ovh_list { get; set; }
		[ProtoMember(33)] public bool МДЛП { get; set; }
		[ProtoMember(34)] public string КодТовараПоставщика { get; set; }
		[ProtoMember(35)] public string НаименованиеТовараПоставщика { get; set; }
		[ProtoMember(36)] public string ПроизводительТовараПоставщика { get; set; }
		[ProtoMember(37)] public DateTime ДатаПроизводства { get; set; }
		[ProtoMember(38)] public double ЦенаБезНДС { get; set; }
		[ProtoMember(39)] public Entity ПартияПретензии { get; set; }
		[ProtoMember(40)] public Entity Претензия { get; set; }
	}
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public Entity АвторПускаВПродажу { get; set; }
	[ProtoMember(3)] public bool БезЗаказа { get; set; }
	[ProtoMember(4)] public bool ВведенАвтоматическиПоПретензии { get; set; }
	[ProtoMember(5)] public string ВремяПускаВПродажу { get; set; }
	[ProtoMember(6)] public bool ВычНП { get; set; }
	[ProtoMember(7)] public DateTime ДатаЗаказа { get; set; }
	[ProtoMember(8)] public DateTime ДатаОплаты { get; set; }
	[ProtoMember(9)] public DateTime ДатаОснования { get; set; }
	[ProtoMember(10)] public DateTime ДатаСФ { get; set; }
	[ProtoMember(11)] public Union ДокументОснование { get; set; }
	[ProtoMember(12)] public Entity Клиент { get; set; }
	[ProtoMember(13)] public double НДСПоставщика { get; set; }
	[ProtoMember(14)] public string НомерОснования { get; set; }
	[ProtoMember(15)] public string НомерСЧФ { get; set; }
	[ProtoMember(16)] public bool Оплачено { get; set; }
	[ProtoMember(17)] public double ОрдернаяСхема { get; set; }
	[ProtoMember(18)] public Entity Отдел { get; set; }
	[ProtoMember(19)] public bool ПриемПодЗаказ { get; set; }
	[ProtoMember(20)] public double ПроцентПодЗаказ { get; set; }
	[ProtoMember(21)] public bool РазрешенаПриемка { get; set; }
	[ProtoMember(22)] public bool РасчетОтСуммы { get; set; }
	[ProtoMember(23)] public Entity СкладТранзит { get; set; }
	[ProtoMember(24)] public Entity СтатусПриходнойНакладной { get; set; }
	[ProtoMember(25)] public double Сумма0 { get; set; }
	[ProtoMember(26)] public double Сумма10 { get; set; }
	[ProtoMember(27)] public double Сумма20 { get; set; }
	[ProtoMember(28)] public bool СуммаВключаетНДС { get; set; }
	[ProtoMember(29)] public double СуммаНДС10 { get; set; }
	[ProtoMember(30)] public double СуммаНДС20 { get; set; }
	[ProtoMember(31)] public double СуммаПоставщика { get; set; }
	[ProtoMember(32)] public Entity ТипНакладной { get; set; }
	[ProtoMember(33)] public Entity ТипПрихода { get; set; }
	[ProtoMember(34)] public bool Упрощенка { get; set; }
	[ProtoMember(35)] public Entity Фирма { get; set; }
	[ProtoMember(36)] public DateTime Дата { get; set; }
	[ProtoMember(37)] public string Номер { get; set; }
	[ProtoMember(38)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(39)] public bool Проведен { get; set; }
	[ProtoMember(40)] public string КодСкладаОтправителяМДЛП { get; set; }
	[ProtoMember(41)] public double СуммаДокумента { get; set; }
	[ProtoMember(42)] public Entity Договор { get; set; }
	[ProtoMember(43)] public string Комментарий { get; set; }
	[ProtoMember(44)] public List<Приходная.ТоварыЗапись> Товары { get; set; } = new List<Приходная.ТоварыЗапись>();
}

[ProtoContract] public sealed class ПриходныйКассовыйОрдер
{
	[ProtoContract] public sealed class РасшифровкаПлатежаЗапись
	{
		[ProtoMember(1)] public Union ДокументРасчетовСКонтрагентом { get; set; }
		[ProtoMember(2)] public double Сумма { get; set; }
		[ProtoMember(3)] public Entity ФирмаДокументаРасчетов { get; set; }
	}
	[ProtoMember(1)] public Entity Аптека { get; set; }
	[ProtoMember(2)] public Entity ВидОперации { get; set; }
	[ProtoMember(3)] public bool Выгружается { get; set; }
	[ProtoMember(4)] public Union ДокументОснование { get; set; }
	[ProtoMember(5)] public Entity КассаККМ { get; set; }
	[ProtoMember(6)] public Entity Кассир { get; set; }
	[ProtoMember(7)] public string Комментарий { get; set; }
	[ProtoMember(8)] public string Основание { get; set; }
	[ProtoMember(9)] public Entity Ответственный { get; set; }
	[ProtoMember(10)] public Entity ОтчетОРозничныхПродажах { get; set; }
	[ProtoMember(11)] public string Приложение { get; set; }
	[ProtoMember(12)] public string ПринятоОт { get; set; }
	[ProtoMember(13)] public Entity Сотрудник { get; set; }
	[ProtoMember(14)] public double СуммаДокумента { get; set; }
	[ProtoMember(15)] public Entity Фирма { get; set; }
	[ProtoMember(16)] public DateTime Дата { get; set; }
	[ProtoMember(17)] public string Номер { get; set; }
	[ProtoMember(18)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(19)] public bool Проведен { get; set; }
	[ProtoMember(20)] public Entity Касса { get; set; }
	[ProtoMember(21)] public List<ПриходныйКассовыйОрдер.РасшифровкаПлатежаЗапись> РасшифровкаПлатежа { get; set; } = new List<ПриходныйКассовыйОрдер.РасшифровкаПлатежаЗапись>();
}

[ProtoContract] public sealed class РасформированиеРезерва
{
	[ProtoContract] public sealed class СнятиеРезерваЗапись
	{
		[ProtoMember(1)] public double ВыполняемоеДействие { get; set; }
		[ProtoMember(2)] public Union Документ { get; set; }
		[ProtoMember(3)] public double Количество { get; set; }
		[ProtoMember(4)] public Entity Отдел { get; set; }
		[ProtoMember(5)] public Entity Партия { get; set; }
		[ProtoMember(6)] public Entity Товар { get; set; }
		[ProtoMember(7)] public bool Флаг { get; set; }
	}
	[ProtoMember(1)] public Entity АдресХранения { get; set; }
	[ProtoMember(2)] public DateTime ДатаНачала { get; set; }
	[ProtoMember(3)] public DateTime ДатаОкончания { get; set; }
	[ProtoMember(4)] public DateTime Дата { get; set; }
	[ProtoMember(5)] public string Номер { get; set; }
	[ProtoMember(6)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(7)] public bool Проведен { get; set; }
	[ProtoMember(8)] public List<РасформированиеРезерва.СнятиеРезерваЗапись> СнятиеРезерва { get; set; } = new List<РасформированиеРезерва.СнятиеРезерваЗапись>();
}

[ProtoContract] public sealed class Расходная
{
	[ProtoContract] public sealed class ВозвратСамовывозовЗапись
	{
		[ProtoMember(1)] public double Количество { get; set; }
		[ProtoMember(2)] public Entity Отдел { get; set; }
		[ProtoMember(3)] public Entity Партия { get; set; }
		[ProtoMember(4)] public bool СписыватьОстатки { get; set; }
		[ProtoMember(5)] public Entity Товар { get; set; }
	}
	[ProtoContract] public sealed class ТоварыЗапись
	{
		[ProtoMember(1)] public double ИдентификаторСтроки { get; set; }
		[ProtoMember(2)] public double Количество { get; set; }
		[ProtoMember(3)] public Entity Партия { get; set; }
		[ProtoMember(4)] public Entity Претензия { get; set; }
		[ProtoMember(5)] public double РозничнаяСумма { get; set; }
		[ProtoMember(6)] public double РозничнаяЦена { get; set; }
		[ProtoMember(7)] public double Сумма { get; set; }
		[ProtoMember(8)] public double СуммаНДС { get; set; }
		[ProtoMember(9)] public double СуммаНП { get; set; }
		[ProtoMember(10)] public Entity Товар { get; set; }
		[ProtoMember(11)] public double Цена { get; set; }
	}
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public bool БезУчетаДефектуры { get; set; }
	[ProtoMember(3)] public bool ВведенАвтоматически { get; set; }
	[ProtoMember(4)] public bool Выгружен { get; set; }
	[ProtoMember(5)] public bool ВычНП { get; set; }
	[ProtoMember(6)] public DateTime ДатаОснования { get; set; }
	[ProtoMember(7)] public Entity Клиент { get; set; }
	[ProtoMember(8)] public bool Недовоз { get; set; }
	[ProtoMember(9)] public string НомерОснования { get; set; }
	[ProtoMember(10)] public double ОрдернаяСхема { get; set; }
	[ProtoMember(11)] public Entity Отдел { get; set; }
	[ProtoMember(12)] public Entity ТипНакладной { get; set; }
	[ProtoMember(13)] public Entity Фирма { get; set; }
	[ProtoMember(14)] public DateTime Дата { get; set; }
	[ProtoMember(15)] public string Номер { get; set; }
	[ProtoMember(16)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(17)] public bool Проведен { get; set; }
	[ProtoMember(18)] public Entity ВидОснования { get; set; }
	[ProtoMember(19)] public Entity Договор { get; set; }
	[ProtoMember(20)] public List<Расходная.ВозвратСамовывозовЗапись> ВозвратСамовывозов { get; set; } = new List<Расходная.ВозвратСамовывозовЗапись>();
	[ProtoMember(21)] public List<Расходная.ТоварыЗапись> Товары { get; set; } = new List<Расходная.ТоварыЗапись>();
}

[ProtoContract] public sealed class РасходныйКассовыйОрдер
{
	[ProtoContract] public sealed class РасшифровкаПлатежаЗапись
	{
		[ProtoMember(1)] public Union ДокументРасчетовСКонтрагентом { get; set; }
		[ProtoMember(2)] public double Сумма { get; set; }
		[ProtoMember(3)] public Entity ФирмаДокументаРасчетов { get; set; }
	}
	[ProtoMember(1)] public Entity Аптека { get; set; }
	[ProtoMember(2)] public Entity ВидОперации { get; set; }
	[ProtoMember(3)] public bool Выгружается { get; set; }
	[ProtoMember(4)] public string Выдать { get; set; }
	[ProtoMember(5)] public Union ДокументОснование { get; set; }
	[ProtoMember(6)] public Entity КассаККМ { get; set; }
	[ProtoMember(7)] public Entity Кассир { get; set; }
	[ProtoMember(8)] public string Комментарий { get; set; }
	[ProtoMember(9)] public string Основание { get; set; }
	[ProtoMember(10)] public Entity Ответственный { get; set; }
	[ProtoMember(11)] public string ПоДокументу { get; set; }
	[ProtoMember(12)] public Entity Сотрудник { get; set; }
	[ProtoMember(13)] public double СуммаДокумента { get; set; }
	[ProtoMember(14)] public Entity Фирма { get; set; }
	[ProtoMember(15)] public DateTime Дата { get; set; }
	[ProtoMember(16)] public string Номер { get; set; }
	[ProtoMember(17)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(18)] public bool Проведен { get; set; }
	[ProtoMember(19)] public Entity Касса { get; set; }
	[ProtoMember(20)] public Entity КассаПолучатель { get; set; }
	[ProtoMember(21)] public List<РасходныйКассовыйОрдер.РасшифровкаПлатежаЗапись> РасшифровкаПлатежа { get; set; } = new List<РасходныйКассовыйОрдер.РасшифровкаПлатежаЗапись>();
}

[ProtoContract] public sealed class РеализацияТоваров
{
	[ProtoContract] public sealed class ТоварыЗапись
	{
		[ProtoMember(1)] public double Всего { get; set; }
		[ProtoMember(2)] public Entity Документ { get; set; }
		[ProtoMember(3)] public double Количество { get; set; }
		[ProtoMember(4)] public Entity Партия { get; set; }
		[ProtoMember(5)] public double Скидка { get; set; }
		[ProtoMember(6)] public Entity СтавкаНДС { get; set; }
		[ProtoMember(7)] public double Сумма { get; set; }
		[ProtoMember(8)] public double СуммаНДС { get; set; }
		[ProtoMember(9)] public Entity Товар { get; set; }
		[ProtoMember(10)] public double Цена { get; set; }
		[ProtoMember(11)] public double РозничнаяЦена { get; set; }
		[ProtoMember(12)] public double КодСтроки { get; set; }
		[ProtoMember(13)] public bool МДЛП { get; set; }
		[ProtoMember(14)] public double ПереплатаПоКомиссии { get; set; }
	}
	[ProtoMember(1)] public DateTime ДатаОплаты { get; set; }
	[ProtoMember(2)] public DateTime ДатаСчетФактуры { get; set; }
	[ProtoMember(3)] public Entity ДокументОснование { get; set; }
	[ProtoMember(4)] public Entity Клиент { get; set; }
	[ProtoMember(5)] public string НомерСчетФактуры { get; set; }
	[ProtoMember(6)] public Entity Организация { get; set; }
	[ProtoMember(7)] public Entity ПунктСамовывоза { get; set; }
	[ProtoMember(8)] public Entity Склад { get; set; }
	[ProtoMember(9)] public double СуммаДокумента { get; set; }
	[ProtoMember(10)] public double СуммаНДС { get; set; }
	[ProtoMember(11)] public DateTime Дата { get; set; }
	[ProtoMember(12)] public string Номер { get; set; }
	[ProtoMember(13)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(14)] public bool Проведен { get; set; }
	[ProtoMember(15)] public string ID_77 { get; set; }
	[ProtoMember(16)] public Entity Договор { get; set; }
	[ProtoMember(17)] public Entity Агент { get; set; }
	[ProtoMember(18)] public Entity ДоговорАгента { get; set; }
	[ProtoMember(19)] public string Комментарий { get; set; }
	[ProtoMember(20)] public List<РеализацияТоваров.ТоварыЗапись> Товары { get; set; } = new List<РеализацияТоваров.ТоварыЗапись>();
}

[ProtoContract] public sealed class Сборка
{
	[ProtoContract] public sealed class ПотериЗапись
	{
		[ProtoMember(1)] public Union Документ { get; set; }
		[ProtoMember(2)] public double Остаток { get; set; }
		[ProtoMember(3)] public Entity Партия { get; set; }
		[ProtoMember(4)] public double Резерв { get; set; }
		[ProtoMember(5)] public Entity Товар { get; set; }
	}
	[ProtoContract] public sealed class ПрогрессСборкиЗапись
	{
		[ProtoMember(1)] public Union Документ { get; set; }
		[ProtoMember(2)] public double Зона { get; set; }
		[ProtoMember(3)] public double КоличествоНеХватило { get; set; }
		[ProtoMember(4)] public double КоличествоСобрано { get; set; }
		[ProtoMember(5)] public Entity Партия { get; set; }
		[ProtoMember(6)] public DateTime Период { get; set; }
		[ProtoMember(7)] public double Стелаж { get; set; }
		[ProtoMember(8)] public Entity Товар { get; set; }
		[ProtoMember(9)] public bool Холод { get; set; }
		[ProtoMember(10)] public double Ячейка { get; set; }
	}
	[ProtoContract] public sealed class ТоварыЗапись
	{
		[ProtoMember(1)] public Union Документ { get; set; }
		[ProtoMember(2)] public double Количество { get; set; }
		[ProtoMember(3)] public double КоличествоНеХватило { get; set; }
		[ProtoMember(4)] public double КоличествоСобрано { get; set; }
		[ProtoMember(5)] public Entity Партия { get; set; }
		[ProtoMember(6)] public Entity Товар { get; set; }
		[ProtoMember(7)] public bool Холод { get; set; }
	}
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public Entity ДокументОснование { get; set; }
	[ProtoMember(3)] public Entity Склад { get; set; }
	[ProtoMember(4)] public Entity СкладПотерь { get; set; }
	[ProtoMember(5)] public bool СобранНаКассе { get; set; }
	[ProtoMember(6)] public Entity Сотрудник { get; set; }
	[ProtoMember(7)] public Entity ТипСборки { get; set; }
	[ProtoMember(8)] public bool Холод { get; set; }
	[ProtoMember(9)] public string ШК { get; set; }
	[ProtoMember(10)] public bool ЭтоКоробкаСЦентральногоСклада { get; set; }
	[ProtoMember(11)] public DateTime Дата { get; set; }
	[ProtoMember(12)] public string Номер { get; set; }
	[ProtoMember(13)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(14)] public bool Проведен { get; set; }
	[ProtoMember(15)] public List<Сборка.ПотериЗапись> Потери { get; set; } = new List<Сборка.ПотериЗапись>();
	[ProtoMember(16)] public List<Сборка.ПрогрессСборкиЗапись> ПрогрессСборки { get; set; } = new List<Сборка.ПрогрессСборкиЗапись>();
	[ProtoMember(17)] public List<Сборка.ТоварыЗапись> Товары { get; set; } = new List<Сборка.ТоварыЗапись>();
}

[ProtoContract] public sealed class СписаниеОстатков
{
	[ProtoContract] public sealed class ОстаткиТовараЗапись
	{
		[ProtoMember(1)] public bool ВидДвиженияРасход { get; set; }
		[ProtoMember(2)] public Union ДокументРезерва { get; set; }
		[ProtoMember(3)] public double Остаток { get; set; }
		[ProtoMember(4)] public Entity Партия { get; set; }
		[ProtoMember(5)] public double Резерв { get; set; }
		[ProtoMember(6)] public Entity ТипОперации { get; set; }
		[ProtoMember(7)] public Entity Товар { get; set; }
		[ProtoMember(8)] public bool МДЛП { get; set; }
		[ProtoMember(9)] public Entity Статус { get; set; }
		[ProtoMember(10)] public double Сумма { get; set; }
		[ProtoMember(11)] public double Цена { get; set; }
	}
	[ProtoContract] public sealed class ИдентификаторыМДЛПЗапись
	{
		[ProtoMember(1)] public string Идентификатор { get; set; }
		[ProtoMember(2)] public string КлючУникальности { get; set; }
		[ProtoMember(3)] public Entity Номенклатура { get; set; }
		[ProtoMember(4)] public Entity Партия { get; set; }
	}
	[ProtoMember(1)] public Entity АдресХранения { get; set; }
	[ProtoMember(2)] public Entity ДокументОснование { get; set; }
	[ProtoMember(3)] public string Комментарий { get; set; }
	[ProtoMember(4)] public Entity Ответственный { get; set; }
	[ProtoMember(5)] public Entity Отдел { get; set; }
	[ProtoMember(6)] public DateTime Дата { get; set; }
	[ProtoMember(7)] public string Номер { get; set; }
	[ProtoMember(8)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(9)] public bool Проведен { get; set; }
	[ProtoMember(10)] public Entity ПричинаСписания { get; set; }
	[ProtoMember(11)] public Union КонтрагентПоУничтожениюЛП { get; set; }
	[ProtoMember(12)] public Union СкладКонтрагента { get; set; }
	[ProtoMember(13)] public Entity Статус { get; set; }
	[ProtoMember(14)] public List<СписаниеОстатков.ОстаткиТовараЗапись> ОстаткиТовара { get; set; } = new List<СписаниеОстатков.ОстаткиТовараЗапись>();
	[ProtoMember(15)] public List<СписаниеОстатков.ИдентификаторыМДЛПЗапись> ИдентификаторыМДЛП { get; set; } = new List<СписаниеОстатков.ИдентификаторыМДЛПЗапись>();
}

[ProtoContract] public sealed class СписаниеПретензии
{
	[ProtoContract] public sealed class ТоварыЗапись
	{
		[ProtoMember(1)] public double Количество { get; set; }
		[ProtoMember(2)] public Entity Партия { get; set; }
		[ProtoMember(3)] public double Сумма { get; set; }
		[ProtoMember(4)] public Entity Товар { get; set; }
		[ProtoMember(5)] public double Цена { get; set; }
		[ProtoMember(6)] public Entity Претензия { get; set; }
	}
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public DateTime ДатаОснования { get; set; }
	[ProtoMember(3)] public string НомерОснования { get; set; }
	[ProtoMember(4)] public Entity Отдел { get; set; }
	[ProtoMember(5)] public DateTime Дата { get; set; }
	[ProtoMember(6)] public string Номер { get; set; }
	[ProtoMember(7)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(8)] public bool Проведен { get; set; }
	[ProtoMember(9)] public Entity ВидОснования { get; set; }
	[ProtoMember(10)] public List<СписаниеПретензии.ТоварыЗапись> Товары { get; set; } = new List<СписаниеПретензии.ТоварыЗапись>();
}

[ProtoContract] public sealed class УстановкаВознагражденийКурьерам
{
	[ProtoContract] public sealed class ВознагражденияЗаВходВЗонуЗапись
	{
		[ProtoMember(1)] public Entity ЗонаДоставки { get; set; }
		[ProtoMember(2)] public double ИдентификаторСтроки { get; set; }
		[ProtoMember(3)] public double Сумма { get; set; }
	}
	[ProtoContract] public sealed class ВознагражденияПоВремениЗапись
	{
		[ProtoMember(1)] public DateTime Время { get; set; }
		[ProtoMember(2)] public Entity ЗонаДоставки { get; set; }
		[ProtoMember(3)] public double ИдентификаторСтроки { get; set; }
		[ProtoMember(4)] public double Сумма { get; set; }
	}
	[ProtoMember(1)] public string Комментарий { get; set; }
	[ProtoMember(2)] public Entity Ответственный { get; set; }
	[ProtoMember(3)] public Entity Склад { get; set; }
	[ProtoMember(4)] public DateTime Дата { get; set; }
	[ProtoMember(5)] public string Номер { get; set; }
	[ProtoMember(6)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(7)] public bool Проведен { get; set; }
	[ProtoMember(8)] public List<УстановкаВознагражденийКурьерам.ВознагражденияЗаВходВЗонуЗапись> ВознагражденияЗаВходВЗону { get; set; } = new List<УстановкаВознагражденийКурьерам.ВознагражденияЗаВходВЗонуЗапись>();
	[ProtoMember(9)] public List<УстановкаВознагражденийКурьерам.ВознагражденияПоВремениЗапись> ВознагражденияПоВремени { get; set; } = new List<УстановкаВознагражденийКурьерам.ВознагражденияПоВремениЗапись>();
}

[ProtoContract] public sealed class УстановкаКомиссииПартнера
{
	[ProtoContract] public sealed class КомиссияЗапись
	{
		[ProtoMember(1)] public double МаксЦена { get; set; }
		[ProtoMember(2)] public double ПроцентКомиссии { get; set; }
	}
	[ProtoMember(1)] public Entity БазаРасчета { get; set; }
	[ProtoMember(2)] public Entity Договор { get; set; }
	[ProtoMember(3)] public Entity Контрагент { get; set; }
	[ProtoMember(4)] public Entity Ответственный { get; set; }
	[ProtoMember(5)] public Entity РегионРаботы { get; set; }
	[ProtoMember(6)] public Entity ТипДоставки { get; set; }
	[ProtoMember(7)] public DateTime Дата { get; set; }
	[ProtoMember(8)] public string Номер { get; set; }
	[ProtoMember(9)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(10)] public bool Проведен { get; set; }
	[ProtoMember(11)] public List<УстановкаКомиссииПартнера.КомиссияЗапись> Комиссия { get; set; } = new List<УстановкаКомиссииПартнера.КомиссияЗапись>();
}

[ProtoContract] public sealed class УстановкаПараметровНачисленияКурьерам
{
	[ProtoContract] public sealed class НачисленияЗапись
	{
		[ProtoMember(1)] public DateTime ДатаВремяОперации { get; set; }
		[ProtoMember(2)] public double ПределДо { get; set; }
		[ProtoMember(3)] public double ПределОт { get; set; }
		[ProtoMember(4)] public double СуммаФикс { get; set; }
	}
	[ProtoMember(1)] public Entity ЗонаДоставки { get; set; }
	[ProtoMember(2)] public string Комментарий { get; set; }
	[ProtoMember(3)] public Entity МестоХранения { get; set; }
	[ProtoMember(4)] public Entity Ответственный { get; set; }
	[ProtoMember(5)] public Entity Подразделение { get; set; }
	[ProtoMember(6)] public DateTime Дата { get; set; }
	[ProtoMember(7)] public string Номер { get; set; }
	[ProtoMember(8)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(9)] public bool Проведен { get; set; }
	[ProtoMember(10)] public Entity Должность { get; set; }
	[ProtoMember(11)] public List<УстановкаПараметровНачисленияКурьерам.НачисленияЗапись> Начисления { get; set; } = new List<УстановкаПараметровНачисленияКурьерам.НачисленияЗапись>();
}

[ProtoContract] public sealed class УстановкаТарифовДляКурьеров
{
	[ProtoContract] public sealed class ВознагражденияПоВремениЗапись
	{
		[ProtoMember(1)] public DateTime ВремяНачала { get; set; }
		[ProtoMember(2)] public DateTime ВремяОкончания { get; set; }
		[ProtoMember(3)] public Entity ЗонаДоставки { get; set; }
		[ProtoMember(4)] public double ИдентификаторСтроки { get; set; }
		[ProtoMember(5)] public double Сумма { get; set; }
		[ProtoMember(6)] public Union ТипДокументаДоставки { get; set; }
	}
	[ProtoContract] public sealed class ВознагражденияПоЗонамЗапись
	{
		[ProtoMember(1)] public Entity ЗонаДоставки { get; set; }
		[ProtoMember(2)] public double ИдентификаторСтроки { get; set; }
		[ProtoMember(3)] public double Сумма { get; set; }
		[ProtoMember(4)] public double СуммаЗаСрочнуюДоставку { get; set; }
	}
	[ProtoContract] public sealed class КурьерыЗапись
	{
		[ProtoMember(1)] public Entity Курьер { get; set; }
	}
	[ProtoMember(1)] public Entity АдресХранения { get; set; }
	[ProtoMember(2)] public string Комментарий { get; set; }
	[ProtoMember(3)] public double Оклад { get; set; }
	[ProtoMember(4)] public Entity Ответственный { get; set; }
	[ProtoMember(5)] public Entity ФормаРасчета { get; set; }
	[ProtoMember(6)] public DateTime Дата { get; set; }
	[ProtoMember(7)] public string Номер { get; set; }
	[ProtoMember(8)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(9)] public bool Проведен { get; set; }
	[ProtoMember(10)] public List<УстановкаТарифовДляКурьеров.ВознагражденияПоВремениЗапись> ВознагражденияПоВремени { get; set; } = new List<УстановкаТарифовДляКурьеров.ВознагражденияПоВремениЗапись>();
	[ProtoMember(11)] public List<УстановкаТарифовДляКурьеров.ВознагражденияПоЗонамЗапись> ВознагражденияПоЗонам { get; set; } = new List<УстановкаТарифовДляКурьеров.ВознагражденияПоЗонамЗапись>();
	[ProtoMember(12)] public List<УстановкаТарифовДляКурьеров.КурьерыЗапись> Курьеры { get; set; } = new List<УстановкаТарифовДляКурьеров.КурьерыЗапись>();
}

[ProtoContract] public sealed class УстановкаЦенГосРеестра
{
	[ProtoContract] public sealed class ТоварыЗапись
	{
		[ProtoMember(1)] public DateTime ДатаРегистрационногоУдостоверения { get; set; }
		[ProtoMember(2)] public Entity Номенклатура { get; set; }
		[ProtoMember(3)] public string НомерРегистрационногоУдостоверения { get; set; }
		[ProtoMember(4)] public double Цена { get; set; }
		[ProtoMember(5)] public string Штрихкод { get; set; }
	}
	[ProtoMember(1)] public DateTime Дата { get; set; }
	[ProtoMember(2)] public string Номер { get; set; }
	[ProtoMember(3)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(4)] public bool Проведен { get; set; }
	[ProtoMember(5)] public List<УстановкаЦенГосРеестра.ТоварыЗапись> Товары { get; set; } = new List<УстановкаЦенГосРеестра.ТоварыЗапись>();
}

[ProtoContract] public sealed class УтверждениеТоварныхМатриц
{
	[ProtoContract] public sealed class СоставМатрицыЗапись
	{
		[ProtoMember(1)] public bool АктивностьНоменклатуры { get; set; }
		[ProtoMember(2)] public double НеснижаемыйОстаток { get; set; }
		[ProtoMember(3)] public Entity Номенклатура { get; set; }
		[ProtoMember(4)] public Entity УсловиеВхождения { get; set; }
	}
	[ProtoMember(1)] public Entity ВидУтверждения { get; set; }
	[ProtoMember(2)] public Entity Ответственный { get; set; }
	[ProtoMember(3)] public Entity ТоварнаяМатрица { get; set; }
	[ProtoMember(4)] public DateTime Дата { get; set; }
	[ProtoMember(5)] public string Номер { get; set; }
	[ProtoMember(6)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(7)] public bool Проведен { get; set; }
	[ProtoMember(8)] public List<УтверждениеТоварныхМатриц.СоставМатрицыЗапись> СоставМатрицы { get; set; } = new List<УтверждениеТоварныхМатриц.СоставМатрицыЗапись>();
}

[ProtoContract] public sealed class ФискализацияЗаказаКлиента
{
	[ProtoMember(1)] public Entity ВидОперации { get; set; }
	[ProtoMember(2)] public Entity ЗаказКлиента { get; set; }
	[ProtoMember(3)] public string КодОшибки { get; set; }
	[ProtoMember(4)] public string ОписаниеОшибки { get; set; }
	[ProtoMember(5)] public Entity ФормаОплаты { get; set; }
	[ProtoMember(6)] public Entity Чек { get; set; }
	[ProtoMember(7)] public Entity ШаблонЧека { get; set; }
	[ProtoMember(8)] public DateTime Дата { get; set; }
	[ProtoMember(9)] public string Номер { get; set; }
	[ProtoMember(10)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(11)] public string ИдентификаторЗаказаКлиента { get; set; }
	[ProtoMember(12)] public string Комментарий { get; set; }
	[ProtoMember(13)] public Entity Курьер { get; set; }
}

[ProtoContract] public sealed class Чеки
{
	[ProtoContract] public sealed class ИспользованныеБуклетыЗапись
	{
		[ProtoMember(1)] public string booklet_barcode { get; set; }
		[ProtoMember(2)] public DateTime check_date { get; set; }
		[ProtoMember(3)] public string t_check_h_id { get; set; }
	}
	[ProtoContract] public sealed class РеализацияЗапись
	{
		[ProtoMember(1)] public Entity ВариантРезервирования { get; set; }
		[ProtoMember(2)] public double Всего { get; set; }
		[ProtoMember(3)] public double Дисконт { get; set; }
		[ProtoMember(4)] public Union Документ { get; set; }
		[ProtoMember(5)] public Entity ЗСЯ { get; set; }
		[ProtoMember(6)] public double Количество { get; set; }
		[ProtoMember(7)] public bool НуженРецепт { get; set; }
		[ProtoMember(8)] public Entity Отдел { get; set; }
		[ProtoMember(9)] public Entity Партия { get; set; }
		[ProtoMember(10)] public double Себестоимость { get; set; }
		[ProtoMember(11)] public double Скидка { get; set; }
		[ProtoMember(12)] public Entity СтавкаНДС { get; set; }
		[ProtoMember(13)] public double Сумма { get; set; }
		[ProtoMember(14)] public double СуммаНДС { get; set; }
		[ProtoMember(15)] public Entity ТипКомплектацииЗаказа { get; set; }
		[ProtoMember(16)] public Entity Товар { get; set; }
		[ProtoMember(17)] public double Цена { get; set; }
		[ProtoMember(18)] public string ИдентификаторСтроки { get; set; }
		[ProtoMember(19)] public double Бонусы { get; set; }
		[ProtoMember(20)] public double КоличествоКВозмещению { get; set; }
		[ProtoMember(21)] public double СуммаВозмещения { get; set; }
		[ProtoMember(22)] public double ЦенаЕдиницыВозмещения { get; set; }
		[ProtoMember(23)] public double СуммаСкидкиПоРецепту { get; set; }
		[ProtoMember(24)] public Entity ТипЦены { get; set; }
	}
	[ProtoContract] public sealed class СписаниеПакетовЗапись
	{
		[ProtoMember(1)] public Entity ЗСЯ { get; set; }
		[ProtoMember(2)] public double Количество { get; set; }
		[ProtoMember(3)] public Entity Отдел { get; set; }
		[ProtoMember(4)] public Entity Заказ { get; set; }
		[ProtoMember(5)] public Entity МестоЗаказа { get; set; }
	}
	[ProtoContract] public sealed class ТаблицаСкидочныеПрограммыЗапись
	{
		[ProtoMember(1)] public string CARDNUMBER { get; set; }
		[ProtoMember(2)] public string id_Товар { get; set; }
		[ProtoMember(3)] public string Line { get; set; }
		[ProtoMember(4)] public string LineDoc { get; set; }
		[ProtoMember(5)] public DateTime OPERDATE { get; set; }
		[ProtoMember(6)] public string PRODUCTCODE { get; set; }
		[ProtoMember(7)] public Entity project { get; set; }
		[ProtoMember(8)] public string ИДДокумента { get; set; }
		[ProtoMember(9)] public string Идентификатор { get; set; }
		[ProtoMember(10)] public string ИДПартии { get; set; }
		[ProtoMember(11)] public string Количество { get; set; }
		[ProtoMember(12)] public string НомерДокумента { get; set; }
		[ProtoMember(13)] public double ПроцентСкидки { get; set; }
		[ProtoMember(14)] public string СтавкаНДС { get; set; }
		[ProtoMember(15)] public string Сумма { get; set; }
		[ProtoMember(16)] public string СуммаСкидки { get; set; }
		[ProtoMember(17)] public Entity ТипСкидочнойПрограммы { get; set; }
		[ProtoMember(18)] public Entity Товар { get; set; }
		[ProtoMember(19)] public string Цена { get; set; }
	}
	[ProtoContract] public sealed class ОплатаЗапись
	{
		[ProtoMember(1)] public double Сумма { get; set; }
		[ProtoMember(2)] public Entity ТипОплаты { get; set; }
	}
	[ProtoMember(1)] public string authcode { get; set; }
	[ProtoMember(2)] public string ID_77 { get; set; }
	[ProtoMember(3)] public string RRN { get; set; }
	[ProtoMember(4)] public bool ВебСкидкаАстрозенник { get; set; }
	[ProtoMember(5)] public Entity ВидОперации { get; set; }
	[ProtoMember(6)] public Union ДокументОснование { get; set; }
	[ProtoMember(7)] public bool Доставка { get; set; }
	[ProtoMember(8)] public Entity Кассир { get; set; }
	[ProtoMember(9)] public Entity ККМ { get; set; }
	[ProtoMember(10)] public string Комментарий { get; set; }
	[ProtoMember(11)] public Entity КонтрагентЭквайер { get; set; }
	[ProtoMember(12)] public string КПК { get; set; }
	[ProtoMember(13)] public string НомерКарты { get; set; }
	[ProtoMember(14)] public double НомерСмены { get; set; }
	[ProtoMember(15)] public string НомерТранзакцииЭквайера { get; set; }
	[ProtoMember(16)] public double НомерЧека { get; set; }
	[ProtoMember(17)] public bool ОплатаПоКарте { get; set; }
	[ProtoMember(18)] public Entity Организация { get; set; }
	[ProtoMember(19)] public Entity Отдел { get; set; }
	[ProtoMember(20)] public Entity ОтчетОРозничныхПродажах { get; set; }
	[ProtoMember(21)] public double СуммаДокумента { get; set; }
	[ProtoMember(22)] public Entity ТипОплаты { get; set; }
	[ProtoMember(23)] public string ФН { get; set; }
	[ProtoMember(24)] public string ФПД { get; set; }
	[ProtoMember(25)] public DateTime Дата { get; set; }
	[ProtoMember(26)] public string Номер { get; set; }
	[ProtoMember(27)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(28)] public bool Проведен { get; set; }
	[ProtoMember(29)] public Entity ЗаказКлиента { get; set; }
	[ProtoMember(30)] public bool ОтключитьОбработкуПоОнлайнЭквайрингу { get; set; }
	[ProtoMember(31)] public Entity БонуснаяПрограмма { get; set; }
	[ProtoMember(32)] public double БонусыНачисленные { get; set; }
	[ProtoMember(33)] public string ХешКарты { get; set; }
	[ProtoMember(34)] public bool ЭтоКартаСбербанка { get; set; }
	[ProtoMember(35)] public Entity РучноеПроцессирование { get; set; }
	[ProtoMember(36)] public bool БольшойЧек { get; set; }
	[ProtoMember(37)] public Entity Курьер { get; set; }
	[ProtoMember(38)] public Entity ТипЦены { get; set; }
	[ProtoMember(39)] public List<Чеки.ИспользованныеБуклетыЗапись> ИспользованныеБуклеты { get; set; } = new List<Чеки.ИспользованныеБуклетыЗапись>();
	[ProtoMember(40)] public List<Чеки.РеализацияЗапись> Реализация { get; set; } = new List<Чеки.РеализацияЗапись>();
	[ProtoMember(41)] public List<Чеки.СписаниеПакетовЗапись> СписаниеПакетов { get; set; } = new List<Чеки.СписаниеПакетовЗапись>();
	[ProtoMember(42)] public List<Чеки.ТаблицаСкидочныеПрограммыЗапись> ТаблицаСкидочныеПрограммы { get; set; } = new List<Чеки.ТаблицаСкидочныеПрограммыЗапись>();
	[ProtoMember(43)] public List<Чеки.ОплатаЗапись> Оплата { get; set; } = new List<Чеки.ОплатаЗапись>();
}
}
}
