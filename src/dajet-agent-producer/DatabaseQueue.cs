using System.Collections.Generic;

namespace DaJet.Agent.Producer
{
    public sealed class DatabaseQueue
    {
        public string TableName { get; set; }
        public string ObjectName { get; set; }
        public List<TableField> Fields { get; set; } = new List<TableField>();
    }
    public sealed class TableField
    {
        public string Name { get; set; }
        public string Property { get; set; }
    }
}