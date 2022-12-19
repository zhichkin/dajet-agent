using ProtoBuf;
using DaJet.ProtoBuf;
using System;
using System.Collections.Generic;

namespace erp_model
{
namespace РегистрНакопления
{

[ProtoContract] public sealed class ДенежныеСредстваВПути
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ДенежныеСредстваВПути.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity АдресХранения { get; set; }
		[ProtoMember(3)] public Entity ЗаказКлиента { get; set; }
		[ProtoMember(4)] public double Сумма { get; set; }
		[ProtoMember(5)] public RecordType ВидДвижения { get; set; }
		[ProtoMember(6)] public DateTime Период { get; set; }
	}
	[ProtoMember(1)] public ДенежныеСредстваВПути.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ДенежныеСредстваВПути.Запись> insert { get; set; } = new List<ДенежныеСредстваВПути.Запись>();
}

[ProtoContract] public sealed class ДенежныеСредстваКВозвратуКурьером
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ДенежныеСредстваКВозвратуКурьером.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity АдресХранения { get; set; }
		[ProtoMember(3)] public Union ЗаказКлиента { get; set; }
		[ProtoMember(4)] public Entity Курьер { get; set; }
		[ProtoMember(5)] public Entity МаршрутныйЛист { get; set; }
		[ProtoMember(6)] public double Сумма { get; set; }
		[ProtoMember(7)] public RecordType ВидДвижения { get; set; }
		[ProtoMember(8)] public DateTime Период { get; set; }
	}
	[ProtoMember(1)] public ДенежныеСредстваКВозвратуКурьером.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ДенежныеСредстваКВозвратуКурьером.Запись> insert { get; set; } = new List<ДенежныеСредстваКВозвратуКурьером.Запись>();
}

[ProtoContract] public sealed class ДенежныеСредстваККМ
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ДенежныеСредстваККМ.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Union ДокументРасчетов { get; set; }
		[ProtoMember(3)] public Entity КассаККМ { get; set; }
		[ProtoMember(4)] public double Сумма { get; set; }
		[ProtoMember(5)] public RecordType ВидДвижения { get; set; }
		[ProtoMember(6)] public DateTime Период { get; set; }
	}
	[ProtoMember(1)] public ДенежныеСредстваККМ.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ДенежныеСредстваККМ.Запись> insert { get; set; } = new List<ДенежныеСредстваККМ.Запись>();
}

[ProtoContract] public sealed class ДенежныеСредстваНаличныеВСейфе
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ДенежныеСредстваНаличныеВСейфе.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity Аптека { get; set; }
		[ProtoMember(3)] public Entity Фирма { get; set; }
		[ProtoMember(4)] public double Сумма { get; set; }
		[ProtoMember(5)] public RecordType ВидДвижения { get; set; }
		[ProtoMember(6)] public DateTime Период { get; set; }
	}
	[ProtoMember(1)] public ДенежныеСредстваНаличныеВСейфе.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ДенежныеСредстваНаличныеВСейфе.Запись> insert { get; set; } = new List<ДенежныеСредстваНаличныеВСейфе.Запись>();
}

[ProtoContract] public sealed class ДоступныеИнтервалыДоставки
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ДоступныеИнтервалыДоставки.Ключ Ключ { get; set; }
		[ProtoMember(2)] public DateTime ДатаДоставки { get; set; }
		[ProtoMember(3)] public Entity ЗонаДоставки { get; set; }
		[ProtoMember(4)] public Entity Интервал { get; set; }
		[ProtoMember(5)] public double Количество { get; set; }
		[ProtoMember(6)] public RecordType ВидДвижения { get; set; }
		[ProtoMember(7)] public DateTime Период { get; set; }
	}
	[ProtoMember(1)] public ДоступныеИнтервалыДоставки.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ДоступныеИнтервалыДоставки.Запись> insert { get; set; } = new List<ДоступныеИнтервалыДоставки.Запись>();
}

[ProtoContract] public sealed class ЗаказыВДоставкеКурьером
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ЗаказыВДоставкеКурьером.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity ЗаказКлиента { get; set; }
		[ProtoMember(3)] public Entity Курьер { get; set; }
		[ProtoMember(4)] public double Количество { get; set; }
		[ProtoMember(5)] public RecordType ВидДвижения { get; set; }
		[ProtoMember(6)] public DateTime Период { get; set; }
	}
	[ProtoMember(1)] public ЗаказыВДоставкеКурьером.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ЗаказыВДоставкеКурьером.Запись> insert { get; set; } = new List<ЗаказыВДоставкеКурьером.Запись>();
}

[ProtoContract] public sealed class ЗаказыВДоставкеКурьеромВозврат
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ЗаказыВДоставкеКурьеромВозврат.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity ЗаказКлиента { get; set; }
		[ProtoMember(3)] public Entity ОтчетОДоставке { get; set; }
		[ProtoMember(4)] public double Количество { get; set; }
		[ProtoMember(5)] public RecordType ВидДвижения { get; set; }
		[ProtoMember(6)] public DateTime Период { get; set; }
	}
	[ProtoMember(1)] public ЗаказыВДоставкеКурьеромВозврат.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ЗаказыВДоставкеКурьеромВозврат.Запись> insert { get; set; } = new List<ЗаказыВДоставкеКурьеромВозврат.Запись>();
}

[ProtoContract] public sealed class ЗаказыВКурьерскойСлужбе
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ЗаказыВКурьерскойСлужбе.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity ЗаказКлиента { get; set; }
		[ProtoMember(3)] public Entity МестоХранения { get; set; }
		[ProtoMember(4)] public Entity Ячейка { get; set; }
		[ProtoMember(5)] public double Количество { get; set; }
		[ProtoMember(6)] public RecordType ВидДвижения { get; set; }
		[ProtoMember(7)] public DateTime Период { get; set; }
	}
	[ProtoMember(1)] public ЗаказыВКурьерскойСлужбе.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ЗаказыВКурьерскойСлужбе.Запись> insert { get; set; } = new List<ЗаказыВКурьерскойСлужбе.Запись>();
}

[ProtoContract] public sealed class ЗаказыВозвращенные
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ЗаказыВозвращенные.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity ЗаказКлиента { get; set; }
		[ProtoMember(3)] public double Количество { get; set; }
		[ProtoMember(4)] public DateTime Период { get; set; }
	}
	[ProtoMember(1)] public ЗаказыВозвращенные.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ЗаказыВозвращенные.Запись> insert { get; set; } = new List<ЗаказыВозвращенные.Запись>();
}

[ProtoContract] public sealed class ЗаказыВыданныеКурьеру
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ЗаказыВыданныеКурьеру.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity АдресХранения { get; set; }
		[ProtoMember(3)] public Union ЗаказКлиента { get; set; }
		[ProtoMember(4)] public Entity Курьер { get; set; }
		[ProtoMember(5)] public Entity МаршрутныйЛист { get; set; }
		[ProtoMember(6)] public double Количество { get; set; }
		[ProtoMember(7)] public RecordType ВидДвижения { get; set; }
		[ProtoMember(8)] public DateTime Период { get; set; }
	}
	[ProtoMember(1)] public ЗаказыВыданныеКурьеру.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ЗаказыВыданныеКурьеру.Запись> insert { get; set; } = new List<ЗаказыВыданныеКурьеру.Запись>();
}

[ProtoContract] public sealed class ЗаказыДоставленные
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ЗаказыДоставленные.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity ЗаказКлиента { get; set; }
		[ProtoMember(3)] public double Количество { get; set; }
		[ProtoMember(4)] public DateTime Период { get; set; }
	}
	[ProtoMember(1)] public ЗаказыДоставленные.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ЗаказыДоставленные.Запись> insert { get; set; } = new List<ЗаказыДоставленные.Запись>();
}

[ProtoContract] public sealed class ЗаказыКВозвратуКурьером
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ЗаказыКВозвратуКурьером.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity АдресХранения { get; set; }
		[ProtoMember(3)] public Union ЗаказКлиента { get; set; }
		[ProtoMember(4)] public Entity Курьер { get; set; }
		[ProtoMember(5)] public Entity МаршрутныйЛист { get; set; }
		[ProtoMember(6)] public double Количество { get; set; }
		[ProtoMember(7)] public RecordType ВидДвижения { get; set; }
		[ProtoMember(8)] public DateTime Период { get; set; }
	}
	[ProtoMember(1)] public ЗаказыКВозвратуКурьером.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ЗаказыКВозвратуКурьером.Запись> insert { get; set; } = new List<ЗаказыКВозвратуКурьером.Запись>();
}

[ProtoContract] public sealed class ЗаказыКлиентов
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ЗаказыКлиентов.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity ЗаказКлиента { get; set; }
		[ProtoMember(3)] public Entity Номенклатура { get; set; }
		[ProtoMember(4)] public Entity Склад { get; set; }
		[ProtoMember(5)] public Entity ПричинаОтмены { get; set; }
		[ProtoMember(6)] public double Количество { get; set; }
		[ProtoMember(7)] public double Сумма { get; set; }
		[ProtoMember(8)] public RecordType ВидДвижения { get; set; }
		[ProtoMember(9)] public DateTime Период { get; set; }
	}
	[ProtoMember(1)] public ЗаказыКлиентов.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ЗаказыКлиентов.Запись> insert { get; set; } = new List<ЗаказыКлиентов.Запись>();
}

[ProtoContract] public sealed class ЗаказыПереданныеПеревозчику
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ЗаказыПереданныеПеревозчику.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity ЗаказКлиента { get; set; }
		[ProtoMember(3)] public Entity Перевозчик { get; set; }
		[ProtoMember(4)] public double Сумма { get; set; }
		[ProtoMember(5)] public RecordType ВидДвижения { get; set; }
		[ProtoMember(6)] public DateTime Период { get; set; }
	}
	[ProtoMember(1)] public ЗаказыПереданныеПеревозчику.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ЗаказыПереданныеПеревозчику.Запись> insert { get; set; } = new List<ЗаказыПереданныеПеревозчику.Запись>();
}

[ProtoContract] public sealed class ЗакупкиКонтрагентов
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ЗакупкиКонтрагентов.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity Контрагент { get; set; }
		[ProtoMember(3)] public Entity ПоставщикКонтрагента { get; set; }
		[ProtoMember(4)] public Entity СкладКонтрагента { get; set; }
		[ProtoMember(5)] public double Количество { get; set; }
		[ProtoMember(6)] public double Сумма { get; set; }
		[ProtoMember(7)] public DateTime Период { get; set; }
	}
	[ProtoMember(1)] public ЗакупкиКонтрагентов.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ЗакупкиКонтрагентов.Запись> insert { get; set; } = new List<ЗакупкиКонтрагентов.Запись>();
}

[ProtoContract] public sealed class МестаЗаказов
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public МестаЗаказов.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity Заказ { get; set; }
		[ProtoMember(3)] public Entity Маршрут { get; set; }
		[ProtoMember(4)] public Entity МестоЗаказа { get; set; }
		[ProtoMember(5)] public double Приемка { get; set; }
		[ProtoMember(6)] public double Раскладка { get; set; }
		[ProtoMember(7)] public RecordType ВидДвижения { get; set; }
		[ProtoMember(8)] public DateTime Период { get; set; }
	}
	[ProtoMember(1)] public МестаЗаказов.Ключ delete { get; set; }
	[ProtoMember(2)] public List<МестаЗаказов.Запись> insert { get; set; } = new List<МестаЗаказов.Запись>();
}

[ProtoContract] public sealed class ОстаткиТовара
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ОстаткиТовара.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Union ДокументРезерва { get; set; }
		[ProtoMember(3)] public Entity Отдел { get; set; }
		[ProtoMember(4)] public Entity Партия { get; set; }
		[ProtoMember(5)] public Entity Товар { get; set; }
		[ProtoMember(6)] public Entity ТипОперации { get; set; }
		[ProtoMember(7)] public double Остаток { get; set; }
		[ProtoMember(8)] public double Резерв { get; set; }
		[ProtoMember(9)] public RecordType ВидДвижения { get; set; }
		[ProtoMember(10)] public DateTime Период { get; set; }
	}
	[ProtoMember(1)] public ОстаткиТовара.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ОстаткиТовара.Запись> insert { get; set; } = new List<ОстаткиТовара.Запись>();
}

[ProtoContract] public sealed class ПределыНагрузкиПоЗонам
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ПределыНагрузкиПоЗонам.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity АдресХранения { get; set; }
		[ProtoMember(3)] public DateTime ДатаДоставки { get; set; }
		[ProtoMember(4)] public double ДлинаИнтервала { get; set; }
		[ProtoMember(5)] public Entity ЗонаДоставки { get; set; }
		[ProtoMember(6)] public DateTime НачалоИнтервала { get; set; }
		[ProtoMember(7)] public double КоличествоПлан { get; set; }
		[ProtoMember(8)] public double КоличествоФакт { get; set; }
		[ProtoMember(9)] public DateTime Период { get; set; }
	}
	[ProtoMember(1)] public ПределыНагрузкиПоЗонам.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ПределыНагрузкиПоЗонам.Запись> insert { get; set; } = new List<ПределыНагрузкиПоЗонам.Запись>();
}

[ProtoContract] public sealed class Претензии
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public Претензии.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity ДокументПретензии { get; set; }
		[ProtoMember(3)] public double ИдентификаторСтроки { get; set; }
		[ProtoMember(4)] public Entity Партия { get; set; }
		[ProtoMember(5)] public Entity Претензия { get; set; }
		[ProtoMember(6)] public Entity Склад { get; set; }
		[ProtoMember(7)] public double Количество { get; set; }
		[ProtoMember(8)] public double Сумма { get; set; }
		[ProtoMember(9)] public RecordType ВидДвижения { get; set; }
		[ProtoMember(10)] public DateTime Период { get; set; }
	}
	[ProtoMember(1)] public Претензии.Ключ delete { get; set; }
	[ProtoMember(2)] public List<Претензии.Запись> insert { get; set; } = new List<Претензии.Запись>();
}

[ProtoContract] public sealed class ПродажиКонтрагентам
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ПродажиКонтрагентам.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity Договор { get; set; }
		[ProtoMember(3)] public Entity Контрагент { get; set; }
		[ProtoMember(4)] public Entity Организация { get; set; }
		[ProtoMember(5)] public double СуммаВозврат { get; set; }
		[ProtoMember(6)] public double СуммаНДСВозврат { get; set; }
		[ProtoMember(7)] public double СуммаНДСПродажи { get; set; }
		[ProtoMember(8)] public double СуммаПродажи { get; set; }
		[ProtoMember(9)] public DateTime Период { get; set; }
	}
	[ProtoMember(1)] public ПродажиКонтрагентам.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ПродажиКонтрагентам.Запись> insert { get; set; } = new List<ПродажиКонтрагентам.Запись>();
}

[ProtoContract] public sealed class ПродажиКонтрагентов
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ПродажиКонтрагентов.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity Контрагент { get; set; }
		[ProtoMember(3)] public Entity СкладКонтрагента { get; set; }
		[ProtoMember(4)] public double Количество { get; set; }
		[ProtoMember(5)] public double СуммаЗакупки { get; set; }
		[ProtoMember(6)] public double СуммаПродажи { get; set; }
		[ProtoMember(7)] public DateTime Период { get; set; }
	}
	[ProtoMember(1)] public ПродажиКонтрагентов.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ПродажиКонтрагентов.Запись> insert { get; set; } = new List<ПродажиКонтрагентов.Запись>();
}

[ProtoContract] public sealed class ПродажиПоБонуснымПрограммам
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ПродажиПоБонуснымПрограммам.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity ВидОперации { get; set; }
		[ProtoMember(3)] public Entity ЗаказКлиента { get; set; }
		[ProtoMember(4)] public Entity Организация { get; set; }
		[ProtoMember(5)] public double СуммаБонусов { get; set; }
		[ProtoMember(6)] public DateTime Период { get; set; }
		[ProtoMember(7)] public Entity БонуснаяПрограмма { get; set; }
		[ProtoMember(8)] public double СуммаБонусовНачисленная { get; set; }
	}
	[ProtoMember(1)] public ПродажиПоБонуснымПрограммам.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ПродажиПоБонуснымПрограммам.Запись> insert { get; set; } = new List<ПродажиПоБонуснымПрограммам.Запись>();
}

[ProtoContract] public sealed class РасчетыСПоставщиками
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public РасчетыСПоставщиками.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity ЗаказПоставщику { get; set; }
		[ProtoMember(3)] public Entity Поставщик { get; set; }
		[ProtoMember(4)] public double Сумма { get; set; }
		[ProtoMember(5)] public RecordType ВидДвижения { get; set; }
		[ProtoMember(6)] public DateTime Период { get; set; }
	}
	[ProtoMember(1)] public РасчетыСПоставщиками.Ключ delete { get; set; }
	[ProtoMember(2)] public List<РасчетыСПоставщиками.Запись> insert { get; set; } = new List<РасчетыСПоставщиками.Запись>();
}

[ProtoContract] public sealed class Реализация
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public Реализация.Ключ Ключ { get; set; }
		[ProtoMember(2)] public bool ВГО { get; set; }
		[ProtoMember(3)] public Union ДокументПродажи { get; set; }
		[ProtoMember(4)] public Entity ККМ { get; set; }
		[ProtoMember(5)] public Entity Клиент { get; set; }
		[ProtoMember(6)] public Entity Операция { get; set; }
		[ProtoMember(7)] public Entity Организация { get; set; }
		[ProtoMember(8)] public Entity Отдел { get; set; }
		[ProtoMember(9)] public Entity Партия { get; set; }
		[ProtoMember(10)] public Entity СтавкаНДС { get; set; }
		[ProtoMember(11)] public Entity ТипОплаты { get; set; }
		[ProtoMember(12)] public Entity Товар { get; set; }
		[ProtoMember(13)] public DateTime ДатаПродажи { get; set; }
		[ProtoMember(14)] public Entity ТипЦены { get; set; }
		[ProtoMember(15)] public double Всего { get; set; }
		[ProtoMember(16)] public double Количество { get; set; }
		[ProtoMember(17)] public double СебеСтоимость { get; set; }
		[ProtoMember(18)] public double Скидка { get; set; }
		[ProtoMember(19)] public double Сумма { get; set; }
		[ProtoMember(20)] public double СуммаНДС { get; set; }
		[ProtoMember(21)] public DateTime Период { get; set; }
		[ProtoMember(22)] public Entity Курьер { get; set; }
	}
	[ProtoMember(1)] public Реализация.Ключ delete { get; set; }
	[ProtoMember(2)] public List<Реализация.Запись> insert { get; set; } = new List<Реализация.Запись>();
}

[ProtoContract] public sealed class СуммыВозмещенияПоЛьготнымЭлектроннымРецептам
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public СуммыВозмещенияПоЛьготнымЭлектроннымРецептам.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Union Документ { get; set; }
		[ProtoMember(3)] public Entity Партия { get; set; }
		[ProtoMember(4)] public Entity Товар { get; set; }
		[ProtoMember(5)] public double ЦенаЕдиницыВозмещения { get; set; }
		[ProtoMember(6)] public double КоличествоКВозмещению { get; set; }
		[ProtoMember(7)] public double Скидка { get; set; }
		[ProtoMember(8)] public double СуммаВозмещения { get; set; }
		[ProtoMember(9)] public double СуммаДоплаты { get; set; }
		[ProtoMember(10)] public DateTime Период { get; set; }
	}
	[ProtoMember(1)] public СуммыВозмещенияПоЛьготнымЭлектроннымРецептам.Ключ delete { get; set; }
	[ProtoMember(2)] public List<СуммыВозмещенияПоЛьготнымЭлектроннымРецептам.Запись> insert { get; set; } = new List<СуммыВозмещенияПоЛьготнымЭлектроннымРецептам.Запись>();
}

[ProtoContract] public sealed class ТоварыВПути
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ТоварыВПути.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity ДокументОснование { get; set; }
		[ProtoMember(3)] public Entity Номенклатура { get; set; }
		[ProtoMember(4)] public Entity Склад { get; set; }
		[ProtoMember(5)] public double Количество { get; set; }
		[ProtoMember(6)] public RecordType ВидДвижения { get; set; }
		[ProtoMember(7)] public DateTime Период { get; set; }
	}
	[ProtoMember(1)] public ТоварыВПути.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ТоварыВПути.Запись> insert { get; set; } = new List<ТоварыВПути.Запись>();
}
}
}
