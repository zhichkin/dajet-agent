using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaJet.Database.Adapter
{
    /// <summary>
    /// Табличный интерфейс исходящей очереди сообщений
    /// (непериодический независимый регистр сведений)
    /// </summary>
    [Table("РегистрСведений.ИсходящаяОчередьRabbitMQ")] public sealed class DatabaseOutgoingMessage
    {
        /// <summary>
        /// "МоментВремени" Порядковый номер сообщения (может генерироваться средствами СУБД) - numeric(19,0)
        /// </summary>
        [Column("МоментВремени")] [NotMapped] public long MessageNumber { get; set; } = 0L;
        /// <summary>
        /// "Идентификатор" Уникальный идентификатор сообщения - binary(16)
        /// </summary>
        [Column("Идентификатор")] public Guid Uuid { get; set; }
        /// <summary>
        /// "Отправитель" Код или UUID отправителя сообщения - nvarchar(36)
        /// </summary>
        [Column("Отправитель")] public string Sender { get; set; } = string.Empty;
        /// <summary>
        /// "Получатели" Коды или UUID получателей сообщения в формате CSV - nvarchar(max)
        /// </summary>
        [Column("Получатели")] public string Recipients { get; set; } = string.Empty;
        /// <summary>
        /// "ТипОперации" Тип операции: INSERT, UPDATE или DELETE - nvarchar(6)
        /// </summary>
        [Column("ТипОперации")] public string OperationType { get; set; } = string.Empty;
        /// <summary>
        /// "ТипСообщения" Тип сообщения, например, "Справочник.Номенклатура" - nvarchar(1024)
        /// </summary>
        [Column("ТипСообщения")] public string MessageType { get; set; } = string.Empty;
        /// <summary>
        /// "ТелоСообщения" Тело сообщения в формате JSON или XML - nvarchar(max)
        /// </summary>
        [Column("ТелоСообщения")] public string MessageBody { get; set; } = string.Empty;
        /// <summary>
        /// "ДатаВремя" Время создания сообщения - datetime2
        /// </summary>
        [Column("ДатаВремя")] public DateTime DateTimeStamp { get; set; } = DateTime.Now;
    }
}