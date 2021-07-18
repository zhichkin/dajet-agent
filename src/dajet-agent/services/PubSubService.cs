using DaJet.Agent.Service.Model;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DaJet.Agent.Service.Services
{
    public interface IPubSubService
    {
        List<Node> SelectNodes();
        Node SelectNode(int id);
        void CreateNode(Node node);
        void UpdateNode(Node node);
        void DeleteNode(Node node);
    }
    public sealed class PubSubService : IPubSubService
    {
        private const string CONST_DATABASE_CATALOG = "data";
        private const string CONST_DATABASE_FILENAME = "dajet.db";
        private AppSettings Settings { get; set; }
        public PubSubService(IOptions<AppSettings> options)
        {
            Settings = options.Value;
            InitializeDatabase();
        }
        private string DatabaseCatalog
        {
            get
            {
                string databaseCatalog = Path.Combine(Settings.AppCatalog, CONST_DATABASE_CATALOG);
                if (!Directory.Exists(databaseCatalog))
                {
                    Directory.CreateDirectory(databaseCatalog);
                }
                return databaseCatalog;
            }
        }
        private string DatabaseFilePath
        {
            get
            {
                return Path.Combine(Settings.AppCatalog, DatabaseCatalog, CONST_DATABASE_FILENAME);
            }
        }
        private string ConnectionString
        {
            get
            {
                return new SqliteConnectionStringBuilder()
                {
                    DataSource = DatabaseFilePath,
                    Mode = SqliteOpenMode.ReadWriteCreate
                }
                .ToString();
            }
        }
        private void InitializeDatabase()
        {
            if (File.Exists(DatabaseFilePath)) return;

            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = CreateTableScript_Nodes();
                    command.ExecuteNonQuery();
                }
            }
        }

        #region "Nodes"
        private string CreateTableScript_Nodes()
        {
            StringBuilder script = new StringBuilder();
            script.AppendLine("CREATE TABLE IF NOT EXISTS nodes (");
            script.AppendLine("id INTEGER PRIMARY KEY,");
            script.AppendLine("code TEXT NOT NULL UNIQUE,");
            script.AppendLine("description TEXT NOT NULL");
            script.Append(");");
            return script.ToString();
        }

        private string SelectNodesScript()
        {
            StringBuilder script = new StringBuilder();
            script.AppendLine("SELECT id, code, description FROM nodes;");
            return script.ToString();
        }
        private string SelectNodeScript()
        {
            StringBuilder script = new StringBuilder();
            script.AppendLine("SELECT id, code, description FROM nodes WHERE id = @id;");
            return script.ToString();
        }
        private string CreateNodeScript()
        {
            StringBuilder script = new StringBuilder();
            script.AppendLine("INSERT INTO nodes (code, description) VALUES (@code, @description);");
            script.AppendLine("SELECT LAST_INSERT_ROWID();");
            return script.ToString();
        }
        private string UpdateNodeScript()
        {
            StringBuilder script = new StringBuilder();
            script.AppendLine("UPDATE nodes SET code = @code, description = @description WHERE id = @id;");
            return script.ToString();
        }
        private string DeleteNodeScript()
        {
            StringBuilder script = new StringBuilder();
            script.AppendLine("DELETE FROM nodes WHERE id = @id;");
            return script.ToString();
        }

        public List<Node> SelectNodes()
        {
            List<Node> list = new List<Node>();
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = SelectNodesScript();
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Node()
                            {
                                Id = reader.GetInt32(0),
                                Code = reader.GetString(1),
                                Description = reader.GetString(2)
                            });
                        }
                        reader.Close();
                    }
                }
            }
            return list;
        }
        public Node SelectNode(int id)
        {
            Node node = null;
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = SelectNodeScript();
                    command.Parameters.AddWithValue("id", id);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            node = new Node()
                            {
                                Id = reader.GetInt32(0),
                                Code = reader.GetString(1),
                                Description = reader.GetString(2)
                            };
                        }
                        reader.Close();
                    }
                }
            }
            return node;
        }
        public void CreateNode(Node node)
        {
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = CreateNodeScript();
                    command.Parameters.AddWithValue("code", node.Code);
                    command.Parameters.AddWithValue("description", node.Description);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            node.Id = reader.GetInt32(0);
                        }
                        reader.Close();
                    }
                }
            }
        }
        public void UpdateNode(Node node)
        {
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = UpdateNodeScript();
                    command.Parameters.AddWithValue("id", node.Id);
                    command.Parameters.AddWithValue("code", node.Code);
                    command.Parameters.AddWithValue("description", node.Description);
                    int affected = command.ExecuteNonQuery();
                }
            }
        }
        public void DeleteNode(Node node)
        {
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = DeleteNodeScript();
                    command.Parameters.AddWithValue("id", node.Id);
                    int affected = command.ExecuteNonQuery();
                }
            }
        }
        #endregion
    }
}