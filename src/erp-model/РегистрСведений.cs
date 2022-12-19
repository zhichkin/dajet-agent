using ProtoBuf;
using DaJet.ProtoBuf;
using System;
using System.Collections.Generic;

namespace erp_model
{
namespace РегистрСведений
{

[ProtoContract] public sealed class ABCКлассификацияНоменклатуры
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Аптека { get; set; }
		[ProtoMember(3)] public Entity Номенклатура { get; set; }
		[ProtoMember(4)] public Entity СтандартныйПериод { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ABCКлассификацияНоменклатуры.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity КлассВаловогоДохода { get; set; }
		[ProtoMember(3)] public Entity КлассПродаж { get; set; }
	}
	[ProtoMember(1)] public ABCКлассификацияНоменклатуры.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ABCКлассификацияНоменклатуры.Запись> insert { get; set; } = new List<ABCКлассификацияНоменклатуры.Запись>();
}

[ProtoContract] public sealed class astro_card_block
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public string card_number { get; set; }
		[ProtoMember(3)] public DateTime export_datetime { get; set; }
		[ProtoMember(4)] public string id { get; set; }
		[ProtoMember(5)] public Entity project { get; set; }
		[ProtoMember(6)] public DateTime reactivation_date { get; set; }
		[ProtoMember(7)] public DateTime stop_date { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public astro_card_block.Ключ Ключ { get; set; }
	}
	[ProtoMember(1)] public astro_card_block.Ключ delete { get; set; }
	[ProtoMember(2)] public List<astro_card_block.Запись> insert { get; set; } = new List<astro_card_block.Запись>();
}

[ProtoContract] public sealed class astro_card_rules
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public DateTime _from { get; set; }
		[ProtoMember(3)] public string card_type { get; set; }
		[ProtoMember(4)] public string description { get; set; }
		[ProtoMember(5)] public DateTime export_date { get; set; }
		[ProtoMember(6)] public double id { get; set; }
		[ProtoMember(7)] public Entity project { get; set; }
		[ProtoMember(8)] public string rule_id { get; set; }
		[ProtoMember(9)] public string sku { get; set; }
		[ProtoMember(10)] public string territory { get; set; }
		[ProtoMember(11)] public DateTime to { get; set; }
		[ProtoMember(12)] public string version { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public astro_card_rules.Ключ Ключ { get; set; }
		[ProtoMember(2)] public double discount { get; set; }
	}
	[ProtoMember(1)] public astro_card_rules.Ключ delete { get; set; }
	[ProtoMember(2)] public List<astro_card_rules.Запись> insert { get; set; } = new List<astro_card_rules.Запись>();
}

[ProtoContract] public sealed class astro_card_types
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public string code { get; set; }
		[ProtoMember(3)] public string description { get; set; }
		[ProtoMember(4)] public DateTime export_date { get; set; }
		[ProtoMember(5)] public double id { get; set; }
		[ProtoMember(6)] public Entity project { get; set; }
		[ProtoMember(7)] public string territory { get; set; }
		[ProtoMember(8)] public string version { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public astro_card_types.Ключ Ключ { get; set; }
		[ProtoMember(2)] public string _from { get; set; }
		[ProtoMember(3)] public string to { get; set; }
	}
	[ProtoMember(1)] public astro_card_types.Ключ delete { get; set; }
	[ProtoMember(2)] public List<astro_card_types.Запись> insert { get; set; } = new List<astro_card_types.Запись>();
}

[ProtoContract] public sealed class astro_personal_rules
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public DateTime _from { get; set; }
		[ProtoMember(3)] public string card_number { get; set; }
		[ProtoMember(4)] public string description { get; set; }
		[ProtoMember(5)] public DateTime export_date { get; set; }
		[ProtoMember(6)] public double id { get; set; }
		[ProtoMember(7)] public Entity project { get; set; }
		[ProtoMember(8)] public string sku { get; set; }
		[ProtoMember(9)] public DateTime to { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public astro_personal_rules.Ключ Ключ { get; set; }
		[ProtoMember(2)] public double discount { get; set; }
	}
	[ProtoMember(1)] public astro_personal_rules.Ключ delete { get; set; }
	[ProtoMember(2)] public List<astro_personal_rules.Запись> insert { get; set; } = new List<astro_personal_rules.Запись>();
}

[ProtoContract] public sealed class t_astro_used_booklets
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public t_astro_used_booklets.Ключ Ключ { get; set; }
		[ProtoMember(2)] public string booklet_barcode { get; set; }
		[ProtoMember(3)] public DateTime check_date { get; set; }
		[ProtoMember(4)] public string t_check_h_id { get; set; }
		[ProtoMember(5)] public bool Действует { get; set; }
		[ProtoMember(6)] public DateTime Период { get; set; }
	}
	[ProtoMember(1)] public t_astro_used_booklets.Ключ delete { get; set; }
	[ProtoMember(2)] public List<t_astro_used_booklets.Запись> insert { get; set; } = new List<t_astro_used_booklets.Запись>();
}

[ProtoContract] public sealed class t_check_ucs
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public string authcode { get; set; }
		[ProtoMember(3)] public string card_id { get; set; }
		[ProtoMember(4)] public string rrn { get; set; }
		[ProtoMember(5)] public string TerminalID { get; set; }
		[ProtoMember(6)] public DateTime ДатаТранзакции { get; set; }
		[ProtoMember(7)] public string ККМ { get; set; }
		[ProtoMember(8)] public string НомерКПК { get; set; }
		[ProtoMember(9)] public string НомерСмены { get; set; }
		[ProtoMember(10)] public double НомерЧека { get; set; }
		[ProtoMember(11)] public double СуммаЧека { get; set; }
		[ProtoMember(12)] public double ТипТранзакции { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public t_check_ucs.Ключ Ключ { get; set; }
		[ProtoMember(2)] public bool Выгружено { get; set; }
	}
	[ProtoMember(1)] public t_check_ucs.Ключ delete { get; set; }
	[ProtoMember(2)] public List<t_check_ucs.Запись> insert { get; set; } = new List<t_check_ucs.Запись>();
}

[ProtoContract] public sealed class t_defect_series
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public string code { get; set; }
		[ProtoMember(3)] public string code_egk { get; set; }
		[ProtoMember(4)] public string id_defect { get; set; }
		[ProtoMember(5)] public string name { get; set; }
		[ProtoMember(6)] public string series { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public t_defect_series.Ключ Ключ { get; set; }
	}
	[ProtoMember(1)] public t_defect_series.Ключ delete { get; set; }
	[ProtoMember(2)] public List<t_defect_series.Запись> insert { get; set; } = new List<t_defect_series.Запись>();
}

[ProtoContract] public sealed class t_rr
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public bool _active { get; set; }
		[ProtoMember(3)] public double id { get; set; }
		[ProtoMember(4)] public string id_rr { get; set; }
		[ProtoMember(5)] public string id_rr_main { get; set; }
		[ProtoMember(6)] public double is_price { get; set; }
		[ProtoMember(7)] public string name_rr { get; set; }
		[ProtoMember(8)] public double rr_code { get; set; }
		[ProtoMember(9)] public string sklad { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public t_rr.Ключ Ключ { get; set; }
		[ProtoMember(2)] public DateTime ДатаЗагрузки { get; set; }
	}
	[ProtoMember(1)] public t_rr.Ключ delete { get; set; }
	[ProtoMember(2)] public List<t_rr.Запись> insert { get; set; } = new List<t_rr.Запись>();
}

[ProtoContract] public sealed class t_rr_sklad
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public string code_asna { get; set; }
		[ProtoMember(3)] public double id_rr { get; set; }
		[ProtoMember(4)] public string id_sklad { get; set; }
		[ProtoMember(5)] public bool is_own { get; set; }
		[ProtoMember(6)] public bool isPharm { get; set; }
		[ProtoMember(7)] public bool isRobot { get; set; }
		[ProtoMember(8)] public bool isSite { get; set; }
		[ProtoMember(9)] public bool isVPharm { get; set; }
		[ProtoMember(10)] public bool isWork { get; set; }
		[ProtoMember(11)] public string phone { get; set; }
		[ProtoMember(12)] public double tc { get; set; }
		[ProtoMember(13)] public double vtc { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public t_rr_sklad.Ключ Ключ { get; set; }
		[ProtoMember(2)] public DateTime ДатаЗагрузки { get; set; }
	}
	[ProtoMember(1)] public t_rr_sklad.Ключ delete { get; set; }
	[ProtoMember(2)] public List<t_rr_sklad.Запись> insert { get; set; } = new List<t_rr_sklad.Запись>();
}

[ProtoContract] public sealed class t_tc
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public string default_phone { get; set; }
		[ProtoMember(3)] public double id { get; set; }
		[ProtoMember(4)] public string id_sklad { get; set; }
		[ProtoMember(5)] public string id_tc { get; set; }
		[ProtoMember(6)] public string name_full { get; set; }
		[ProtoMember(7)] public string name_tc { get; set; }
		[ProtoMember(8)] public string sms_sender_name { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public t_tc.Ключ Ключ { get; set; }
		[ProtoMember(2)] public DateTime ДатаЗагрузки { get; set; }
	}
	[ProtoMember(1)] public t_tc.Ключ delete { get; set; }
	[ProtoMember(2)] public List<t_tc.Запись> insert { get; set; } = new List<t_tc.Запись>();
}

[ProtoContract] public sealed class t_ucs
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public string kkm { get; set; }
		[ProtoMember(3)] public string TCPIP { get; set; }
		[ProtoMember(4)] public string TCPPort { get; set; }
		[ProtoMember(5)] public string TerminalDriverID { get; set; }
		[ProtoMember(6)] public string TerminalID { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public t_ucs.Ключ Ключ { get; set; }
		[ProtoMember(2)] public DateTime ДатаЗагрузки { get; set; }
	}
	[ProtoMember(1)] public t_ucs.Ключ delete { get; set; }
	[ProtoMember(2)] public List<t_ucs.Запись> insert { get; set; } = new List<t_ucs.Запись>();
}

[ProtoContract] public sealed class v_gvls_rr
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public double id { get; set; }
		[ProtoMember(3)] public string id_rr { get; set; }
		[ProtoMember(4)] public double max_k { get; set; }
		[ProtoMember(5)] public double max_price { get; set; }
		[ProtoMember(6)] public double min_price { get; set; }
		[ProtoMember(7)] public double perc { get; set; }
		[ProtoMember(8)] public double perc_obl { get; set; }
		[ProtoMember(9)] public double perc_obl_opt { get; set; }
		[ProtoMember(10)] public double perc_opt { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public v_gvls_rr.Ключ Ключ { get; set; }
	}
	[ProtoMember(1)] public v_gvls_rr.Ключ delete { get; set; }
	[ProtoMember(2)] public List<v_gvls_rr.Запись> insert { get; set; } = new List<v_gvls_rr.Запись>();
}

[ProtoContract] public sealed class v_nac_def
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public string id_reg { get; set; }
		[ProtoMember(3)] public string id_tc { get; set; }
		[ProtoMember(4)] public double nac { get; set; }
		[ProtoMember(5)] public double price_max { get; set; }
		[ProtoMember(6)] public double price_min { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public v_nac_def.Ключ Ключ { get; set; }
	}
	[ProtoMember(1)] public v_nac_def.Ключ delete { get; set; }
	[ProtoMember(2)] public List<v_nac_def.Запись> insert { get; set; } = new List<v_nac_def.Запись>();
}

[ProtoContract] public sealed class XYZКлассификацияНоменклатуры
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Аптека { get; set; }
		[ProtoMember(3)] public Entity Номенклатура { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public XYZКлассификацияНоменклатуры.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity Класс { get; set; }
		[ProtoMember(3)] public double КоэффициентВариации { get; set; }
	}
	[ProtoMember(1)] public XYZКлассификацияНоменклатуры.Ключ delete { get; set; }
	[ProtoMember(2)] public List<XYZКлассификацияНоменклатуры.Запись> insert { get; set; } = new List<XYZКлассификацияНоменклатуры.Запись>();
}

[ProtoContract] public sealed class АдресныеОбъекты
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Идентификатор { get; set; }
		[ProtoMember(3)] public double КодСубъектаРФ { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public АдресныеОбъекты.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity ДополнительныеАдресныеСведения { get; set; }
		[ProtoMember(3)] public double КодКЛАДР { get; set; }
		[ProtoMember(4)] public Entity МуниципальныйРодительскийИдентификатор { get; set; }
		[ProtoMember(5)] public string Наименование { get; set; }
		[ProtoMember(6)] public Entity РодительскийИдентификатор { get; set; }
		[ProtoMember(7)] public string Сокращение { get; set; }
		[ProtoMember(8)] public double Уровень { get; set; }
	}
	[ProtoMember(1)] public АдресныеОбъекты.Ключ delete { get; set; }
	[ProtoMember(2)] public List<АдресныеОбъекты.Запись> insert { get; set; } = new List<АдресныеОбъекты.Запись>();
}

[ProtoContract] public sealed class АктуальныеУсловияВознагражденийКурьеров
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public АктуальныеУсловияВознагражденийКурьеров.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity Склад { get; set; }
		[ProtoMember(3)] public DateTime Период { get; set; }
		[ProtoMember(4)] public Entity ВидНачисления { get; set; }
		[ProtoMember(5)] public Entity Курьер { get; set; }
		[ProtoMember(6)] public Entity ФормаРасчета { get; set; }
		[ProtoMember(7)] public double Оклад { get; set; }
	}
	[ProtoMember(1)] public АктуальныеУсловияВознагражденийКурьеров.Ключ delete { get; set; }
	[ProtoMember(2)] public List<АктуальныеУсловияВознагражденийКурьеров.Запись> insert { get; set; } = new List<АктуальныеУсловияВознагражденийКурьеров.Запись>();
}

[ProtoContract] public sealed class АктуальныйТоварПоставщика
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public string КодТовараПоставщика { get; set; }
		[ProtoMember(3)] public Entity Поставщик { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public АктуальныйТоварПоставщика.Ключ Ключ { get; set; }
		[ProtoMember(2)] public string Наименование { get; set; }
		[ProtoMember(3)] public string Производитель { get; set; }
		[ProtoMember(4)] public Entity Товар { get; set; }
	}
	[ProtoMember(1)] public АктуальныйТоварПоставщика.Ключ delete { get; set; }
	[ProtoMember(2)] public List<АктуальныйТоварПоставщика.Запись> insert { get; set; } = new List<АктуальныйТоварПоставщика.Запись>();
}

[ProtoContract] public sealed class АссортиментнаяМатрица
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Аптека { get; set; }
		[ProtoMember(3)] public Entity Номенклатура { get; set; }
		[ProtoMember(4)] public DateTime Период { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public АссортиментнаяМатрица.Ключ Ключ { get; set; }
		[ProtoMember(2)] public DateTime ДатаФормирования { get; set; }
		[ProtoMember(3)] public bool Активный { get; set; }
		[ProtoMember(4)] public bool Обязательный { get; set; }
		[ProtoMember(5)] public Entity СписокИсточник { get; set; }
	}
	[ProtoMember(1)] public АссортиментнаяМатрица.Ключ delete { get; set; }
	[ProtoMember(2)] public List<АссортиментнаяМатрица.Запись> insert { get; set; } = new List<АссортиментнаяМатрица.Запись>();
}

[ProtoContract] public sealed class БезопасноеХранилищеДанных
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Владелец { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public БезопасноеХранилищеДанных.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity Данные { get; set; }
	}
	[ProtoMember(1)] public БезопасноеХранилищеДанных.Ключ delete { get; set; }
	[ProtoMember(2)] public List<БезопасноеХранилищеДанных.Запись> insert { get; set; } = new List<БезопасноеХранилищеДанных.Запись>();
}

[ProtoContract] public sealed class БэкМаржа
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Номенклатура { get; set; }
		[ProtoMember(3)] public DateTime Период { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public БэкМаржа.Ключ Ключ { get; set; }
		[ProtoMember(2)] public double Процент { get; set; }
	}
	[ProtoMember(1)] public БэкМаржа.Ключ delete { get; set; }
	[ProtoMember(2)] public List<БэкМаржа.Запись> insert { get; set; } = new List<БэкМаржа.Запись>();
}

[ProtoContract] public sealed class ВидыКонтрагентов
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity ВидКонтрагента { get; set; }
		[ProtoMember(3)] public Entity Контрагент { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ВидыКонтрагентов.Ключ Ключ { get; set; }
	}
	[ProtoMember(1)] public ВидыКонтрагентов.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ВидыКонтрагентов.Запись> insert { get; set; } = new List<ВидыКонтрагентов.Запись>();
}

[ProtoContract] public sealed class ГотовностьКПланированиюДокументовДоставки
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity АдресХранения { get; set; }
		[ProtoMember(3)] public Union ДокументДоставки { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ГотовностьКПланированиюДокументовДоставки.Ключ Ключ { get; set; }
	}
	[ProtoMember(1)] public ГотовностьКПланированиюДокументовДоставки.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ГотовностьКПланированиюДокументовДоставки.Запись> insert { get; set; } = new List<ГотовностьКПланированиюДокументовДоставки.Запись>();
}

[ProtoContract] public sealed class ГрафикиРаботыКурьеров
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ГрафикиРаботыКурьеров.Ключ Ключ { get; set; }
		[ProtoMember(2)] public DateTime НачалоИнтервала { get; set; }
		[ProtoMember(3)] public Entity Сотрудник { get; set; }
		[ProtoMember(4)] public Entity Ответственный { get; set; }
		[ProtoMember(5)] public Entity Зона { get; set; }
		[ProtoMember(6)] public DateTime КонецИнтервала { get; set; }
		[ProtoMember(7)] public Entity Подразделение { get; set; }
		[ProtoMember(8)] public Entity ПрофильПередвижения { get; set; }
		[ProtoMember(9)] public Entity ТипЗанятости { get; set; }
		[ProtoMember(10)] public Entity ФормаРасчета { get; set; }
	}
	[ProtoMember(1)] public ГрафикиРаботыКурьеров.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ГрафикиРаботыКурьеров.Запись> insert { get; set; } = new List<ГрафикиРаботыКурьеров.Запись>();
}

[ProtoContract] public sealed class ГрафикРаботы
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ГрафикРаботы.Ключ Ключ { get; set; }
		[ProtoMember(2)] public DateTime ВремяВыхода { get; set; }
		[ProtoMember(3)] public DateTime День { get; set; }
		[ProtoMember(4)] public Entity Должность { get; set; }
		[ProtoMember(5)] public DateTime Месяц { get; set; }
		[ProtoMember(6)] public Entity Профиль { get; set; }
		[ProtoMember(7)] public Entity Регион { get; set; }
		[ProtoMember(8)] public Entity Сотрудник { get; set; }
		[ProtoMember(9)] public Entity ТипДня { get; set; }
		[ProtoMember(10)] public bool Заявка { get; set; }
		[ProtoMember(11)] public DateTime Период { get; set; }
	}
	[ProtoMember(1)] public ГрафикРаботы.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ГрафикРаботы.Запись> insert { get; set; } = new List<ГрафикРаботы.Запись>();
}

[ProtoContract] public sealed class ГруппыЗакупкиНоменклатуры
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity ГруппаЗакупки { get; set; }
		[ProtoMember(3)] public Entity Номенклатура { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ГруппыЗакупкиНоменклатуры.Ключ Ключ { get; set; }
	}
	[ProtoMember(1)] public ГруппыЗакупкиНоменклатуры.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ГруппыЗакупкиНоменклатуры.Запись> insert { get; set; } = new List<ГруппыЗакупкиНоменклатуры.Запись>();
}

[ProtoContract] public sealed class ГруппыЗначенийДоступа
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public double ГруппаДанных { get; set; }
		[ProtoMember(3)] public Union ГруппаЗначенийДоступа { get; set; }
		[ProtoMember(4)] public Union ЗначениеДоступа { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ГруппыЗначенийДоступа.Ключ Ключ { get; set; }
	}
	[ProtoMember(1)] public ГруппыЗначенийДоступа.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ГруппыЗначенийДоступа.Запись> insert { get; set; } = new List<ГруппыЗначенийДоступа.Запись>();
}

[ProtoContract] public sealed class ДанныеОплатыЗаказовКлиентов
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity ЗаказКлиента { get; set; }
		[ProtoMember(3)] public Entity ФормаОплаты { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ДанныеОплатыЗаказовКлиентов.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity Курьер { get; set; }
		[ProtoMember(3)] public string RRN { get; set; }
		[ProtoMember(4)] public Entity БанкЭквайер { get; set; }
		[ProtoMember(5)] public DateTime ДатаПлатежа { get; set; }
		[ProtoMember(6)] public string КодАвторизации { get; set; }
		[ProtoMember(7)] public string НомерКарты { get; set; }
		[ProtoMember(8)] public string НомерТранзакции { get; set; }
		[ProtoMember(9)] public Entity СпособОнлайнОплаты { get; set; }
		[ProtoMember(10)] public double СуммаПлатежа { get; set; }
	}
	[ProtoMember(1)] public ДанныеОплатыЗаказовКлиентов.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ДанныеОплатыЗаказовКлиентов.Запись> insert { get; set; } = new List<ДанныеОплатыЗаказовКлиентов.Запись>();
}

[ProtoContract] public sealed class ДанныеПроизводственногоКалендаря
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public double Год { get; set; }
		[ProtoMember(3)] public DateTime Дата { get; set; }
		[ProtoMember(4)] public Entity ПроизводственныйКалендарь { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ДанныеПроизводственногоКалендаря.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity ВидДня { get; set; }
		[ProtoMember(3)] public DateTime ДатаПереноса { get; set; }
	}
	[ProtoMember(1)] public ДанныеПроизводственногоКалендаря.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ДанныеПроизводственногоКалендаря.Запись> insert { get; set; } = new List<ДанныеПроизводственногоКалендаря.Запись>();
}

[ProtoContract] public sealed class ДатыЗапретаИзменения
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Union Объект { get; set; }
		[ProtoMember(3)] public Union Пользователь { get; set; }
		[ProtoMember(4)] public Entity Раздел { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ДатыЗапретаИзменения.Ключ Ключ { get; set; }
		[ProtoMember(2)] public string Комментарий { get; set; }
		[ProtoMember(3)] public string ОписаниеДатыЗапрета { get; set; }
		[ProtoMember(4)] public DateTime ДатаЗапрета { get; set; }
	}
	[ProtoMember(1)] public ДатыЗапретаИзменения.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ДатыЗапретаИзменения.Запись> insert { get; set; } = new List<ДатыЗапретаИзменения.Запись>();
}

[ProtoContract] public sealed class ДвоичныеДанныеФайлов
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Union Файл { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ДвоичныеДанныеФайлов.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity ДвоичныеДанныеФайла { get; set; }
	}
	[ProtoMember(1)] public ДвоичныеДанныеФайлов.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ДвоичныеДанныеФайлов.Запись> insert { get; set; } = new List<ДвоичныеДанныеФайлов.Запись>();
}

[ProtoContract] public sealed class ДобавочныеНомераVoximplant
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity ОснованиеЗвонка { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ДобавочныеНомераVoximplant.Ключ Ключ { get; set; }
		[ProtoMember(2)] public string ДобавочныйНомер { get; set; }
	}
	[ProtoMember(1)] public ДобавочныеНомераVoximplant.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ДобавочныеНомераVoximplant.Запись> insert { get; set; } = new List<ДобавочныеНомераVoximplant.Запись>();
}

[ProtoContract] public sealed class ДолжностиСотрудника
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Должность { get; set; }
		[ProtoMember(3)] public Entity Сотрудник { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ДолжностиСотрудника.Ключ Ключ { get; set; }
		[ProtoMember(2)] public bool РасчетДоплатПоИнтервалуВЗаказеКлиента { get; set; }
	}
	[ProtoMember(1)] public ДолжностиСотрудника.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ДолжностиСотрудника.Запись> insert { get; set; } = new List<ДолжностиСотрудника.Запись>();
}

[ProtoContract] public sealed class ДомаЗданияСтроения
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity АдресныйОбъект { get; set; }
		[ProtoMember(3)] public Entity ДополнительныеАдресныеСведения { get; set; }
		[ProtoMember(4)] public double КодСубъектаРФ { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ДомаЗданияСтроения.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity Строения { get; set; }
	}
	[ProtoMember(1)] public ДомаЗданияСтроения.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ДомаЗданияСтроения.Запись> insert { get; set; } = new List<ДомаЗданияСтроения.Запись>();
}

[ProtoContract] public sealed class ДополнительныеАдресныеСведения
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Идентификатор { get; set; }
		[ProtoMember(3)] public double КодСубъектаРФ { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ДополнительныеАдресныеСведения.Ключ Ключ { get; set; }
		[ProtoMember(2)] public double OKATO { get; set; }
		[ProtoMember(3)] public double КодИФНСФЛ { get; set; }
		[ProtoMember(4)] public double КодИФНСЮЛ { get; set; }
		[ProtoMember(5)] public double КодУчасткаИФНСФЛ { get; set; }
		[ProtoMember(6)] public double КодУчасткаИФНСЮЛ { get; set; }
		[ProtoMember(7)] public double ОКТМО { get; set; }
		[ProtoMember(8)] public double ПочтовыйИндекс { get; set; }
	}
	[ProtoMember(1)] public ДополнительныеАдресныеСведения.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ДополнительныеАдресныеСведения.Запись> insert { get; set; } = new List<ДополнительныеАдресныеСведения.Запись>();
}

[ProtoContract] public sealed class ДопустимыеИнтервалыРассылкиСМС
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регион { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ДопустимыеИнтервалыРассылкиСМС.Ключ Ключ { get; set; }
		[ProtoMember(2)] public DateTime НачалоИнтервала { get; set; }
		[ProtoMember(3)] public DateTime ОкончаниеИнтервала { get; set; }
	}
	[ProtoMember(1)] public ДопустимыеИнтервалыРассылкиСМС.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ДопустимыеИнтервалыРассылкиСМС.Запись> insert { get; set; } = new List<ДопустимыеИнтервалыРассылкиСМС.Запись>();
}

[ProtoContract] public sealed class ДоступКТипамПроблем
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity ГруппаДоступа { get; set; }
		[ProtoMember(3)] public Entity ТипПроблемы { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ДоступКТипамПроблем.Ключ Ключ { get; set; }
	}
	[ProtoMember(1)] public ДоступКТипамПроблем.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ДоступКТипамПроблем.Запись> insert { get; set; } = new List<ДоступКТипамПроблем.Запись>();
}

[ProtoContract] public sealed class ДоступностьПровайдеровПоЭлектроннымРецептам
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity АдресХранения { get; set; }
		[ProtoMember(3)] public Entity Курьер { get; set; }
		[ProtoMember(4)] public Entity Провайдер { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ДоступностьПровайдеровПоЭлектроннымРецептам.Ключ Ключ { get; set; }
	}
	[ProtoMember(1)] public ДоступностьПровайдеровПоЭлектроннымРецептам.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ДоступностьПровайдеровПоЭлектроннымРецептам.Запись> insert { get; set; } = new List<ДоступностьПровайдеровПоЭлектроннымРецептам.Запись>();
}

[ProtoContract] public sealed class ДоступныеСпособыОнлайнОплаты
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity БанкЭквайер { get; set; }
		[ProtoMember(3)] public Entity СпособОнлайнОплаты { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ДоступныеСпособыОнлайнОплаты.Ключ Ключ { get; set; }
		[ProtoMember(2)] public bool Доступен { get; set; }
		[ProtoMember(3)] public bool ПроверкаКурьера { get; set; }
	}
	[ProtoMember(1)] public ДоступныеСпособыОнлайнОплаты.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ДоступныеСпособыОнлайнОплаты.Запись> insert { get; set; } = new List<ДоступныеСпособыОнлайнОплаты.Запись>();
}

[ProtoContract] public sealed class ЗависимостиПравДоступа
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity ПодчиненнаяТаблица { get; set; }
		[ProtoMember(3)] public Union ТипВедущейТаблицы { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ЗависимостиПравДоступа.Ключ Ключ { get; set; }
	}
	[ProtoMember(1)] public ЗависимостиПравДоступа.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ЗависимостиПравДоступа.Запись> insert { get; set; } = new List<ЗависимостиПравДоступа.Запись>();
}

[ProtoContract] public sealed class ЗагруженныеВерсииАдресныхСведений
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public double КодСубъектаРФ { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ЗагруженныеВерсииАдресныхСведений.Ключ Ключ { get; set; }
		[ProtoMember(2)] public string Версия { get; set; }
		[ProtoMember(3)] public DateTime ДатаВерсии { get; set; }
		[ProtoMember(4)] public DateTime ДатаЗагрузки { get; set; }
	}
	[ProtoMember(1)] public ЗагруженныеВерсииАдресныхСведений.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ЗагруженныеВерсииАдресныхСведений.Запись> insert { get; set; } = new List<ЗагруженныеВерсииАдресныхСведений.Запись>();
}

[ProtoContract] public sealed class ЗаказыКлиентаКВозвратуКурьером
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity ВидОперации { get; set; }
		[ProtoMember(3)] public DateTime ДатаВозврата { get; set; }
		[ProtoMember(4)] public Entity ЗаданиеНаДоставку { get; set; }
		[ProtoMember(5)] public Entity ЗаказКлиента { get; set; }
		[ProtoMember(6)] public Entity Курьер { get; set; }
		[ProtoMember(7)] public Entity ОтчетОДоставке { get; set; }
		[ProtoMember(8)] public Entity УпаковкаЗаказаКлиента { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ЗаказыКлиентаКВозвратуКурьером.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity ЗакрытиеСменыКурьера { get; set; }
		[ProtoMember(3)] public Entity ЧекККМ { get; set; }
	}
	[ProtoMember(1)] public ЗаказыКлиентаКВозвратуКурьером.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ЗаказыКлиентаКВозвратуКурьером.Запись> insert { get; set; } = new List<ЗаказыКлиентаКВозвратуКурьером.Запись>();
}

[ProtoContract] public sealed class ЗаказыКлиентаКВыдачеКурьеру
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public DateTime ВремяВыходаПоГрафику { get; set; }
		[ProtoMember(3)] public DateTime ДатаДоставки { get; set; }
		[ProtoMember(4)] public Entity ЗаданиеНаДоставку { get; set; }
		[ProtoMember(5)] public Entity ЗаказКлиента { get; set; }
		[ProtoMember(6)] public Entity Курьер { get; set; }
		[ProtoMember(7)] public Entity УпаковкаЗаказаКлиента { get; set; }
		[ProtoMember(8)] public Entity ЧекККМ { get; set; }
		[ProtoMember(9)] public Entity Ячейка { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ЗаказыКлиентаКВыдачеКурьеру.Ключ Ключ { get; set; }
	}
	[ProtoMember(1)] public ЗаказыКлиентаКВыдачеКурьеру.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ЗаказыКлиентаКВыдачеКурьеру.Запись> insert { get; set; } = new List<ЗаказыКлиентаКВыдачеКурьеру.Запись>();
}

[ProtoContract] public sealed class ЗаказыКлиентовВДоставкеКОтмене
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity ЗаказКлиента { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ЗаказыКлиентовВДоставкеКОтмене.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity Пользователь { get; set; }
		[ProtoMember(3)] public Entity ПричинаОтмены { get; set; }
		[ProtoMember(4)] public DateTime ДатаОтмены { get; set; }
		[ProtoMember(5)] public bool Обработан { get; set; }
	}
	[ProtoMember(1)] public ЗаказыКлиентовВДоставкеКОтмене.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ЗаказыКлиентовВДоставкеКОтмене.Запись> insert { get; set; } = new List<ЗаказыКлиентовВДоставкеКОтмене.Запись>();
}

[ProtoContract] public sealed class ЗаказыКлиентовВМаршрутныхЛистах
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ЗаказыКлиентовВМаршрутныхЛистах.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Union ЗаказКлиента { get; set; }
		[ProtoMember(3)] public Entity Склад { get; set; }
		[ProtoMember(4)] public DateTime ДатаВыхода { get; set; }
		[ProtoMember(5)] public DateTime ДатаДоставки { get; set; }
		[ProtoMember(6)] public Union Курьер { get; set; }
		[ProtoMember(7)] public Entity МаршрутныйЛист { get; set; }
		[ProtoMember(8)] public DateTime ПлановоеВремяПрибытия { get; set; }
		[ProtoMember(9)] public double Порядок { get; set; }
		[ProtoMember(10)] public Entity УдалитьСклад { get; set; }
		[ProtoMember(11)] public DateTime Период { get; set; }
		[ProtoMember(12)] public Entity ПунктСамовывоза { get; set; }
		[ProtoMember(13)] public string НомерЗаказаСокращенный { get; set; }
	}
	[ProtoMember(1)] public ЗаказыКлиентовВМаршрутныхЛистах.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ЗаказыКлиентовВМаршрутныхЛистах.Запись> insert { get; set; } = new List<ЗаказыКлиентовВМаршрутныхЛистах.Запись>();
}

[ProtoContract] public sealed class ЗаказыКлиентовКПодборуСертификатов
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity ЗаказКлиента { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ЗаказыКлиентовКПодборуСертификатов.Ключ Ключ { get; set; }
		[ProtoMember(2)] public bool СертификатыПодобраны { get; set; }
	}
	[ProtoMember(1)] public ЗаказыКлиентовКПодборуСертификатов.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ЗаказыКлиентовКПодборуСертификатов.Запись> insert { get; set; } = new List<ЗаказыКлиентовКПодборуСертификатов.Запись>();
}

[ProtoContract] public sealed class ЗаказыКлиентовСБП
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity ЗаказКлиента { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ЗаказыКлиентовСБП.Ключ Ключ { get; set; }
		[ProtoMember(2)] public DateTime ДатаЗапроса { get; set; }
		[ProtoMember(3)] public Entity ОтветСервиса { get; set; }
		[ProtoMember(4)] public string СсылкаНаОплату { get; set; }
	}
	[ProtoMember(1)] public ЗаказыКлиентовСБП.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ЗаказыКлиентовСБП.Запись> insert { get; set; } = new List<ЗаказыКлиентовСБП.Запись>();
}

[ProtoContract] public sealed class ЗаказыМаркетплейсов
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity ЗаказКлиента { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ЗаказыМаркетплейсов.Ключ Ключ { get; set; }
		[ProtoMember(2)] public string ВнешнийНомерЗаказа { get; set; }
		[ProtoMember(3)] public Entity Проект { get; set; }
	}
	[ProtoMember(1)] public ЗаказыМаркетплейсов.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ЗаказыМаркетплейсов.Запись> insert { get; set; } = new List<ЗаказыМаркетплейсов.Запись>();
}

[ProtoContract] public sealed class ЗаказыПеревозчиков
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Union ДокументДоставки { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ЗаказыПеревозчиков.Ключ Ключ { get; set; }
		[ProtoMember(2)] public string IDЗаказаНаДоставку { get; set; }
		[ProtoMember(3)] public string НомерЗаказаНаДоставку { get; set; }
		[ProtoMember(4)] public Entity Перевозчик { get; set; }
		[ProtoMember(5)] public string ТелефонКурьера { get; set; }
		[ProtoMember(6)] public string ФИОКурьера { get; set; }
	}
	[ProtoMember(1)] public ЗаказыПеревозчиков.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ЗаказыПеревозчиков.Запись> insert { get; set; } = new List<ЗаказыПеревозчиков.Запись>();
}

[ProtoContract] public sealed class ЗаказыСИзменившемсяСатусом
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Заказ { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ЗаказыСИзменившемсяСатусом.Ключ Ключ { get; set; }
	}
	[ProtoMember(1)] public ЗаказыСИзменившемсяСатусом.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ЗаказыСИзменившемсяСатусом.Запись> insert { get; set; } = new List<ЗаказыСИзменившемсяСатусом.Запись>();
}

[ProtoContract] public sealed class ЗаказыСПриостановленнымПроцессингом
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity ЗаказКлиента { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ЗаказыСПриостановленнымПроцессингом.Ключ Ключ { get; set; }
		[ProtoMember(2)] public DateTime Дата { get; set; }
		[ProtoMember(3)] public Entity Причина { get; set; }
	}
	[ProtoMember(1)] public ЗаказыСПриостановленнымПроцессингом.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ЗаказыСПриостановленнымПроцессингом.Запись> insert { get; set; } = new List<ЗаказыСПриостановленнымПроцессингом.Запись>();
}

[ProtoContract] public sealed class ЗапросыККТ
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity АдресХранения { get; set; }
		[ProtoMember(3)] public Entity ККМ { get; set; }
		[ProtoMember(4)] public Entity КомандаККТ { get; set; }
		[ProtoMember(5)] public Entity ТипЗаписи { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ЗапросыККТ.Ключ Ключ { get; set; }
		[ProtoMember(2)] public DateTime ВремяЗаписи { get; set; }
		[ProtoMember(3)] public string ТекстОшибки { get; set; }
		[ProtoMember(4)] public Union Результат { get; set; }
	}
	[ProtoMember(1)] public ЗапросыККТ.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ЗапросыККТ.Запись> insert { get; set; } = new List<ЗапросыККТ.Запись>();
}

[ProtoContract] public sealed class ЗвонкиАТС
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public string ИдентификаторЗвонка { get; set; }
		[ProtoMember(3)] public Entity СервисТелефонии { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ЗвонкиАТС.Ключ Ключ { get; set; }
		[ProtoMember(2)] public string Комментарий { get; set; }
		[ProtoMember(3)] public DateTime ВремяЗвонка { get; set; }
		[ProtoMember(4)] public Entity ДанныеЗвонка { get; set; }
		[ProtoMember(5)] public double ДлительностьЗвонка { get; set; }
		[ProtoMember(6)] public double ДлительностьОжидания { get; set; }
		[ProtoMember(7)] public Entity ПредметЗвонка { get; set; }
		[ProtoMember(8)] public Entity ТипЗвонка { get; set; }
		[ProtoMember(9)] public double ДлительностьРазговора { get; set; }
	}
	[ProtoMember(1)] public ЗвонкиАТС.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ЗвонкиАТС.Запись> insert { get; set; } = new List<ЗвонкиАТС.Запись>();
}

[ProtoContract] public sealed class ЗемельныеУчастки
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity АдресныйОбъект { get; set; }
		[ProtoMember(3)] public Entity ДополнительныеАдресныеСведения { get; set; }
		[ProtoMember(4)] public double КодСубъектаРФ { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ЗемельныеУчастки.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity Участки { get; set; }
	}
	[ProtoMember(1)] public ЗемельныеУчастки.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ЗемельныеУчастки.Запись> insert { get; set; } = new List<ЗемельныеУчастки.Запись>();
}

[ProtoContract] public sealed class ЗначенияГруппДоступа
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity ГруппаДоступа { get; set; }
		[ProtoMember(3)] public Union ЗначениеДоступа { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ЗначенияГруппДоступа.Ключ Ключ { get; set; }
		[ProtoMember(2)] public bool ЗначениеРазрешено { get; set; }
	}
	[ProtoMember(1)] public ЗначенияГруппДоступа.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ЗначенияГруппДоступа.Запись> insert { get; set; } = new List<ЗначенияГруппДоступа.Запись>();
}

[ProtoContract] public sealed class ЗначенияГруппДоступаПоУмолчанию
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity ГруппаДоступа { get; set; }
		[ProtoMember(3)] public Union ТипЗначенийДоступа { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ЗначенияГруппДоступаПоУмолчанию.Ключ Ключ { get; set; }
		[ProtoMember(2)] public bool БезНастройки { get; set; }
		[ProtoMember(3)] public bool ВсеРазрешеныБезИсключений { get; set; }
		[ProtoMember(4)] public bool ВсеРазрешены { get; set; }
	}
	[ProtoMember(1)] public ЗначенияГруппДоступаПоУмолчанию.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ЗначенияГруппДоступаПоУмолчанию.Запись> insert { get; set; } = new List<ЗначенияГруппДоступаПоУмолчанию.Запись>();
}

[ProtoContract] public sealed class ЗоныДоставкиПеревозчиков
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity ЗонаДоставки { get; set; }
		[ProtoMember(3)] public Entity Перевозчик { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ЗоныДоставкиПеревозчиков.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity ТочкаСамовывоза { get; set; }
	}
	[ProtoMember(1)] public ЗоныДоставкиПеревозчиков.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ЗоныДоставкиПеревозчиков.Запись> insert { get; set; } = new List<ЗоныДоставкиПеревозчиков.Запись>();
}

[ProtoContract] public sealed class ЗоныДоставкиСПроцессингом1С8
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity АдресХранения { get; set; }
		[ProtoMember(3)] public Entity ЗонаДоставки { get; set; }
		[ProtoMember(4)] public Entity Регион { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ЗоныДоставкиСПроцессингом1С8.Ключ Ключ { get; set; }
		[ProtoMember(2)] public bool ДоставкаЗаЧас { get; set; }
		[ProtoMember(3)] public double ПорогCуммыБесплатнойДоставки { get; set; }
		[ProtoMember(4)] public double Приоритет { get; set; }
		[ProtoMember(5)] public double СтоимостьДоставки { get; set; }
		[ProtoMember(6)] public bool СборкаЗаказовАптек { get; set; }
	}
	[ProtoMember(1)] public ЗоныДоставкиСПроцессингом1С8.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ЗоныДоставкиСПроцессингом1С8.Запись> insert { get; set; } = new List<ЗоныДоставкиСПроцессингом1С8.Запись>();
}

[ProtoContract] public sealed class ЗСЯНоменклатуры
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public string Зона { get; set; }
		[ProtoMember(3)] public Entity МестоЗСЯ { get; set; }
		[ProtoMember(4)] public Entity Номенклатура { get; set; }
		[ProtoMember(5)] public Entity Склад { get; set; }
		[ProtoMember(6)] public string Стеллаж { get; set; }
		[ProtoMember(7)] public string Ячейка { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ЗСЯНоменклатуры.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity ЗонаСсылка { get; set; }
	}
	[ProtoMember(1)] public ЗСЯНоменклатуры.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ЗСЯНоменклатуры.Запись> insert { get; set; } = new List<ЗСЯНоменклатуры.Запись>();
}

[ProtoContract] public sealed class ИдентификаторыМДЛП
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Union ОбъектМДЛП { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ИдентификаторыМДЛП.Ключ Ключ { get; set; }
		[ProtoMember(2)] public bool Основной { get; set; }
		[ProtoMember(3)] public string Идентификатор { get; set; }
	}
	[ProtoMember(1)] public ИдентификаторыМДЛП.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ИдентификаторыМДЛП.Запись> insert { get; set; } = new List<ИдентификаторыМДЛП.Запись>();
}

[ProtoContract] public sealed class Инвентаризация_Сканирование
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public bool ДобавленноВручную { get; set; }
		[ProtoMember(3)] public Entity ДокументИнвентаризации { get; set; }
		[ProtoMember(4)] public Entity МестоЗСЯ { get; set; }
		[ProtoMember(5)] public double НомерПросчета { get; set; }
		[ProtoMember(6)] public Entity Склад { get; set; }
		[ProtoMember(7)] public Entity Сотрудник { get; set; }
		[ProtoMember(8)] public Entity Товар { get; set; }
		[ProtoMember(9)] public DateTime Период { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public Инвентаризация_Сканирование.Ключ Ключ { get; set; }
		[ProtoMember(2)] public double Количество { get; set; }
		[ProtoMember(3)] public string ОписаниеОшибки { get; set; }
		[ProtoMember(4)] public bool Ошибка { get; set; }
		[ProtoMember(5)] public string ШК { get; set; }
	}
	[ProtoMember(1)] public Инвентаризация_Сканирование.Ключ delete { get; set; }
	[ProtoMember(2)] public List<Инвентаризация_Сканирование.Запись> insert { get; set; } = new List<Инвентаризация_Сканирование.Запись>();
}

[ProtoContract] public sealed class ИсполнителиЗадачПоПретензиям
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Исполнитель { get; set; }
		[ProtoMember(3)] public Union Подразделение { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ИсполнителиЗадачПоПретензиям.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity Должность { get; set; }
	}
	[ProtoMember(1)] public ИсполнителиЗадачПоПретензиям.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ИсполнителиЗадачПоПретензиям.Запись> insert { get; set; } = new List<ИсполнителиЗадачПоПретензиям.Запись>();
}

[ProtoContract] public sealed class ИспользуемыеВидыДоступа
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Union ТипЗначенийДоступа { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ИспользуемыеВидыДоступа.Ключ Ключ { get; set; }
	}
	[ProtoMember(1)] public ИспользуемыеВидыДоступа.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ИспользуемыеВидыДоступа.Запись> insert { get; set; } = new List<ИспользуемыеВидыДоступа.Запись>();
}

[ProtoContract] public sealed class ИспользуемыеВидыДоступаПоТаблицам
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Union Таблица { get; set; }
		[ProtoMember(3)] public Union ТипЗначенийДоступа { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ИспользуемыеВидыДоступаПоТаблицам.Ключ Ключ { get; set; }
	}
	[ProtoMember(1)] public ИспользуемыеВидыДоступаПоТаблицам.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ИспользуемыеВидыДоступаПоТаблицам.Запись> insert { get; set; } = new List<ИспользуемыеВидыДоступаПоТаблицам.Запись>();
}

[ProtoContract] public sealed class ИсторияАдресныхОбъектов
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Идентификатор { get; set; }
		[ProtoMember(3)] public double КодСубъектаРФ { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ИсторияАдресныхОбъектов.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity АдресныйОбъект { get; set; }
		[ProtoMember(3)] public Entity ДополнительныеАдресныеСведения { get; set; }
		[ProtoMember(4)] public double КодКЛАДР { get; set; }
		[ProtoMember(5)] public Entity МуниципальныйРодительскийИдентификатор { get; set; }
		[ProtoMember(6)] public string Наименование { get; set; }
		[ProtoMember(7)] public DateTime НачалоДействияЗаписи { get; set; }
		[ProtoMember(8)] public DateTime ОкончаниеДействияЗаписи { get; set; }
		[ProtoMember(9)] public double Операция { get; set; }
		[ProtoMember(10)] public Entity РодительскийИдентификатор { get; set; }
		[ProtoMember(11)] public string Сокращение { get; set; }
		[ProtoMember(12)] public double ТекущийКодСубъектаРФ { get; set; }
		[ProtoMember(13)] public double Уровень { get; set; }
	}
	[ProtoMember(1)] public ИсторияАдресныхОбъектов.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ИсторияАдресныхОбъектов.Запись> insert { get; set; } = new List<ИсторияАдресныхОбъектов.Запись>();
}

[ProtoContract] public sealed class ИсторияЗаданийКурьерамПоЗаказамКлиентов
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Union ДокументРегистратор { get; set; }
		[ProtoMember(3)] public Entity ЗаказКлиента { get; set; }
		[ProtoMember(4)] public DateTime Период { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ИсторияЗаданийКурьерамПоЗаказамКлиентов.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity Курьер { get; set; }
	}
	[ProtoMember(1)] public ИсторияЗаданийКурьерамПоЗаказамКлиентов.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ИсторияЗаданийКурьерамПоЗаказамКлиентов.Запись> insert { get; set; } = new List<ИсторияЗаданийКурьерамПоЗаказамКлиентов.Запись>();
}

[ProtoContract] public sealed class ИсторияСтатусовЗаказовКлиентов
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity ЗаказКлиента { get; set; }
		[ProtoMember(3)] public DateTime Период { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ИсторияСтатусовЗаказовКлиентов.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Union Пользователь { get; set; }
		[ProtoMember(3)] public bool ЕстьПроблема { get; set; }
		[ProtoMember(4)] public Entity ПричинаПроблемы { get; set; }
		[ProtoMember(5)] public string ТекстПроблемы { get; set; }
		[ProtoMember(6)] public Entity ТипПроблемы { get; set; }
		[ProtoMember(7)] public bool УдалитьОтменен { get; set; }
		[ProtoMember(8)] public bool УдалитьПозвонитьКлиенту { get; set; }
		[ProtoMember(9)] public Entity Статус { get; set; }
	}
	[ProtoMember(1)] public ИсторияСтатусовЗаказовКлиентов.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ИсторияСтатусовЗаказовКлиентов.Запись> insert { get; set; } = new List<ИсторияСтатусовЗаказовКлиентов.Запись>();
}

[ProtoContract] public sealed class КалендарныеГрафики
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public double Год { get; set; }
		[ProtoMember(3)] public DateTime ДатаГрафика { get; set; }
		[ProtoMember(4)] public Entity Календарь { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public КалендарныеГрафики.Ключ Ключ { get; set; }
		[ProtoMember(2)] public bool ДеньВключенВГрафик { get; set; }
		[ProtoMember(3)] public double КоличествоДнейВГрафикеСНачалаГода { get; set; }
	}
	[ProtoMember(1)] public КалендарныеГрафики.Ключ delete { get; set; }
	[ProtoMember(2)] public List<КалендарныеГрафики.Запись> insert { get; set; } = new List<КалендарныеГрафики.Запись>();
}

[ProtoContract] public sealed class КарточныеПрограммыТоварыСоСкидками
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Товар { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public КарточныеПрограммыТоварыСоСкидками.Ключ Ключ { get; set; }
		[ProtoMember(2)] public string Сообщение { get; set; }
		[ProtoMember(3)] public Entity ТипСкидочнойКарты { get; set; }
	}
	[ProtoMember(1)] public КарточныеПрограммыТоварыСоСкидками.Ключ delete { get; set; }
	[ProtoMember(2)] public List<КарточныеПрограммыТоварыСоСкидками.Запись> insert { get; set; } = new List<КарточныеПрограммыТоварыСоСкидками.Запись>();
}

[ProtoContract] public sealed class КомиссииПартнеров
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public КомиссииПартнеров.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity БазаРасчета { get; set; }
		[ProtoMember(3)] public Entity Контрагент { get; set; }
		[ProtoMember(4)] public double МаксЦена { get; set; }
		[ProtoMember(5)] public Entity РегионРаботы { get; set; }
		[ProtoMember(6)] public Entity ТипДоставки { get; set; }
		[ProtoMember(7)] public Entity Договор { get; set; }
		[ProtoMember(8)] public double ПроцентКомиссии { get; set; }
		[ProtoMember(9)] public DateTime Период { get; set; }
	}
	[ProtoMember(1)] public КомиссииПартнеров.Ключ delete { get; set; }
	[ProtoMember(2)] public List<КомиссииПартнеров.Запись> insert { get; set; } = new List<КомиссииПартнеров.Запись>();
}

[ProtoContract] public sealed class КомплектацияЗаказов_РаспределениеЗаказовПоЗСЯ
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public КомплектацияЗаказов_РаспределениеЗаказовПоЗСЯ.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity ЗСЯ { get; set; }
		[ProtoMember(3)] public Entity Заказ { get; set; }
		[ProtoMember(4)] public DateTime Период { get; set; }
	}
	[ProtoMember(1)] public КомплектацияЗаказов_РаспределениеЗаказовПоЗСЯ.Ключ delete { get; set; }
	[ProtoMember(2)] public List<КомплектацияЗаказов_РаспределениеЗаказовПоЗСЯ.Запись> insert { get; set; } = new List<КомплектацияЗаказов_РаспределениеЗаказовПоЗСЯ.Запись>();
}

[ProtoContract] public sealed class КоординатыЗаказовКлиентов
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Union ЗаказКлиента { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public КоординатыЗаказовКлиентов.Ключ Ключ { get; set; }
		[ProtoMember(2)] public double Долгота { get; set; }
		[ProtoMember(3)] public double Широта { get; set; }
	}
	[ProtoMember(1)] public КоординатыЗаказовКлиентов.Ключ delete { get; set; }
	[ProtoMember(2)] public List<КоординатыЗаказовКлиентов.Запись> insert { get; set; } = new List<КоординатыЗаказовКлиентов.Запись>();
}

[ProtoContract] public sealed class ЛогОперацийСберСпасибо
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity ВидОперации { get; set; }
		[ProtoMember(3)] public Entity Документ { get; set; }
		[ProtoMember(4)] public DateTime Период { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ЛогОперацийСберСпасибо.Ключ Ключ { get; set; }
		[ProtoMember(2)] public string АдресПокупки { get; set; }
		[ProtoMember(3)] public string ЗапросВСервис { get; set; }
		[ProtoMember(4)] public string ИдентификаторДокумента { get; set; }
		[ProtoMember(5)] public double ИдентификаторДокументаВСервисе { get; set; }
		[ProtoMember(6)] public string ИдентификаторОперации { get; set; }
		[ProtoMember(7)] public double ИдентификаторСети { get; set; }
		[ProtoMember(8)] public string ИдентификаторТерминала { get; set; }
		[ProtoMember(9)] public double ИдентификаторТорговогоПредприятия { get; set; }
		[ProtoMember(10)] public string ИдентификаторТорговойТочки { get; set; }
		[ProtoMember(11)] public string КодОшибки { get; set; }
		[ProtoMember(12)] public string ОписаниеОшибки { get; set; }
		[ProtoMember(13)] public string ОтветИзСервиса { get; set; }
		[ProtoMember(14)] public string ПредставлениеДокумента { get; set; }
		[ProtoMember(15)] public Entity ПричинаЗапретаСписанияБонусов { get; set; }
		[ProtoMember(16)] public Entity Статус { get; set; }
	}
	[ProtoMember(1)] public ЛогОперацийСберСпасибо.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ЛогОперацийСберСпасибо.Запись> insert { get; set; } = new List<ЛогОперацийСберСпасибо.Запись>();
}

[ProtoContract] public sealed class ЛогРегламентныхЗаданий
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public string ИдентификаторЗаписи { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ЛогРегламентныхЗаданий.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity ВидЛога { get; set; }
		[ProtoMember(3)] public string ИдентификаторСессии { get; set; }
		[ProtoMember(4)] public Entity ИнформационнаяБаза { get; set; }
		[ProtoMember(5)] public string Комментарий { get; set; }
		[ProtoMember(6)] public string Наименование { get; set; }
		[ProtoMember(7)] public DateTime ПериодЗаписи { get; set; }
		[ProtoMember(8)] public Entity ТипЗаписи { get; set; }
	}
	[ProtoMember(1)] public ЛогРегламентныхЗаданий.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ЛогРегламентныхЗаданий.Запись> insert { get; set; } = new List<ЛогРегламентныхЗаданий.Запись>();
}

[ProtoContract] public sealed class ЛокальныеСогласованияТовараПоставщика
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity ИБ { get; set; }
		[ProtoMember(3)] public string КодТовараПоставщика { get; set; }
		[ProtoMember(4)] public Entity Поставщик { get; set; }
		[ProtoMember(5)] public DateTime Период { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ЛокальныеСогласованияТовараПоставщика.Ключ Ключ { get; set; }
		[ProtoMember(2)] public string Наименование { get; set; }
		[ProtoMember(3)] public string Производитель { get; set; }
		[ProtoMember(4)] public Entity Товар { get; set; }
	}
	[ProtoMember(1)] public ЛокальныеСогласованияТовараПоставщика.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ЛокальныеСогласованияТовараПоставщика.Запись> insert { get; set; } = new List<ЛокальныеСогласованияТовараПоставщика.Запись>();
}

[ProtoContract] public sealed class МестаХраненияАптеки
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity АдресХранения { get; set; }
		[ProtoMember(3)] public Entity ТипМестаХранения { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public МестаХраненияАптеки.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity МестоХранения { get; set; }
	}
	[ProtoMember(1)] public МестаХраненияАптеки.Ключ delete { get; set; }
	[ProtoMember(2)] public List<МестаХраненияАптеки.Запись> insert { get; set; } = new List<МестаХраненияАптеки.Запись>();
}

[ProtoContract] public sealed class МестаХраненияПоДоговору
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Договор { get; set; }
		[ProtoMember(3)] public string КодПолучателя { get; set; }
		[ProtoMember(4)] public Entity МестоХранения { get; set; }
		[ProtoMember(5)] public Entity Поставщик { get; set; }
		[ProtoMember(6)] public double ТипПрайса { get; set; }
		[ProtoMember(7)] public Entity Фирма { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public МестаХраненияПоДоговору.Ключ Ключ { get; set; }
		[ProtoMember(2)] public string КлючСвязи { get; set; }
		[ProtoMember(3)] public bool Активный { get; set; }
		[ProtoMember(4)] public DateTime ДатаОжидаемойПоставки { get; set; }
		[ProtoMember(5)] public double КратностьУпаковки { get; set; }
		[ProtoMember(6)] public bool ПрайсДоступенОператорам { get; set; }
	}
	[ProtoMember(1)] public МестаХраненияПоДоговору.Ключ delete { get; set; }
	[ProtoMember(2)] public List<МестаХраненияПоДоговору.Запись> insert { get; set; } = new List<МестаХраненияПоДоговору.Запись>();
}

[ProtoContract] public sealed class НаборыЗначенийДоступа
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Union ЗначениеДоступа { get; set; }
		[ProtoMember(3)] public double НомерНабора { get; set; }
		[ProtoMember(4)] public Union Объект { get; set; }
		[ProtoMember(5)] public Entity Уточнение { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public НаборыЗначенийДоступа.Ключ Ключ { get; set; }
		[ProtoMember(2)] public bool ЗначениеБезГрупп { get; set; }
		[ProtoMember(3)] public bool СтандартноеЗначение { get; set; }
		[ProtoMember(4)] public bool Изменение { get; set; }
		[ProtoMember(5)] public bool Чтение { get; set; }
	}
	[ProtoMember(1)] public НаборыЗначенийДоступа.Ключ delete { get; set; }
	[ProtoMember(2)] public List<НаборыЗначенийДоступа.Запись> insert { get; set; } = new List<НаборыЗначенийДоступа.Запись>();
}

[ProtoContract] public sealed class НазначенияПоЭлектроннымРецептам
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity ВидОперации { get; set; }
		[ProtoMember(3)] public Entity Документ { get; set; }
		[ProtoMember(4)] public string ИдентификаторЗапроса { get; set; }
		[ProtoMember(5)] public string ИдентификаторСтроки { get; set; }
		[ProtoMember(6)] public string КодРецепта { get; set; }
		[ProtoMember(7)] public DateTime Период { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public НазначенияПоЭлектроннымРецептам.Ключ Ключ { get; set; }
		[ProtoMember(2)] public string Дозировка { get; set; }
		[ProtoMember(3)] public string ЕдиницаОтпуска { get; set; }
		[ProtoMember(4)] public string КодПрепарата { get; set; }
		[ProtoMember(5)] public string КоличествоЕдиниц { get; set; }
		[ProtoMember(6)] public string ЛекарственнаяФорма { get; set; }
		[ProtoMember(7)] public string НаименованиеМНН { get; set; }
		[ProtoMember(8)] public string ОбозначениеСправочника { get; set; }
		[ProtoMember(9)] public string РецептурноеНазначение { get; set; }
		[ProtoMember(10)] public string СпособПрименения { get; set; }
	}
	[ProtoMember(1)] public НазначенияПоЭлектроннымРецептам.Ключ delete { get; set; }
	[ProtoMember(2)] public List<НазначенияПоЭлектроннымРецептам.Запись> insert { get; set; } = new List<НазначенияПоЭлектроннымРецептам.Запись>();
}

[ProtoContract] public sealed class НаличиеНоменклатурыПоПериодам
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Аптека { get; set; }
		[ProtoMember(3)] public Entity Номенклатура { get; set; }
		[ProtoMember(4)] public Entity СтандартныйПериод { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public НаличиеНоменклатурыПоПериодам.Ключ Ключ { get; set; }
		[ProtoMember(2)] public DateTime ДатаИмпорта { get; set; }
		[ProtoMember(3)] public double КоличествоДней { get; set; }
	}
	[ProtoMember(1)] public НаличиеНоменклатурыПоПериодам.Ключ delete { get; set; }
	[ProtoMember(2)] public List<НаличиеНоменклатурыПоПериодам.Запись> insert { get; set; } = new List<НаличиеНоменклатурыПоПериодам.Запись>();
}

[ProtoContract] public sealed class НаличиеФайлов
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Union ОбъектСФайлами { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public НаличиеФайлов.Ключ Ключ { get; set; }
		[ProtoMember(2)] public string ИдентификаторОбъекта { get; set; }
		[ProtoMember(3)] public bool ЕстьФайлы { get; set; }
	}
	[ProtoMember(1)] public НаличиеФайлов.Ключ delete { get; set; }
	[ProtoMember(2)] public List<НаличиеФайлов.Запись> insert { get; set; } = new List<НаличиеФайлов.Запись>();
}

[ProtoContract] public sealed class НапоминанияПользователя
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public DateTime ВремяСобытия { get; set; }
		[ProtoMember(3)] public Entity Пользователь { get; set; }
		[ProtoMember(4)] public Union Источник { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public НапоминанияПользователя.Ключ Ключ { get; set; }
		[ProtoMember(2)] public string Идентификатор { get; set; }
		[ProtoMember(3)] public string ИмяРеквизитаИсточника { get; set; }
		[ProtoMember(4)] public double ИнтервалВремениНапоминания { get; set; }
		[ProtoMember(5)] public string Описание { get; set; }
		[ProtoMember(6)] public string ПредставлениеИсточника { get; set; }
		[ProtoMember(7)] public Entity Расписание { get; set; }
		[ProtoMember(8)] public Entity СпособУстановкиВремениНапоминания { get; set; }
		[ProtoMember(9)] public DateTime СрокНапоминания { get; set; }
	}
	[ProtoMember(1)] public НапоминанияПользователя.Ключ delete { get; set; }
	[ProtoMember(2)] public List<НапоминанияПользователя.Запись> insert { get; set; } = new List<НапоминанияПользователя.Запись>();
}

[ProtoContract] public sealed class НаследованиеНастроекПравОбъектов
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Объект { get; set; }
		[ProtoMember(3)] public Entity Родитель { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public НаследованиеНастроекПравОбъектов.Ключ Ключ { get; set; }
		[ProtoMember(2)] public double УровеньИспользования { get; set; }
		[ProtoMember(3)] public bool Наследовать { get; set; }
	}
	[ProtoMember(1)] public НаследованиеНастроекПравОбъектов.Ключ delete { get; set; }
	[ProtoMember(2)] public List<НаследованиеНастроекПравОбъектов.Запись> insert { get; set; } = new List<НаследованиеНастроекПравОбъектов.Запись>();
}

[ProtoContract] public sealed class НастройкаСозданияЦеныПартии
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity РегионРаботы { get; set; }
		[ProtoMember(3)] public Entity ТипЦены { get; set; }
		[ProtoMember(4)] public DateTime Период { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public НастройкаСозданияЦеныПартии.Ключ Ключ { get; set; }
	}
	[ProtoMember(1)] public НастройкаСозданияЦеныПартии.Ключ delete { get; set; }
	[ProtoMember(2)] public List<НастройкаСозданияЦеныПартии.Запись> insert { get; set; } = new List<НастройкаСозданияЦеныПартии.Запись>();
}

[ProtoContract] public sealed class НастройкиАдресовХраненияСберСпасибо
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity АдресХранения { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public НастройкиАдресовХраненияСберСпасибо.Ключ Ключ { get; set; }
		[ProtoMember(2)] public string ВариантСозданияЗащищенногоСоединения { get; set; }
		[ProtoMember(3)] public double ИдентификаторТорговогоПредприятия { get; set; }
		[ProtoMember(4)] public bool ИспользоватьСберСпасибо { get; set; }
	}
	[ProtoMember(1)] public НастройкиАдресовХраненияСберСпасибо.Ключ delete { get; set; }
	[ProtoMember(2)] public List<НастройкиАдресовХраненияСберСпасибо.Запись> insert { get; set; } = new List<НастройкиАдресовХраненияСберСпасибо.Запись>();
}

[ProtoContract] public sealed class НастройкиАптеки
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity АдресХранения { get; set; }
		[ProtoMember(3)] public DateTime Период { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public НастройкиАптеки.Ключ Ключ { get; set; }
		[ProtoMember(2)] public bool АвторизацияПользователяПоШК { get; set; }
		[ProtoMember(3)] public string АдресСервисаМДЛПДляНовогоРегламентаОтправкиСтатусов { get; set; }
		[ProtoMember(4)] public Entity ВариантСборки { get; set; }
		[ProtoMember(5)] public bool ВключенНовыйРегламентПоОтправкеСтатусовИдентификаторовМДЛП { get; set; }
		[ProtoMember(6)] public bool ВозможнаДоставкаСоСкладаРегиона { get; set; }
		[ProtoMember(7)] public bool ВозможнаДоставкаСЦС { get; set; }
		[ProtoMember(8)] public bool ВыводитьЗонуДоставкиВЭтикеткуГрузоместа { get; set; }
		[ProtoMember(9)] public bool ВыводитьНаПечатьЭтикеткиЗаказовКлиентовПриПриемке { get; set; }
		[ProtoMember(10)] public double ГлубинаАнализаЗаказовКлиентов { get; set; }
		[ProtoMember(11)] public bool ДоставкаВПартнерскиеАптеки { get; set; }
		[ProtoMember(12)] public bool ЗагружатьУпаковкиИзWMS { get; set; }
		[ProtoMember(13)] public bool ЗаписыватьВОчередьФискализацииЗаказыИз77 { get; set; }
		[ProtoMember(14)] public bool ЗаписыватьЗаказыВОчередьФискализации { get; set; }
		[ProtoMember(15)] public bool ИспользоватьНовыйКонтрольОстатковТоваров { get; set; }
		[ProtoMember(16)] public bool ИспользоватьСборкуДоставочныхЗаказовНаТСД { get; set; }
		[ProtoMember(17)] public bool КонтролироватьПривязкуЭлектронныхРецептовВРМК { get; set; }
		[ProtoMember(18)] public bool КонтролироватьСтатусЗаказаКлиентаПриПриемке { get; set; }
		[ProtoMember(19)] public bool КонтрольСуммыНалоговВККМОтключен { get; set; }
		[ProtoMember(20)] public Entity КупонДляВСП { get; set; }
		[ProtoMember(21)] public double ЛимитСтрокИзЗаказовПриФормированииСборки { get; set; }
		[ProtoMember(22)] public bool ОтображатьСамовывозыПоСтарымЗаказамНаРМК { get; set; }
		[ProtoMember(23)] public bool ПерепроводитьЗаказыКлиентовПоКоробуПередСозданиемРазмещения { get; set; }
		[ProtoMember(24)] public bool ПечатьУпаковокЗаказовНаТСД { get; set; }
		[ProtoMember(25)] public bool ПланированиеНесобранныхЗаказов { get; set; }
		[ProtoMember(26)] public double ПлановоеВремяСборкиЗаказов { get; set; }
		[ProtoMember(27)] public bool ПроводитьЗаказыКСборкеЕслиНетДвижений { get; set; }
		[ProtoMember(28)] public Entity ПроектСамокат { get; set; }
		[ProtoMember(29)] public Entity ПроектСберМаркет { get; set; }
		[ProtoMember(30)] public bool РазрешитьПолучениеЭлектронныхРецептовЗаказаБезСканированияКодов { get; set; }
		[ProtoMember(31)] public bool РазрешитьСборкуБезРецепта { get; set; }
		[ProtoMember(32)] public bool РазрешитьСоздаватьПеремещенияСоСкладаВозвратов { get; set; }
		[ProtoMember(33)] public bool СинхронизироватьВремяККТПередОткрытиемСмены { get; set; }
		[ProtoMember(34)] public bool СоздаватьПеремещениеНаСкладПотерьВТранзакцииСРазмещениемВЯчейки { get; set; }
		[ProtoMember(35)] public bool СортировкаЗСЯПриСборкеНаТСД { get; set; }
		[ProtoMember(36)] public bool УдалитьОрдернаяСхемаПеремещений { get; set; }
		[ProtoMember(37)] public bool УпрощенныйПодборПартийВПеремещение { get; set; }
		[ProtoMember(38)] public bool УстановкаПорядкаВЗСЯ { get; set; }
		[ProtoMember(39)] public bool ВключитьФункционалФФД12 { get; set; }
		[ProtoMember(40)] public bool ПоточныйПересчетПотоварно { get; set; }
		[ProtoMember(41)] public bool ПечататьВЧекеИнформациюTelegram { get; set; }
		[ProtoMember(42)] public bool АвторизацияСотрудникаДляПриемки { get; set; }
		[ProtoMember(43)] public bool КонтрольКиЗПриВозвратеНаПромежуточныйХаб { get; set; }
		[ProtoMember(44)] public bool РазрешитьПриниматьСтарыеЗаказы { get; set; }
		[ProtoMember(45)] public bool ИспользоватьСхемуУчетаДДС0522ВОФ { get; set; }
		[ProtoMember(46)] public bool ИспользоватьСхемуУчетаДДС0522ВРМК { get; set; }
		[ProtoMember(47)] public Entity ОперационныйБуфер { get; set; }
		[ProtoMember(48)] public Entity Сейф { get; set; }
		[ProtoMember(49)] public bool УчитыватьКоличествоУпаковочныхЯчеекПриСборкеНаТСД { get; set; }
		[ProtoMember(50)] public double ДнейИСГ { get; set; }
		[ProtoMember(51)] public string ПечатнаяФормаУпаковокЗаказов { get; set; }
		[ProtoMember(52)] public double ЛимитСтрокВЧеке { get; set; }
		[ProtoMember(53)] public bool РасформировыватьЗаказыБезУпаковок { get; set; }
		[ProtoMember(54)] public DateTime ДатаНовогоАлгоритмаРасчетаЦенИнвентаризации { get; set; }
		[ProtoMember(55)] public bool АвтоматическиСоздаватьПеремещенияПриВыдачеКурьеру { get; set; }
	}
	[ProtoMember(1)] public НастройкиАптеки.Ключ delete { get; set; }
	[ProtoMember(2)] public List<НастройкиАптеки.Запись> insert { get; set; } = new List<НастройкиАптеки.Запись>();
}

[ProtoContract] public sealed class НастройкиВерсионированияОбъектов
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Union ТипОбъекта { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public НастройкиВерсионированияОбъектов.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity Вариант { get; set; }
		[ProtoMember(3)] public bool Использовать { get; set; }
		[ProtoMember(4)] public Entity СрокХраненияВерсий { get; set; }
	}
	[ProtoMember(1)] public НастройкиВерсионированияОбъектов.Ключ delete { get; set; }
	[ProtoMember(2)] public List<НастройкиВерсионированияОбъектов.Запись> insert { get; set; } = new List<НастройкиВерсионированияОбъектов.Запись>();
}

[ProtoContract] public sealed class НастройкиКомандПечати
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Владелец { get; set; }
		[ProtoMember(3)] public string УникальныйИдентификатор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public НастройкиКомандПечати.Ключ Ключ { get; set; }
		[ProtoMember(2)] public bool Видимость { get; set; }
	}
	[ProtoMember(1)] public НастройкиКомандПечати.Ключ delete { get; set; }
	[ProtoMember(2)] public List<НастройкиКомандПечати.Запись> insert { get; set; } = new List<НастройкиКомандПечати.Запись>();
}

[ProtoContract] public sealed class НастройкиОбменаПоКонтрагентуСМДЛП
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Union Контрагент { get; set; }
		[ProtoMember(3)] public Entity ТипДокумента { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public НастройкиОбменаПоКонтрагентуСМДЛП.Ключ Ключ { get; set; }
		[ProtoMember(2)] public DateTime ДатаВключения { get; set; }
		[ProtoMember(3)] public bool Использование { get; set; }
		[ProtoMember(4)] public Entity ТипСообщения { get; set; }
	}
	[ProtoMember(1)] public НастройкиОбменаПоКонтрагентуСМДЛП.Ключ delete { get; set; }
	[ProtoMember(2)] public List<НастройкиОбменаПоКонтрагентуСМДЛП.Запись> insert { get; set; } = new List<НастройкиОбменаПоКонтрагентуСМДЛП.Запись>();
}

[ProtoContract] public sealed class НастройкиОбработкиЗаказовПоЭквайрингу
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity БанкЭквайер { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public НастройкиОбработкиЗаказовПоЭквайрингу.Ключ Ключ { get; set; }
		[ProtoMember(2)] public string АдресСервиса { get; set; }
		[ProtoMember(3)] public Entity КонтрагентЭквайер { get; set; }
		[ProtoMember(4)] public double МаксимальнаяДлительностьОбработкиСек { get; set; }
	}
	[ProtoMember(1)] public НастройкиОбработкиЗаказовПоЭквайрингу.Ключ delete { get; set; }
	[ProtoMember(2)] public List<НастройкиОбработкиЗаказовПоЭквайрингу.Запись> insert { get; set; } = new List<НастройкиОбработкиЗаказовПоЭквайрингу.Запись>();
}

[ProtoContract] public sealed class НастройкиОперацийПоЭлектроннымРецептам
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Провайдер { get; set; }
		[ProtoMember(3)] public Entity Фирма { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public НастройкиОперацийПоЭлектроннымРецептам.Ключ Ключ { get; set; }
		[ProtoMember(2)] public string АдресСервиса { get; set; }
		[ProtoMember(3)] public string ИдентификаторВызывающейСистемы { get; set; }
		[ProtoMember(4)] public string НаименованиеВызывающейСистемы { get; set; }
		[ProtoMember(5)] public string ОсновнойСправочник { get; set; }
		[ProtoMember(6)] public bool ПодписыватьВСервисе { get; set; }
		[ProtoMember(7)] public bool ПодписыватьЦП { get; set; }
		[ProtoMember(8)] public Entity СпособАутентификации { get; set; }
		[ProtoMember(9)] public string УдалитьАдресСервисаЦП { get; set; }
		[ProtoMember(10)] public Entity ДанныеДляWSОпределения { get; set; }
		[ProtoMember(11)] public bool ПолучатьWSОпределениеИзФайлов { get; set; }
	}
	[ProtoMember(1)] public НастройкиОперацийПоЭлектроннымРецептам.Ключ delete { get; set; }
	[ProtoMember(2)] public List<НастройкиОперацийПоЭлектроннымРецептам.Запись> insert { get; set; } = new List<НастройкиОперацийПоЭлектроннымРецептам.Запись>();
}

[ProtoContract] public sealed class НастройкиОрганизацийСберСпасибо
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Организация { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public НастройкиОрганизацийСберСпасибо.Ключ Ключ { get; set; }
		[ProtoMember(2)] public string АдресСервиса { get; set; }
		[ProtoMember(3)] public double ИдентификаторСети { get; set; }
	}
	[ProtoMember(1)] public НастройкиОрганизацийСберСпасибо.Ключ delete { get; set; }
	[ProtoMember(2)] public List<НастройкиОрганизацийСберСпасибо.Запись> insert { get; set; } = new List<НастройкиОрганизацийСберСпасибо.Запись>();
}

[ProtoContract] public sealed class НастройкиОчисткиФайлов
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity ТипВладельцаФайла { get; set; }
		[ProtoMember(3)] public Union ВладелецФайла { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public НастройкиОчисткиФайлов.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity Действие { get; set; }
		[ProtoMember(3)] public Entity ПериодОчистки { get; set; }
		[ProtoMember(4)] public Entity ПравилоОтбора { get; set; }
		[ProtoMember(5)] public bool ЭтоФайл { get; set; }
	}
	[ProtoMember(1)] public НастройкиОчисткиФайлов.Ключ delete { get; set; }
	[ProtoMember(2)] public List<НастройкиОчисткиФайлов.Запись> insert { get; set; } = new List<НастройкиОчисткиФайлов.Запись>();
}

[ProtoContract] public sealed class НастройкиПеревозчиков
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Перевозчик { get; set; }
		[ProtoMember(3)] public DateTime Период { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public НастройкиПеревозчиков.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity ДополнительныеПараметры { get; set; }
		[ProtoMember(3)] public bool ДоступноДляПланирования { get; set; }
		[ProtoMember(4)] public bool ЗапретИзмененияАдресаДоставки { get; set; }
		[ProtoMember(5)] public bool ЗапретИзмененияДанныхКлиента { get; set; }
		[ProtoMember(6)] public bool ЗапретИзмененияДатыДоставки { get; set; }
		[ProtoMember(7)] public bool ЗапретРучногоСозданияЗаказа { get; set; }
		[ProtoMember(8)] public bool ИспользоватьСлепокЦен { get; set; }
		[ProtoMember(9)] public bool ЛекарственныеСредства { get; set; }
		[ProtoMember(10)] public bool НеПересчитыватьСтоимостьДоставки { get; set; }
		[ProtoMember(11)] public bool НеПроверятьЗонуДоставки { get; set; }
		[ProtoMember(12)] public bool НеПроверятьЭлектроннуюПочту { get; set; }
		[ProtoMember(13)] public bool НеРезервироватьТоварНаОсновномСкладеРегиона { get; set; }
		[ProtoMember(14)] public bool ОтключитьОповещения { get; set; }
		[ProtoMember(15)] public double СрокДоставки { get; set; }
		[ProtoMember(16)] public Entity ФормаОплаты { get; set; }
		[ProtoMember(17)] public bool Холод { get; set; }
		[ProtoMember(18)] public bool ЗаполняетсяВTMS { get; set; }
		[ProtoMember(19)] public string ПрефиксЗаказов { get; set; }
		[ProtoMember(20)] public bool УстановитьРозничныеЦеныВЗаказах { get; set; }
	}
	[ProtoMember(1)] public НастройкиПеревозчиков.Ключ delete { get; set; }
	[ProtoMember(2)] public List<НастройкиПеревозчиков.Запись> insert { get; set; } = new List<НастройкиПеревозчиков.Запись>();
}

[ProtoContract] public sealed class НастройкиПравОбъектов
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Объект { get; set; }
		[ProtoMember(3)] public Union Пользователь { get; set; }
		[ProtoMember(4)] public string Право { get; set; }
		[ProtoMember(5)] public Union Таблица { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public НастройкиПравОбъектов.Ключ Ключ { get; set; }
		[ProtoMember(2)] public double ПорядокНастройки { get; set; }
		[ProtoMember(3)] public double УровеньЗапрещенияИзменения { get; set; }
		[ProtoMember(4)] public double УровеньЗапрещенияПрава { get; set; }
		[ProtoMember(5)] public double УровеньЗапрещенияЧтения { get; set; }
		[ProtoMember(6)] public double УровеньРазрешенияИзменения { get; set; }
		[ProtoMember(7)] public double УровеньРазрешенияПрава { get; set; }
		[ProtoMember(8)] public double УровеньРазрешенияЧтения { get; set; }
		[ProtoMember(9)] public bool НаследованиеРазрешено { get; set; }
		[ProtoMember(10)] public bool ПравоЗапрещено { get; set; }
	}
	[ProtoMember(1)] public НастройкиПравОбъектов.Ключ delete { get; set; }
	[ProtoMember(2)] public List<НастройкиПравОбъектов.Запись> insert { get; set; } = new List<НастройкиПравОбъектов.Запись>();
}

[ProtoContract] public sealed class НастройкиСкидокПоКарточнымПрограммам
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity АдресХранения { get; set; }
		[ProtoMember(3)] public Entity ТипКарты { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public НастройкиСкидокПоКарточнымПрограммам.Ключ Ключ { get; set; }
		[ProtoMember(2)] public bool ЗапросЧерезВеб { get; set; }
		[ProtoMember(3)] public string ИдентификаторАптеки { get; set; }
		[ProtoMember(4)] public string ИНН { get; set; }
		[ProtoMember(5)] public string КодАптеки { get; set; }
	}
	[ProtoMember(1)] public НастройкиСкидокПоКарточнымПрограммам.Ключ delete { get; set; }
	[ProtoMember(2)] public List<НастройкиСкидокПоКарточнымПрограммам.Запись> insert { get; set; } = new List<НастройкиСкидокПоКарточнымПрограммам.Запись>();
}

[ProtoContract] public sealed class НачисленияТарифыРасчета
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public НачисленияТарифыРасчета.Ключ Ключ { get; set; }
		[ProtoMember(2)] public DateTime ДатаВремяОперации { get; set; }
		[ProtoMember(3)] public Entity Должность { get; set; }
		[ProtoMember(4)] public Entity ЗонаДоставки { get; set; }
		[ProtoMember(5)] public double ПределДо { get; set; }
		[ProtoMember(6)] public double ПределОт { get; set; }
		[ProtoMember(7)] public Entity ТипДоплаты { get; set; }
		[ProtoMember(8)] public Entity ТипДоставки { get; set; }
		[ProtoMember(9)] public double СуммаФикс { get; set; }
		[ProtoMember(10)] public DateTime Период { get; set; }
	}
	[ProtoMember(1)] public НачисленияТарифыРасчета.Ключ delete { get; set; }
	[ProtoMember(2)] public List<НачисленияТарифыРасчета.Запись> insert { get; set; } = new List<НачисленияТарифыРасчета.Запись>();
}

[ProtoContract] public sealed class НомераТелефоновVoximplant
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регион { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public НомераТелефоновVoximplant.Ключ Ключ { get; set; }
		[ProtoMember(2)] public bool Доступен { get; set; }
		[ProtoMember(3)] public string НомерТелефона { get; set; }
	}
	[ProtoMember(1)] public НомераТелефоновVoximplant.Ключ delete { get; set; }
	[ProtoMember(2)] public List<НомераТелефоновVoximplant.Запись> insert { get; set; } = new List<НомераТелефоновVoximplant.Запись>();
}

[ProtoContract] public sealed class ОперацииПоЭлектроннымРецептам
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity ВидОперации { get; set; }
		[ProtoMember(3)] public Entity Документ { get; set; }
		[ProtoMember(4)] public string ИдентификаторЗапроса { get; set; }
		[ProtoMember(5)] public string КодРецепта { get; set; }
		[ProtoMember(6)] public DateTime Период { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ОперацииПоЭлектроннымРецептам.Ключ Ключ { get; set; }
		[ProtoMember(2)] public string OKATO { get; set; }
		[ProtoMember(3)] public Entity АдресХранения { get; set; }
		[ProtoMember(4)] public string ВрачебнаяДолжность { get; set; }
		[ProtoMember(5)] public DateTime ДатаВыписыванияРецепта { get; set; }
		[ProtoMember(6)] public DateTime ДатаОкончанияДействия { get; set; }
		[ProtoMember(7)] public DateTime ДатаОтвета { get; set; }
		[ProtoMember(8)] public string Дозировка { get; set; }
		[ProtoMember(9)] public Entity ДокументРегистрацииПродажи { get; set; }
		[ProtoMember(10)] public string ЕдиницаОтпуска { get; set; }
		[ProtoMember(11)] public string ЗапросВСервисСтрокой { get; set; }
		[ProtoMember(12)] public string ИдентификаторВызывающейСистемы { get; set; }
		[ProtoMember(13)] public string ИдентификаторЗапросаРегистрацииПродажи { get; set; }
		[ProtoMember(14)] public string ИдентификаторОтвета { get; set; }
		[ProtoMember(15)] public string ИдентификаторТочкиОтпуска { get; set; }
		[ProtoMember(16)] public string КодОшибки { get; set; }
		[ProtoMember(17)] public string КодПрепарата { get; set; }
		[ProtoMember(18)] public string КоличествоЕдиниц { get; set; }
		[ProtoMember(19)] public string ЛекарственнаяФорма { get; set; }
		[ProtoMember(20)] public string НаименованиеМНН { get; set; }
		[ProtoMember(21)] public string НаименованиеМО { get; set; }
		[ProtoMember(22)] public string НомерРецепта { get; set; }
		[ProtoMember(23)] public string ОбозначениеСправочника { get; set; }
		[ProtoMember(24)] public string ОписаниеОшибки { get; set; }
		[ProtoMember(25)] public string ОРГН { get; set; }
		[ProtoMember(26)] public string ОтветИзСервисаСтрокой { get; set; }
		[ProtoMember(27)] public bool ПризнакПолногоОтпуска { get; set; }
		[ProtoMember(28)] public Entity ПризнакСпециальногоКонтроля { get; set; }
		[ProtoMember(29)] public Entity Провайдер { get; set; }
		[ProtoMember(30)] public string РецептурноеНазначение { get; set; }
		[ProtoMember(31)] public string СведенияОВрачебнойКомиссии { get; set; }
		[ProtoMember(32)] public string СпециальныеОтметки { get; set; }
		[ProtoMember(33)] public string СпособПрименения { get; set; }
		[ProtoMember(34)] public string СрокДействияРецепта { get; set; }
		[ProtoMember(35)] public Entity Срочность { get; set; }
		[ProtoMember(36)] public Entity СтатусРецепта { get; set; }
		[ProtoMember(37)] public Entity СтатусТранзакцииВСервисе { get; set; }
		[ProtoMember(38)] public string ТелефонДляУточнений { get; set; }
		[ProtoMember(39)] public string ТелефонОрганизации { get; set; }
		[ProtoMember(40)] public Entity ТипРецепта { get; set; }
		[ProtoMember(41)] public Entity Фармацевт { get; set; }
		[ProtoMember(42)] public string ФИОВрача { get; set; }
		[ProtoMember(43)] public string ШтампМО { get; set; }
	}
	[ProtoMember(1)] public ОперацииПоЭлектроннымРецептам.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ОперацииПоЭлектроннымРецептам.Запись> insert { get; set; } = new List<ОперацииПоЭлектроннымРецептам.Запись>();
}

[ProtoContract] public sealed class ОперацииФискализацииВСервисе
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity ВидОперации { get; set; }
		[ProtoMember(3)] public Entity Документ { get; set; }
		[ProtoMember(4)] public DateTime Период { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ОперацииФискализацииВСервисе.Ключ Ключ { get; set; }
		[ProtoMember(2)] public string АдресЧекаНаСайтеОФД { get; set; }
		[ProtoMember(3)] public DateTime ДатаЧекаИзФН { get; set; }
		[ProtoMember(4)] public Entity ЗаказКлиента { get; set; }
		[ProtoMember(5)] public string ИдентификаторГруппыККТ { get; set; }
		[ProtoMember(6)] public string ИдентификаторДокумента { get; set; }
		[ProtoMember(7)] public string ИдентификаторЗаписиОтложеннойФискализации { get; set; }
		[ProtoMember(8)] public string ИдентификаторОшибки { get; set; }
		[ProtoMember(9)] public string КодККТ { get; set; }
		[ProtoMember(10)] public double КодОшибки { get; set; }
		[ProtoMember(11)] public string НаименованиеСервера { get; set; }
		[ProtoMember(12)] public double НомерСмены { get; set; }
		[ProtoMember(13)] public string НомерФН { get; set; }
		[ProtoMember(14)] public double НомерЧекаВСмене { get; set; }
		[ProtoMember(15)] public string ОписаниеОшибки { get; set; }
		[ProtoMember(16)] public string РегистрационныйНомерККТ { get; set; }
		[ProtoMember(17)] public Entity РежимПолученияДанных { get; set; }
		[ProtoMember(18)] public Entity СтатусОбработкиДокумента { get; set; }
		[ProtoMember(19)] public double СуммаЧека { get; set; }
		[ProtoMember(20)] public double ФискальныйНомерДокумента { get; set; }
		[ProtoMember(21)] public double ФискальныйПризнакДокумента { get; set; }
		[ProtoMember(22)] public string ВнешнийИдентификатор { get; set; }
		[ProtoMember(23)] public string ЗапросВСервис { get; set; }
		[ProtoMember(24)] public string ОтветИзСервиса { get; set; }
	}
	[ProtoMember(1)] public ОперацииФискализацииВСервисе.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ОперацииФискализацииВСервисе.Запись> insert { get; set; } = new List<ОперацииФискализацииВСервисе.Запись>();
}

[ProtoContract] public sealed class ОриентирыАдресныхОбъектов
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Идентификатор { get; set; }
		[ProtoMember(3)] public double КодСубъектаРФ { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ОриентирыАдресныхОбъектов.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity АдресныйОбъект { get; set; }
		[ProtoMember(3)] public Entity Дополнительно { get; set; }
		[ProtoMember(4)] public Entity Описание { get; set; }
		[ProtoMember(5)] public double ПочтовыйИндекс { get; set; }
	}
	[ProtoMember(1)] public ОриентирыАдресныхОбъектов.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ОриентирыАдресныхОбъектов.Запись> insert { get; set; } = new List<ОриентирыАдресныхОбъектов.Запись>();
}

[ProtoContract] public sealed class ОсновныеДоговорыКонтрагентов
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity ВидДоговора { get; set; }
		[ProtoMember(3)] public Entity Контрагент { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ОсновныеДоговорыКонтрагентов.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity Договор { get; set; }
	}
	[ProtoMember(1)] public ОсновныеДоговорыКонтрагентов.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ОсновныеДоговорыКонтрагентов.Запись> insert { get; set; } = new List<ОсновныеДоговорыКонтрагентов.Запись>();
}

[ProtoContract] public sealed class ОстаткиНоменклатурыКонтрагентов
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ОстаткиНоменклатурыКонтрагентов.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity СкладКонтрагента { get; set; }
		[ProtoMember(3)] public double Количество { get; set; }
		[ProtoMember(4)] public DateTime Период { get; set; }
	}
	[ProtoMember(1)] public ОстаткиНоменклатурыКонтрагентов.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ОстаткиНоменклатурыКонтрагентов.Запись> insert { get; set; } = new List<ОстаткиНоменклатурыКонтрагентов.Запись>();
}

[ProtoContract] public sealed class ОстаткиТекущие
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Аптека { get; set; }
		[ProtoMember(3)] public Entity Номенклатура { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ОстаткиТекущие.Ключ Ключ { get; set; }
		[ProtoMember(2)] public DateTime ДатаИмпорта { get; set; }
		[ProtoMember(3)] public double КарантинМДЛП { get; set; }
		[ProtoMember(4)] public double Количество { get; set; }
		[ProtoMember(5)] public double КоличествоБезИСГ { get; set; }
		[ProtoMember(6)] public double КоличествоВПути { get; set; }
		[ProtoMember(7)] public double Себестоимость { get; set; }
		[ProtoMember(8)] public double Сумма { get; set; }
	}
	[ProtoMember(1)] public ОстаткиТекущие.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ОстаткиТекущие.Запись> insert { get; set; } = new List<ОстаткиТекущие.Запись>();
}

[ProtoContract] public sealed class ОтветственныеЛицаАптек
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity АдресХранения { get; set; }
		[ProtoMember(3)] public DateTime Период { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ОтветственныеЛицаАптек.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity Администратор { get; set; }
		[ProtoMember(3)] public Entity ДиректорДепартамента { get; set; }
		[ProtoMember(4)] public Entity РегиональныйДиректор { get; set; }
		[ProtoMember(5)] public Entity ТерриториальныйУправляющий { get; set; }
	}
	[ProtoMember(1)] public ОтветственныеЛицаАптек.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ОтветственныеЛицаАптек.Запись> insert { get; set; } = new List<ОтветственныеЛицаАптек.Запись>();
}

[ProtoContract] public sealed class ОтложеннаяФискализацияЗаказовКлиентов
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity ВидОперации { get; set; }
		[ProtoMember(3)] public string ИдентификаторЗаказаКлиента { get; set; }
		[ProtoMember(4)] public DateTime Период { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ОтложеннаяФискализацияЗаказовКлиентов.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity ЗаказКлиента { get; set; }
		[ProtoMember(3)] public string ИдентификаторЗаписи { get; set; }
		[ProtoMember(4)] public bool ОбработкаПоЭквайрингуЗавершена { get; set; }
		[ProtoMember(5)] public Entity СтатусФискализации { get; set; }
		[ProtoMember(6)] public string ТекстОшибки { get; set; }
		[ProtoMember(7)] public Entity ФормаОплаты { get; set; }
		[ProtoMember(8)] public Entity Чек { get; set; }
		[ProtoMember(9)] public bool ОтключитьОбработкуПоОнлайнЭквайрингу { get; set; }
		[ProtoMember(10)] public Entity ФискализацияЗаказаКлиента { get; set; }
		[ProtoMember(11)] public Entity АдресХранения { get; set; }
	}
	[ProtoMember(1)] public ОтложеннаяФискализацияЗаказовКлиентов.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ОтложеннаяФискализацияЗаказовКлиентов.Запись> insert { get; set; } = new List<ОтложеннаяФискализацияЗаказовКлиентов.Запись>();
}

[ProtoContract] public sealed class ОтложенноеФормированиеВозвратовТоваровОтПокупателей
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Документ { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ОтложенноеФормированиеВозвратовТоваровОтПокупателей.Ключ Ключ { get; set; }
		[ProtoMember(2)] public string Описание { get; set; }
		[ProtoMember(3)] public Entity ЗаказКлиента { get; set; }
		[ProtoMember(4)] public bool МДЛП { get; set; }
		[ProtoMember(5)] public Entity Статус { get; set; }
		[ProtoMember(6)] public Entity СтатусПриемки { get; set; }
	}
	[ProtoMember(1)] public ОтложенноеФормированиеВозвратовТоваровОтПокупателей.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ОтложенноеФормированиеВозвратовТоваровОтПокупателей.Запись> insert { get; set; } = new List<ОтложенноеФормированиеВозвратовТоваровОтПокупателей.Запись>();
}

[ProtoContract] public sealed class ОтложенноеФормированиеДокументовПартнерам
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Union Документ { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ОтложенноеФормированиеДокументовПартнерам.Ключ Ключ { get; set; }
		[ProtoMember(2)] public string Описание { get; set; }
		[ProtoMember(3)] public DateTime ДатаДобавления { get; set; }
		[ProtoMember(4)] public bool Ошибка { get; set; }
		[ProtoMember(5)] public Entity Пользователь { get; set; }
		[ProtoMember(6)] public string НомерЗадачиВТреккере { get; set; }
	}
	[ProtoMember(1)] public ОтложенноеФормированиеДокументовПартнерам.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ОтложенноеФормированиеДокументовПартнерам.Запись> insert { get; set; } = new List<ОтложенноеФормированиеДокументовПартнерам.Запись>();
}

[ProtoContract] public sealed class ОтправкаСМС
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ОтправкаСМС.Ключ Ключ { get; set; }
		[ProtoMember(2)] public string approved { get; set; }
		[ProtoMember(3)] public string created { get; set; }
		[ProtoMember(4)] public string id_t_tc { get; set; }
		[ProtoMember(5)] public string iddoc { get; set; }
		[ProtoMember(6)] public string phone { get; set; }
		[ProtoMember(7)] public string sms { get; set; }
		[ProtoMember(8)] public string type { get; set; }
		[ProtoMember(9)] public bool Выгружен { get; set; }
		[ProtoMember(10)] public DateTime ДатаВыгрузки { get; set; }
		[ProtoMember(11)] public DateTime Период { get; set; }
	}
	[ProtoMember(1)] public ОтправкаСМС.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ОтправкаСМС.Запись> insert { get; set; } = new List<ОтправкаСМС.Запись>();
}

[ProtoContract] public sealed class ОтсканированныеНеподтвержденныеКИЗНаСборке
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Документ { get; set; }
		[ProtoMember(3)] public string Идентификатор { get; set; }
		[ProtoMember(4)] public string КлючУникальности { get; set; }
		[ProtoMember(5)] public Union МестоДеятельности { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ОтсканированныеНеподтвержденныеКИЗНаСборке.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity Номенклатура { get; set; }
		[ProtoMember(3)] public Entity Партия { get; set; }
		[ProtoMember(4)] public DateTime СрокГодности { get; set; }
		[ProtoMember(5)] public Entity Статус { get; set; }
		[ProtoMember(6)] public DateTime ДатаЗаписи { get; set; }
	}
	[ProtoMember(1)] public ОтсканированныеНеподтвержденныеКИЗНаСборке.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ОтсканированныеНеподтвержденныеКИЗНаСборке.Запись> insert { get; set; } = new List<ОтсканированныеНеподтвержденныеКИЗНаСборке.Запись>();
}

[ProtoContract] public sealed class ОчередьЗаказовКлиентов
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity ЗаказКлиента { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ОчередьЗаказовКлиентов.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity ВидЗаказаКлиента { get; set; }
		[ProtoMember(3)] public Entity Оператор { get; set; }
	}
	[ProtoMember(1)] public ОчередьЗаказовКлиентов.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ОчередьЗаказовКлиентов.Запись> insert { get; set; } = new List<ОчередьЗаказовКлиентов.Запись>();
}

[ProtoContract] public sealed class ОчередьОперацийСберСпасибо
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Документ { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ОчередьОперацийСберСпасибо.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity ВидОперации { get; set; }
		[ProtoMember(3)] public DateTime ДатаДобавления { get; set; }
		[ProtoMember(4)] public DateTime ДатаИзменения { get; set; }
		[ProtoMember(5)] public double ИдентификаторДокументаВСервисе { get; set; }
		[ProtoMember(6)] public string ИдентификаторОперацииДобавления { get; set; }
		[ProtoMember(7)] public string ИдентификаторОперацииИзменения { get; set; }
		[ProtoMember(8)] public string КодОшибки { get; set; }
		[ProtoMember(9)] public string ОписаниеОшибки { get; set; }
		[ProtoMember(10)] public bool ПодтвердитьЧекПродажиСОтменойБонусов { get; set; }
	}
	[ProtoMember(1)] public ОчередьОперацийСберСпасибо.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ОчередьОперацийСберСпасибо.Запись> insert { get; set; } = new List<ОчередьОперацийСберСпасибо.Запись>();
}

[ProtoContract] public sealed class ОчередьОтложенныхОпераций
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Union Объект { get; set; }
		[ProtoMember(3)] public Entity Операция { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ОчередьОтложенныхОпераций.Ключ Ключ { get; set; }
		[ProtoMember(2)] public string Информация { get; set; }
		[ProtoMember(3)] public DateTime ДатаЗаписи { get; set; }
		[ProtoMember(4)] public bool ОбменРИБ { get; set; }
		[ProtoMember(5)] public Entity Статус { get; set; }
	}
	[ProtoMember(1)] public ОчередьОтложенныхОпераций.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ОчередьОтложенныхОпераций.Запись> insert { get; set; } = new List<ОчередьОтложенныхОпераций.Запись>();
}

[ProtoContract] public sealed class ОчищенныеЧеки
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity АдресХранения { get; set; }
		[ProtoMember(3)] public double Количество { get; set; }
		[ProtoMember(4)] public Entity Номенклатура { get; set; }
		[ProtoMember(5)] public Entity Партия { get; set; }
		[ProtoMember(6)] public Entity Сотрудник { get; set; }
		[ProtoMember(7)] public double Сумма { get; set; }
		[ProtoMember(8)] public double Цена { get; set; }
		[ProtoMember(9)] public DateTime Период { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ОчищенныеЧеки.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity Чек { get; set; }
	}
	[ProtoMember(1)] public ОчищенныеЧеки.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ОчищенныеЧеки.Запись> insert { get; set; } = new List<ОчищенныеЧеки.Запись>();
}

[ProtoContract] public sealed class ПараметрыОбменаСПартнером
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Контрагент { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ПараметрыОбменаСПартнером.Ключ Ключ { get; set; }
		[ProtoMember(2)] public string dts_name_nakl_bn { get; set; }
		[ProtoMember(3)] public string dts_name_voz { get; set; }
		[ProtoMember(4)] public string Email { get; set; }
		[ProtoMember(5)] public string file_name_status { get; set; }
		[ProtoMember(6)] public string FTPИмяПользователя { get; set; }
		[ProtoMember(7)] public string FTPПапкаДляВыгрузкиНакладной { get; set; }
		[ProtoMember(8)] public string FTPПапкаДляВыгрузкиПрайса { get; set; }
		[ProtoMember(9)] public string FTPПапкаДляЗагрузкиСтатусов { get; set; }
		[ProtoMember(10)] public string FTPПароль { get; set; }
		[ProtoMember(11)] public string FTPСервер { get; set; }
		[ProtoMember(12)] public bool send_bn { get; set; }
		[ProtoMember(13)] public bool Активный { get; set; }
		[ProtoMember(14)] public string ИмяНумератора { get; set; }
		[ProtoMember(15)] public bool ИспользоватьКодЕаптека { get; set; }
		[ProtoMember(16)] public bool ЛидирующийНольВНакладной { get; set; }
		[ProtoMember(17)] public string МаскаФайлаСтатусов { get; set; }
		[ProtoMember(18)] public string МодульНакладные { get; set; }
		[ProtoMember(19)] public string МодульПрайс { get; set; }
		[ProtoMember(20)] public string МодульСтатус { get; set; }
		[ProtoMember(21)] public bool ОтправлятьМестаЗаказа { get; set; }
		[ProtoMember(22)] public bool РазрешеноОтправлятьНакладнуюДоОтправкиЗаказа { get; set; }
		[ProtoMember(23)] public Entity ТипВозвратаТовара { get; set; }
		[ProtoMember(24)] public Entity ТипОтправкиЭлектроннойНакладной { get; set; }
	}
	[ProtoMember(1)] public ПараметрыОбменаСПартнером.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ПараметрыОбменаСПартнером.Запись> insert { get; set; } = new List<ПараметрыОбменаСПартнером.Запись>();
}

[ProtoContract] public sealed class ПартииАдресовХранения
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity АдресХранения { get; set; }
		[ProtoMember(3)] public Entity Партия { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ПартииАдресовХранения.Ключ Ключ { get; set; }
	}
	[ProtoMember(1)] public ПартииАдресовХранения.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ПартииАдресовХранения.Запись> insert { get; set; } = new List<ПартииАдресовХранения.Запись>();
}

[ProtoContract] public sealed class ПереносыДатДоставкиЗаказов
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity ЗаказКлиента { get; set; }
		[ProtoMember(3)] public DateTime Период { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ПереносыДатДоставкиЗаказов.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity Пользователь { get; set; }
		[ProtoMember(3)] public Entity Причина { get; set; }
		[ProtoMember(4)] public DateTime ДатаКонечная { get; set; }
		[ProtoMember(5)] public DateTime ДатаНачальная { get; set; }
		[ProtoMember(6)] public Entity ИнтервалКонечный { get; set; }
		[ProtoMember(7)] public Entity ИнтервалНачальный { get; set; }
	}
	[ProtoMember(1)] public ПереносыДатДоставкиЗаказов.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ПереносыДатДоставкиЗаказов.Запись> insert { get; set; } = new List<ПереносыДатДоставкиЗаказов.Запись>();
}

[ProtoContract] public sealed class ПереносыДатОтменыЗаказов
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity ЗаказКлиента { get; set; }
		[ProtoMember(3)] public DateTime Период { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ПереносыДатОтменыЗаказов.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity Пользователь { get; set; }
		[ProtoMember(3)] public Entity Причина { get; set; }
		[ProtoMember(4)] public DateTime ДатаОтмены { get; set; }
	}
	[ProtoMember(1)] public ПереносыДатОтменыЗаказов.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ПереносыДатОтменыЗаказов.Запись> insert { get; set; } = new List<ПереносыДатОтменыЗаказов.Запись>();
}

[ProtoContract] public sealed class ПериодыНерабочихДнейКалендаря
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public double НомерПериода { get; set; }
		[ProtoMember(3)] public Entity ПроизводственныйКалендарь { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ПериодыНерабочихДнейКалендаря.Ключ Ключ { get; set; }
		[ProtoMember(2)] public string Основание { get; set; }
		[ProtoMember(3)] public DateTime ДатаНачала { get; set; }
		[ProtoMember(4)] public DateTime ДатаОкончания { get; set; }
	}
	[ProtoMember(1)] public ПериодыНерабочихДнейКалендаря.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ПериодыНерабочихДнейКалендаря.Запись> insert { get; set; } = new List<ПериодыНерабочихДнейКалендаря.Запись>();
}

[ProtoContract] public sealed class ПланДоставкиПоКурьерам
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public double ВесЗаказа { get; set; }
		[ProtoMember(3)] public double ВремяВыхода { get; set; }
		[ProtoMember(4)] public DateTime ДатаВыхода { get; set; }
		[ProtoMember(5)] public Entity ЗаданиеНаДоставку { get; set; }
		[ProtoMember(6)] public Entity ЗаказКлиента { get; set; }
		[ProtoMember(7)] public Entity Курьер { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ПланДоставкиПоКурьерам.Ключ Ключ { get; set; }
		[ProtoMember(2)] public DateTime ДатаВремяДоставки { get; set; }
		[ProtoMember(3)] public double Долгота { get; set; }
		[ProtoMember(4)] public Entity ОтчетОДоставке { get; set; }
		[ProtoMember(5)] public double ПорядокДоставки { get; set; }
		[ProtoMember(6)] public double СуммаВознаграждения { get; set; }
		[ProtoMember(7)] public string ТелефонКлиента { get; set; }
		[ProtoMember(8)] public bool УточнитьКоординаты { get; set; }
		[ProtoMember(9)] public Entity ЧекККМ { get; set; }
		[ProtoMember(10)] public double Широта { get; set; }
	}
	[ProtoMember(1)] public ПланДоставкиПоКурьерам.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ПланДоставкиПоКурьерам.Запись> insert { get; set; } = new List<ПланДоставкиПоКурьерам.Запись>();
}

[ProtoContract] public sealed class ПланированиеДокументовДоставки
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ПланированиеДокументовДоставки.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity АдресХранения { get; set; }
		[ProtoMember(3)] public Union ДокументДоставки { get; set; }
		[ProtoMember(4)] public string АдресДоставки { get; set; }
		[ProtoMember(5)] public double Вес { get; set; }
		[ProtoMember(6)] public DateTime ВремяПо { get; set; }
		[ProtoMember(7)] public DateTime ВремяС { get; set; }
		[ProtoMember(8)] public DateTime ДатаДоставки { get; set; }
		[ProtoMember(9)] public Entity ЗонаДоставки { get; set; }
		[ProtoMember(10)] public double Объем { get; set; }
		[ProtoMember(11)] public double Порядок { get; set; }
		[ProtoMember(12)] public Entity ПровайдерПоЭлектроннымРецептам { get; set; }
		[ProtoMember(13)] public Entity ПунктСамовывоза { get; set; }
		[ProtoMember(14)] public Union СпособДоставки { get; set; }
		[ProtoMember(15)] public double СуммаДокумента { get; set; }
		[ProtoMember(16)] public Entity ВидПеревозки { get; set; }
		[ProtoMember(17)] public string ИдентификаторыЗаказовПровайдера { get; set; }
	}
	[ProtoMember(1)] public ПланированиеДокументовДоставки.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ПланированиеДокументовДоставки.Запись> insert { get; set; } = new List<ПланированиеДокументовДоставки.Запись>();
}

[ProtoContract] public sealed class ПодтверждениеСкидкиАЗ
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public string ИДДокумента { get; set; }
		[ProtoMember(3)] public string Идентификатор { get; set; }
		[ProtoMember(4)] public Entity ТипСкидочнойПрограммы { get; set; }
		[ProtoMember(5)] public Entity Чек { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ПодтверждениеСкидкиАЗ.Ключ Ключ { get; set; }
		[ProtoMember(2)] public string ТекстОшибки { get; set; }
		[ProtoMember(3)] public string КодАвторизации { get; set; }
		[ProtoMember(4)] public double КоличествоЗапросов { get; set; }
		[ProtoMember(5)] public bool Подтвержден { get; set; }
		[ProtoMember(6)] public bool ЭтоВозврат { get; set; }
	}
	[ProtoMember(1)] public ПодтверждениеСкидкиАЗ.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ПодтверждениеСкидкиАЗ.Запись> insert { get; set; } = new List<ПодтверждениеСкидкиАЗ.Запись>();
}

[ProtoContract] public sealed class ПоказателиМетрик
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Union Категория { get; set; }
		[ProtoMember(3)] public Entity Метрика { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ПоказателиМетрик.Ключ Ключ { get; set; }
		[ProtoMember(2)] public double Значение { get; set; }
	}
	[ProtoMember(1)] public ПоказателиМетрик.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ПоказателиМетрик.Запись> insert { get; set; } = new List<ПоказателиМетрик.Запись>();
}

[ProtoContract] public sealed class ПоказателиСдельнойЗарплатыКурьеров
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Union АдресДоставки { get; set; }
		[ProtoMember(3)] public DateTime ДатаДоставки { get; set; }
		[ProtoMember(4)] public Union ЗаказКлиента { get; set; }
		[ProtoMember(5)] public Entity ЗонаДоставки { get; set; }
		[ProtoMember(6)] public Entity Курьер { get; set; }
		[ProtoMember(7)] public Union МаршрутныйЛист { get; set; }
		[ProtoMember(8)] public Entity Склад { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ПоказателиСдельнойЗарплатыКурьеров.Ключ Ключ { get; set; }
		[ProtoMember(2)] public string Комментарий { get; set; }
		[ProtoMember(3)] public double КоличествоАдресов { get; set; }
		[ProtoMember(4)] public double КоличествоВозвратов { get; set; }
		[ProtoMember(5)] public double КоличествоДоставлено { get; set; }
		[ProtoMember(6)] public double КоличествоЗаказов { get; set; }
		[ProtoMember(7)] public double КоличествоОпозданий { get; set; }
		[ProtoMember(8)] public double КоличествоПереносов { get; set; }
		[ProtoMember(9)] public double СуммаЗаВходВЗону { get; set; }
		[ProtoMember(10)] public double СуммаЗаДоставку { get; set; }
		[ProtoMember(11)] public Entity ФормаРасчета { get; set; }
	}
	[ProtoMember(1)] public ПоказателиСдельнойЗарплатыКурьеров.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ПоказателиСдельнойЗарплатыКурьеров.Запись> insert { get; set; } = new List<ПоказателиСдельнойЗарплатыКурьеров.Запись>();
}

[ProtoContract] public sealed class ПользовательскиеМакетыПечати
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public string ИмяМакета { get; set; }
		[ProtoMember(3)] public string Объект { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ПользовательскиеМакетыПечати.Ключ Ключ { get; set; }
		[ProtoMember(2)] public bool Использование { get; set; }
		[ProtoMember(3)] public Entity Макет { get; set; }
	}
	[ProtoMember(1)] public ПользовательскиеМакетыПечати.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ПользовательскиеМакетыПечати.Запись> insert { get; set; } = new List<ПользовательскиеМакетыПечати.Запись>();
}

[ProtoContract] public sealed class ПорядокПримененияСписковАМ
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public double Приоритет { get; set; }
		[ProtoMember(3)] public Entity ТипСписка { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ПорядокПримененияСписковАМ.Ключ Ключ { get; set; }
	}
	[ProtoMember(1)] public ПорядокПримененияСписковАМ.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ПорядокПримененияСписковАМ.Запись> insert { get; set; } = new List<ПорядокПримененияСписковАМ.Запись>();
}

[ProtoContract] public sealed class Поставщики
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public double supplier_id { get; set; }
		[ProtoMember(3)] public bool Активно { get; set; }
		[ProtoMember(4)] public Entity Договор { get; set; }
		[ProtoMember(5)] public Entity Поставщик { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public Поставщики.Ключ Ключ { get; set; }
		[ProtoMember(2)] public string АдресFTP { get; set; }
		[ProtoMember(3)] public string Логин { get; set; }
		[ProtoMember(4)] public string Папка { get; set; }
		[ProtoMember(5)] public string Пароль { get; set; }
		[ProtoMember(6)] public Entity ПрайсПоставщика { get; set; }
		[ProtoMember(7)] public string Путь { get; set; }
	}
	[ProtoMember(1)] public Поставщики.Ключ delete { get; set; }
	[ProtoMember(2)] public List<Поставщики.Запись> insert { get; set; } = new List<Поставщики.Запись>();
}

[ProtoContract] public sealed class ПраваРолей
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity ОбъектМетаданных { get; set; }
		[ProtoMember(3)] public Entity Роль { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ПраваРолей.Ключ Ключ { get; set; }
		[ProtoMember(2)] public bool ПравоДобавление { get; set; }
		[ProtoMember(3)] public bool ПравоДобавлениеБезОграничения { get; set; }
		[ProtoMember(4)] public bool ПравоИзменение { get; set; }
		[ProtoMember(5)] public bool ПравоИзменениеБезОграничения { get; set; }
		[ProtoMember(6)] public bool ПравоИнтерактивноеДобавление { get; set; }
		[ProtoMember(7)] public bool ПравоПросмотр { get; set; }
		[ProtoMember(8)] public bool ПравоРедактирование { get; set; }
		[ProtoMember(9)] public bool ПравоЧтениеБезОграничения { get; set; }
	}
	[ProtoMember(1)] public ПраваРолей.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ПраваРолей.Запись> insert { get; set; } = new List<ПраваРолей.Запись>();
}

[ProtoContract] public sealed class ПравилаДляОбменаДанными
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity ВидПравил { get; set; }
		[ProtoMember(3)] public string ИмяПланаОбмена { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ПравилаДляОбменаДанными.Ключ Ключ { get; set; }
		[ProtoMember(2)] public bool ИспользоватьФильтрВыборочнойРегистрацииОбъектов { get; set; }
		[ProtoMember(3)] public bool ПравилаЗагружены { get; set; }
		[ProtoMember(4)] public string ИмяМакетаПравил { get; set; }
		[ProtoMember(5)] public string ИмяМакетаПравилКорреспондента { get; set; }
		[ProtoMember(6)] public string ИмяПланаОбменаИзПравил { get; set; }
		[ProtoMember(7)] public string ИмяФайлаОбработкиДляОтладкиВыгрузки { get; set; }
		[ProtoMember(8)] public string ИмяФайлаОбработкиДляОтладкиЗагрузки { get; set; }
		[ProtoMember(9)] public string ИмяФайлаПравил { get; set; }
		[ProtoMember(10)] public string ИмяФайлаПротоколаОбмена { get; set; }
		[ProtoMember(11)] public string ИнформацияОПравилах { get; set; }
		[ProtoMember(12)] public Entity ИсточникПравил { get; set; }
		[ProtoMember(13)] public bool НеОстанавливатьПоОшибке { get; set; }
		[ProtoMember(14)] public Entity ПравилаXML { get; set; }
		[ProtoMember(15)] public Entity ПравилаXMLКорреспондента { get; set; }
		[ProtoMember(16)] public Entity ПравилаЗачитанные { get; set; }
		[ProtoMember(17)] public Entity ПравилаЗачитанныеКорреспондента { get; set; }
		[ProtoMember(18)] public bool РежимОтладки { get; set; }
		[ProtoMember(19)] public bool РежимОтладкиВыгрузки { get; set; }
		[ProtoMember(20)] public bool РежимОтладкиЗагрузки { get; set; }
		[ProtoMember(21)] public bool РежимПротоколированияОбменаДанными { get; set; }
	}
	[ProtoMember(1)] public ПравилаДляОбменаДанными.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ПравилаДляОбменаДанными.Запись> insert { get; set; } = new List<ПравилаДляОбменаДанными.Запись>();
}

[ProtoContract] public sealed class ПределыНагрузкиПоЗонамПараметры
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity АдресХранения { get; set; }
		[ProtoMember(3)] public DateTime ДатаДоставки { get; set; }
		[ProtoMember(4)] public double ДлинаИнтервала { get; set; }
		[ProtoMember(5)] public Entity ЗонаДоставки { get; set; }
		[ProtoMember(6)] public DateTime НачалоИнтервала { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ПределыНагрузкиПоЗонамПараметры.Ключ Ключ { get; set; }
		[ProtoMember(2)] public bool Водительский { get; set; }
		[ProtoMember(3)] public bool Доставка { get; set; }
		[ProtoMember(4)] public double ОбнулятьЗаМинут { get; set; }
		[ProtoMember(5)] public bool Самовывоз { get; set; }
	}
	[ProtoMember(1)] public ПределыНагрузкиПоЗонамПараметры.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ПределыНагрузкиПоЗонамПараметры.Запись> insert { get; set; } = new List<ПределыНагрузкиПоЗонамПараметры.Запись>();
}

[ProtoContract] public sealed class ПредельныеЗначенияКоличестваВЧеке
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Номенклатура { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ПредельныеЗначенияКоличестваВЧеке.Ключ Ключ { get; set; }
		[ProtoMember(2)] public double Количество { get; set; }
		[ProtoMember(3)] public double КоличествоСреднее { get; set; }
		[ProtoMember(4)] public double КоличествоЧеков { get; set; }
	}
	[ProtoMember(1)] public ПредельныеЗначенияКоличестваВЧеке.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ПредельныеЗначенияКоличестваВЧеке.Запись> insert { get; set; } = new List<ПредельныеЗначенияКоличестваВЧеке.Запись>();
}

[ProtoContract] public sealed class Препараты7ВЗН
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Номенклатура { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public Препараты7ВЗН.Ключ Ключ { get; set; }
	}
	[ProtoMember(1)] public Препараты7ВЗН.Ключ delete { get; set; }
	[ProtoMember(2)] public List<Препараты7ВЗН.Запись> insert { get; set; } = new List<Препараты7ВЗН.Запись>();
}

[ProtoContract] public sealed class ПринятыеМаршруты
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Маршрут { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ПринятыеМаршруты.Ключ Ключ { get; set; }
		[ProtoMember(2)] public bool Выгружен { get; set; }
		[ProtoMember(3)] public bool Принят { get; set; }
	}
	[ProtoMember(1)] public ПринятыеМаршруты.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ПринятыеМаршруты.Запись> insert { get; set; } = new List<ПринятыеМаршруты.Запись>();
}

[ProtoContract] public sealed class ПричиныИзмененияАдресныхСведений
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Идентификатор { get; set; }
		[ProtoMember(3)] public Entity ИзмененныйОбъект { get; set; }
		[ProtoMember(4)] public double КодСубъектаРФ { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ПричиныИзмененияАдресныхСведений.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity Описание { get; set; }
		[ProtoMember(3)] public bool СодержитОписание { get; set; }
	}
	[ProtoMember(1)] public ПричиныИзмененияАдресныхСведений.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ПричиныИзмененияАдресныхСведений.Запись> insert { get; set; } = new List<ПричиныИзмененияАдресныхСведений.Запись>();
}

[ProtoContract] public sealed class ПродажиПоЭлектроннымРецептам
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity ВидОперации { get; set; }
		[ProtoMember(3)] public Entity Документ { get; set; }
		[ProtoMember(4)] public string ИдентификаторЗапроса { get; set; }
		[ProtoMember(5)] public string ИдентификаторСтроки { get; set; }
		[ProtoMember(6)] public string КодРецепта { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ПродажиПоЭлектроннымРецептам.Ключ Ключ { get; set; }
		[ProtoMember(2)] public string АдресАптеки { get; set; }
		[ProtoMember(3)] public string ГлобальныйИдентификаторЛП { get; set; }
		[ProtoMember(4)] public DateTime ДатаПродажи { get; set; }
		[ProtoMember(5)] public string ИдентификаторАптеки { get; set; }
		[ProtoMember(6)] public string ИдентификаторСтрокиНазначений { get; set; }
		[ProtoMember(7)] public string КодПрепарата { get; set; }
		[ProtoMember(8)] public double КоличествоВторичныхУпаковок { get; set; }
		[ProtoMember(9)] public double КоличествоПервичныхУпаковок { get; set; }
		[ProtoMember(10)] public string НазваниеАптеки { get; set; }
		[ProtoMember(11)] public string НаименованиеПрепарата { get; set; }
		[ProtoMember(12)] public double ОбщееКоличествоЕдиницОтпуска { get; set; }
		[ProtoMember(13)] public string СерийныйНомерУпаковкиЛП { get; set; }
		[ProtoMember(14)] public double СтоимостьПрепарата { get; set; }
		[ProtoMember(15)] public string ТелефонАптеки { get; set; }
	}
	[ProtoMember(1)] public ПродажиПоЭлектроннымРецептам.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ПродажиПоЭлектроннымРецептам.Запись> insert { get; set; } = new List<ПродажиПоЭлектроннымРецептам.Запись>();
}

[ProtoContract] public sealed class ПромокодыТелефон
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public string ПромоКод { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ПромокодыТелефон.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity Аптека { get; set; }
		[ProtoMember(3)] public DateTime ДатаЗагрузки { get; set; }
		[ProtoMember(4)] public Entity Сотрудник { get; set; }
		[ProtoMember(5)] public Entity Группа { get; set; }
		[ProtoMember(6)] public DateTime ДатаВыдачи { get; set; }
		[ProtoMember(7)] public double МинимальнаяСумма { get; set; }
		[ProtoMember(8)] public double Номинал { get; set; }
		[ProtoMember(9)] public Entity ПричинаВыдачи { get; set; }
		[ProtoMember(10)] public DateTime СрокДействия { get; set; }
		[ProtoMember(11)] public string Телефон { get; set; }
		[ProtoMember(12)] public Union Документ { get; set; }
	}
	[ProtoMember(1)] public ПромокодыТелефон.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ПромокодыТелефон.Запись> insert { get; set; } = new List<ПромокодыТелефон.Запись>();
}

[ProtoContract] public sealed class РазмерыВознагражденийКурьерам
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public РазмерыВознагражденийКурьерам.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Union ДокументУсловий { get; set; }
		[ProtoMember(3)] public Entity ЗонаДоставки { get; set; }
		[ProtoMember(4)] public double Сумма { get; set; }
		[ProtoMember(5)] public double СуммаЗаСрочнуюДоставку { get; set; }
	}
	[ProtoMember(1)] public РазмерыВознагражденийКурьерам.Ключ delete { get; set; }
	[ProtoMember(2)] public List<РазмерыВознагражденийКурьерам.Запись> insert { get; set; } = new List<РазмерыВознагражденийКурьерам.Запись>();
}

[ProtoContract] public sealed class РазмерыВознагражденийКурьерамПоВремени
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public РазмерыВознагражденийКурьерамПоВремени.Ключ Ключ { get; set; }
		[ProtoMember(2)] public DateTime ВремяНачало { get; set; }
		[ProtoMember(3)] public DateTime ВремяОкончание { get; set; }
		[ProtoMember(4)] public Union ДокументУсловий { get; set; }
		[ProtoMember(5)] public Entity ЗонаДоставки { get; set; }
		[ProtoMember(6)] public double Сумма { get; set; }
		[ProtoMember(7)] public Union ТипДокументаДоставки { get; set; }
	}
	[ProtoMember(1)] public РазмерыВознагражденийКурьерамПоВремени.Ключ delete { get; set; }
	[ProtoMember(2)] public List<РазмерыВознагражденийКурьерамПоВремени.Запись> insert { get; set; } = new List<РазмерыВознагражденийКурьерамПоВремени.Запись>();
}

[ProtoContract] public sealed class РегистрацияТоварныхМатриц
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public РегистрацияТоварныхМатриц.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity Номенклатура { get; set; }
		[ProtoMember(3)] public Entity ТоварнаяМатрица { get; set; }
		[ProtoMember(4)] public bool АктивностьНоменклатуры { get; set; }
		[ProtoMember(5)] public double НеснижаемыйОстаток { get; set; }
		[ProtoMember(6)] public Entity УсловиеВхождения { get; set; }
		[ProtoMember(7)] public DateTime Период { get; set; }
	}
	[ProtoMember(1)] public РегистрацияТоварныхМатриц.Ключ delete { get; set; }
	[ProtoMember(2)] public List<РегистрацияТоварныхМатриц.Запись> insert { get; set; } = new List<РегистрацияТоварныхМатриц.Запись>();
}

[ProtoContract] public sealed class РеестрТоваровМДЛП
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Union Документ { get; set; }
		[ProtoMember(3)] public string Идентификатор { get; set; }
		[ProtoMember(4)] public string КлючУникальности { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public РеестрТоваровМДЛП.Ключ Ключ { get; set; }
		[ProtoMember(2)] public string QRКод { get; set; }
		[ProtoMember(3)] public bool ЗапроситьДанныеВШлюзе { get; set; }
		[ProtoMember(4)] public string Описание { get; set; }
		[ProtoMember(5)] public bool Ошибка { get; set; }
		[ProtoMember(6)] public double ИдентификаторСтроки { get; set; }
		[ProtoMember(7)] public Entity Партия { get; set; }
		[ProtoMember(8)] public bool Подтверждено { get; set; }
		[ProtoMember(9)] public Entity ПриемныйОрдер { get; set; }
		[ProtoMember(10)] public string Серия { get; set; }
		[ProtoMember(11)] public DateTime СрокГодности { get; set; }
		[ProtoMember(12)] public Entity Товар { get; set; }
		[ProtoMember(13)] public DateTime ДатаВыбытия { get; set; }
		[ProtoMember(14)] public DateTime ДатаСозданияЗаписи { get; set; }
	}
	[ProtoMember(1)] public РеестрТоваровМДЛП.Ключ delete { get; set; }
	[ProtoMember(2)] public List<РеестрТоваровМДЛП.Запись> insert { get; set; } = new List<РеестрТоваровМДЛП.Запись>();
}

[ProtoContract] public sealed class Рецепты
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity ЗаказКлиента { get; set; }
		[ProtoMember(3)] public Entity Номенклатура { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public Рецепты.Ключ Ключ { get; set; }
		[ProtoMember(2)] public DateTime Дата { get; set; }
		[ProtoMember(3)] public string ДействующееВещество { get; set; }
		[ProtoMember(4)] public string Доктор { get; set; }
		[ProtoMember(5)] public string ИдентификаторЗаказаПровайдера { get; set; }
		[ProtoMember(6)] public string Клиника { get; set; }
		[ProtoMember(7)] public string КодРецепта { get; set; }
		[ProtoMember(8)] public bool ПодтверждатьЗаказУПровайдераЭР { get; set; }
		[ProtoMember(9)] public Entity Провайдер { get; set; }
		[ProtoMember(10)] public bool Льготный { get; set; }
	}
	[ProtoMember(1)] public Рецепты.Ключ delete { get; set; }
	[ProtoMember(2)] public List<Рецепты.Запись> insert { get; set; } = new List<Рецепты.Запись>();
}

[ProtoContract] public sealed class РучныеИзмененияГрафиковРаботы
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public double Год { get; set; }
		[ProtoMember(3)] public Entity ГрафикРаботы { get; set; }
		[ProtoMember(4)] public DateTime ДатаГрафика { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public РучныеИзмененияГрафиковРаботы.Ключ Ключ { get; set; }
		[ProtoMember(2)] public bool РучноеИзменение { get; set; }
	}
	[ProtoMember(1)] public РучныеИзмененияГрафиковРаботы.Ключ delete { get; set; }
	[ProtoMember(2)] public List<РучныеИзмененияГрафиковРаботы.Запись> insert { get; set; } = new List<РучныеИзмененияГрафиковРаботы.Запись>();
}

[ProtoContract] public sealed class СведенияПоПартиям
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public СведенияПоПартиям.Ключ Ключ { get; set; }
		[ProtoMember(2)] public double ИдентификаторСтроки { get; set; }
		[ProtoMember(3)] public DateTime ДатаРеализацииПроизводителем { get; set; }
		[ProtoMember(4)] public bool МДЛП { get; set; }
		[ProtoMember(5)] public Entity Номенклатура { get; set; }
		[ProtoMember(6)] public Entity НомерГТД { get; set; }
		[ProtoMember(7)] public Entity Производитель { get; set; }
		[ProtoMember(8)] public string Серия { get; set; }
		[ProtoMember(9)] public DateTime СрокГодности { get; set; }
		[ProtoMember(10)] public Entity СтавкаНДС { get; set; }
		[ProtoMember(11)] public double ЦенаГосРеестра { get; set; }
		[ProtoMember(12)] public double ЦенаЗакупочная { get; set; }
		[ProtoMember(13)] public double ЦенаПроизводителя { get; set; }
		[ProtoMember(14)] public DateTime Период { get; set; }
	}
	[ProtoMember(1)] public СведенияПоПартиям.Ключ delete { get; set; }
	[ProtoMember(2)] public List<СведенияПоПартиям.Запись> insert { get; set; } = new List<СведенияПоПартиям.Запись>();
}

[ProtoContract] public sealed class СвойстваЗаказовЮЛ
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity ЗаказКлиента { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public СвойстваЗаказовЮЛ.Ключ Ключ { get; set; }
		[ProtoMember(2)] public bool ВОжиданииРезерва { get; set; }
		[ProtoMember(3)] public Union Грузополучатель { get; set; }
		[ProtoMember(4)] public Entity ДанныеССайта { get; set; }
		[ProtoMember(5)] public DateTime ДатаВыставленияСчета { get; set; }
		[ProtoMember(6)] public DateTime ДатаОплаты { get; set; }
		[ProtoMember(7)] public DateTime ДатаОтмены { get; set; }
		[ProtoMember(8)] public DateTime ДатаПодтверждения { get; set; }
		[ProtoMember(9)] public string КоментарийПлатежныхДокументов { get; set; }
		[ProtoMember(10)] public Entity СкладКонтрагента { get; set; }
		[ProtoMember(11)] public DateTime ДатаВыставленияСчетДоговора { get; set; }
	}
	[ProtoMember(1)] public СвойстваЗаказовЮЛ.Ключ delete { get; set; }
	[ProtoMember(2)] public List<СвойстваЗаказовЮЛ.Запись> insert { get; set; } = new List<СвойстваЗаказовЮЛ.Запись>();
}

[ProtoContract] public sealed class СвязиАдресныхОбъектов
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Идентификатор { get; set; }
		[ProtoMember(3)] public double КодСубъектаРФ { get; set; }
		[ProtoMember(4)] public Entity МуниципальныйРодительскийИдентификатор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public СвязиАдресныхОбъектов.Ключ Ключ { get; set; }
	}
	[ProtoMember(1)] public СвязиАдресныхОбъектов.Ключ delete { get; set; }
	[ProtoMember(2)] public List<СвязиАдресныхОбъектов.Запись> insert { get; set; } = new List<СвязиАдресныхОбъектов.Запись>();
}

[ProtoContract] public sealed class СервисыЧаевыхКурьеров
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Курьер { get; set; }
		[ProtoMember(3)] public DateTime Период { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public СервисыЧаевыхКурьеров.Ключ Ключ { get; set; }
		[ProtoMember(2)] public string Идентификатор { get; set; }
		[ProtoMember(3)] public Entity Сервис { get; set; }
		[ProtoMember(4)] public string СсылкаДляОплаты { get; set; }
	}
	[ProtoMember(1)] public СервисыЧаевыхКурьеров.Ключ delete { get; set; }
	[ProtoMember(2)] public List<СервисыЧаевыхКурьеров.Запись> insert { get; set; } = new List<СервисыЧаевыхКурьеров.Запись>();
}

[ProtoContract] public sealed class СкладыРегионов
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регион { get; set; }
		[ProtoMember(3)] public Union Склад { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public СкладыРегионов.Ключ Ключ { get; set; }
		[ProtoMember(2)] public bool ВозможнаРеализацияИСГ { get; set; }
		[ProtoMember(3)] public double ДнейПоставки { get; set; }
		[ProtoMember(4)] public double Приоритет { get; set; }
	}
	[ProtoMember(1)] public СкладыРегионов.Ключ delete { get; set; }
	[ProtoMember(2)] public List<СкладыРегионов.Запись> insert { get; set; } = new List<СкладыРегионов.Запись>();
}

[ProtoContract] public sealed class СлужебныеАдресныеСведения
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public double Идентификатор { get; set; }
		//[ProtoMember(3)] public string Ключ { get; set; }
		[ProtoMember(4)] public string Тип { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public СлужебныеАдресныеСведения.Ключ Ключ { get; set; }
		[ProtoMember(2)] public string Значение { get; set; }
	}
	[ProtoMember(1)] public СлужебныеАдресныеСведения.Ключ delete { get; set; }
	[ProtoMember(2)] public List<СлужебныеАдресныеСведения.Запись> insert { get; set; } = new List<СлужебныеАдресныеСведения.Запись>();
}

[ProtoContract] public sealed class СМССписокДляОтправки
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public string ИдентификаторСообщения { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public СМССписокДляОтправки.Ключ Ключ { get; set; }
		[ProtoMember(2)] public DateTime ДатаВыгрузки { get; set; }
		[ProtoMember(3)] public DateTime ДатаСоздания { get; set; }
		[ProtoMember(4)] public double ИдентификаторСообщенияВнешнегоИсточника { get; set; }
		[ProtoMember(5)] public Entity Ответственный { get; set; }
		[ProtoMember(6)] public bool ОтправлятьPUSHУведомление { get; set; }
		[ProtoMember(7)] public bool Выгружен { get; set; }
		[ProtoMember(8)] public Union Документ { get; set; }
		[ProtoMember(9)] public string НомерТелефона { get; set; }
		[ProtoMember(10)] public DateTime ПланируемаяДатаОтправки { get; set; }
		[ProtoMember(11)] public Entity Сайт { get; set; }
		[ProtoMember(12)] public string ТекстСообщения { get; set; }
		[ProtoMember(13)] public Entity ШаблонСообщения { get; set; }
	}
	[ProtoMember(1)] public СМССписокДляОтправки.Ключ delete { get; set; }
	[ProtoMember(2)] public List<СМССписокДляОтправки.Запись> insert { get; set; } = new List<СМССписокДляОтправки.Запись>();
}

[ProtoContract] public sealed class СобытияЗаказовКлиентов
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity ЗаказКлиента { get; set; }
		[ProtoMember(3)] public DateTime Период { get; set; }
		[ProtoMember(4)] public Entity ТипСобытия { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public СобытияЗаказовКлиентов.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Union Пользователь { get; set; }
		[ProtoMember(3)] public string ИдентификаторЗвонка { get; set; }
		[ProtoMember(4)] public string ИдентификаторСообщения { get; set; }
		[ProtoMember(5)] public Entity Оператор { get; set; }
		[ProtoMember(6)] public Entity ШаблонСообщения { get; set; }
		[ProtoMember(7)] public string Комментарий { get; set; }
	}
	[ProtoMember(1)] public СобытияЗаказовКлиентов.Ключ delete { get; set; }
	[ProtoMember(2)] public List<СобытияЗаказовКлиентов.Запись> insert { get; set; } = new List<СобытияЗаказовКлиентов.Запись>();
}

[ProtoContract] public sealed class СоответствиеНоменклатурыКонтрагентов
{
	[ProtoContract] public sealed class Ключ
	{
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public СоответствиеНоменклатурыКонтрагентов.Ключ Ключ { get; set; }
		[ProtoMember(2)] public bool Авто { get; set; }
		[ProtoMember(3)] public Entity Пользователь { get; set; }
		[ProtoMember(4)] public double КоэффициентСхожести { get; set; }
		[ProtoMember(5)] public Entity Номенклатура { get; set; }
	}
	[ProtoMember(1)] public СоответствиеНоменклатурыКонтрагентов.Ключ delete { get; set; }
	[ProtoMember(2)] public List<СоответствиеНоменклатурыКонтрагентов.Запись> insert { get; set; } = new List<СоответствиеНоменклатурыКонтрагентов.Запись>();
}

[ProtoContract] public sealed class СоответствиеПоставщиковКонтрагентов
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity ПоставщикКонтрагента { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public СоответствиеПоставщиковКонтрагентов.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity Контрагент { get; set; }
	}
	[ProtoMember(1)] public СоответствиеПоставщиковКонтрагентов.Ключ delete { get; set; }
	[ProtoMember(2)] public List<СоответствиеПоставщиковКонтрагентов.Запись> insert { get; set; } = new List<СоответствиеПоставщиковКонтрагентов.Запись>();
}

[ProtoContract] public sealed class СоответствиеСтатусовЗаказовКлиентов
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity СтатусОбработки { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public СоответствиеСтатусовЗаказовКлиентов.Ключ Ключ { get; set; }
		[ProtoMember(2)] public bool ЕстьПроблема { get; set; }
		[ProtoMember(3)] public bool Отменен { get; set; }
		[ProtoMember(4)] public bool ПозвонитьКлиенту { get; set; }
		[ProtoMember(5)] public Entity Статус { get; set; }
	}
	[ProtoMember(1)] public СоответствиеСтатусовЗаказовКлиентов.Ключ delete { get; set; }
	[ProtoMember(2)] public List<СоответствиеСтатусовЗаказовКлиентов.Запись> insert { get; set; } = new List<СоответствиеСтатусовЗаказовКлиентов.Запись>();
}

[ProtoContract] public sealed class СоответствиеСтатусовЗаказовКлиентовИСайтов
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Сайт { get; set; }
		[ProtoMember(3)] public Entity СтатусЗаказа { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public СоответствиеСтатусовЗаказовКлиентовИСайтов.Ключ Ключ { get; set; }
		[ProtoMember(2)] public string СтатусНаСайте { get; set; }
	}
	[ProtoMember(1)] public СоответствиеСтатусовЗаказовКлиентовИСайтов.Ключ delete { get; set; }
	[ProtoMember(2)] public List<СоответствиеСтатусовЗаказовКлиентовИСайтов.Запись> insert { get; set; } = new List<СоответствиеСтатусовЗаказовКлиентовИСайтов.Запись>();
}

[ProtoContract] public sealed class СоставыГруппПользователей
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Union ГруппаПользователей { get; set; }
		[ProtoMember(3)] public Union Пользователь { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public СоставыГруппПользователей.Ключ Ключ { get; set; }
		[ProtoMember(2)] public bool Используется { get; set; }
	}
	[ProtoMember(1)] public СоставыГруппПользователей.Ключ delete { get; set; }
	[ProtoMember(2)] public List<СоставыГруппПользователей.Запись> insert { get; set; } = new List<СоставыГруппПользователей.Запись>();
}

[ProtoContract] public sealed class СтавкиНДСПоНоменклатуре
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Товар { get; set; }
		[ProtoMember(3)] public DateTime Период { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public СтавкиНДСПоНоменклатуре.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity СтавкаНДС { get; set; }
	}
	[ProtoMember(1)] public СтавкиНДСПоНоменклатуре.Ключ delete { get; set; }
	[ProtoMember(2)] public List<СтавкиНДСПоНоменклатуре.Запись> insert { get; set; } = new List<СтавкиНДСПоНоменклатуре.Запись>();
}

[ProtoContract] public sealed class СтатусыЗадачПоПретензиям
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity ЗадачаПоПретензии { get; set; }
		[ProtoMember(3)] public DateTime Период { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public СтатусыЗадачПоПретензиям.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity Пользователь { get; set; }
		[ProtoMember(3)] public Entity Исполнитель { get; set; }
		[ProtoMember(4)] public Union Подразделение { get; set; }
		[ProtoMember(5)] public Entity Статус { get; set; }
	}
	[ProtoMember(1)] public СтатусыЗадачПоПретензиям.Ключ delete { get; set; }
	[ProtoMember(2)] public List<СтатусыЗадачПоПретензиям.Запись> insert { get; set; } = new List<СтатусыЗадачПоПретензиям.Запись>();
}

[ProtoContract] public sealed class СтатусыЗаказовКлиентовКОтправкеПартнерам
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity ЗаказКлиента { get; set; }
		[ProtoMember(3)] public Entity Контрагент { get; set; }
		[ProtoMember(4)] public Entity ИдентификаторЗаписи { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public СтатусыЗаказовКлиентовКОтправкеПартнерам.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity Статус { get; set; }
		[ProtoMember(3)] public DateTime ДатаВремя { get; set; }
	}
	[ProtoMember(1)] public СтатусыЗаказовКлиентовКОтправкеПартнерам.Ключ delete { get; set; }
	[ProtoMember(2)] public List<СтатусыЗаказовКлиентовКОтправкеПартнерам.Запись> insert { get; set; } = new List<СтатусыЗаказовКлиентовКОтправкеПартнерам.Запись>();
}

[ProtoContract] public sealed class СтатусыЗаказовМаршрута
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public СтатусыЗаказовМаршрута.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Union ЗаказКлиента { get; set; }
		[ProtoMember(3)] public Union МаршрутныйЛист { get; set; }
		[ProtoMember(4)] public DateTime ДатаКоординат { get; set; }
		[ProtoMember(5)] public Entity ТочкаСамовывоза { get; set; }
		[ProtoMember(6)] public double ВремяОтклоненияОтГрафика { get; set; }
		[ProtoMember(7)] public double РасстояниеОтклоненияОтПунктаНазначения { get; set; }
		[ProtoMember(8)] public Entity СтатусДоставки { get; set; }
	}
	[ProtoMember(1)] public СтатусыЗаказовМаршрута.Ключ delete { get; set; }
	[ProtoMember(2)] public List<СтатусыЗаказовМаршрута.Запись> insert { get; set; } = new List<СтатусыЗаказовМаршрута.Запись>();
}

[ProtoContract] public sealed class СтатусыМаршрутныхЛистов
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity МаршрутныйЛист { get; set; }
		[ProtoMember(3)] public DateTime Период { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public СтатусыМаршрутныхЛистов.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity Статус { get; set; }
	}
	[ProtoMember(1)] public СтатусыМаршрутныхЛистов.Ключ delete { get; set; }
	[ProtoMember(2)] public List<СтатусыМаршрутныхЛистов.Запись> insert { get; set; } = new List<СтатусыМаршрутныхЛистов.Запись>();
}

[ProtoContract] public sealed class СтатусыОбработкиЗаказовПоЭквайрингу
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity ВидОперации { get; set; }
		[ProtoMember(3)] public Entity ЗаказКлиента { get; set; }
		[ProtoMember(4)] public DateTime Период { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public СтатусыОбработкиЗаказовПоЭквайрингу.Ключ Ключ { get; set; }
		[ProtoMember(2)] public string RRN { get; set; }
		[ProtoMember(3)] public Entity БанкЭквайер { get; set; }
		[ProtoMember(4)] public DateTime ДатаРегистрацииЗаказа { get; set; }
		[ProtoMember(5)] public string КодАвторизации { get; set; }
		[ProtoMember(6)] public double КодОтвета { get; set; }
		[ProtoMember(7)] public double КодОшибки { get; set; }
		[ProtoMember(8)] public Entity КонтрагентЭквайер { get; set; }
		[ProtoMember(9)] public string НомерЗаказа { get; set; }
		[ProtoMember(10)] public string НомерКарты { get; set; }
		[ProtoMember(11)] public string НомерТранзакции { get; set; }
		[ProtoMember(12)] public string ОписаниеКодаОтвета { get; set; }
		[ProtoMember(13)] public string ОписаниеОшибки { get; set; }
		[ProtoMember(14)] public string СодержимоеОтветаСтрокой { get; set; }
		[ProtoMember(15)] public Entity СтатусЗаказа { get; set; }
		[ProtoMember(16)] public string СтатусПлатежа { get; set; }
		[ProtoMember(17)] public double Сумма { get; set; }
		[ProtoMember(18)] public double СуммаБонусовВозвращенная { get; set; }
		[ProtoMember(19)] public double СуммаБонусовПодтвержденная { get; set; }
		[ProtoMember(20)] public double СуммаБонусовСписанная { get; set; }
		[ProtoMember(21)] public double СуммаВозвращенная { get; set; }
		[ProtoMember(22)] public double СуммаПодтвержденная { get; set; }
		[ProtoMember(23)] public double СуммаСписанная { get; set; }
		[ProtoMember(24)] public Entity Чек { get; set; }
	}
	[ProtoMember(1)] public СтатусыОбработкиЗаказовПоЭквайрингу.Ключ delete { get; set; }
	[ProtoMember(2)] public List<СтатусыОбработкиЗаказовПоЭквайрингу.Запись> insert { get; set; } = new List<СтатусыОбработкиЗаказовПоЭквайрингу.Запись>();
}

[ProtoContract] public sealed class СтатусыОбработкиЗаказовПоЭквайрингуБонусныеПрограммы
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity БонуснаяПрограмма { get; set; }
		[ProtoMember(3)] public Entity ВидОперации { get; set; }
		[ProtoMember(4)] public Entity ЗаказКлиента { get; set; }
		[ProtoMember(5)] public DateTime Период { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public СтатусыОбработкиЗаказовПоЭквайрингуБонусныеПрограммы.Ключ Ключ { get; set; }
		[ProtoMember(2)] public double СуммаБонусовВозвращенная { get; set; }
		[ProtoMember(3)] public double СуммаБонусовПодтвержденная { get; set; }
		[ProtoMember(4)] public double СуммаБонусовСписанная { get; set; }
	}
	[ProtoMember(1)] public СтатусыОбработкиЗаказовПоЭквайрингуБонусныеПрограммы.Ключ delete { get; set; }
	[ProtoMember(2)] public List<СтатусыОбработкиЗаказовПоЭквайрингуБонусныеПрограммы.Запись> insert { get; set; } = new List<СтатусыОбработкиЗаказовПоЭквайрингуБонусныеПрограммы.Запись>();
}

[ProtoContract] public sealed class СтатусыПретензийКлиентов
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity ПретензияКлиента { get; set; }
		[ProtoMember(3)] public DateTime Период { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public СтатусыПретензийКлиентов.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity Пользователь { get; set; }
		[ProtoMember(3)] public Entity Куратор { get; set; }
		[ProtoMember(4)] public Entity Статус { get; set; }
	}
	[ProtoMember(1)] public СтатусыПретензийКлиентов.Ключ delete { get; set; }
	[ProtoMember(2)] public List<СтатусыПретензийКлиентов.Запись> insert { get; set; } = new List<СтатусыПретензийКлиентов.Запись>();
}

[ProtoContract] public sealed class ТаблицыГруппДоступа
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity ГруппаДоступа { get; set; }
		[ProtoMember(3)] public Union Таблица { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ТаблицыГруппДоступа.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Union ТипТаблицы { get; set; }
		[ProtoMember(3)] public bool ПравоДобавление { get; set; }
		[ProtoMember(4)] public bool ПравоДобавлениеБезОграничения { get; set; }
		[ProtoMember(5)] public bool ПравоИзменение { get; set; }
		[ProtoMember(6)] public bool ПравоИзменениеБезОграничения { get; set; }
		[ProtoMember(7)] public bool ПравоЧтениеБезОграничения { get; set; }
	}
	[ProtoMember(1)] public ТаблицыГруппДоступа.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ТаблицыГруппДоступа.Запись> insert { get; set; } = new List<ТаблицыГруппДоступа.Запись>();
}

[ProtoContract] public sealed class ТабПродажиАстраЗенека
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ТабПродажиАстраЗенека.Ключ Ключ { get; set; }
		[ProtoMember(2)] public string CARDNUMBER { get; set; }
		[ProtoMember(3)] public string id_Товар { get; set; }
		[ProtoMember(4)] public string Line { get; set; }
		[ProtoMember(5)] public string LineDoc { get; set; }
		[ProtoMember(6)] public DateTime OPERDATE { get; set; }
		[ProtoMember(7)] public string PRODUCTCODE { get; set; }
		[ProtoMember(8)] public Entity project { get; set; }
		[ProtoMember(9)] public string ИДДокумента { get; set; }
		[ProtoMember(10)] public string Идентификатор { get; set; }
		[ProtoMember(11)] public string ИДПартии { get; set; }
		[ProtoMember(12)] public string Количество { get; set; }
		[ProtoMember(13)] public string НомерДокумента { get; set; }
		[ProtoMember(14)] public double ПроцентСкидки { get; set; }
		[ProtoMember(15)] public string СтавкаНДС { get; set; }
		[ProtoMember(16)] public string Сумма { get; set; }
		[ProtoMember(17)] public string СуммаСкидки { get; set; }
		[ProtoMember(18)] public Entity Товар { get; set; }
		[ProtoMember(19)] public string Цена { get; set; }
		[ProtoMember(20)] public bool Действует { get; set; }
		[ProtoMember(21)] public DateTime Период { get; set; }
	}
	[ProtoMember(1)] public ТабПродажиАстраЗенека.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ТабПродажиАстраЗенека.Запись> insert { get; set; } = new List<ТабПродажиАстраЗенека.Запись>();
}

[ProtoContract] public sealed class ТерминалыОнлайнОплаты
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity АдресХранения { get; set; }
		[ProtoMember(3)] public Entity БанкЭквайер { get; set; }
		[ProtoMember(4)] public Entity Курьер { get; set; }
		[ProtoMember(5)] public Entity СпособОнлайнОплаты { get; set; }
		[ProtoMember(6)] public DateTime Период { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ТерминалыОнлайнОплаты.Ключ Ключ { get; set; }
		[ProtoMember(2)] public bool Заявка { get; set; }
		[ProtoMember(3)] public string Идентификатор { get; set; }
		[ProtoMember(4)] public string НомерДоговораМерчанта { get; set; }
		[ProtoMember(5)] public string НомерТелефона { get; set; }
		[ProtoMember(6)] public DateTime ДатаБлокировки { get; set; }
	}
	[ProtoMember(1)] public ТерминалыОнлайнОплаты.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ТерминалыОнлайнОплаты.Запись> insert { get; set; } = new List<ТерминалыОнлайнОплаты.Запись>();
}

[ProtoContract] public sealed class ТоварыПоставщика
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public DateTime ДатаВвода { get; set; }
		[ProtoMember(3)] public string КодТовараПоставщика { get; set; }
		[ProtoMember(4)] public string Наименование { get; set; }
		[ProtoMember(5)] public Entity Поставщик { get; set; }
		[ProtoMember(6)] public string Производитель { get; set; }
		[ProtoMember(7)] public Entity Товар { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ТоварыПоставщика.Ключ Ключ { get; set; }
	}
	[ProtoMember(1)] public ТоварыПоставщика.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ТоварыПоставщика.Запись> insert { get; set; } = new List<ТоварыПоставщика.Запись>();
}

[ProtoContract] public sealed class ТоварыСобранныеПоЗаказуВАптеке
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity ЗаказКлиента { get; set; }
		[ProtoMember(3)] public bool МДЛП { get; set; }
		[ProtoMember(4)] public Entity Номенклатура { get; set; }
		[ProtoMember(5)] public Entity Партия { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ТоварыСобранныеПоЗаказуВАптеке.Ключ Ключ { get; set; }
		[ProtoMember(2)] public double КоличествоСобрано { get; set; }
	}
	[ProtoMember(1)] public ТоварыСобранныеПоЗаказуВАптеке.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ТоварыСобранныеПоЗаказуВАптеке.Запись> insert { get; set; } = new List<ТоварыСобранныеПоЗаказуВАптеке.Запись>();
}

[ProtoContract] public sealed class ТребуемыйКомплектДокументов
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity ВидПечатнойФормы { get; set; }
		[ProtoMember(3)] public Entity Контрагент { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ТребуемыйКомплектДокументов.Ключ Ключ { get; set; }
		[ProtoMember(2)] public double КоличествоЭкземпляров { get; set; }
	}
	[ProtoMember(1)] public ТребуемыйКомплектДокументов.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ТребуемыйКомплектДокументов.Запись> insert { get; set; } = new List<ТребуемыйКомплектДокументов.Запись>();
}

[ProtoContract] public sealed class ТреккингКурьера
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public DateTime Дата { get; set; }
		[ProtoMember(3)] public Entity Курьер { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ТреккингКурьера.Ключ Ключ { get; set; }
		[ProtoMember(2)] public double Долгота { get; set; }
		[ProtoMember(3)] public double РасстояниеДоПВЗ { get; set; }
		[ProtoMember(4)] public double Широта { get; set; }
	}
	[ProtoMember(1)] public ТреккингКурьера.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ТреккингКурьера.Запись> insert { get; set; } = new List<ТреккингКурьера.Запись>();
}

[ProtoContract] public sealed class УровниСокращенийАдресныхСведений
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public string Сокращение { get; set; }
		[ProtoMember(3)] public double Уровень { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public УровниСокращенийАдресныхСведений.Ключ Ключ { get; set; }
		[ProtoMember(2)] public string Значение { get; set; }
	}
	[ProtoMember(1)] public УровниСокращенийАдресныхСведений.Ключ delete { get; set; }
	[ProtoMember(2)] public List<УровниСокращенийАдресныхСведений.Запись> insert { get; set; } = new List<УровниСокращенийАдресныхСведений.Запись>();
}

[ProtoContract] public sealed class УчастникиФранчайзи
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity АдресХранения { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public УчастникиФранчайзи.Ключ Ключ { get; set; }
		[ProtoMember(2)] public DateTime ДатаПо { get; set; }
		[ProtoMember(3)] public bool Использовать { get; set; }
		[ProtoMember(4)] public Entity Франчайзи { get; set; }
	}
	[ProtoMember(1)] public УчастникиФранчайзи.Ключ delete { get; set; }
	[ProtoMember(2)] public List<УчастникиФранчайзи.Запись> insert { get; set; } = new List<УчастникиФранчайзи.Запись>();
}

[ProtoContract] public sealed class ФармЛицензииКонтрагентов
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Контрагент { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ФармЛицензииКонтрагентов.Ключ Ключ { get; set; }
		[ProtoMember(2)] public DateTime Дата { get; set; }
		[ProtoMember(3)] public string Номер { get; set; }
	}
	[ProtoMember(1)] public ФармЛицензииКонтрагентов.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ФармЛицензииКонтрагентов.Запись> insert { get; set; } = new List<ФармЛицензииКонтрагентов.Запись>();
}

[ProtoContract] public sealed class ХарактеристикиМаршрутныхЛистов
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity МаршрутныйЛист { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ХарактеристикиМаршрутныхЛистов.Ключ Ключ { get; set; }
		[ProtoMember(2)] public double Вес { get; set; }
		[ProtoMember(3)] public bool ЕстьОпозданияПростои { get; set; }
		[ProtoMember(4)] public double КоличествоТочекДоставки { get; set; }
		[ProtoMember(5)] public double Объем { get; set; }
	}
	[ProtoMember(1)] public ХарактеристикиМаршрутныхЛистов.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ХарактеристикиМаршрутныхЛистов.Запись> insert { get; set; } = new List<ХарактеристикиМаршрутныхЛистов.Запись>();
}

[ProtoContract] public sealed class ЦеныГосРеестра
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ЦеныГосРеестра.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity Номенклатура { get; set; }
		[ProtoMember(3)] public DateTime ДатаРегистрационногоУдостоверения { get; set; }
		[ProtoMember(4)] public string НомерРегистрационногоУдостоверения { get; set; }
		[ProtoMember(5)] public double Цена { get; set; }
		[ProtoMember(6)] public DateTime Период { get; set; }
	}
	[ProtoMember(1)] public ЦеныГосРеестра.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ЦеныГосРеестра.Запись> insert { get; set; } = new List<ЦеныГосРеестра.Запись>();
}

[ProtoContract] public sealed class ЦеныГосРеестраПоШтрихкодам
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ЦеныГосРеестраПоШтрихкодам.Ключ Ключ { get; set; }
		[ProtoMember(2)] public string Штрихкод { get; set; }
		[ProtoMember(3)] public DateTime ДатаРегистрационногоУдостоверения { get; set; }
		[ProtoMember(4)] public string НомерРегистрационногоУдостоверения { get; set; }
		[ProtoMember(5)] public double Цена { get; set; }
		[ProtoMember(6)] public DateTime Период { get; set; }
	}
	[ProtoMember(1)] public ЦеныГосРеестраПоШтрихкодам.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ЦеныГосРеестраПоШтрихкодам.Запись> insert { get; set; } = new List<ЦеныГосРеестраПоШтрихкодам.Запись>();
}

[ProtoContract] public sealed class ЦеныНоменклатурыВАптеке
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Регистратор { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ЦеныНоменклатурыВАптеке.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity Аптека { get; set; }
		[ProtoMember(3)] public Entity Номенклатура { get; set; }
		[ProtoMember(4)] public Entity Партия { get; set; }
		[ProtoMember(5)] public double Цена { get; set; }
		[ProtoMember(6)] public DateTime Период { get; set; }
	}
	[ProtoMember(1)] public ЦеныНоменклатурыВАптеке.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ЦеныНоменклатурыВАптеке.Запись> insert { get; set; } = new List<ЦеныНоменклатурыВАптеке.Запись>();
}

[ProtoContract] public sealed class ШаблоныПечатиКиЗДляКонтрагентов
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity Контрагент { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ШаблоныПечатиКиЗДляКонтрагентов.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity ШаблонПечати { get; set; }
	}
	[ProtoMember(1)] public ШаблоныПечатиКиЗДляКонтрагентов.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ШаблоныПечатиКиЗДляКонтрагентов.Запись> insert { get; set; } = new List<ШаблоныПечатиКиЗДляКонтрагентов.Запись>();
}

[ProtoContract] public sealed class ШаблоныСообщенийЗаказовКлиентов
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public bool ЕстьПроблема { get; set; }
		[ProtoMember(3)] public bool Отменен { get; set; }
		[ProtoMember(4)] public Entity СпособДоставки { get; set; }
		[ProtoMember(5)] public Entity СтатусДокумента { get; set; }
		[ProtoMember(6)] public Entity ТипСобытия { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ШаблоныСообщенийЗаказовКлиентов.Ключ Ключ { get; set; }
		[ProtoMember(2)] public bool ОтправлятьPUSHУведомление { get; set; }
		[ProtoMember(3)] public Entity ШаблонПисьма { get; set; }
		[ProtoMember(4)] public Entity ШаблонСМС { get; set; }
	}
	[ProtoMember(1)] public ШаблоныСообщенийЗаказовКлиентов.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ШаблоныСообщенийЗаказовКлиентов.Запись> insert { get; set; } = new List<ШаблоныСообщенийЗаказовКлиентов.Запись>();
}

[ProtoContract] public sealed class ШаблоныФайловОбменаКонтрагентов
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity ВидИмпортируемыхДанных { get; set; }
		[ProtoMember(3)] public Entity Контрагент { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ШаблоныФайловОбменаКонтрагентов.Ключ Ключ { get; set; }
		[ProtoMember(2)] public Entity ШаблонФайла { get; set; }
	}
	[ProtoMember(1)] public ШаблоныФайловОбменаКонтрагентов.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ШаблоныФайловОбменаКонтрагентов.Запись> insert { get; set; } = new List<ШаблоныФайловОбменаКонтрагентов.Запись>();
}

[ProtoContract] public sealed class ШтрихкодыУпаковокЗаказовКлиентов
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public Entity ЗаказКлиента { get; set; }
		[ProtoMember(3)] public string ШтрихкодВложеннойУпаковки { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ШтрихкодыУпаковокЗаказовКлиентов.Ключ Ключ { get; set; }
		[ProtoMember(2)] public bool СодержитСтекло { get; set; }
		[ProtoMember(3)] public bool ХранитьВХолоде { get; set; }
		[ProtoMember(4)] public Union ДокументТранспортировки { get; set; }
		[ProtoMember(5)] public string ЗСЯ { get; set; }
		[ProtoMember(6)] public bool КоробочнаяСборка { get; set; }
		[ProtoMember(7)] public string КороткийНомерВложеннойУпаковки { get; set; }
		[ProtoMember(8)] public string КороткийНомерУпаковки { get; set; }
		[ProtoMember(9)] public string ШтрихкодУпаковки { get; set; }
	}
	[ProtoMember(1)] public ШтрихкодыУпаковокЗаказовКлиентов.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ШтрихкодыУпаковокЗаказовКлиентов.Запись> insert { get; set; } = new List<ШтрихкодыУпаковокЗаказовКлиентов.Запись>();
}

[ProtoContract] public sealed class ЭлектронныеНакладныеПоставщиков
{
	[ProtoContract] public sealed class Ключ
	{
		[ProtoMember(2)] public double ИД { get; set; }
		[ProtoMember(3)] public string ИмяФайла { get; set; }
		[ProtoMember(4)] public string НомерДокумента { get; set; }
	}
	[ProtoContract] public sealed class Запись
	{
		[ProtoMember(1)] public ЭлектронныеНакладныеПоставщиков.Ключ Ключ { get; set; }
		[ProtoMember(2)] public string ГТД { get; set; }
		[ProtoMember(3)] public DateTime ДатаВыдачиСертификата { get; set; }
		[ProtoMember(4)] public DateTime ДатаДокумента { get; set; }
		[ProtoMember(5)] public DateTime ДатаЗагрузки { get; set; }
		[ProtoMember(6)] public DateTime ДатаЗаказаПоставщику { get; set; }
		[ProtoMember(7)] public DateTime ДатаИзготовления { get; set; }
		[ProtoMember(8)] public DateTime ДатаОплаты { get; set; }
		[ProtoMember(9)] public DateTime ДатаРегистрацииЦены { get; set; }
		[ProtoMember(10)] public DateTime ДатаСчета { get; set; }
		[ProtoMember(11)] public DateTime ДействиеСертификата { get; set; }
		[ProtoMember(12)] public Entity Договор { get; set; }
		[ProtoMember(13)] public string ЕдИзм { get; set; }
		[ProtoMember(14)] public double ЖНВЛС { get; set; }
		[ProtoMember(15)] public Entity ЗагруженныйДокумент { get; set; }
		[ProtoMember(16)] public double ЗарегистрированнаяЦена { get; set; }
		[ProtoMember(17)] public string Код { get; set; }
		[ProtoMember(18)] public string КодГрузополучателя { get; set; }
		[ProtoMember(19)] public string КодЕГК { get; set; }
		[ProtoMember(20)] public string КодПоставщика { get; set; }
		[ProtoMember(21)] public double КолВУпаковке { get; set; }
		[ProtoMember(22)] public double Количество { get; set; }
		[ProtoMember(23)] public string Наименование { get; set; }
		[ProtoMember(24)] public double НаценкаИмпортера { get; set; }
		[ProtoMember(25)] public double НаценкаОптовогоЗвена { get; set; }
		[ProtoMember(26)] public double НДСЕдиницыТовара { get; set; }
		[ProtoMember(27)] public double НомерЗаказаПоставщику { get; set; }
		[ProtoMember(28)] public string НомерСертификата { get; set; }
		[ProtoMember(29)] public string НомерСчета { get; set; }
		[ProtoMember(30)] public double ОптоваяЦена { get; set; }
		[ProtoMember(31)] public double ОптоваяЦенаБезНДС { get; set; }
		[ProtoMember(32)] public string ОрганСертификации { get; set; }
		[ProtoMember(33)] public Entity Поставщик { get; set; }
		[ProtoMember(34)] public string Производитель { get; set; }
		[ProtoMember(35)] public string Серия { get; set; }
		[ProtoMember(36)] public DateTime СрокГодности { get; set; }
		[ProtoMember(37)] public double СтавкаНДС { get; set; }
		[ProtoMember(38)] public double СтавкаНСП { get; set; }
		[ProtoMember(39)] public string СтранаПроизводителя { get; set; }
		[ProtoMember(40)] public string СтранаПроисхождения { get; set; }
		[ProtoMember(41)] public double СуммаБезНДСПоСтроке { get; set; }
		[ProtoMember(42)] public bool СуммаВключаетНДС { get; set; }
		[ProtoMember(43)] public double СуммаДокументаБезНДС { get; set; }
		[ProtoMember(44)] public double СуммаДокументаНДС10 { get; set; }
		[ProtoMember(45)] public double СуммаДокументаНДС10_БезНДС { get; set; }
		[ProtoMember(46)] public double СуммаДокументаНДС18 { get; set; }
		[ProtoMember(47)] public double СуммаДокументаНДС18_БезНДС { get; set; }
		[ProtoMember(48)] public double СуммаДокументаНеОблагНДС { get; set; }
		[ProtoMember(49)] public double СуммаДокументаСНДС { get; set; }
		[ProtoMember(50)] public double СуммаНДС10ПоСтроке { get; set; }
		[ProtoMember(51)] public double СуммаНДС10ПоСтрокеБезНДС { get; set; }
		[ProtoMember(52)] public double СуммаНДС18ПоСтроке { get; set; }
		[ProtoMember(53)] public double СуммаНДС18ПоСтрокеБезНДС { get; set; }
		[ProtoMember(54)] public double СуммаНДСПоСтроке { get; set; }
		[ProtoMember(55)] public double СуммаНеОблагНДСПоСтроке { get; set; }
		[ProtoMember(56)] public double СуммаПоДокументуСНДС10 { get; set; }
		[ProtoMember(57)] public double СуммаПоДокументуСНДС18 { get; set; }
		[ProtoMember(58)] public double СуммаПоСтрокеСНДС10 { get; set; }
		[ProtoMember(59)] public double СуммаПоСтрокеСНДС18 { get; set; }
		[ProtoMember(60)] public double СуммаСтрока { get; set; }
		[ProtoMember(61)] public double СуммаСтрока_2 { get; set; }
		[ProtoMember(62)] public double ЦенаПроизводителяБезНДС { get; set; }
		[ProtoMember(63)] public double ЦенаПроизводителяСНДС { get; set; }
		[ProtoMember(64)] public double ЦенаРозничная { get; set; }
		[ProtoMember(65)] public string ШтрихКод { get; set; }
	}
	[ProtoMember(1)] public ЭлектронныеНакладныеПоставщиков.Ключ delete { get; set; }
	[ProtoMember(2)] public List<ЭлектронныеНакладныеПоставщиков.Запись> insert { get; set; } = new List<ЭлектронныеНакладныеПоставщиков.Запись>();
}
}
}
