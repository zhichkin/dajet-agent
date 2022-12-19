using ProtoBuf;
using DaJet.ProtoBuf;
using System;
using System.Collections.Generic;

namespace erp_model
{
namespace Справочник
{

[ProtoContract] public sealed class ABCКлассы
{
	[ProtoMember(1)] public double ПроцентПоУмолчанию { get; set; }
	[ProtoMember(2)] public double Код { get; set; }
	[ProtoMember(3)] public string Наименование { get; set; }
	[ProtoMember(4)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class delПрофильПоставщика
{
	[ProtoContract] public sealed class ПроцентДозаказаУпаковкаЗапись
	{
		[ProtoMember(1)] public double Коэффициент { get; set; }
		[ProtoMember(2)] public double ЦенаУпаковки { get; set; }
	}
	[ProtoContract] public sealed class ПроцентКоррекцииПрайсаЗапись
	{
		[ProtoMember(1)] public double Коэффициент { get; set; }
		[ProtoMember(2)] public double МаксЦена { get; set; }
	}
	[ProtoMember(1)] public bool Активный { get; set; }
	[ProtoMember(2)] public DateTime ДатаОжидаемойПоставки { get; set; }
	[ProtoMember(3)] public double КоэфКорректировкиОстатка { get; set; }
	[ProtoMember(4)] public double КратностьУпаковки { get; set; }
	[ProtoMember(5)] public double МинСрокГодности { get; set; }
	[ProtoMember(6)] public double МинСуммаЗаказа { get; set; }
	[ProtoMember(7)] public bool Отгружает { get; set; }
	[ProtoMember(8)] public double Отсрочка { get; set; }
	[ProtoMember(9)] public bool ПрайсДоступенОператорам { get; set; }
	[ProtoMember(10)] public double ПроцентРетробонус { get; set; }
	[ProtoMember(11)] public double СрокГодностиПоУмолчанию { get; set; }
	[ProtoMember(12)] public double ЧастотаЗагрузкиПрайса { get; set; }
	[ProtoMember(13)] public double Код { get; set; }
	[ProtoMember(14)] public string Наименование { get; set; }
	[ProtoMember(15)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(16)] public List<delПрофильПоставщика.ПроцентДозаказаУпаковкаЗапись> ПроцентДозаказаУпаковка { get; set; } = new List<delПрофильПоставщика.ПроцентДозаказаУпаковкаЗапись>();
	[ProtoMember(17)] public List<delПрофильПоставщика.ПроцентКоррекцииПрайсаЗапись> ПроцентКоррекцииПрайса { get; set; } = new List<delПрофильПоставщика.ПроцентКоррекцииПрайсаЗапись>();
}

[ProtoContract] public sealed class АдресХранения
{
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
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public string Адрес { get; set; }
	[ProtoMember(3)] public bool ЗагружатьПриходыПоСкладуИз77 { get; set; }
	[ProtoMember(4)] public bool ЗагружатьСамовывозы { get; set; }
	[ProtoMember(5)] public Entity КонтрагентДляПеремещений { get; set; }
	[ProtoMember(6)] public double КоэффициентКорректировкиСреднейСкорости { get; set; }
	[ProtoMember(7)] public DateTime ЛицензияДатаВыдачи { get; set; }
	[ProtoMember(8)] public string ЛицензияНомер { get; set; }
	[ProtoMember(9)] public double МинимальноеКоличествоДнейНаличия { get; set; }
	[ProtoMember(10)] public double НомерАптеки { get; set; }
	[ProtoMember(11)] public double ОсновнойВидЦенника { get; set; }
	[ProtoMember(12)] public Entity РегионРаботы { get; set; }
	[ProtoMember(13)] public Entity Статус { get; set; }
	[ProtoMember(14)] public Entity ТипСклада { get; set; }
	[ProtoMember(15)] public Entity Фирма { get; set; }
	[ProtoMember(16)] public double Код { get; set; }
	[ProtoMember(17)] public string Наименование { get; set; }
	[ProtoMember(18)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(19)] public Entity Администратор { get; set; }
	[ProtoMember(20)] public DateTime ДатаОткрытия { get; set; }
	[ProtoMember(21)] public Entity Расположение { get; set; }
	[ProtoMember(22)] public Entity ТерриториальныйУправляющий { get; set; }
	[ProtoMember(23)] public string БазовыйURL { get; set; }
	[ProtoMember(24)] public bool УчитыватьПродажиСобранныеНаДругихСкладах { get; set; }
	[ProtoMember(25)] public double ЦифровойКодСФ { get; set; }
	[ProtoMember(26)] public bool ЕстьЛицензияНаОптовуюТорговлюЛС { get; set; }
	[ProtoMember(27)] public Entity ВидWMS { get; set; }
	[ProtoMember(28)] public string ИдентификаторТочкиОтпускаЕМИАС { get; set; }
	[ProtoMember(29)] public double МаксимумСтрокВЗаказеПоставщику { get; set; }
	[ProtoMember(30)] public Entity СписокАптекДляСреднейСкоростиПродаж { get; set; }
	[ProtoMember(31)] public bool ОрдернаяСхемаПеремещений { get; set; }
	[ProtoMember(32)] public Entity ВидДоставки { get; set; }
	[ProtoMember(33)] public Entity КатегорияАптекиАссортиментнаяМатрица { get; set; }
	[ProtoMember(34)] public Entity КатегорияАптекиОбщая { get; set; }
	[ProtoMember(35)] public Entity КатегорияАптекиУправление { get; set; }
	[ProtoMember(36)] public bool ИсключитьПотребностиРеципиентов { get; set; }
	[ProtoMember(37)] public double ОбщаяПлощадь { get; set; }
	[ProtoMember(38)] public double ПлощадьТорговогоЗала { get; set; }
	[ProtoMember(39)] public Entity ДиректорОкруга { get; set; }
	[ProtoMember(40)] public string РегионУправления { get; set; }
	[ProtoMember(41)] public List<АдресХранения.КонтактнаяИнформацияЗапись> КонтактнаяИнформация { get; set; } = new List<АдресХранения.КонтактнаяИнформацияЗапись>();
}

[ProtoContract] public sealed class АктОбУничтоженииПрисоединенныеФайлы
{
	[ProtoMember(1)] public Union Автор { get; set; }
	[ProtoMember(2)] public Entity ВладелецФайла { get; set; }
	[ProtoMember(3)] public DateTime ДатаЗаема { get; set; }
	[ProtoMember(4)] public DateTime ДатаМодификацииУниверсальная { get; set; }
	[ProtoMember(5)] public DateTime ДатаСоздания { get; set; }
	[ProtoMember(6)] public bool Зашифрован { get; set; }
	[ProtoMember(7)] public Union Изменил { get; set; }
	[ProtoMember(8)] public double ИндексКартинки { get; set; }
	[ProtoMember(9)] public string Описание { get; set; }
	[ProtoMember(10)] public bool ПодписанЭП { get; set; }
	[ProtoMember(11)] public string ПутьКФайлу { get; set; }
	[ProtoMember(12)] public double Размер { get; set; }
	[ProtoMember(13)] public string Расширение { get; set; }
	[ProtoMember(14)] public Union Редактирует { get; set; }
	[ProtoMember(15)] public Entity СтатусИзвлеченияТекста { get; set; }
	[ProtoMember(16)] public Entity ТекстХранилище { get; set; }
	[ProtoMember(17)] public Entity ТипХраненияФайла { get; set; }
	[ProtoMember(18)] public Entity Том { get; set; }
	[ProtoMember(19)] public Entity ФайлХранилище { get; set; }
	[ProtoMember(20)] public bool ХранитьВерсии { get; set; }
	[ProtoMember(21)] public string Наименование { get; set; }
	[ProtoMember(22)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(23)] public Entity Родитель { get; set; }
	[ProtoMember(24)] public bool ЭтоГруппа { get; set; }
}

[ProtoContract] public sealed class Банки
{
	[ProtoMember(1)] public string Адрес { get; set; }
	[ProtoMember(2)] public string Город { get; set; }
	[ProtoMember(3)] public string КоррСчет { get; set; }
	[ProtoMember(4)] public double РучноеИзменение { get; set; }
	[ProtoMember(5)] public string СВИФТБИК { get; set; }
	[ProtoMember(6)] public Entity Страна { get; set; }
	[ProtoMember(7)] public string Телефоны { get; set; }
	[ProtoMember(8)] public string Код { get; set; }
	[ProtoMember(9)] public string Наименование { get; set; }
	[ProtoMember(10)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(11)] public Entity Родитель { get; set; }
}

[ProtoContract] public sealed class БанковскиеСчета
{
	[ProtoMember(1)] public Entity Банк { get; set; }
	[ProtoMember(2)] public Entity БанкРасчетов { get; set; }
	[ProtoMember(3)] public Entity ВариантВыводаМесяца { get; set; }
	[ProtoMember(4)] public Entity ВариантУказанияКПП { get; set; }
	[ProtoMember(5)] public string ВидСчета { get; set; }
	[ProtoMember(6)] public DateTime ДатаЗакрытия { get; set; }
	[ProtoMember(7)] public DateTime ДатаОткрытия { get; set; }
	[ProtoMember(8)] public bool Недействителен { get; set; }
	[ProtoMember(9)] public string НомерИДатаРазрешения { get; set; }
	[ProtoMember(10)] public string НомерСчета { get; set; }
	[ProtoMember(11)] public bool СуммаБезКопеек { get; set; }
	[ProtoMember(12)] public string СчетУчета { get; set; }
	[ProtoMember(13)] public string ТекстКорреспондента { get; set; }
	[ProtoMember(14)] public string ТекстНазначения { get; set; }
	[ProtoMember(15)] public Entity Владелец { get; set; }
	[ProtoMember(16)] public string Код { get; set; }
	[ProtoMember(17)] public string Наименование { get; set; }
	[ProtoMember(18)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class БрендыПартнеров
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public string АнглНаименование { get; set; }
	[ProtoMember(3)] public bool ВыгружатьНаСайтНомерТелефонаКЦ { get; set; }
	[ProtoMember(4)] public string Код { get; set; }
	[ProtoMember(5)] public string Наименование { get; set; }
	[ProtoMember(6)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(7)] public DateTime ДатаОбновленияИзMDM { get; set; }
	[ProtoMember(8)] public bool ЗагружатьНоменклатуруИзMDM { get; set; }
	[ProtoMember(9)] public bool ИспользоватьОграничениеНоменклатуры { get; set; }
}

[ProtoContract] public sealed class БрендыПроизводителей
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public string АнглНаименование { get; set; }
	[ProtoMember(3)] public bool ОтображатьНаСайте { get; set; }
	[ProtoMember(4)] public double Код { get; set; }
	[ProtoMember(5)] public string Наименование { get; set; }
	[ProtoMember(6)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class Вендор
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public string Код { get; set; }
	[ProtoMember(3)] public string Наименование { get; set; }
	[ProtoMember(4)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(5)] public Entity Родитель { get; set; }
	[ProtoMember(6)] public Entity Страна { get; set; }
}

[ProtoContract] public sealed class ВидыДокументов
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public string Код { get; set; }
	[ProtoMember(3)] public string Наименование { get; set; }
	[ProtoMember(4)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class ВидыКонтактнойИнформации
{
	[ProtoContract] public sealed class ПредставленияЗапись
	{
		[ProtoMember(1)] public string КодЯзыка { get; set; }
		[ProtoMember(2)] public string Наименование { get; set; }
	}
	[ProtoMember(1)] public string ВидПоляДругое { get; set; }
	[ProtoMember(2)] public bool ВключатьСтрануВПредставление { get; set; }
	[ProtoMember(3)] public bool ЗапретитьРедактированиеПользователем { get; set; }
	[ProtoMember(4)] public bool Используется { get; set; }
	[ProtoMember(5)] public bool МеждународныйФорматАдреса { get; set; }
	[ProtoMember(6)] public bool МожноИзменятьСпособРедактирования { get; set; }
	[ProtoMember(7)] public bool ОбязательноеЗаполнение { get; set; }
	[ProtoMember(8)] public bool ПроверятьКорректность { get; set; }
	[ProtoMember(9)] public bool ПроверятьПоФИАС { get; set; }
	[ProtoMember(10)] public bool РазрешитьВводНесколькихЗначений { get; set; }
	[ProtoMember(11)] public double РеквизитДопУпорядочивания { get; set; }
	[ProtoMember(12)] public bool СкрыватьНеактуальныеАдреса { get; set; }
	[ProtoMember(13)] public bool ТелефонCДобавочнымНомером { get; set; }
	[ProtoMember(14)] public Entity Тип { get; set; }
	[ProtoMember(15)] public bool ТолькоНациональныйАдрес { get; set; }
	[ProtoMember(16)] public bool УказыватьОКТМО { get; set; }
	[ProtoMember(17)] public bool ХранитьИсториюИзменений { get; set; }
	[ProtoMember(18)] public string Наименование { get; set; }
	[ProtoMember(19)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(20)] public Entity Родитель { get; set; }
	[ProtoMember(21)] public bool ВводитьНомерПоМаске { get; set; }
	[ProtoMember(22)] public string ВидРедактирования { get; set; }
	[ProtoMember(23)] public string ИдентификаторДляФормул { get; set; }
	[ProtoMember(24)] public string ИмяГруппы { get; set; }
	[ProtoMember(25)] public string ИмяПредопределенногоВида { get; set; }
	[ProtoMember(26)] public bool ИсправлятьУстаревшиеАдреса { get; set; }
	[ProtoMember(27)] public string МаскаНомераТелефона { get; set; }
	[ProtoMember(28)] public Union EmailАдресаХранения { get; set; }
	[ProtoMember(29)] public Union EmailДляОтправкиЗаказов { get; set; }
	[ProtoMember(30)] public Union EmailДляСвязи { get; set; }
	[ProtoMember(31)] public Union EmailКлиента { get; set; }
	[ProtoMember(32)] public Union EmailПользователя { get; set; }
	[ProtoMember(33)] public Union АдресТочкиСамовывоза { get; set; }
	[ProtoMember(34)] public Union ДокументЗаказКлиента { get; set; }
	[ProtoMember(35)] public Union СправочникАдресХранения { get; set; }
	[ProtoMember(36)] public Union СправочникКонтрагенты { get; set; }
	[ProtoMember(37)] public Union СправочникПользователи { get; set; }
	[ProtoMember(38)] public Union СправочникСотрудники { get; set; }
	[ProtoMember(39)] public Union СправочникТочкиСамовывоза { get; set; }
	[ProtoMember(40)] public Union ТелефонАдресаХранения { get; set; }
	[ProtoMember(41)] public Union ТелефонКлиента { get; set; }
	[ProtoMember(42)] public Union ТелефонКонтрагента { get; set; }
	[ProtoMember(43)] public Union ТелефонКурьера { get; set; }
	[ProtoMember(44)] public Union ТелефонПользователя { get; set; }
	[ProtoMember(45)] public Union ТелефонСотрудникаДомашний { get; set; }
	[ProtoMember(46)] public Union ТелефонСотрудникаРабочий { get; set; }
	[ProtoMember(47)] public Union ТелефонТочкиСамовывоза { get; set; }
	[ProtoMember(48)] public List<ВидыКонтактнойИнформации.ПредставленияЗапись> Представления { get; set; } = new List<ВидыКонтактнойИнформации.ПредставленияЗапись>();
}

[ProtoContract] public sealed class ВидыКонтрагентов
{
	[ProtoContract] public sealed class СкрытыеРеквизитыЗапись
	{
		[ProtoMember(1)] public string ИмяРеквизита { get; set; }
	}
	[ProtoMember(1)] public string Код { get; set; }
	[ProtoMember(2)] public string Наименование { get; set; }
	[ProtoMember(3)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(4)] public List<ВидыКонтрагентов.СкрытыеРеквизитыЗапись> СкрытыеРеквизиты { get; set; } = new List<ВидыКонтрагентов.СкрытыеРеквизитыЗапись>();
}

[ProtoContract] public sealed class ВидыНоменклатуры
{
	[ProtoContract] public sealed class РеквизитыДляКонтроляНоменклатурыЗапись
	{
		[ProtoMember(1)] public string ИмяРеквизита { get; set; }
	}
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public bool RX { get; set; }
	[ProtoMember(3)] public Entity ТипНоменклатуры { get; set; }
	[ProtoMember(4)] public string Код { get; set; }
	[ProtoMember(5)] public string Наименование { get; set; }
	[ProtoMember(6)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(7)] public bool АвтоматическоеНаименованиеНоменклатуры { get; set; }
	[ProtoMember(8)] public bool Подарок { get; set; }
	[ProtoMember(9)] public List<ВидыНоменклатуры.РеквизитыДляКонтроляНоменклатурыЗапись> РеквизитыДляКонтроляНоменклатуры { get; set; } = new List<ВидыНоменклатуры.РеквизитыДляКонтроляНоменклатурыЗапись>();
}

[ProtoContract] public sealed class ВнешниеПользователи
{
	[ProtoContract] public sealed class ДополнительныеРеквизитыЗапись
	{
		[ProtoMember(1)] public string Значение { get; set; }
		[ProtoMember(2)] public string Свойство { get; set; }
		[ProtoMember(3)] public string ТекстоваяСтрока { get; set; }
	}
	[ProtoMember(1)] public Entity ИдентификаторПользователяИБ { get; set; }
	[ProtoMember(2)] public Entity ИдентификаторПользователяСервиса { get; set; }
	[ProtoMember(3)] public string Комментарий { get; set; }
	[ProtoMember(4)] public bool Недействителен { get; set; }
	[ProtoMember(5)] public string ОбъектАвторизации { get; set; }
	[ProtoMember(6)] public bool Подготовлен { get; set; }
	[ProtoMember(7)] public Entity СвойстваПользователяИБ { get; set; }
	[ProtoMember(8)] public bool УстановитьРолиНепосредственно { get; set; }
	[ProtoMember(9)] public string Наименование { get; set; }
	[ProtoMember(10)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(11)] public List<ВнешниеПользователи.ДополнительныеРеквизитыЗапись> ДополнительныеРеквизиты { get; set; } = new List<ВнешниеПользователи.ДополнительныеРеквизитыЗапись>();
}

[ProtoContract] public sealed class ГруппаЗакупки
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public string Код { get; set; }
	[ProtoMember(3)] public string Наименование { get; set; }
	[ProtoMember(4)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class ГруппыВнешнихПользователей
{
	[ProtoContract] public sealed class НазначениеЗапись
	{
		[ProtoMember(1)] public string ТипПользователей { get; set; }
	}
	[ProtoContract] public sealed class РолиЗапись
	{
		[ProtoMember(1)] public Entity Роль { get; set; }
	}
	[ProtoContract] public sealed class СоставЗапись
	{
		[ProtoMember(1)] public Entity ВнешнийПользователь { get; set; }
	}
	[ProtoMember(1)] public bool ВсеОбъектыАвторизации { get; set; }
	[ProtoMember(2)] public string Комментарий { get; set; }
	[ProtoMember(3)] public string Наименование { get; set; }
	[ProtoMember(4)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(5)] public Entity Родитель { get; set; }
	[ProtoMember(6)] public List<ГруппыВнешнихПользователей.НазначениеЗапись> Назначение { get; set; } = new List<ГруппыВнешнихПользователей.НазначениеЗапись>();
	[ProtoMember(7)] public List<ГруппыВнешнихПользователей.РолиЗапись> Роли { get; set; } = new List<ГруппыВнешнихПользователей.РолиЗапись>();
	[ProtoMember(8)] public List<ГруппыВнешнихПользователей.СоставЗапись> Состав { get; set; } = new List<ГруппыВнешнихПользователей.СоставЗапись>();
}

[ProtoContract] public sealed class ГруппыДоступа
{
	[ProtoContract] public sealed class ВидыДоступаЗапись
	{
		[ProtoMember(1)] public Union ВидДоступа { get; set; }
		[ProtoMember(2)] public bool ВсеРазрешены { get; set; }
	}
	[ProtoContract] public sealed class ЗначенияДоступаЗапись
	{
		[ProtoMember(1)] public Union ВидДоступа { get; set; }
		[ProtoMember(2)] public Union ЗначениеДоступа { get; set; }
		[ProtoMember(3)] public bool ВключаяНижестоящие { get; set; }
	}
	[ProtoContract] public sealed class ПользователиЗапись
	{
		[ProtoMember(1)] public Union Пользователь { get; set; }
	}
	[ProtoMember(1)] public string Комментарий { get; set; }
	[ProtoMember(2)] public bool ОсновнаяГруппаДоступаПоставляемогоПрофиля { get; set; }
	[ProtoMember(3)] public Entity Ответственный { get; set; }
	[ProtoMember(4)] public Union Пользователь { get; set; }
	[ProtoMember(5)] public Entity Профиль { get; set; }
	[ProtoMember(6)] public string Наименование { get; set; }
	[ProtoMember(7)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(8)] public Entity Родитель { get; set; }
	[ProtoMember(9)] public List<ГруппыДоступа.ВидыДоступаЗапись> ВидыДоступа { get; set; } = new List<ГруппыДоступа.ВидыДоступаЗапись>();
	[ProtoMember(10)] public List<ГруппыДоступа.ЗначенияДоступаЗапись> ЗначенияДоступа { get; set; } = new List<ГруппыДоступа.ЗначенияДоступаЗапись>();
	[ProtoMember(11)] public List<ГруппыДоступа.ПользователиЗапись> Пользователи { get; set; } = new List<ГруппыДоступа.ПользователиЗапись>();
}

[ProtoContract] public sealed class ГруппыЗакупки
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public string Код { get; set; }
	[ProtoMember(3)] public string Наименование { get; set; }
	[ProtoMember(4)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(5)] public Entity Родитель { get; set; }
}

[ProtoContract] public sealed class ГруппыЗакупкиНоменклатуры
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public Entity ГруппаЗакупки { get; set; }
	[ProtoMember(3)] public string Код { get; set; }
	[ProtoMember(4)] public string Наименование { get; set; }
	[ProtoMember(5)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class ГруппыПользователей
{
	[ProtoContract] public sealed class СоставЗапись
	{
		[ProtoMember(1)] public Entity Пользователь { get; set; }
	}
	[ProtoMember(1)] public string Комментарий { get; set; }
	[ProtoMember(2)] public string Наименование { get; set; }
	[ProtoMember(3)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(4)] public Entity Родитель { get; set; }
	[ProtoMember(5)] public Union ВсеПользователи { get; set; }
	[ProtoMember(6)] public List<ГруппыПользователей.СоставЗапись> Состав { get; set; } = new List<ГруппыПользователей.СоставЗапись>();
}

[ProtoContract] public sealed class Деление
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public double Коэффициент { get; set; }
	[ProtoMember(3)] public double Тип { get; set; }
	[ProtoMember(4)] public Entity ТоварПолучаемый { get; set; }
	[ProtoMember(5)] public Entity Владелец { get; set; }
	[ProtoMember(6)] public string Код { get; set; }
	[ProtoMember(7)] public string Наименование { get; set; }
	[ProtoMember(8)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class ДержателиКонтракта
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public string Код { get; set; }
	[ProtoMember(3)] public string Наименование { get; set; }
	[ProtoMember(4)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class ДиапазоныНаценок
{
	[ProtoContract] public sealed class ШкалаДиапазоновЗапись
	{
		[ProtoMember(1)] public double ВерхняяГраница { get; set; }
		[ProtoMember(2)] public double Наценка { get; set; }
		[ProtoMember(3)] public double НаценкаI { get; set; }
		[ProtoMember(4)] public double НаценкаP { get; set; }
		[ProtoMember(5)] public double НаценкаR { get; set; }
		[ProtoMember(6)] public double НаценкаМаксимальная { get; set; }
		[ProtoMember(7)] public double НаценкаМинимальная { get; set; }
		[ProtoMember(8)] public double НижняяГраница { get; set; }
	}
	[ProtoMember(1)] public string Код { get; set; }
	[ProtoMember(2)] public string Наименование { get; set; }
	[ProtoMember(3)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(4)] public List<ДиапазоныНаценок.ШкалаДиапазоновЗапись> ШкалаДиапазонов { get; set; } = new List<ДиапазоныНаценок.ШкалаДиапазоновЗапись>();
}

[ProtoContract] public sealed class Договоры
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public Entity ВидДоговора { get; set; }
	[ProtoMember(3)] public DateTime ДатаДоговора { get; set; }
	[ProtoMember(4)] public DateTime ДатаПосдеднегоИзмененияСостояния { get; set; }
	[ProtoMember(5)] public bool ДоступенКоробПрайс { get; set; }
	[ProtoMember(6)] public string КодПолучателя { get; set; }
	[ProtoMember(7)] public bool Комиссия { get; set; }
	[ProtoMember(8)] public double КоэфКорректировкиОстатка { get; set; }
	[ProtoMember(9)] public Entity Лицензия { get; set; }
	[ProtoMember(10)] public Entity Менеджер { get; set; }
	[ProtoMember(11)] public double МинСрокГодности { get; set; }
	[ProtoMember(12)] public string Номер { get; set; }
	[ProtoMember(13)] public double Отсрочка { get; set; }
	[ProtoMember(14)] public bool РаспределятьСуммуДоставки { get; set; }
	[ProtoMember(15)] public Entity СкладОприходования { get; set; }
	[ProtoMember(16)] public Entity СкладТранзита { get; set; }
	[ProtoMember(17)] public Entity СостояниеДоговора { get; set; }
	[ProtoMember(18)] public Entity СостояниеЛицензии { get; set; }
	[ProtoMember(19)] public double СрокГодностиПоУмолчанию { get; set; }
	[ProtoMember(20)] public DateTime СрокДействия { get; set; }
	[ProtoMember(21)] public DateTime СрокДействияЛицензии { get; set; }
	[ProtoMember(22)] public Entity Фирма { get; set; }
	[ProtoMember(23)] public Entity ФормаСобственности { get; set; }
	[ProtoMember(24)] public double ЧастотаЗагрузкиПрайса { get; set; }
	[ProtoMember(25)] public Entity Владелец { get; set; }
	[ProtoMember(26)] public string Код { get; set; }
	[ProtoMember(27)] public string Наименование { get; set; }
	[ProtoMember(28)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(29)] public Entity ТипДоговора { get; set; }
	[ProtoMember(30)] public Entity Город { get; set; }
	[ProtoMember(31)] public double СтавкаПени { get; set; }
	[ProtoMember(32)] public double СуммаЛимитРасчетов { get; set; }
}

[ProtoContract] public sealed class ДоговорыСостояниеДоговора
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public string Код { get; set; }
	[ProtoMember(3)] public string Наименование { get; set; }
	[ProtoMember(4)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class ДоговорыСостояниеЛицензии
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public string Код { get; set; }
	[ProtoMember(3)] public string Наименование { get; set; }
	[ProtoMember(4)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class ДоговорыТипЛицензии
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public string Код { get; set; }
	[ProtoMember(3)] public string Наименование { get; set; }
	[ProtoMember(4)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class ДоговорыФормаСобственности
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public string Код { get; set; }
	[ProtoMember(3)] public string Наименование { get; set; }
	[ProtoMember(4)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class Дозировки
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public string Код { get; set; }
	[ProtoMember(3)] public string Наименование { get; set; }
	[ProtoMember(4)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class Должности
{
	[ProtoMember(1)] public bool ИспользоватьВГрафике { get; set; }
	[ProtoMember(2)] public string Код { get; set; }
	[ProtoMember(3)] public string Наименование { get; set; }
	[ProtoMember(4)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(5)] public double МаксимальныйВес { get; set; }
	[ProtoMember(6)] public double МаксимальныйОбъем { get; set; }
	[ProtoMember(7)] public bool СдельнаяОплатаТруда { get; set; }
}

[ProtoContract] public sealed class ЗабракованныеСерии
{
	[ProtoContract] public sealed class ПриказыЗапись
	{
		[ProtoMember(1)] public DateTime ДатаДокумента { get; set; }
		[ProtoMember(2)] public string Лаборатория { get; set; }
		[ProtoMember(3)] public string НомерДокумента { get; set; }
		[ProtoMember(4)] public bool ОтменаЗабраковки { get; set; }
		[ProtoMember(5)] public string Примечание { get; set; }
	}
	[ProtoMember(1)] public bool ВсеСерии { get; set; }
	[ProtoMember(2)] public DateTime ДатаОбновления { get; set; }
	[ProtoMember(3)] public DateTime ДатаПоследнегоПриказа { get; set; }
	[ProtoMember(4)] public Entity Номенклатура { get; set; }
	[ProtoMember(5)] public string НомерПоследнегоПриказа { get; set; }
	[ProtoMember(6)] public double НомерРЛС { get; set; }
	[ProtoMember(7)] public string Препарат { get; set; }
	[ProtoMember(8)] public string Производитель { get; set; }
	[ProtoMember(9)] public Entity Статус { get; set; }
	[ProtoMember(10)] public string Наименование { get; set; }
	[ProtoMember(11)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(12)] public List<ЗабракованныеСерии.ПриказыЗапись> Приказы { get; set; } = new List<ЗабракованныеСерии.ПриказыЗапись>();
}

[ProtoContract] public sealed class ЗаводскиеШК
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public Entity Производитель { get; set; }
	[ProtoMember(3)] public Entity Владелец { get; set; }
	[ProtoMember(4)] public double Код { get; set; }
	[ProtoMember(5)] public string Наименование { get; set; }
	[ProtoMember(6)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class Заметки
{
	[ProtoMember(1)] public Entity Автор { get; set; }
	[ProtoMember(2)] public DateTime ДатаИзменения { get; set; }
	[ProtoMember(3)] public bool ДляРабочегоСтола { get; set; }
	[ProtoMember(4)] public Entity Пометка { get; set; }
	[ProtoMember(5)] public Entity Предмет { get; set; }
	[ProtoMember(6)] public string ПредставлениеПредмета { get; set; }
	[ProtoMember(7)] public Entity Содержание { get; set; }
	[ProtoMember(8)] public string ТекстСодержания { get; set; }
	[ProtoMember(9)] public string Наименование { get; set; }
	[ProtoMember(10)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(11)] public Entity Родитель { get; set; }
}

[ProtoContract] public sealed class ЗонаХранения
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public Entity Подотдел { get; set; }
	[ProtoMember(3)] public double Порядок { get; set; }
	[ProtoMember(4)] public bool ПродажаККМ { get; set; }
	[ProtoMember(5)] public Entity Склад { get; set; }
	[ProtoMember(6)] public bool Холод { get; set; }
	[ProtoMember(7)] public double Код { get; set; }
	[ProtoMember(8)] public string Наименование { get; set; }
	[ProtoMember(9)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(10)] public Entity Родитель { get; set; }
}

[ProtoContract] public sealed class ЗоныДоставки
{
	[ProtoContract] public sealed class КоординатыЗапись
	{
		[ProtoMember(1)] public double Долгота { get; set; }
		[ProtoMember(2)] public double Контур { get; set; }
		[ProtoMember(3)] public double Широта { get; set; }
	}
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public double Порядок { get; set; }
	[ProtoMember(3)] public Entity Регион { get; set; }
	[ProtoMember(4)] public string Код { get; set; }
	[ProtoMember(5)] public string Наименование { get; set; }
	[ProtoMember(6)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(7)] public Entity Родитель { get; set; }
	[ProtoMember(8)] public bool ЗаМКАД { get; set; }
	[ProtoMember(9)] public string Комментарий { get; set; }
	[ProtoMember(10)] public Entity УровеньСервиса { get; set; }
	[ProtoMember(11)] public List<ЗоныДоставки.КоординатыЗапись> Координаты { get; set; } = new List<ЗоныДоставки.КоординатыЗапись>();
}

[ProtoContract] public sealed class ИдентификаторыОбъектовМетаданных
{
	[ProtoMember(1)] public bool БезДанных { get; set; }
	[ProtoMember(2)] public Union ЗначениеПустойСсылки { get; set; }
	[ProtoMember(3)] public string Имя { get; set; }
	[ProtoMember(4)] public Entity КлючОбъектаМетаданных { get; set; }
	[ProtoMember(5)] public Entity НоваяСсылка { get; set; }
	[ProtoMember(6)] public string ПолноеИмя { get; set; }
	[ProtoMember(7)] public string ПолныйСиноним { get; set; }
	[ProtoMember(8)] public double ПорядокКоллекции { get; set; }
	[ProtoMember(9)] public string Синоним { get; set; }
	[ProtoMember(10)] public string Наименование { get; set; }
	[ProtoMember(11)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(12)] public Entity Родитель { get; set; }
}

[ProtoContract] public sealed class Интервалы
{
	[ProtoMember(1)] public bool Активность { get; set; }
	[ProtoMember(2)] public DateTime ВремяПо { get; set; }
	[ProtoMember(3)] public DateTime ВремяС { get; set; }
	[ProtoMember(4)] public double Порядок { get; set; }
	[ProtoMember(5)] public string Код { get; set; }
	[ProtoMember(6)] public string Наименование { get; set; }
	[ProtoMember(7)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class ИнформационныеБазыРИБД
{
	[ProtoContract] public sealed class РасписаниеФоновыхЗаданийЗапись
	{
		[ProtoMember(1)] public bool Действует { get; set; }
		[ProtoMember(2)] public string ОписаниеРасписания { get; set; }
		[ProtoMember(3)] public string Расписание { get; set; }
		[ProtoMember(4)] public string РегламентноеЗадание { get; set; }
	}
	[ProtoMember(1)] public string ВерсияКонфигурации { get; set; }
	[ProtoMember(2)] public string КодУзла { get; set; }
	[ProtoMember(3)] public double МаксимальноеКоличествоПотоков { get; set; }
	[ProtoMember(4)] public Entity НастройкаВебСервиса { get; set; }
	[ProtoMember(5)] public Entity ОсновнойОбъектУчета { get; set; }
	[ProtoMember(6)] public string Префикс { get; set; }
	[ProtoMember(7)] public Entity ТипИнформационнойБазыРИБД { get; set; }
	[ProtoMember(8)] public string Код { get; set; }
	[ProtoMember(9)] public string Наименование { get; set; }
	[ProtoMember(10)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(11)] public Entity Родитель { get; set; }
	[ProtoMember(12)] public double ЦенообразованиеВариантОпределенияЦеныПродажи { get; set; }
	[ProtoMember(13)] public bool ЦенообразованиеИспользоватьСпецЦены { get; set; }
	[ProtoMember(14)] public double ЦенообразованиеПорцияДляРасчета { get; set; }
	[ProtoMember(15)] public bool ЦенообразованиеРасчетЦенВключен { get; set; }
	[ProtoMember(16)] public bool РассчитыватьСуммуНДСПриФормированииФискальногоЧека { get; set; }
	[ProtoMember(17)] public List<ИнформационныеБазыРИБД.РасписаниеФоновыхЗаданийЗапись> РасписаниеФоновыхЗаданий { get; set; } = new List<ИнформационныеБазыРИБД.РасписаниеФоновыхЗаданийЗапись>();
}

[ProtoContract] public sealed class ИсточникиПретензий
{
	[ProtoMember(1)] public string Код { get; set; }
	[ProtoMember(2)] public string Наименование { get; set; }
	[ProtoMember(3)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(4)] public Entity Родитель { get; set; }
}

[ProtoContract] public sealed class Календари
{
	[ProtoContract] public sealed class РасписаниеРаботыЗапись
	{
		[ProtoMember(1)] public DateTime ВремяНачала { get; set; }
		[ProtoMember(2)] public DateTime ВремяОкончания { get; set; }
		[ProtoMember(3)] public double НомерДня { get; set; }
	}
	[ProtoContract] public sealed class ШаблонЗаполненияЗапись
	{
		[ProtoMember(1)] public bool ДеньВключенВГрафик { get; set; }
	}
	[ProtoMember(1)] public Union ВладелецГрафика { get; set; }
	[ProtoMember(2)] public double ГоризонтПланирования { get; set; }
	[ProtoMember(3)] public DateTime ДатаНачала { get; set; }
	[ProtoMember(4)] public DateTime ДатаОкончания { get; set; }
	[ProtoMember(5)] public DateTime ДатаОтсчета { get; set; }
	[ProtoMember(6)] public string Описание { get; set; }
	[ProtoMember(7)] public Entity ПроизводственныйКалендарь { get; set; }
	[ProtoMember(8)] public Entity СпособЗаполнения { get; set; }
	[ProtoMember(9)] public bool УчитыватьПраздники { get; set; }
	[ProtoMember(10)] public string Наименование { get; set; }
	[ProtoMember(11)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(12)] public Entity Родитель { get; set; }
	[ProtoMember(13)] public bool УчитыватьНерабочиеПериоды { get; set; }
	[ProtoMember(14)] public List<Календари.РасписаниеРаботыЗапись> РасписаниеРаботы { get; set; } = new List<Календари.РасписаниеРаботыЗапись>();
	[ProtoMember(15)] public List<Календари.ШаблонЗаполненияЗапись> ШаблонЗаполнения { get; set; } = new List<Календари.ШаблонЗаполненияЗапись>();
}

[ProtoContract] public sealed class КассаПредприятия
{
	[ProtoMember(1)] public Entity Аптека { get; set; }
	[ProtoMember(2)] public Entity КассаККМ { get; set; }
	[ProtoMember(3)] public string Код { get; set; }
	[ProtoMember(4)] public string Наименование { get; set; }
	[ProtoMember(5)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(6)] public Entity ТипКассы { get; set; }
}

[ProtoContract] public sealed class КатегорииАптек
{
	[ProtoMember(1)] public Entity Раздел { get; set; }
	[ProtoMember(2)] public string Код { get; set; }
	[ProtoMember(3)] public string Наименование { get; set; }
	[ProtoMember(4)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class КатегорииЦен
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public double Скидка { get; set; }
	[ProtoMember(3)] public double Код { get; set; }
	[ProtoMember(4)] public string Наименование { get; set; }
	[ProtoMember(5)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class ККМ
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public bool ЕстьДисплей { get; set; }
	[ProtoMember(3)] public bool ЕстьТерминалОплаты { get; set; }
	[ProtoMember(4)] public string ЗаводскойНомер { get; set; }
	[ProtoMember(5)] public string ИмяКомпьютера { get; set; }
	[ProtoMember(6)] public string Местоположение { get; set; }
	[ProtoMember(7)] public Entity Отдел { get; set; }
	[ProtoMember(8)] public double ПолеСверху { get; set; }
	[ProtoMember(9)] public double ПолеСлева { get; set; }
	[ProtoMember(10)] public double ПолеСнизу { get; set; }
	[ProtoMember(11)] public double ПолеСправа { get; set; }
	[ProtoMember(12)] public bool РазрешитьПодЗаказ { get; set; }
	[ProtoMember(13)] public string РегистрационныйНомер { get; set; }
	[ProtoMember(14)] public Entity Фирма { get; set; }
	[ProtoMember(15)] public string Код { get; set; }
	[ProtoMember(16)] public string Наименование { get; set; }
	[ProtoMember(17)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(18)] public bool АвтоматическаяФискализация { get; set; }
	[ProtoMember(19)] public bool ОблачнаяКасса { get; set; }
}

[ProtoContract] public sealed class КлассификаторБанков
{
	[ProtoMember(1)] public string Адрес { get; set; }
	[ProtoMember(2)] public string АдресМеждународный { get; set; }
	[ProtoMember(3)] public string Город { get; set; }
	[ProtoMember(4)] public string ГородМеждународный { get; set; }
	[ProtoMember(5)] public bool ДеятельностьПрекращена { get; set; }
	[ProtoMember(6)] public string ИНН { get; set; }
	[ProtoMember(7)] public string КоррСчет { get; set; }
	[ProtoMember(8)] public string МеждународноеНаименование { get; set; }
	[ProtoMember(9)] public string СВИФТБИК { get; set; }
	[ProtoMember(10)] public Entity Страна { get; set; }
	[ProtoMember(11)] public string Телефоны { get; set; }
	[ProtoMember(12)] public string Код { get; set; }
	[ProtoMember(13)] public string Наименование { get; set; }
	[ProtoMember(14)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(15)] public Entity Родитель { get; set; }
	[ProtoMember(16)] public Entity БИКРКЦ { get; set; }
	[ProtoMember(17)] public string ВнутреннийКодЦБ { get; set; }
}

[ProtoContract] public sealed class Клиенты
{
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
		[ProtoMember(11)] public Entity РегионРаботы { get; set; }
		[ProtoMember(12)] public string Страна { get; set; }
		[ProtoMember(13)] public Entity Тип { get; set; }
		[ProtoMember(14)] public double Долгота { get; set; }
		[ProtoMember(15)] public string ИдентификаторАдреса { get; set; }
		[ProtoMember(16)] public double Широта { get; set; }
		[ProtoMember(17)] public string Значение { get; set; }
	}
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public DateTime ДатаРегистрации { get; set; }
	[ProtoMember(3)] public DateTime ДатаРождения { get; set; }
	[ProtoMember(4)] public string Комментарий { get; set; }
	[ProtoMember(5)] public Entity Контрагент { get; set; }
	[ProtoMember(6)] public bool Проблемный { get; set; }
	[ProtoMember(7)] public double СкидкаНаСледующийЗаказ { get; set; }
	[ProtoMember(8)] public Entity ТипКлиента { get; set; }
	[ProtoMember(9)] public string Код { get; set; }
	[ProtoMember(10)] public string Наименование { get; set; }
	[ProtoMember(11)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(12)] public string СберПраймID { get; set; }
	[ProtoMember(13)] public Entity ОсновнойКлиент { get; set; }
	[ProtoMember(14)] public List<Клиенты.КонтактнаяИнформацияЗапись> КонтактнаяИнформация { get; set; } = new List<Клиенты.КонтактнаяИнформацияЗапись>();
}

[ProtoContract] public sealed class КодОКП
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public double Код { get; set; }
	[ProtoMember(3)] public string Наименование { get; set; }
	[ProtoMember(4)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class КомандыККТ
{
	[ProtoMember(1)] public bool ДляМониторинга { get; set; }
	[ProtoMember(2)] public string ЗначениеОтвета { get; set; }
	[ProtoMember(3)] public string ЗначениеПараметра { get; set; }
	[ProtoMember(4)] public Entity МестоЗапуска { get; set; }
	[ProtoMember(5)] public string МетодЗапроса { get; set; }
	[ProtoMember(6)] public string МетодУстановкиПараметров { get; set; }
	[ProtoMember(7)] public string Параметр { get; set; }
	[ProtoMember(8)] public string ТипОтвета { get; set; }
	[ProtoMember(9)] public string Код { get; set; }
	[ProtoMember(10)] public string Наименование { get; set; }
	[ProtoMember(11)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class Контрагенты
{
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
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public bool АдресИзКарточки { get; set; }
	[ProtoMember(3)] public Entity ГлавныйКонтрагент { get; set; }
	[ProtoMember(4)] public DateTime ДатаДоговора { get; set; }
	[ProtoMember(5)] public DateTime ДатаРегистрации { get; set; }
	[ProtoMember(6)] public double Дисконт { get; set; }
	[ProtoMember(7)] public string ИНН { get; set; }
	[ProtoMember(8)] public string Информация { get; set; }
	[ProtoMember(9)] public double КодПеревозчика77 { get; set; }
	[ProtoMember(10)] public string Комментарий { get; set; }
	[ProtoMember(11)] public bool КонтрольОстатков { get; set; }
	[ProtoMember(12)] public double КоэффициентКонтроляОстатков { get; set; }
	[ProtoMember(13)] public double КоэффициентКоррекцииЦен { get; set; }
	[ProtoMember(14)] public double МинимальнаяСуммаЗаказа { get; set; }
	[ProtoMember(15)] public double МинимальнаяСуммаПоСтрокеПрайса { get; set; }
	[ProtoMember(16)] public double МинСрокОбменаТовара { get; set; }
	[ProtoMember(17)] public bool НетБонусов { get; set; }
	[ProtoMember(18)] public bool НеТребоватьВводаПроизводителя { get; set; }
	[ProtoMember(19)] public string НомерБонуснойКарты { get; set; }
	[ProtoMember(20)] public string НомерДоговора { get; set; }
	[ProtoMember(21)] public bool Обзвон { get; set; }
	[ProtoMember(22)] public string ОГРН { get; set; }
	[ProtoMember(23)] public string ОКПО { get; set; }
	[ProtoMember(24)] public bool ОсуществляетОтгрузки { get; set; }
	[ProtoMember(25)] public bool ОтключитьКонтрольЦеныОтМинимальнойВПрайсах { get; set; }
	[ProtoMember(26)] public bool ОтключитьКонтрольЦеныОтСебестоимости { get; set; }
	[ProtoMember(27)] public double Отсрочка { get; set; }
	[ProtoMember(28)] public double ПериодПоставки { get; set; }
	[ProtoMember(29)] public string ПолнНаименование { get; set; }
	[ProtoMember(30)] public double ПорядокСортировкиПриВыбореВЗаказеКлиента { get; set; }
	[ProtoMember(31)] public string ПочтовыйАдрес { get; set; }
	[ProtoMember(32)] public bool РаботаетПоДоговоруКомиссии { get; set; }
	[ProtoMember(33)] public bool Рассылка { get; set; }
	[ProtoMember(34)] public Entity РегионРаботы { get; set; }
	[ProtoMember(35)] public double Скидка { get; set; }
	[ProtoMember(36)] public DateTime СрокДействияДоговора { get; set; }
	[ProtoMember(37)] public double СрокДействияПрайсЛиста { get; set; }
	[ProtoMember(38)] public Entity СтатусПоставщика { get; set; }
	[ProtoMember(39)] public string Телефоны { get; set; }
	[ProtoMember(40)] public bool Упрощенка { get; set; }
	[ProtoMember(41)] public bool УчаствуетВАвтозаказе { get; set; }
	[ProtoMember(42)] public bool УчаствуетВКонтролеЦены { get; set; }
	[ProtoMember(43)] public string ЭлектронныйАдрес { get; set; }
	[ProtoMember(44)] public string ЭлектронныйАдресОтправкиПрайсов { get; set; }
	[ProtoMember(45)] public string ЮридическийАдрес { get; set; }
	[ProtoMember(46)] public string Код { get; set; }
	[ProtoMember(47)] public string Наименование { get; set; }
	[ProtoMember(48)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(49)] public Entity Родитель { get; set; }
	[ProtoMember(50)] public string ЭлектронныйАдресОтправкиНакладных { get; set; }
	[ProtoMember(51)] public string ЭлектронныйАдресОтправкиОтказов { get; set; }
	[ProtoMember(52)] public Entity ИсключаемыйАссортимент { get; set; }
	[ProtoMember(53)] public Entity БанковскийСчетПоУмолчанию { get; set; }
	[ProtoMember(54)] public bool НеОтправлятьСМС { get; set; }
	[ProtoMember(55)] public Entity ТипКонтрагента { get; set; }
	[ProtoMember(56)] public Entity Фирма { get; set; }
	[ProtoMember(57)] public Entity Проект { get; set; }
	[ProtoMember(58)] public double МаксимумСтрокВЗаказе { get; set; }
	[ProtoMember(59)] public Entity ОтветственныйМенеджер { get; set; }
	[ProtoMember(60)] public double СрокПодачиПретензии { get; set; }
	[ProtoMember(61)] public DateTime ДатаПодключенияНаСайт { get; set; }
	[ProtoMember(62)] public string ОКВЭД { get; set; }
	[ProtoMember(63)] public bool ПечатьКиЗов { get; set; }
	[ProtoMember(64)] public Entity ПравилоЗаполненияДатыВозвратаПоДаннымПартнера { get; set; }
	[ProtoMember(65)] public Entity ПравилоЗаполненияНомераВозвратаПоДаннымПартнера { get; set; }
	[ProtoMember(66)] public string КорректныйИНН { get; set; }
	[ProtoMember(67)] public string КорректныйКПП { get; set; }
	[ProtoMember(68)] public DateTime ДатаПрекращенияСотрудничества { get; set; }
	[ProtoMember(69)] public Entity РегистрироватьИзмененияЗаказов { get; set; }
	[ProtoMember(70)] public bool ВыездЗаВозвратомПослеДатыОтменыЗаказаКлиента { get; set; }
	[ProtoMember(71)] public double ПартнерскаяКомиссия { get; set; }
	[ProtoMember(72)] public Entity ОбособленноеПодразделение { get; set; }
	[ProtoMember(73)] public List<Контрагенты.КонтактнаяИнформацияЗапись> КонтактнаяИнформация { get; set; } = new List<Контрагенты.КонтактнаяИнформацияЗапись>();
}

[ProtoContract] public sealed class Концерны
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public double Код { get; set; }
	[ProtoMember(3)] public string Наименование { get; set; }
	[ProtoMember(4)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class ЛекарственныеФормы
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public string Код { get; set; }
	[ProtoMember(3)] public string Наименование { get; set; }
	[ProtoMember(4)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class Логи
{
	[ProtoMember(1)] public Entity БазаДанных { get; set; }
	[ProtoMember(2)] public Entity ВидЛога { get; set; }
	[ProtoMember(3)] public string ВходящиеДанные { get; set; }
	[ProtoMember(4)] public DateTime ДатаЗавершения { get; set; }
	[ProtoMember(5)] public DateTime ДатаНачала { get; set; }
	[ProtoMember(6)] public Entity РезультатВыполнения { get; set; }
	[ProtoMember(7)] public string РезультатВыполненияПодробно { get; set; }
	[ProtoMember(8)] public string Код { get; set; }
	[ProtoMember(9)] public string Наименование { get; set; }
	[ProtoMember(10)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class МестаХранения
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public bool Коробки { get; set; }
	[ProtoMember(3)] public string НомерТелефонаДляОстановкиСборки { get; set; }
	[ProtoMember(4)] public bool Основной { get; set; }
	[ProtoMember(5)] public Entity ОтветственныйККМ { get; set; }
	[ProtoMember(6)] public bool Подкоробки { get; set; }
	[ProtoMember(7)] public bool Подотделы { get; set; }
	[ProtoMember(8)] public bool СборкаЗаказовОстановлена { get; set; }
	[ProtoMember(9)] public Entity Владелец { get; set; }
	[ProtoMember(10)] public string Код { get; set; }
	[ProtoMember(11)] public string Наименование { get; set; }
	[ProtoMember(12)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(13)] public bool РазрешенаПриемка { get; set; }
}

[ProtoContract] public sealed class МестоЗСЯ
{
	[ProtoMember(1)] public Entity АдресХранения { get; set; }
	[ProtoMember(2)] public Entity ВидМестаЗСЯ { get; set; }
	[ProtoMember(3)] public string Зона { get; set; }
	[ProtoMember(4)] public Entity ЗонаСсылка { get; set; }
	[ProtoMember(5)] public double Объем { get; set; }
	[ProtoMember(6)] public Entity Отдел { get; set; }
	[ProtoMember(7)] public string Стеллаж { get; set; }
	[ProtoMember(8)] public double Удобство { get; set; }
	[ProtoMember(9)] public string Ячейка { get; set; }
	[ProtoMember(10)] public string Код { get; set; }
	[ProtoMember(11)] public string Наименование { get; set; }
	[ProtoMember(12)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(13)] public double Порядок { get; set; }
}

[ProtoContract] public sealed class Метрики
{
	[ProtoContract] public sealed class ПараметрыЗапросаЗапись
	{
		[ProtoMember(1)] public Union Значение { get; set; }
		[ProtoMember(2)] public string Имя { get; set; }
	}
	[ProtoContract] public sealed class ПараметрыЗапросаРасшифровкиЗапись
	{
		[ProtoMember(1)] public Union Значение { get; set; }
		[ProtoMember(2)] public string Имя { get; set; }
	}
	[ProtoMember(1)] public string ВидСравнения { get; set; }
	[ProtoMember(2)] public string Запрос { get; set; }
	[ProtoMember(3)] public string КлючевоеПоле { get; set; }
	[ProtoMember(4)] public bool МониторАПО { get; set; }
	[ProtoMember(5)] public string Описание { get; set; }
	[ProtoMember(6)] public Entity ПользовательскийЗапрос { get; set; }
	[ProtoMember(7)] public Entity Расшифровка { get; set; }
	[ProtoMember(8)] public Entity РашифровкаОтчет { get; set; }
	[ProtoMember(9)] public bool Регламентное { get; set; }
	[ProtoMember(10)] public string Код { get; set; }
	[ProtoMember(11)] public string Наименование { get; set; }
	[ProtoMember(12)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(13)] public Entity Родитель { get; set; }
	[ProtoMember(14)] public bool ИндивидуальныйПоказатель { get; set; }
	[ProtoMember(15)] public double ТипИтогов { get; set; }
	[ProtoMember(16)] public List<Метрики.ПараметрыЗапросаЗапись> ПараметрыЗапроса { get; set; } = new List<Метрики.ПараметрыЗапросаЗапись>();
	[ProtoMember(17)] public List<Метрики.ПараметрыЗапросаРасшифровкиЗапись> ПараметрыЗапросаРасшифровки { get; set; } = new List<Метрики.ПараметрыЗапросаРасшифровкиЗапись>();
}

[ProtoContract] public sealed class МНН
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public bool ЖВЛ { get; set; }
	[ProtoMember(3)] public string Латиница { get; set; }
	[ProtoMember(4)] public double Код { get; set; }
	[ProtoMember(5)] public string Наименование { get; set; }
	[ProtoMember(6)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class НаклейкиМестЗаказа
{
	[ProtoMember(1)] public Union Документ { get; set; }
	[ProtoMember(2)] public double НомерМеста { get; set; }
	[ProtoMember(3)] public Entity Склад { get; set; }
	[ProtoMember(4)] public string УИД { get; set; }
	[ProtoMember(5)] public bool Холод { get; set; }
	[ProtoMember(6)] public string Код { get; set; }
	[ProtoMember(7)] public string Наименование { get; set; }
	[ProtoMember(8)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class НастройкиABCXYZКлассификации
{
	[ProtoContract] public sealed class ПроцентыВариацииЗапись
	{
		[ProtoMember(1)] public Entity Класс { get; set; }
		[ProtoMember(2)] public double Процент { get; set; }
	}
	[ProtoContract] public sealed class ПроцентыКатегорийЗапись
	{
		[ProtoMember(1)] public Entity Класс { get; set; }
		[ProtoMember(2)] public double Процент { get; set; }
	}
	[ProtoMember(1)] public string Код { get; set; }
	[ProtoMember(2)] public string Наименование { get; set; }
	[ProtoMember(3)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(4)] public List<НастройкиABCXYZКлассификации.ПроцентыВариацииЗапись> ПроцентыВариации { get; set; } = new List<НастройкиABCXYZКлассификации.ПроцентыВариацииЗапись>();
	[ProtoMember(5)] public List<НастройкиABCXYZКлассификации.ПроцентыКатегорийЗапись> ПроцентыКатегорий { get; set; } = new List<НастройкиABCXYZКлассификации.ПроцентыКатегорийЗапись>();
}

[ProtoContract] public sealed class НастройкиИнтеграцииMindbox
{
	[ProtoMember(1)] public string Адрес { get; set; }
	[ProtoMember(2)] public double ВремяОжиданияОтвета { get; set; }
	[ProtoMember(3)] public bool ЗащищенноеСоединение { get; set; }
	[ProtoMember(4)] public string ИдентификаторКонечнойТочки { get; set; }
	[ProtoMember(5)] public double КоличествоЭлементовНаСтранице { get; set; }
	[ProtoMember(6)] public string СекретныйКлюч { get; set; }
	[ProtoMember(7)] public string Сервер { get; set; }
	[ProtoMember(8)] public Entity ФорматЗапроса { get; set; }
	[ProtoMember(9)] public Entity ФорматОтвета { get; set; }
	[ProtoMember(10)] public Entity ШаблонСМС { get; set; }
	[ProtoMember(11)] public string Наименование { get; set; }
	[ProtoMember(12)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class НастройкиПодключенийКВебСервисам
{
	[ProtoMember(1)] public string АдресСервиса { get; set; }
	[ProtoMember(2)] public string АдресСервисаРезервный { get; set; }
	[ProtoMember(3)] public Entity АдресХранения { get; set; }
	[ProtoMember(4)] public string ДопКлюч { get; set; }
	[ProtoMember(5)] public string ИмяСервиса { get; set; }
	[ProtoMember(6)] public string Логин { get; set; }
	[ProtoMember(7)] public string НазваниеСервиса { get; set; }
	[ProtoMember(8)] public double Порт { get; set; }
	[ProtoMember(9)] public string ПространствоИмен { get; set; }
	[ProtoMember(10)] public bool СверятьДокументы { get; set; }
	[ProtoMember(11)] public string Секрет { get; set; }
	[ProtoMember(12)] public double Таймаут { get; set; }
	[ProtoMember(13)] public string Токен { get; set; }
	[ProtoMember(14)] public string Код { get; set; }
	[ProtoMember(15)] public string Наименование { get; set; }
	[ProtoMember(16)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class НастройкиПодключения
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public string БазаДанных { get; set; }
	[ProtoMember(3)] public string Драйвер { get; set; }
	[ProtoMember(4)] public string Логин { get; set; }
	[ProtoMember(5)] public string Пароль { get; set; }
	[ProtoMember(6)] public string Сервер { get; set; }
	[ProtoMember(7)] public string Код { get; set; }
	[ProtoMember(8)] public string Наименование { get; set; }
	[ProtoMember(9)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class НачисленияТипыДоплат
{
	[ProtoMember(1)] public bool БезПределов { get; set; }
	[ProtoMember(2)] public Entity Вид { get; set; }
	[ProtoMember(3)] public string Код { get; set; }
	[ProtoMember(4)] public string Наименование { get; set; }
	[ProtoMember(5)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class НачисленияТипыДоставки
{
	[ProtoMember(1)] public double ДлинаИнтервалаОт { get; set; }
	[ProtoMember(2)] public double ДлинаИнтервалаПо { get; set; }
	[ProtoMember(3)] public bool ИспользуетсяAPI { get; set; }
	[ProtoMember(4)] public DateTime НачалоИнтервалаОт { get; set; }
	[ProtoMember(5)] public DateTime НачалоИнтервалаПо { get; set; }
	[ProtoMember(6)] public Entity Сайт { get; set; }
	[ProtoMember(7)] public bool УчитыватьНачалоИнтервала { get; set; }
	[ProtoMember(8)] public string Код { get; set; }
	[ProtoMember(9)] public string Наименование { get; set; }
	[ProtoMember(10)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class НачисленияТипыЗон
{
	[ProtoMember(1)] public string Код { get; set; }
	[ProtoMember(2)] public string Наименование { get; set; }
	[ProtoMember(3)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class Номенклатура
{
	[ProtoContract] public sealed class ЗСЯЗапись
	{
		[ProtoMember(1)] public string Зона { get; set; }
		[ProtoMember(2)] public Entity ЗонаСсылка { get; set; }
		[ProtoMember(3)] public Entity Склад { get; set; }
		[ProtoMember(4)] public string Стеллаж { get; set; }
		[ProtoMember(5)] public string Ячейка { get; set; }
	}
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public Entity БрендПроизводителя { get; set; }
	[ProtoMember(3)] public double Вес { get; set; }
	[ProtoMember(4)] public Entity ВидНоменклатуры { get; set; }
	[ProtoMember(5)] public bool ВиртуальныйТовар { get; set; }
	[ProtoMember(6)] public double ВнешнийКод { get; set; }
	[ProtoMember(7)] public Entity ГлавныйАналог { get; set; }
	[ProtoMember(8)] public Entity ГруппаАналогов { get; set; }
	[ProtoMember(9)] public DateTime ДатаОкончанияРегистрации { get; set; }
	[ProtoMember(10)] public DateTime ДатаСоздания { get; set; }
	[ProtoMember(11)] public Entity Дозировка { get; set; }
	[ProtoMember(12)] public string ДопИнфо { get; set; }
	[ProtoMember(13)] public Entity ЕдиницаИзмеренияДозировки { get; set; }
	[ProtoMember(14)] public Entity ЕдиницаИзмеренияФасовки { get; set; }
	[ProtoMember(15)] public bool ЕстьНаСайте { get; set; }
	[ProtoMember(16)] public bool ЖизненноВажный { get; set; }
	[ProtoMember(17)] public string КраткоеНаименование { get; set; }
	[ProtoMember(18)] public Entity ЛекарственнаяФорма { get; set; }
	[ProtoMember(19)] public bool ЛекарствоИлиБальзамНаОсновеСпирта { get; set; }
	[ProtoMember(20)] public double МаксимальныйСрокГодности { get; set; }
	[ProtoMember(21)] public bool МДЛП { get; set; }
	[ProtoMember(22)] public Entity МНН { get; set; }
	[ProtoMember(23)] public bool Наркотический { get; set; }
	[ProtoMember(24)] public double НачальнаяЗакупка { get; set; }
	[ProtoMember(25)] public bool НеБываетВДоставке { get; set; }
	[ProtoMember(26)] public bool НетРУ { get; set; }
	[ProtoMember(27)] public bool НетСкидки { get; set; }
	[ProtoMember(28)] public bool Новинка { get; set; }
	[ProtoMember(29)] public double Объем { get; set; }
	[ProtoMember(30)] public bool Обязательный { get; set; }
	[ProtoMember(31)] public bool ОбязательныйАссортимент { get; set; }
	[ProtoMember(32)] public double ОграничениеЗакупки { get; set; }
	[ProtoMember(33)] public bool Оригинальный { get; set; }
	[ProtoMember(34)] public bool ОстатокПодЗаказ { get; set; }
	[ProtoMember(35)] public Entity Ответственный { get; set; }
	[ProtoMember(36)] public string ПолноеНаименование { get; set; }
	[ProtoMember(37)] public Entity Производитель { get; set; }
	[ProtoMember(38)] public bool Психотропный { get; set; }
	[ProtoMember(39)] public bool Рецептурный { get; set; }
	[ProtoMember(40)] public bool СписокА { get; set; }
	[ProtoMember(41)] public bool СписокБ { get; set; }
	[ProtoMember(42)] public double СтарыйКод { get; set; }
	[ProtoMember(43)] public Entity СтатусНоменклатуры { get; set; }
	[ProtoMember(44)] public Entity ТипГруппы { get; set; }
	[ProtoMember(45)] public Entity ТипНоменклатуры { get; set; }
	[ProtoMember(46)] public bool ТолькоВАптеке { get; set; }
	[ProtoMember(47)] public string ТорговоеНазвание { get; set; }
	[ProtoMember(48)] public bool ТребоватьРецепт { get; set; }
	[ProtoMember(49)] public Entity Упаковка { get; set; }
	[ProtoMember(50)] public Entity Фасовка { get; set; }
	[ProtoMember(51)] public string ФонетическийКодПолноеНаименование { get; set; }
	[ProtoMember(52)] public string ФонетическийКодСтрока { get; set; }
	[ProtoMember(53)] public string ФормаКраткая { get; set; }
	[ProtoMember(54)] public string ФормаПолная { get; set; }
	[ProtoMember(55)] public bool Холод { get; set; }
	[ProtoMember(56)] public Entity ЭксклюзивныйПоставщик { get; set; }
	[ProtoMember(57)] public double Ячейка { get; set; }
	[ProtoMember(58)] public string Код { get; set; }
	[ProtoMember(59)] public string Наименование { get; set; }
	[ProtoMember(60)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(61)] public Entity Родитель { get; set; }
	[ProtoMember(62)] public Entity ЕдиницаИзмеренияОбъемМасса { get; set; }
	[ProtoMember(63)] public double ОбъемМасса { get; set; }
	[ProtoMember(64)] public Entity ДержательКонтракта { get; set; }
	[ProtoMember(65)] public Entity БрендТМ { get; set; }
	[ProtoMember(66)] public double КритическийОстатокСрокаГодности { get; set; }
	[ProtoMember(67)] public double КритическийОстатокСрокаГодностиПриПриемке { get; set; }
	[ProtoMember(68)] public bool ЗапретКомиссииПартнера { get; set; }
	[ProtoMember(69)] public DateTime ДатаРУ { get; set; }
	[ProtoMember(70)] public string НомерРУ { get; set; }
	[ProtoMember(71)] public bool ВМТ { get; set; }
	[ProtoMember(72)] public bool ИМБ { get; set; }
	[ProtoMember(73)] public bool СТМ { get; set; }
	[ProtoMember(74)] public double СрокГодности { get; set; }
	[ProtoMember(75)] public bool Огнеопасно { get; set; }
	[ProtoMember(76)] public bool ВЗН { get; set; }
	[ProtoMember(77)] public bool ИзъятоИзПродажи { get; set; }
	[ProtoMember(78)] public string Комментарий { get; set; }
	[ProtoMember(79)] public bool НедоступенДляПартнеров { get; set; }
	[ProtoMember(80)] public Entity КлассификаторОригинальности { get; set; }
	[ProtoMember(81)] public List<Номенклатура.ЗСЯЗапись> ЗСЯ { get; set; } = new List<Номенклатура.ЗСЯЗапись>();
}

[ProtoContract] public sealed class НомераГТД
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public Entity Страна { get; set; }
	[ProtoMember(3)] public string Наименование { get; set; }
	[ProtoMember(4)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class Партии
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public string Выдан { get; set; }
	[ProtoMember(3)] public DateTime ГоденДо { get; set; }
	[ProtoMember(4)] public DateTime ДатаРеализацииПроизводителем { get; set; }
	[ProtoMember(5)] public DateTime ДатаСоздания { get; set; }
	[ProtoMember(6)] public Entity Документ { get; set; }
	[ProtoMember(7)] public double ЗакупочнаяЦена { get; set; }
	[ProtoMember(8)] public Entity Клиент { get; set; }
	[ProtoMember(9)] public bool Комиссия { get; set; }
	[ProtoMember(10)] public bool МДЛП { get; set; }
	[ProtoMember(11)] public Entity НомерГТД { get; set; }
	[ProtoMember(12)] public bool Подарок { get; set; }
	[ProtoMember(13)] public Entity Производитель { get; set; }
	[ProtoMember(14)] public double РеестроваяЦена { get; set; }
	[ProtoMember(15)] public double СебестоимостьБезНДС { get; set; }
	[ProtoMember(16)] public string Серия { get; set; }
	[ProtoMember(17)] public string Сертификат { get; set; }
	[ProtoMember(18)] public DateTime СертификатДо { get; set; }
	[ProtoMember(19)] public Entity СтавкаНДС { get; set; }
	[ProtoMember(20)] public Entity Страна { get; set; }
	[ProtoMember(21)] public double ТипДоступности { get; set; }
	[ProtoMember(22)] public Entity Фирма { get; set; }
	[ProtoMember(23)] public double ЦенаПроизводителя { get; set; }
	[ProtoMember(24)] public string ШтрихКод { get; set; }
	[ProtoMember(25)] public Entity Владелец { get; set; }
	[ProtoMember(26)] public string Код { get; set; }
	[ProtoMember(27)] public string Наименование { get; set; }
	[ProtoMember(28)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(29)] public bool СпецЦена { get; set; }
	[ProtoMember(30)] public Entity РодительскаяПартия { get; set; }
}

[ProtoContract] public sealed class Подотдел
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public Entity Отдел { get; set; }
	[ProtoMember(3)] public Entity ТипОтдела { get; set; }
	[ProtoMember(4)] public double Код { get; set; }
	[ProtoMember(5)] public string Наименование { get; set; }
	[ProtoMember(6)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class ПодразделенияОрганизации
{
	[ProtoMember(1)] public string Код { get; set; }
	[ProtoMember(2)] public string Наименование { get; set; }
	[ProtoMember(3)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(4)] public Entity Руководитель { get; set; }
}

[ProtoContract] public sealed class Пользователи
{
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
	[ProtoMember(1)] public Entity ИдентификаторПользователяИБ { get; set; }
	[ProtoMember(2)] public Entity ИдентификаторПользователяСервиса { get; set; }
	[ProtoMember(3)] public string Комментарий { get; set; }
	[ProtoMember(4)] public bool Недействителен { get; set; }
	[ProtoMember(5)] public bool Подготовлен { get; set; }
	[ProtoMember(6)] public Union Подразделение { get; set; }
	[ProtoMember(7)] public Entity СвойстваПользователяИБ { get; set; }
	[ProtoMember(8)] public bool Служебный { get; set; }
	[ProtoMember(9)] public Entity ФизическоеЛицо { get; set; }
	[ProtoMember(10)] public string Наименование { get; set; }
	[ProtoMember(11)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(12)] public Entity Фотография { get; set; }
	[ProtoMember(13)] public List<Пользователи.КонтактнаяИнформацияЗапись> КонтактнаяИнформация { get; set; } = new List<Пользователи.КонтактнаяИнформацияЗапись>();
}

[ProtoContract] public sealed class ПользовательскиеЗапросы
{
	[ProtoContract] public sealed class ПараметрыЗапросаЗапись
	{
		[ProtoMember(1)] public string ВидСравнения { get; set; }
		[ProtoMember(2)] public string Имя { get; set; }
		[ProtoMember(3)] public string Представление { get; set; }
		[ProtoMember(4)] public Union Значение { get; set; }
	}
	[ProtoContract] public sealed class ПериодыОтбораДанныхЗапись
	{
		[ProtoMember(1)] public string ИмяДатаГраницы { get; set; }
		[ProtoMember(2)] public DateTime Период { get; set; }
		[ProtoMember(3)] public double ПериодСдвиг { get; set; }
		[ProtoMember(4)] public Entity ПериодСмещения { get; set; }
		[ProtoMember(5)] public string ПредставлениеДатыГраницы { get; set; }
		[ProtoMember(6)] public Entity ТипГраницыПериода { get; set; }
	}
	[ProtoContract] public sealed class ПоказателиЗапись
	{
		[ProtoMember(1)] public string Имя { get; set; }
		[ProtoMember(2)] public bool Использование { get; set; }
		[ProtoMember(3)] public string Представление { get; set; }
	}
	[ProtoMember(1)] public string Запрос { get; set; }
	[ProtoMember(2)] public string Идентификатор { get; set; }
	[ProtoMember(3)] public string ИмяИсточника { get; set; }
	[ProtoMember(4)] public string ПредставлениеИсточника { get; set; }
	[ProtoMember(5)] public bool ПроизвольныйЗапрос { get; set; }
	[ProtoMember(6)] public string Код { get; set; }
	[ProtoMember(7)] public string Наименование { get; set; }
	[ProtoMember(8)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(9)] public List<ПользовательскиеЗапросы.ПараметрыЗапросаЗапись> ПараметрыЗапроса { get; set; } = new List<ПользовательскиеЗапросы.ПараметрыЗапросаЗапись>();
	[ProtoMember(10)] public List<ПользовательскиеЗапросы.ПериодыОтбораДанныхЗапись> ПериодыОтбораДанных { get; set; } = new List<ПользовательскиеЗапросы.ПериодыОтбораДанныхЗапись>();
	[ProtoMember(11)] public List<ПользовательскиеЗапросы.ПоказателиЗапись> Показатели { get; set; } = new List<ПользовательскиеЗапросы.ПоказателиЗапись>();
}

[ProtoContract] public sealed class ПорядокОплаты
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public double КодБух { get; set; }
	[ProtoMember(3)] public string Код { get; set; }
	[ProtoMember(4)] public string Наименование { get; set; }
	[ProtoMember(5)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class ПоставщикиКонтрагентов
{
	[ProtoMember(1)] public string ИНН { get; set; }
	[ProtoMember(2)] public string КПП { get; set; }
	[ProtoMember(3)] public Entity Владелец { get; set; }
	[ProtoMember(4)] public string Код { get; set; }
	[ProtoMember(5)] public string Наименование { get; set; }
	[ProtoMember(6)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class ПравилаРасчетаПотребности
{
	[ProtoContract] public sealed class ПараметрыАлгоритмовЗапись
	{
		[ProtoMember(1)] public Entity ИДСтроки { get; set; }
		[ProtoMember(2)] public Union ЗначениеПараметра { get; set; }
		[ProtoMember(3)] public Entity Параметр { get; set; }
	}
	[ProtoContract] public sealed class ЭтапыПравилаЗапись
	{
		[ProtoMember(1)] public Entity Алгоритм { get; set; }
		[ProtoMember(2)] public Entity ИДСтроки { get; set; }
		[ProtoMember(3)] public string Наименование { get; set; }
	}
	[ProtoMember(1)] public bool ИспользоватьАссортиментнуюМатрицу { get; set; }
	[ProtoMember(2)] public bool ИспользоватьГруппыАналогов { get; set; }
	[ProtoMember(3)] public string Код { get; set; }
	[ProtoMember(4)] public string Наименование { get; set; }
	[ProtoMember(5)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(6)] public Union ИспользоватьСписок { get; set; }
	[ProtoMember(7)] public List<ПравилаРасчетаПотребности.ПараметрыАлгоритмовЗапись> ПараметрыАлгоритмов { get; set; } = new List<ПравилаРасчетаПотребности.ПараметрыАлгоритмовЗапись>();
	[ProtoMember(8)] public List<ПравилаРасчетаПотребности.ЭтапыПравилаЗапись> ЭтапыПравила { get; set; } = new List<ПравилаРасчетаПотребности.ЭтапыПравилаЗапись>();
}

[ProtoContract] public sealed class ПрайсЛист
{
	[ProtoContract] public sealed class ПрайсМестаХраненияЗапись
	{
		[ProtoMember(1)] public bool ДержимНаСкладе { get; set; }
		[ProtoMember(2)] public Entity Отдел { get; set; }
		[ProtoMember(3)] public double Рейтинг { get; set; }
	}
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public bool БезШК { get; set; }
	[ProtoMember(3)] public bool ДержимНаСкладе { get; set; }
	[ProtoMember(4)] public bool ЖВЛ { get; set; }
	[ProtoMember(5)] public string Комментарий { get; set; }
	[ProtoMember(6)] public DateTime ОжидаемаяДата { get; set; }
	[ProtoMember(7)] public double Порядок { get; set; }
	[ProtoMember(8)] public bool РасчетнаяСЦ { get; set; }
	[ProtoMember(9)] public bool СоставнойТовар { get; set; }
	[ProtoMember(10)] public Entity ТипТорговли { get; set; }
	[ProtoMember(11)] public Entity Товар { get; set; }
	[ProtoMember(12)] public double Код { get; set; }
	[ProtoMember(13)] public string Наименование { get; set; }
	[ProtoMember(14)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(15)] public Entity Родитель { get; set; }
	[ProtoMember(16)] public List<ПрайсЛист.ПрайсМестаХраненияЗапись> ПрайсМестаХранения { get; set; } = new List<ПрайсЛист.ПрайсМестаХраненияЗапись>();
}

[ProtoContract] public sealed class ПрайсМестаХранения
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public bool ДержимНаСкладе { get; set; }
	[ProtoMember(3)] public Entity Отдел { get; set; }
	[ProtoMember(4)] public double Рейтинг { get; set; }
	[ProtoMember(5)] public Entity Владелец { get; set; }
	[ProtoMember(6)] public string Код { get; set; }
	[ProtoMember(7)] public string Наименование { get; set; }
	[ProtoMember(8)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class Претензии
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public bool ОповещатьПоставщика { get; set; }
	[ProtoMember(3)] public bool ФормироватьДвижения { get; set; }
	[ProtoMember(4)] public string Код { get; set; }
	[ProtoMember(5)] public string Наименование { get; set; }
	[ProtoMember(6)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class ПретензииКлиентов
{
	[ProtoContract] public sealed class ОценкиЗапись
	{
		[ProtoMember(1)] public Entity ВидОценки { get; set; }
		[ProtoMember(2)] public string Категории { get; set; }
		[ProtoMember(3)] public Entity Категория { get; set; }
		[ProtoMember(4)] public string Комментарий { get; set; }
		[ProtoMember(5)] public double Оценка { get; set; }
		[ProtoMember(6)] public Entity Причина { get; set; }
	}
	[ProtoMember(1)] public double IDОтзываНаСайте { get; set; }
	[ProtoMember(2)] public Entity Автор { get; set; }
	[ProtoMember(3)] public string АдресЭлектроннойПочтыКлиента { get; set; }
	[ProtoMember(4)] public DateTime ДатаЗакрытия { get; set; }
	[ProtoMember(5)] public DateTime ДатаОтветаКлиенту { get; set; }
	[ProtoMember(6)] public DateTime ДатаРегистрации { get; set; }
	[ProtoMember(7)] public DateTime ДатаСоздания { get; set; }
	[ProtoMember(8)] public Entity Источник { get; set; }
	[ProtoMember(9)] public Entity КатегорияПричиныВозникновения { get; set; }
	[ProtoMember(10)] public Union Клиент { get; set; }
	[ProtoMember(11)] public string Комментарий { get; set; }
	[ProtoMember(12)] public Entity Куратор { get; set; }
	[ProtoMember(13)] public string НашаОценкаПричин { get; set; }
	[ProtoMember(14)] public Entity Обоснована { get; set; }
	[ProtoMember(15)] public string Описание { get; set; }
	[ProtoMember(16)] public Union Основание { get; set; }
	[ProtoMember(17)] public string ОтветКлиенту { get; set; }
	[ProtoMember(18)] public Union ОтветственноеПодразделение { get; set; }
	[ProtoMember(19)] public double Оценка { get; set; }
	[ProtoMember(20)] public double ОценкаПервичная { get; set; }
	[ProtoMember(21)] public Entity Приоритет { get; set; }
	[ProtoMember(22)] public Entity ПричинаВозникновения { get; set; }
	[ProtoMember(23)] public string ПричинаВозникновенияСтрока { get; set; }
	[ProtoMember(24)] public DateTime СрокВыполнения { get; set; }
	[ProtoMember(25)] public Entity Статус { get; set; }
	[ProtoMember(26)] public string ТелефонКлиента { get; set; }
	[ProtoMember(27)] public Entity ТипПретензии { get; set; }
	[ProtoMember(28)] public string Код { get; set; }
	[ProtoMember(29)] public string Наименование { get; set; }
	[ProtoMember(30)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(31)] public Entity ДокументРешение { get; set; }
	[ProtoMember(32)] public DateTime ОтложеноДо { get; set; }
	[ProtoMember(33)] public double СуммаСкидки { get; set; }
	[ProtoMember(34)] public List<ПретензииКлиентов.ОценкиЗапись> Оценки { get; set; } = new List<ПретензииКлиентов.ОценкиЗапись>();
}

[ProtoContract] public sealed class ПретензииКлиентовПрисоединенныеФайлы
{
	[ProtoContract] public sealed class СертификатыШифрованияЗапись
	{
		[ProtoMember(1)] public string Отпечаток { get; set; }
		[ProtoMember(2)] public string Представление { get; set; }
		[ProtoMember(3)] public Entity Сертификат { get; set; }
	}
	[ProtoContract] public sealed class ЭлектронныеПодписиЗапись
	{
		[ProtoMember(1)] public DateTime ДатаПодписи { get; set; }
		[ProtoMember(2)] public string ИмяФайлаПодписи { get; set; }
		[ProtoMember(3)] public string Комментарий { get; set; }
		[ProtoMember(4)] public string КомуВыданСертификат { get; set; }
		[ProtoMember(5)] public string Отпечаток { get; set; }
		[ProtoMember(6)] public Entity Подпись { get; set; }
		[ProtoMember(7)] public Entity Сертификат { get; set; }
		[ProtoMember(8)] public Entity УстановившийПодпись { get; set; }
	}
	[ProtoMember(1)] public Union Автор { get; set; }
	[ProtoMember(2)] public Entity ВладелецФайла { get; set; }
	[ProtoMember(3)] public DateTime ДатаЗаема { get; set; }
	[ProtoMember(4)] public DateTime ДатаМодификацииУниверсальная { get; set; }
	[ProtoMember(5)] public DateTime ДатаСоздания { get; set; }
	[ProtoMember(6)] public bool Зашифрован { get; set; }
	[ProtoMember(7)] public Union Изменил { get; set; }
	[ProtoMember(8)] public double ИндексКартинки { get; set; }
	[ProtoMember(9)] public string Описание { get; set; }
	[ProtoMember(10)] public bool ПодписанЭП { get; set; }
	[ProtoMember(11)] public string ПутьКФайлу { get; set; }
	[ProtoMember(12)] public double Размер { get; set; }
	[ProtoMember(13)] public string Расширение { get; set; }
	[ProtoMember(14)] public Union Редактирует { get; set; }
	[ProtoMember(15)] public Entity СтатусИзвлеченияТекста { get; set; }
	[ProtoMember(16)] public Entity ТекстХранилище { get; set; }
	[ProtoMember(17)] public Entity ТипХраненияФайла { get; set; }
	[ProtoMember(18)] public Entity Том { get; set; }
	[ProtoMember(19)] public Entity ФайлХранилище { get; set; }
	[ProtoMember(20)] public bool ХранитьВерсии { get; set; }
	[ProtoMember(21)] public string Наименование { get; set; }
	[ProtoMember(22)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(23)] public List<ПретензииКлиентовПрисоединенныеФайлы.СертификатыШифрованияЗапись> СертификатыШифрования { get; set; } = new List<ПретензииКлиентовПрисоединенныеФайлы.СертификатыШифрованияЗапись>();
	[ProtoMember(24)] public List<ПретензииКлиентовПрисоединенныеФайлы.ЭлектронныеПодписиЗапись> ЭлектронныеПодписи { get; set; } = new List<ПретензииКлиентовПрисоединенныеФайлы.ЭлектронныеПодписиЗапись>();
}

[ProtoContract] public sealed class ПричиныВозвратов
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public string Код { get; set; }
	[ProtoMember(3)] public string Наименование { get; set; }
	[ProtoMember(4)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(5)] public Entity ТипПеревозки { get; set; }
}

[ProtoContract] public sealed class ПричиныНевыполненияЗаказовКлиентов
{
	[ProtoMember(1)] public string Описание { get; set; }
	[ProtoMember(2)] public string Наименование { get; set; }
	[ProtoMember(3)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(4)] public Entity Родитель { get; set; }
	[ProtoMember(5)] public Entity Назначение { get; set; }
}

[ProtoContract] public sealed class ПричиныПретензий
{
	[ProtoMember(1)] public string Код { get; set; }
	[ProtoMember(2)] public string Наименование { get; set; }
	[ProtoMember(3)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(4)] public Entity Родитель { get; set; }
	[ProtoMember(5)] public Entity ТипПретензии { get; set; }
}

[ProtoContract] public sealed class ПричиныПроблемВЗаказахКлиента
{
	[ProtoMember(1)] public Entity Событие { get; set; }
	[ProtoMember(2)] public Entity ТипПроблемы { get; set; }
	[ProtoMember(3)] public string Код { get; set; }
	[ProtoMember(4)] public string Наименование { get; set; }
	[ProtoMember(5)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class Проекты
{
	[ProtoMember(1)] public Entity Сайт { get; set; }
	[ProtoMember(2)] public string Код { get; set; }
	[ProtoMember(3)] public string Наименование { get; set; }
	[ProtoMember(4)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(5)] public bool ЗапретИзмененияАдресаДоставки { get; set; }
	[ProtoMember(6)] public bool ЗапретИзмененияДанныхКлиента { get; set; }
	[ProtoMember(7)] public bool ЗапретИзмененияДатыДоставки { get; set; }
	[ProtoMember(8)] public string ИдентификаторПроекта { get; set; }
	[ProtoMember(9)] public bool НеПересчитыватьСтоимостьДоставки { get; set; }
	[ProtoMember(10)] public bool НеРезервироватьТоварНаОсновномСкладеРегиона { get; set; }
	[ProtoMember(11)] public bool ОтключитьОповещения { get; set; }
	[ProtoMember(12)] public Entity Перевозчик { get; set; }
	[ProtoMember(13)] public Entity РозничныйПокупатель { get; set; }
	[ProtoMember(14)] public bool НеОкруглятьНДС { get; set; }
	[ProtoMember(15)] public bool ПроверкаДублейЗаказовПоПолучателю { get; set; }
	[ProtoMember(16)] public bool ВыгружатьКИЗ { get; set; }
	[ProtoMember(17)] public bool СрочнаяДоставка { get; set; }
	[ProtoMember(18)] public string ТипСообщенияВОчередиRabbitMQ { get; set; }
	[ProtoMember(19)] public bool ОтдельныйДоговорДляКаждогоРегиона { get; set; }
}

[ProtoContract] public sealed class Производители
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public double КодСтраны { get; set; }
	[ProtoMember(3)] public double Код { get; set; }
	[ProtoMember(4)] public string Наименование { get; set; }
	[ProtoMember(5)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(6)] public Entity Родитель { get; set; }
}

[ProtoContract] public sealed class ПроизводственныеКалендари
{
	[ProtoMember(1)] public Entity БазовыйКалендарь { get; set; }
	[ProtoMember(2)] public string Код { get; set; }
	[ProtoMember(3)] public string Наименование { get; set; }
	[ProtoMember(4)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class ПрофилиГруппДоступа
{
	[ProtoContract] public sealed class ВидыДоступаЗапись
	{
		[ProtoMember(1)] public Union ВидДоступа { get; set; }
		[ProtoMember(2)] public bool ВсеРазрешены { get; set; }
		[ProtoMember(3)] public bool Предустановленный { get; set; }
	}
	[ProtoContract] public sealed class ЗначенияДоступаЗапись
	{
		[ProtoMember(1)] public Union ВидДоступа { get; set; }
		[ProtoMember(2)] public Union ЗначениеДоступа { get; set; }
		[ProtoMember(3)] public bool ВключаяНижестоящие { get; set; }
	}
	[ProtoContract] public sealed class НазначениеЗапись
	{
		[ProtoMember(1)] public Entity ТипПользователей { get; set; }
	}
	[ProtoContract] public sealed class РолиЗапись
	{
		[ProtoMember(1)] public Union Роль { get; set; }
	}
	[ProtoMember(1)] public Entity ИдентификаторПоставляемыхДанных { get; set; }
	[ProtoMember(2)] public string Комментарий { get; set; }
	[ProtoMember(3)] public bool ПоставляемыйПрофильИзменен { get; set; }
	[ProtoMember(4)] public string Наименование { get; set; }
	[ProtoMember(5)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(6)] public Entity Родитель { get; set; }
	[ProtoMember(7)] public List<ПрофилиГруппДоступа.ВидыДоступаЗапись> ВидыДоступа { get; set; } = new List<ПрофилиГруппДоступа.ВидыДоступаЗапись>();
	[ProtoMember(8)] public List<ПрофилиГруппДоступа.ЗначенияДоступаЗапись> ЗначенияДоступа { get; set; } = new List<ПрофилиГруппДоступа.ЗначенияДоступаЗапись>();
	[ProtoMember(9)] public List<ПрофилиГруппДоступа.НазначениеЗапись> Назначение { get; set; } = new List<ПрофилиГруппДоступа.НазначениеЗапись>();
	[ProtoMember(10)] public List<ПрофилиГруппДоступа.РолиЗапись> Роли { get; set; } = new List<ПрофилиГруппДоступа.РолиЗапись>();
}

[ProtoContract] public sealed class ПрофилиПередвижений
{
	[ProtoMember(1)] public double МаксимальныйВес { get; set; }
	[ProtoMember(2)] public double МаксимальныйОбъем { get; set; }
	[ProtoMember(3)] public double СредняяСкорость { get; set; }
	[ProtoMember(4)] public Entity ТипПередвижения { get; set; }
	[ProtoMember(5)] public string Код { get; set; }
	[ProtoMember(6)] public string Наименование { get; set; }
	[ProtoMember(7)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class РегионРаботы
{
	[ProtoContract] public sealed class МаксимальныеНаценкиПоЖВЗапись
	{
		[ProtoMember(1)] public double ВерхняяГраница { get; set; }
		[ProtoMember(2)] public double НаценкаМаксимальнаяОпт { get; set; }
		[ProtoMember(3)] public double НаценкаМаксимальнаяРозница { get; set; }
		[ProtoMember(4)] public double НижняяГраница { get; set; }
	}
	[ProtoContract] public sealed class СписокТелефоновЗапись
	{
		[ProtoMember(1)] public string Телефон { get; set; }
	}
	[ProtoContract] public sealed class ПочтовыеИндексыЗапись
	{
		[ProtoMember(1)] public double КонецДиапазона { get; set; }
		[ProtoMember(2)] public double НачалоДиапазона { get; set; }
	}
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public bool ВыгружатьПКУ { get; set; }
	[ProtoMember(3)] public Entity ЗонаДоставкиПоУмолчанию { get; set; }
	[ProtoMember(4)] public double КодСубъектаРФ { get; set; }
	[ProtoMember(5)] public bool НеЗагружатьИнтервалыРегламентнымЗаданием { get; set; }
	[ProtoMember(6)] public Entity СкладПоУмолчанию { get; set; }
	[ProtoMember(7)] public string ШаблонURLТовара { get; set; }
	[ProtoMember(8)] public double Код { get; set; }
	[ProtoMember(9)] public string Наименование { get; set; }
	[ProtoMember(10)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(11)] public Entity Родитель { get; set; }
	[ProtoMember(12)] public Entity ОсновнойРегион { get; set; }
	[ProtoMember(13)] public string ЧасовойПояс { get; set; }
	[ProtoMember(14)] public bool ДоставкаПоЭР { get; set; }
	[ProtoMember(15)] public string НомерТелефонаИсходящегоЗвонкаAsterisk { get; set; }
	[ProtoMember(16)] public List<РегионРаботы.МаксимальныеНаценкиПоЖВЗапись> МаксимальныеНаценкиПоЖВ { get; set; } = new List<РегионРаботы.МаксимальныеНаценкиПоЖВЗапись>();
	[ProtoMember(17)] public List<РегионРаботы.СписокТелефоновЗапись> СписокТелефонов { get; set; } = new List<РегионРаботы.СписокТелефоновЗапись>();
	[ProtoMember(18)] public List<РегионРаботы.ПочтовыеИндексыЗапись> ПочтовыеИндексы { get; set; } = new List<РегионРаботы.ПочтовыеИндексыЗапись>();
}

[ProtoContract] public sealed class Сайты
{
	[ProtoMember(1)] public string АдресСайта { get; set; }
	[ProtoMember(2)] public bool ВыгружатьПрайсыПоставщиков { get; set; }
	[ProtoMember(3)] public string ИмяКартинки { get; set; }
	[ProtoMember(4)] public double КодПартнера { get; set; }
	[ProtoMember(5)] public bool МобильноеПриложение { get; set; }
	[ProtoMember(6)] public Entity ОсновнойСайт { get; set; }
	[ProtoMember(7)] public string СсылкаНаКартинку { get; set; }
	[ProtoMember(8)] public Entity ХранилищеКартинки { get; set; }
	[ProtoMember(9)] public string Код { get; set; }
	[ProtoMember(10)] public string Наименование { get; set; }
	[ProtoMember(11)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(12)] public string ШаблонАдресаКаталогаТоваров { get; set; }
}

[ProtoContract] public sealed class СервисыЧаевых
{
	[ProtoMember(1)] public string АдресРегистрации { get; set; }
	[ProtoMember(2)] public string АдресСервиса { get; set; }
	[ProtoMember(3)] public string Код { get; set; }
	[ProtoMember(4)] public string Наименование { get; set; }
	[ProtoMember(5)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class СкидочныеПрограммыПоКарте
{
	[ProtoMember(1)] public bool БезБуклета { get; set; }
	[ProtoMember(2)] public string Идентификатор { get; set; }
	[ProtoMember(3)] public string Идентификатор_char50 { get; set; }
	[ProtoMember(4)] public double ЛимитВДнях { get; set; }
	[ProtoMember(5)] public Entity ТипСкидочнойКарты { get; set; }
	[ProtoMember(6)] public Entity ТоварКарта { get; set; }
	[ProtoMember(7)] public string Код { get; set; }
	[ProtoMember(8)] public string Наименование { get; set; }
	[ProtoMember(9)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class СкладыКонтрагентов
{
	[ProtoContract] public sealed class КонтактнаяИнформацияЗапись
	{
		[ProtoMember(1)] public string АдресЭП { get; set; }
		[ProtoMember(2)] public Entity Вид { get; set; }
		[ProtoMember(3)] public Entity ВидДляСписка { get; set; }
		[ProtoMember(4)] public string Город { get; set; }
		[ProtoMember(5)] public string ДоменноеИмяСервера { get; set; }
		[ProtoMember(6)] public string Значение { get; set; }
		[ProtoMember(7)] public string ЗначенияПолей { get; set; }
		[ProtoMember(8)] public string НомерТелефона { get; set; }
		[ProtoMember(9)] public string НомерТелефонаБезКодов { get; set; }
		[ProtoMember(10)] public string Представление { get; set; }
		[ProtoMember(11)] public string Регион { get; set; }
		[ProtoMember(12)] public string Страна { get; set; }
		[ProtoMember(13)] public Entity Тип { get; set; }
	}
	[ProtoMember(1)] public string Адрес { get; set; }
	[ProtoMember(2)] public Entity Бренд { get; set; }
	[ProtoMember(3)] public string ВнешнийКод { get; set; }
	[ProtoMember(4)] public Entity ГрафикРаботы { get; set; }
	[ProtoMember(5)] public double ДокументЭДО { get; set; }
	[ProtoMember(6)] public double Долгота { get; set; }
	[ProtoMember(7)] public string КодЭДО { get; set; }
	[ProtoMember(8)] public string КПП { get; set; }
	[ProtoMember(9)] public Entity ПунктСамовывоза { get; set; }
	[ProtoMember(10)] public bool Работает { get; set; }
	[ProtoMember(11)] public Entity Регион { get; set; }
	[ProtoMember(12)] public double Широта { get; set; }
	[ProtoMember(13)] public Entity Владелец { get; set; }
	[ProtoMember(14)] public string Код { get; set; }
	[ProtoMember(15)] public string Наименование { get; set; }
	[ProtoMember(16)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(17)] public List<СкладыКонтрагентов.КонтактнаяИнформацияЗапись> КонтактнаяИнформация { get; set; } = new List<СкладыКонтрагентов.КонтактнаяИнформацияЗапись>();
}

[ProtoContract] public sealed class Сотрудники
{
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
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public Entity ВидДоговораКурьера { get; set; }
	[ProtoMember(3)] public string мпкЛогин { get; set; }
	[ProtoMember(4)] public string мпкПароль { get; set; }
	[ProtoMember(5)] public Union Подразделение { get; set; }
	[ProtoMember(6)] public string ФИО { get; set; }
	[ProtoMember(7)] public string ШтрихКод { get; set; }
	[ProtoMember(8)] public double ШтрихКодЧисло { get; set; }
	[ProtoMember(9)] public string Код { get; set; }
	[ProtoMember(10)] public string Наименование { get; set; }
	[ProtoMember(11)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(12)] public Entity Родитель { get; set; }
	[ProtoMember(13)] public DateTime ДатаРождения { get; set; }
	[ProtoMember(14)] public DateTime ДокументДатаВыдачи { get; set; }
	[ProtoMember(15)] public string ДокументКемВыдан { get; set; }
	[ProtoMember(16)] public string ДокументКодПодразделения { get; set; }
	[ProtoMember(17)] public string ДокументНомер { get; set; }
	[ProtoMember(18)] public string ДокументСерия { get; set; }
	[ProtoMember(19)] public string МестоЖительства { get; set; }
	[ProtoMember(20)] public string МестоРождения { get; set; }
	[ProtoMember(21)] public bool ФискализацияЧековПриВыдачеЗаказовКлиенту { get; set; }
	[ProtoMember(22)] public Entity ВидЗаказовКлиента { get; set; }
	[ProtoMember(23)] public Entity СтатусСотрудника { get; set; }
	[ProtoMember(24)] public Entity ФормаРасчетаСКурьером { get; set; }
	[ProtoMember(25)] public Entity ДолжностьРаботникаФармацевтическойОрганизации { get; set; }
	[ProtoMember(26)] public string СНИЛС { get; set; }
	[ProtoMember(27)] public Entity ПрофильПередвижения { get; set; }
	[ProtoMember(28)] public List<Сотрудники.КонтактнаяИнформацияЗапись> КонтактнаяИнформация { get; set; } = new List<Сотрудники.КонтактнаяИнформацияЗапись>();
}

[ProtoContract] public sealed class СотрудникиПрисоединенныеФайлы
{
	[ProtoContract] public sealed class СертификатыШифрованияЗапись
	{
		[ProtoMember(1)] public string Отпечаток { get; set; }
		[ProtoMember(2)] public string Представление { get; set; }
		[ProtoMember(3)] public Entity Сертификат { get; set; }
	}
	[ProtoContract] public sealed class ЭлектронныеПодписиЗапись
	{
		[ProtoMember(1)] public DateTime ДатаПодписи { get; set; }
		[ProtoMember(2)] public string ИмяФайлаПодписи { get; set; }
		[ProtoMember(3)] public string Комментарий { get; set; }
		[ProtoMember(4)] public string КомуВыданСертификат { get; set; }
		[ProtoMember(5)] public string Отпечаток { get; set; }
		[ProtoMember(6)] public Entity Подпись { get; set; }
		[ProtoMember(7)] public Entity Сертификат { get; set; }
		[ProtoMember(8)] public Entity УстановившийПодпись { get; set; }
	}
	[ProtoMember(1)] public Union Автор { get; set; }
	[ProtoMember(2)] public Entity ВладелецФайла { get; set; }
	[ProtoMember(3)] public DateTime ДатаЗаема { get; set; }
	[ProtoMember(4)] public DateTime ДатаМодификацииУниверсальная { get; set; }
	[ProtoMember(5)] public DateTime ДатаСоздания { get; set; }
	[ProtoMember(6)] public bool Зашифрован { get; set; }
	[ProtoMember(7)] public Union Изменил { get; set; }
	[ProtoMember(8)] public double ИндексКартинки { get; set; }
	[ProtoMember(9)] public string Описание { get; set; }
	[ProtoMember(10)] public bool ПодписанЭП { get; set; }
	[ProtoMember(11)] public string ПутьКФайлу { get; set; }
	[ProtoMember(12)] public double Размер { get; set; }
	[ProtoMember(13)] public string Расширение { get; set; }
	[ProtoMember(14)] public Union Редактирует { get; set; }
	[ProtoMember(15)] public Entity СтатусИзвлеченияТекста { get; set; }
	[ProtoMember(16)] public Entity ТекстХранилище { get; set; }
	[ProtoMember(17)] public Entity ТипХраненияФайла { get; set; }
	[ProtoMember(18)] public Entity Том { get; set; }
	[ProtoMember(19)] public Entity ФайлХранилище { get; set; }
	[ProtoMember(20)] public bool ХранитьВерсии { get; set; }
	[ProtoMember(21)] public string Наименование { get; set; }
	[ProtoMember(22)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(23)] public List<СотрудникиПрисоединенныеФайлы.СертификатыШифрованияЗапись> СертификатыШифрования { get; set; } = new List<СотрудникиПрисоединенныеФайлы.СертификатыШифрованияЗапись>();
	[ProtoMember(24)] public List<СотрудникиПрисоединенныеФайлы.ЭлектронныеПодписиЗапись> ЭлектронныеПодписи { get; set; } = new List<СотрудникиПрисоединенныеФайлы.ЭлектронныеПодписиЗапись>();
}

[ProtoContract] public sealed class СтавкиНДС
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public double Ставка { get; set; }
	[ProtoMember(3)] public double Код { get; set; }
	[ProtoMember(4)] public string Наименование { get; set; }
	[ProtoMember(5)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(6)] public Union НДС0 { get; set; }
	[ProtoMember(7)] public Union НДС10 { get; set; }
	[ProtoMember(8)] public Union НДС18 { get; set; }
	[ProtoMember(9)] public Union НДС20 { get; set; }
}

[ProtoContract] public sealed class СтандартныеПериоды
{
	[ProtoMember(1)] public double СмещениеДатаНачала { get; set; }
	[ProtoMember(2)] public double СмещениеДатаОкончания { get; set; }
	[ProtoMember(3)] public string Код { get; set; }
	[ProtoMember(4)] public string Наименование { get; set; }
	[ProtoMember(5)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class СтатусыАктуальностиЗаказов
{
	[ProtoMember(1)] public double Порядок { get; set; }
	[ProtoMember(2)] public string Код { get; set; }
	[ProtoMember(3)] public string Наименование { get; set; }
	[ProtoMember(4)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class СтатусыОбработкиЗаказа
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public bool ДвижениеяПоДоставке { get; set; }
	[ProtoMember(3)] public double Порядок { get; set; }
	[ProtoMember(4)] public double ПорядокВАптеках { get; set; }
	[ProtoMember(5)] public double СтатусПартнеров { get; set; }
	[ProtoMember(6)] public double Код { get; set; }
	[ProtoMember(7)] public string Наименование { get; set; }
	[ProtoMember(8)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class СтатусыТовараПодЗаказ
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public bool Дефектура { get; set; }
	[ProtoMember(3)] public bool ПередаватьНаСборку { get; set; }
	[ProtoMember(4)] public bool Расчет { get; set; }
	[ProtoMember(5)] public double Код { get; set; }
	[ProtoMember(6)] public string Наименование { get; set; }
	[ProtoMember(7)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class Страны
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public string Код { get; set; }
	[ProtoMember(3)] public string Наименование { get; set; }
	[ProtoMember(4)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class СтраныМира
{
	[ProtoMember(1)] public string КодАльфа2 { get; set; }
	[ProtoMember(2)] public string КодАльфа3 { get; set; }
	[ProtoMember(3)] public string НаименованиеПолное { get; set; }
	[ProtoMember(4)] public bool УчастникЕАЭС { get; set; }
	[ProtoMember(5)] public string Код { get; set; }
	[ProtoMember(6)] public string Наименование { get; set; }
	[ProtoMember(7)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(8)] public string МеждународноеНаименование { get; set; }
	[ProtoMember(9)] public Union Россия { get; set; }
}

[ProtoContract] public sealed class ТипЦены
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public double Код { get; set; }
	[ProtoMember(3)] public string Наименование { get; set; }
	[ProtoMember(4)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class ТипыОтправкиЭлНакладных
{
	[ProtoMember(1)] public double Код { get; set; }
	[ProtoMember(2)] public string Наименование { get; set; }
	[ProtoMember(3)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class ТипыУниверсальныхСписков
{
	[ProtoMember(1)] public bool ВлияетНаАМ { get; set; }
	[ProtoMember(2)] public bool ИсключатьИзМаркетинговогоОкругления { get; set; }
	[ProtoMember(3)] public bool ИспользоватьАптеки { get; set; }
	[ProtoMember(4)] public bool ИспользоватьНоменклатуру { get; set; }
	[ProtoMember(5)] public bool ИспользоватьПараметр1 { get; set; }
	[ProtoMember(6)] public bool ИспользоватьПараметр2 { get; set; }
	[ProtoMember(7)] public bool ИспользоватьПараметр3 { get; set; }
	[ProtoMember(8)] public bool ИспользоватьПараметр4 { get; set; }
	[ProtoMember(9)] public bool ИспользоватьПараметр5 { get; set; }
	[ProtoMember(10)] public bool ИспользоватьПоставщиков { get; set; }
	[ProtoMember(11)] public bool ИспользоватьТочкиСамовывоза { get; set; }
	[ProtoMember(12)] public string Комментарий { get; set; }
	[ProtoMember(13)] public string НаименованиеПараметр1 { get; set; }
	[ProtoMember(14)] public string НаименованиеПараметр2 { get; set; }
	[ProtoMember(15)] public string НаименованиеПараметр3 { get; set; }
	[ProtoMember(16)] public string НаименованиеПараметр4 { get; set; }
	[ProtoMember(17)] public string НаименованиеПараметр5 { get; set; }
	[ProtoMember(18)] public bool ОбратноеДействиеПоОкончаниюСрокаДействия { get; set; }
	[ProtoMember(19)] public Entity ПолучательУведомлений { get; set; }
	[ProtoMember(20)] public bool ПроверятьЗаполнениеАптек { get; set; }
	[ProtoMember(21)] public bool ПроверятьЗаполнениеНоменклатуры { get; set; }
	[ProtoMember(22)] public bool ПроверятьЗаполнениеПоставщиков { get; set; }
	[ProtoMember(23)] public bool ПроверятьЗаполнениеТочекСамовывоза { get; set; }
	[ProtoMember(24)] public Entity Раздел { get; set; }
	[ProtoMember(25)] public double СпособВлиянияНаАМ { get; set; }
	[ProtoMember(26)] public double УведомлениеЗаДнейДоСрока { get; set; }
	[ProtoMember(27)] public double ЧислоДнейДляЗапускаАвтоматическойРасценки { get; set; }
	[ProtoMember(28)] public string Код { get; set; }
	[ProtoMember(29)] public string Наименование { get; set; }
	[ProtoMember(30)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(31)] public Entity Родитель { get; set; }
	[ProtoMember(32)] public bool РегистрироватьНоменклатуруКОбменуНаСайт { get; set; }
	[ProtoMember(33)] public bool ИспользоватьПроекты { get; set; }
	[ProtoMember(34)] public bool ПроверятьЗаполнениеПроектов { get; set; }
}

[ProtoContract] public sealed class ТоварнаяМатрица
{
	[ProtoMember(1)] public bool Активна { get; set; }
	[ProtoMember(2)] public string Описание { get; set; }
	[ProtoMember(3)] public Entity Ответственный { get; set; }
	[ProtoMember(4)] public Entity УсловиевхожденияПоУмолчанию { get; set; }
	[ProtoMember(5)] public string Код { get; set; }
	[ProtoMember(6)] public string Наименование { get; set; }
	[ProtoMember(7)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(8)] public Entity Родитель { get; set; }
}

[ProtoContract] public sealed class ТомаХраненияФайлов
{
	[ProtoMember(1)] public string Комментарий { get; set; }
	[ProtoMember(2)] public double МаксимальныйРазмер { get; set; }
	[ProtoMember(3)] public string ПолныйПутьLinux { get; set; }
	[ProtoMember(4)] public string ПолныйПутьWindows { get; set; }
	[ProtoMember(5)] public double ПорядокЗаполнения { get; set; }
	[ProtoMember(6)] public string Код { get; set; }
	[ProtoMember(7)] public string Наименование { get; set; }
	[ProtoMember(8)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(9)] public DateTime ВремяПоследнейОчисткиФайлов { get; set; }
}

[ProtoContract] public sealed class ТорговыеМарки
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public bool ОтображатьНаСайте { get; set; }
	[ProtoMember(3)] public string Код { get; set; }
	[ProtoMember(4)] public string Наименование { get; set; }
	[ProtoMember(5)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class ТочкиСамовывоза
{
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
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public Entity АдресХранения { get; set; }
	[ProtoMember(3)] public Entity ВидТочкиСамовывоза { get; set; }
	[ProtoMember(4)] public string ВнешнийИдентификатор { get; set; }
	[ProtoMember(5)] public bool ИспользоватьПорядокСортировкиТочкиСамовывоза { get; set; }
	[ProtoMember(6)] public Entity Контрагент { get; set; }
	[ProtoMember(7)] public double ПорядокСортировкиПриВыбореВЗаказеКлиента { get; set; }
	[ProtoMember(8)] public Entity Регион { get; set; }
	[ProtoMember(9)] public bool СборкаУПартнера { get; set; }
	[ProtoMember(10)] public double Код { get; set; }
	[ProtoMember(11)] public string Наименование { get; set; }
	[ProtoMember(12)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(13)] public double МинимальнаяСуммаСамовывоза { get; set; }
	[ProtoMember(14)] public bool СборкаНаАптеке { get; set; }
	[ProtoMember(15)] public Entity ГрафикРаботы { get; set; }
	[ProtoMember(16)] public bool КоробочнаяСборка { get; set; }
	[ProtoMember(17)] public double Долгота { get; set; }
	[ProtoMember(18)] public double Широта { get; set; }
	[ProtoMember(19)] public double КодПартнера { get; set; }
	[ProtoMember(20)] public Entity Статус { get; set; }
	[ProtoMember(21)] public Entity Бренд { get; set; }
	[ProtoMember(22)] public bool РассылкаОбычныйТекст { get; set; }
	[ProtoMember(23)] public bool НетОплатыКартой { get; set; }
	[ProtoMember(24)] public string Комментарий { get; set; }
	[ProtoMember(25)] public Entity РегионЦенообразования { get; set; }
	[ProtoMember(26)] public List<ТочкиСамовывоза.КонтактнаяИнформацияЗапись> КонтактнаяИнформация { get; set; } = new List<ТочкиСамовывоза.КонтактнаяИнформацияЗапись>();
}

[ProtoContract] public sealed class УниверсальныеСписки
{
	[ProtoContract] public sealed class АптекиЗапись
	{
		[ProtoMember(1)] public Entity Аптека { get; set; }
	}
	[ProtoContract] public sealed class ПоставщикиЗапись
	{
		[ProtoMember(1)] public Entity Поставщик { get; set; }
	}
	[ProtoContract] public sealed class ПроектыЗапись
	{
		[ProtoMember(1)] public Entity Проект { get; set; }
	}
	[ProtoContract] public sealed class ТоварыЗапись
	{
		[ProtoMember(1)] public string Комментарий { get; set; }
		[ProtoMember(2)] public Entity Номенклатура { get; set; }
		[ProtoMember(3)] public double Параметр1 { get; set; }
		[ProtoMember(4)] public double Параметр2 { get; set; }
		[ProtoMember(5)] public double Параметр3 { get; set; }
		[ProtoMember(6)] public double Параметр4 { get; set; }
		[ProtoMember(7)] public double Параметр5 { get; set; }
	}
	[ProtoContract] public sealed class ТочкиСамовывозаЗапись
	{
		[ProtoMember(1)] public Entity ТочкаСамовывоза { get; set; }
	}
	[ProtoMember(1)] public string Акция { get; set; }
	[ProtoMember(2)] public bool ВлияетНаАМ { get; set; }
	[ProtoMember(3)] public DateTime ВремяСоздания { get; set; }
	[ProtoMember(4)] public DateTime ДатаНачалаДействия { get; set; }
	[ProtoMember(5)] public DateTime ДатаОкончанияДействия { get; set; }
	[ProtoMember(6)] public bool ДинамическоеФормированиеАптек { get; set; }
	[ProtoMember(7)] public bool ДинамическоеФормированиеНоменклатуры { get; set; }
	[ProtoMember(8)] public bool ДинамическоеФормированиеПоставщиков { get; set; }
	[ProtoMember(9)] public bool ДинамическоеФормированиеТочекСамовывоза { get; set; }
	[ProtoMember(10)] public string Комментарий { get; set; }
	[ProtoMember(11)] public Entity НастройкиФормированияАптек { get; set; }
	[ProtoMember(12)] public Entity НастройкиФормированияНоменклатуры { get; set; }
	[ProtoMember(13)] public Entity НастройкиФормированияПоставщиков { get; set; }
	[ProtoMember(14)] public Entity НастройкиФормированияТочекСамовывоза { get; set; }
	[ProtoMember(15)] public bool ОбратноеДействиеПоОкончаниюСрокаДействия { get; set; }
	[ProtoMember(16)] public bool ОбязательныйАссортимент { get; set; }
	[ProtoMember(17)] public Entity Ответственный { get; set; }
	[ProtoMember(18)] public double Параметр1 { get; set; }
	[ProtoMember(19)] public double Параметр2 { get; set; }
	[ProtoMember(20)] public double Параметр3 { get; set; }
	[ProtoMember(21)] public double Параметр4 { get; set; }
	[ProtoMember(22)] public double Параметр5 { get; set; }
	[ProtoMember(23)] public string ПараметрКомментарий { get; set; }
	[ProtoMember(24)] public Entity Пользователь { get; set; }
	[ProtoMember(25)] public Entity Раздел { get; set; }
	[ProtoMember(26)] public double СпособВлиянияНаАМ { get; set; }
	[ProtoMember(27)] public Entity Статус { get; set; }
	[ProtoMember(28)] public Entity Тип { get; set; }
	[ProtoMember(29)] public string Код { get; set; }
	[ProtoMember(30)] public string Наименование { get; set; }
	[ProtoMember(31)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(32)] public bool ДинамическоеФормированиеПроектов { get; set; }
	[ProtoMember(33)] public Entity НастройкиФормированияПроектов { get; set; }
	[ProtoMember(34)] public List<УниверсальныеСписки.АптекиЗапись> Аптеки { get; set; } = new List<УниверсальныеСписки.АптекиЗапись>();
	[ProtoMember(35)] public List<УниверсальныеСписки.ПоставщикиЗапись> Поставщики { get; set; } = new List<УниверсальныеСписки.ПоставщикиЗапись>();
	[ProtoMember(36)] public List<УниверсальныеСписки.ПроектыЗапись> Проекты { get; set; } = new List<УниверсальныеСписки.ПроектыЗапись>();
	[ProtoMember(37)] public List<УниверсальныеСписки.ТоварыЗапись> Товары { get; set; } = new List<УниверсальныеСписки.ТоварыЗапись>();
	[ProtoMember(38)] public List<УниверсальныеСписки.ТочкиСамовывозаЗапись> ТочкиСамовывоза { get; set; } = new List<УниверсальныеСписки.ТочкиСамовывозаЗапись>();
}

[ProtoContract] public sealed class Упаковки
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public double Код { get; set; }
	[ProtoMember(3)] public string Наименование { get; set; }
	[ProtoMember(4)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class УпаковкиЗаказовКлиентов
{
	[ProtoMember(1)] public double Номер { get; set; }
	[ProtoMember(2)] public string НомерСокращенный { get; set; }
	[ProtoMember(3)] public bool Холод { get; set; }
	[ProtoMember(4)] public string Наименование { get; set; }
	[ProtoMember(5)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class УсловиеВхожденияВТоварнуюМатрицу
{
	[ProtoMember(1)] public string МакетУсловия { get; set; }
	[ProtoMember(2)] public string НаследуемыеПоляМатрицы { get; set; }
	[ProtoMember(3)] public Entity НастройкаПолейУсловийВхождения { get; set; }
	[ProtoMember(4)] public string Код { get; set; }
	[ProtoMember(5)] public string Наименование { get; set; }
	[ProtoMember(6)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class УчетныеЗаписиЭлектроннойПочты
{
	[ProtoMember(1)] public string АдресЭлектроннойПочты { get; set; }
	[ProtoMember(2)] public Entity ВладелецУчетнойЗаписи { get; set; }
	[ProtoMember(3)] public double ВремяОжидания { get; set; }
	[ProtoMember(4)] public string ИмяПользователя { get; set; }
	[ProtoMember(5)] public bool ИспользоватьДляОтправки { get; set; }
	[ProtoMember(6)] public bool ИспользоватьДляПолучения { get; set; }
	[ProtoMember(7)] public bool ИспользоватьЗащищенноеСоединениеДляВходящейПочты { get; set; }
	[ProtoMember(8)] public bool ИспользоватьЗащищенноеСоединениеДляИсходящейПочты { get; set; }
	[ProtoMember(9)] public bool ОставлятьКопииСообщенийНаСервере { get; set; }
	[ProtoMember(10)] public bool ОтправлятьСкрытыеКопииПисемНаЭтотАдрес { get; set; }
	[ProtoMember(11)] public double ПериодХраненияСообщенийНаСервере { get; set; }
	[ProtoMember(12)] public string Пользователь { get; set; }
	[ProtoMember(13)] public string ПользовательSMTP { get; set; }
	[ProtoMember(14)] public double ПортСервераВходящейПочты { get; set; }
	[ProtoMember(15)] public double ПортСервераИсходящейПочты { get; set; }
	[ProtoMember(16)] public bool ПриОтправкеПисемТребуетсяАвторизация { get; set; }
	[ProtoMember(17)] public string ПротоколВходящейПочты { get; set; }
	[ProtoMember(18)] public string СерверВходящейПочты { get; set; }
	[ProtoMember(19)] public string СерверИсходящейПочты { get; set; }
	[ProtoMember(20)] public bool ТребуетсяВходНаСерверПередОтправкой { get; set; }
	[ProtoMember(21)] public string Наименование { get; set; }
	[ProtoMember(22)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class Фасовки
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public string Код { get; set; }
	[ProtoMember(3)] public string Наименование { get; set; }
	[ProtoMember(4)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class Фирмы
{
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
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public string ГНИ { get; set; }
	[ProtoMember(3)] public Entity ГоловнаяОрганизация { get; set; }
	[ProtoMember(4)] public DateTime ДатаРегистрации { get; set; }
	[ProtoMember(5)] public bool ЕстьЛицензияНаОптовуюТорговлюЛС { get; set; }
	[ProtoMember(6)] public string ИНН { get; set; }
	[ProtoMember(7)] public string Кассир { get; set; }
	[ProtoMember(8)] public string КодНалоговогоОргана { get; set; }
	[ProtoMember(9)] public string ОГРН { get; set; }
	[ProtoMember(10)] public string ОКОНХ { get; set; }
	[ProtoMember(11)] public string ОКПО { get; set; }
	[ProtoMember(12)] public string ПолнНаименование { get; set; }
	[ProtoMember(13)] public string ПочтовыйАдрес { get; set; }
	[ProtoMember(14)] public string Префикс { get; set; }
	[ProtoMember(15)] public Entity СистемаНалогообложения { get; set; }
	[ProtoMember(16)] public string Телефоны { get; set; }
	[ProtoMember(17)] public double ЦифровойИндексОбособленногоПодразделения { get; set; }
	[ProtoMember(18)] public string ЮридическийАдрес { get; set; }
	[ProtoMember(19)] public string Код { get; set; }
	[ProtoMember(20)] public string Наименование { get; set; }
	[ProtoMember(21)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(22)] public Entity БанковскийСчетПоУмолчанию { get; set; }
	[ProtoMember(23)] public bool НеВыгружатьВWMS { get; set; }
	[ProtoMember(24)] public string КодНаСайте { get; set; }
	[ProtoMember(25)] public string ОКВЭД { get; set; }
	[ProtoMember(26)] public string КорректныйИНН { get; set; }
	[ProtoMember(27)] public string КорректныйКПП { get; set; }
	[ProtoMember(28)] public List<Фирмы.КонтактнаяИнформацияЗапись> КонтактнаяИнформация { get; set; } = new List<Фирмы.КонтактнаяИнформацияЗапись>();
}

[ProtoContract] public sealed class Франчайзи
{
	[ProtoMember(1)] public string Код { get; set; }
	[ProtoMember(2)] public string Наименование { get; set; }
	[ProtoMember(3)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class ЦеныПартии
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public DateTime ДатаПереоценки { get; set; }
	[ProtoMember(3)] public Entity РегионРаботы { get; set; }
	[ProtoMember(4)] public Entity ТипЦены { get; set; }
	[ProtoMember(5)] public double Цена { get; set; }
	[ProtoMember(6)] public Entity Владелец { get; set; }
	[ProtoMember(7)] public double Код { get; set; }
	[ProtoMember(8)] public string Наименование { get; set; }
	[ProtoMember(9)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class ЦеныПрайсЛиста
{
	[ProtoMember(1)] public string ID_77 { get; set; }
	[ProtoMember(2)] public double НеНазначатьЦенуРоботом { get; set; }
	[ProtoMember(3)] public Entity РегионРаботы { get; set; }
	[ProtoMember(4)] public double СбрасыватьЦенуПриСледующемПриходе { get; set; }
	[ProtoMember(5)] public Entity ТипЦены { get; set; }
	[ProtoMember(6)] public Entity Владелец { get; set; }
	[ProtoMember(7)] public string Код { get; set; }
	[ProtoMember(8)] public string Наименование { get; set; }
	[ProtoMember(9)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class ШаблоныПечатиКиЗ
{
	[ProtoMember(1)] public string Код { get; set; }
	[ProtoMember(2)] public string Наименование { get; set; }
	[ProtoMember(3)] public bool ПометкаУдаления { get; set; }
}

[ProtoContract] public sealed class ШаблоныСообщений
{
	[ProtoContract] public sealed class ПараметрыЗапись
	{
		[ProtoMember(1)] public string ИмяПараметра { get; set; }
		[ProtoMember(2)] public string ПредставлениеПараметра { get; set; }
		[ProtoMember(3)] public Entity ТипПараметра { get; set; }
	}
	[ProtoContract] public sealed class ПечатныеФормыИВложенияЗапись
	{
		[ProtoMember(1)] public string Идентификатор { get; set; }
		[ProtoMember(2)] public string Имя { get; set; }
	}
	[ProtoMember(1)] public Entity Автор { get; set; }
	[ProtoMember(2)] public Entity ВнешняяОбработка { get; set; }
	[ProtoMember(3)] public string Назначение { get; set; }
	[ProtoMember(4)] public string Отправитель { get; set; }
	[ProtoMember(5)] public bool ОтправлятьВТранслите { get; set; }
	[ProtoMember(6)] public string ПолноеИмяТипаПараметраВводаНаОсновании { get; set; }
	[ProtoMember(7)] public bool ПредназначенДляSMS { get; set; }
	[ProtoMember(8)] public bool ПредназначенДляВводаНаОсновании { get; set; }
	[ProtoMember(9)] public bool ПредназначенДляЭлектронныхПисем { get; set; }
	[ProtoMember(10)] public string ТекстШаблонаSMS { get; set; }
	[ProtoMember(11)] public string ТекстШаблонаПисьма { get; set; }
	[ProtoMember(12)] public string ТекстШаблонаПисьмаHTML { get; set; }
	[ProtoMember(13)] public string ТемаПисьма { get; set; }
	[ProtoMember(14)] public Entity ТипТекстаПисьма { get; set; }
	[ProtoMember(15)] public bool ТолькоДляАвтора { get; set; }
	[ProtoMember(16)] public bool ТранслитерироватьИменаФайлов { get; set; }
	[ProtoMember(17)] public bool УпаковатьВАрхив { get; set; }
	[ProtoMember(18)] public Entity ФорматВложений { get; set; }
	[ProtoMember(19)] public bool ШаблонПоВнешнейОбработке { get; set; }
	[ProtoMember(20)] public string Код { get; set; }
	[ProtoMember(21)] public string Наименование { get; set; }
	[ProtoMember(22)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(23)] public bool ПодписьИПечать { get; set; }
	[ProtoMember(24)] public List<ШаблоныСообщений.ПараметрыЗапись> Параметры { get; set; } = new List<ШаблоныСообщений.ПараметрыЗапись>();
	[ProtoMember(25)] public List<ШаблоныСообщений.ПечатныеФормыИВложенияЗапись> ПечатныеФормыИВложения { get; set; } = new List<ШаблоныСообщений.ПечатныеФормыИВложенияЗапись>();
}

[ProtoContract] public sealed class ШаблоныСообщенийПрисоединенныеФайлы
{
	[ProtoMember(1)] public Union Автор { get; set; }
	[ProtoMember(2)] public Entity ВладелецФайла { get; set; }
	[ProtoMember(3)] public DateTime ДатаЗаема { get; set; }
	[ProtoMember(4)] public DateTime ДатаМодификацииУниверсальная { get; set; }
	[ProtoMember(5)] public DateTime ДатаСоздания { get; set; }
	[ProtoMember(6)] public bool Зашифрован { get; set; }
	[ProtoMember(7)] public string ИДФайлаЭлектронногоПисьма { get; set; }
	[ProtoMember(8)] public Union Изменил { get; set; }
	[ProtoMember(9)] public double ИндексКартинки { get; set; }
	[ProtoMember(10)] public string Описание { get; set; }
	[ProtoMember(11)] public bool ПодписанЭП { get; set; }
	[ProtoMember(12)] public string ПутьКФайлу { get; set; }
	[ProtoMember(13)] public double Размер { get; set; }
	[ProtoMember(14)] public string Расширение { get; set; }
	[ProtoMember(15)] public Union Редактирует { get; set; }
	[ProtoMember(16)] public Entity СтатусИзвлеченияТекста { get; set; }
	[ProtoMember(17)] public Entity ТекстХранилище { get; set; }
	[ProtoMember(18)] public Entity ТипХраненияФайла { get; set; }
	[ProtoMember(19)] public Entity ФайлХранилище { get; set; }
	[ProtoMember(20)] public bool ХранитьВерсии { get; set; }
	[ProtoMember(21)] public string Наименование { get; set; }
	[ProtoMember(22)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(23)] public Entity Том { get; set; }
}

[ProtoContract] public sealed class ШаблоныФайловОбмена
{
	[ProtoContract] public sealed class СоответствиеПолейЗапись
	{
		[ProtoMember(1)] public Union ЗначениеПоУмолчанию { get; set; }
		[ProtoMember(2)] public string ИмяПоляФайла { get; set; }
		[ProtoMember(3)] public double НомерСтолбца { get; set; }
		[ProtoMember(4)] public Entity Поставщик { get; set; }
		[ProtoMember(5)] public Union Поле { get; set; }
	}
	[ProtoMember(1)] public string Кодировка { get; set; }
	[ProtoMember(2)] public Entity Назначение { get; set; }
	[ProtoMember(3)] public double НачальнаяСтрока { get; set; }
	[ProtoMember(4)] public string Разделитель { get; set; }
	[ProtoMember(5)] public string Расширение { get; set; }
	[ProtoMember(6)] public Entity ТипФайла { get; set; }
	[ProtoMember(7)] public string Код { get; set; }
	[ProtoMember(8)] public string Наименование { get; set; }
	[ProtoMember(9)] public bool ПометкаУдаления { get; set; }
	[ProtoMember(10)] public Entity Родитель { get; set; }
	[ProtoMember(11)] public List<ШаблоныФайловОбмена.СоответствиеПолейЗапись> СоответствиеПолей { get; set; } = new List<ШаблоныФайловОбмена.СоответствиеПолейЗапись>();
}
}
}
