using DaJet.Data;
using DaJet.Metadata;
using DaJet.Metadata.Model;
using System.Collections.Generic;
using System.Data;

namespace DaJet.Agent.Service
{
    internal sealed class ExchangePlanHelper
    {
        #region "SQL QUERY SCRIPT TEMPLATES"

        private const string MS_THIS_NODE_SELECT_TEMPLATE =
            "SELECT TOP 1 _Code FROM {EXCHANGE_PLAN_TABLE} WHERE _PredefinedID > 0x00000000000000000000000000000000;";

        private const string MS_EXCHANGE_NODES_SELECT_TEMPLATE =
            "SELECT T1._Code FROM {EXCHANGE_PLAN_TABLE} AS T1 " +
            "INNER JOIN {INFORMATION_REGISTER_TABLE} AS T2 " +
            "ON T1._IDRRef = T2.{NODE_REFERENCE} " +
            "WHERE T1._Marked = 0x00 AND T2.{USE_RABBITMQ} = 0x01;";

        private const string PG_THIS_NODE_SELECT_TEMPLATE =
            "SELECT CAST(_code AS varchar) FROM {EXCHANGE_PLAN_TABLE} WHERE _predefinedid > E'\\\\x00000000000000000000000000000000' LIMIT 1;";

        private const string PG_EXCHANGE_NODES_SELECT_TEMPLATE =
            "SELECT CAST(\"T1\"._code AS varchar) FROM {EXCHANGE_PLAN_TABLE} AS \"T1\" " +
            "INNER JOIN {INFORMATION_REGISTER_TABLE} AS \"T2\" " +
            "ON \"T1\"._idrref = \"T2\".{NODE_REFERENCE} " +
            "WHERE \"T1\"._marked = FALSE AND \"T2\".{USE_RABBITMQ} = TRUE;";

        #endregion

        private readonly string _connectionString;
        private readonly DatabaseProvider _provider;

        private InfoBase _infoBase;
        private Dictionary<string, string> _mappings = new Dictionary<string, string>();

        private string THIS_NODE_SELECT_SCRIPT;
        private string EXCHANGE_NODES_SELECT_SCRIPT;

        internal ExchangePlanHelper(in InfoBase infoBase, DatabaseProvider provider, string connectionString)
        {
            _infoBase = infoBase;
            _provider = provider;
            _connectionString = connectionString;
        }
        private string ConfigureScript(in string template)
        {
            string script = template;

            foreach (var item in _mappings)
            {
                script = script.Replace(item.Key, item.Value);
            }

            return script;
        }
        internal void ConfigureSelectScripts(in string publicationName, in string registerName)
        {
            _mappings.Clear();

            ApplicationObject settings = _infoBase.GetApplicationObjectByName(registerName);
            ApplicationObject publication = _infoBase.GetApplicationObjectByName(publicationName);

            _mappings.Add("{EXCHANGE_PLAN_TABLE}", publication.TableName);
            _mappings.Add("{INFORMATION_REGISTER_TABLE}", settings.TableName);

            foreach (MetadataProperty property in settings.Properties)
            {
                if (property.Name == "Узел")
                {
                    _mappings.Add("{NODE_REFERENCE}", property.Fields[0].Name);
                }
                else if (property.Name == "ИспользоватьОбменДаннымиRabbitMQ")
                {
                    _mappings.Add("{USE_RABBITMQ}", property.Fields[0].Name);
                }
            }

            if (_provider == DatabaseProvider.SQLServer)
            {
                THIS_NODE_SELECT_SCRIPT = ConfigureScript(MS_THIS_NODE_SELECT_TEMPLATE);
                EXCHANGE_NODES_SELECT_SCRIPT = ConfigureScript(MS_EXCHANGE_NODES_SELECT_TEMPLATE);
            }
            else // PostgreSQL
            {
                THIS_NODE_SELECT_SCRIPT = ConfigureScript(PG_THIS_NODE_SELECT_TEMPLATE);
                EXCHANGE_NODES_SELECT_SCRIPT = ConfigureScript(PG_EXCHANGE_NODES_SELECT_TEMPLATE);
            }
        }
        internal string GetThisNode()
        {
            QueryExecutor executor = new QueryExecutor(_provider, in _connectionString);

            string code = executor.ExecuteScalar<string>(THIS_NODE_SELECT_SCRIPT, 10);

            if (string.IsNullOrWhiteSpace(code))
            {
                return string.Empty;
            }
            
            return code.Trim();
        }
        internal List<string> GetExchangeNodes()
        {
            List<string> result = new List<string>();

            QueryExecutor executor = new QueryExecutor(_provider, in _connectionString);

            foreach (IDataReader reader in executor.ExecuteReader(EXCHANGE_NODES_SELECT_SCRIPT, 10))
            {
                string code = reader.IsDBNull(0) ? string.Empty : reader.GetString(0).Trim();

                if (string.IsNullOrWhiteSpace(code))
                {
                    continue;
                }

                result.Add(code);
            }

            return result;
        }
        internal string CreateQueueName(in string senderCode, in string recipientCode)
        {
            return $"РИБ.{senderCode}.{recipientCode}";
        }
    }
}