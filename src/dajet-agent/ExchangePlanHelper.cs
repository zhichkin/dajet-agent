using DaJet.Data;
using DaJet.Metadata;
using DaJet.Metadata.Model;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DaJet.Agent.Service
{
    internal sealed class ExchangePlanHelper
    {
        #region "SQL QUERY SCRIPT TEMPLATES"

        private const string MS_THIS_NODE_SELECT_TEMPLATE =
            "SELECT TOP 1 _Code FROM {EXCHANGE_PLAN_TABLE} WHERE _PredefinedID > 0x00000000000000000000000000000000;";

        private const string MS_EXCHANGE_NODES_SELECT_TEMPLATE =
            "SELECT _Code FROM {EXCHANGE_PLAN_TABLE} WHERE _Marked = 0x00 AND _PredefinedID = 0x00000000000000000000000000000000;";

        private const string PG_THIS_NODE_SELECT_TEMPLATE =
            "SELECT CAST(_code AS varchar) FROM {EXCHANGE_PLAN_TABLE} WHERE _predefinedid > E'\\\\x00000000000000000000000000000000' LIMIT 1;";

        private const string PG_EXCHANGE_NODES_SELECT_TEMPLATE =
            "SELECT CAST(_code AS varchar) FROM {EXCHANGE_PLAN_TABLE} WHERE _marked = FALSE AND _predefinedid = E'\\\\x00000000000000000000000000000000';";

        #endregion

        private readonly string _connectionString;
        private readonly DatabaseProvider _provider;

        private InfoBase _infoBase;
        private Dictionary<string, string> _mappings = new Dictionary<string, string>();

        private string THIS_NODE_SELECT_SCRIPT;
        private string SENDER_NODES_SELECT_SCRIPT;
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
        internal void ConfigureSelectScripts(in string publicationName)
        {
            _mappings.Clear();

            ApplicationObject publication = _infoBase.GetApplicationObjectByName(publicationName);

            _mappings.Add("{EXCHANGE_PLAN_TABLE}", publication.TableName);

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
        private string GetThisNodeCode(string publicationTableName)
        {
            if (_provider == DatabaseProvider.SQLServer)
            {
                THIS_NODE_SELECT_SCRIPT = MS_THIS_NODE_SELECT_TEMPLATE.Replace("{EXCHANGE_PLAN_TABLE}", publicationTableName);
            }
            else // PostgreSQL
            {
                THIS_NODE_SELECT_SCRIPT = PG_THIS_NODE_SELECT_TEMPLATE.Replace("{EXCHANGE_PLAN_TABLE}", publicationTableName);
            }

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
        internal List<string> GetIncomingQueueNames(List<string> publicationNames)
        {
            HashSet<string> queues = new HashSet<string>();

            QueryExecutor executor = new QueryExecutor(_provider, in _connectionString);

            foreach (string publicationName in publicationNames)
            {
                ApplicationObject publication = _infoBase.GetApplicationObjectByName(publicationName);

                string recipient = GetThisNodeCode(publication.TableName);

                if (string.IsNullOrWhiteSpace(recipient))
                {
                    continue;
                }
                else
                {
                    recipient = recipient.Trim();
                }

                if (_provider == DatabaseProvider.SQLServer)
                {
                    SENDER_NODES_SELECT_SCRIPT = MS_EXCHANGE_NODES_SELECT_TEMPLATE
                        .Replace("{EXCHANGE_PLAN_TABLE}", publication.TableName); ;
                }
                else // PostgreSQL
                {
                    SENDER_NODES_SELECT_SCRIPT = PG_EXCHANGE_NODES_SELECT_TEMPLATE
                        .Replace("{EXCHANGE_PLAN_TABLE}", publication.TableName);
                }

                foreach (IDataReader reader in executor.ExecuteReader(SENDER_NODES_SELECT_SCRIPT, 10))
                {
                    string sender = reader.IsDBNull(0) ? string.Empty : reader.GetString(0).Trim();

                    if (string.IsNullOrWhiteSpace(sender))
                    {
                        continue;
                    }

                    string queueName = CreateIncomingQueueName(in sender, in recipient);

                    if (!queues.Contains(queueName))
                    {
                        _ = queues.Add(queueName);
                    }
                }
            }

            return queues.ToList();
        }
        internal string CreateIncomingQueueName(in string senderCode, in string recipientCode)
        {
            return $"РИБ.{senderCode}.{recipientCode}";
        }
    }
}