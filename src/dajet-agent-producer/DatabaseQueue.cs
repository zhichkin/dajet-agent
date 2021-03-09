using System.Collections.Generic;

namespace DaJet.Agent.Producer
{
    public sealed class DatabaseQueue
    {
        public string TableName { get; set; } = string.Empty;
        public string ObjectName { get; set; } = string.Empty;
        public List<TableField> Fields { get; set; } = new List<TableField>();
    }
    public sealed class TableField
    {
        public string Name { get; set; } = string.Empty;
        public string Property { get; set; } = string.Empty;
    }
}