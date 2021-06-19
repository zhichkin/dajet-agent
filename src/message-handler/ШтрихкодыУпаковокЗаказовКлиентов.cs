namespace DaJet.Agent.MessageHandlers
{
    public sealed class ШтрихкодыУпаковокЗаказовКлиентов
    {
        public string ЗаказКлиента { get; set; }
        public string ШтрихкодВложеннойУпаковки { get; set; }
        public string ШтрихкодУпаковки { get; set; }
        public string КороткийНомерУпаковки { get; set; }
        public string КороткийНомерВложеннойУпаковки { get; set; }
        public string ДокументТранспортировки { get; set; }
        public bool КоробочнаяСборка { get; set; }
        public bool ХранитьВХолоде { get; set; }
        public bool СодержитСтекло { get; set; }
    }
}