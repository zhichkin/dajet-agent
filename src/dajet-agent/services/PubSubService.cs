using DaJet.Agent.Model;
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

        List<Publication> SelectPublications();
        List<Publication> SelectPublications(int publisher);
        Publication SelectPublication(int id);
        void CreatePublication(Publication publication);
        void UpdatePublication(Publication publication);
        void DeletePublication(Publication publication);
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

                    command.CommandText = CreateTableScript_Publications();
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
                    // TODO: if (affected == 0) raise error ?
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

        #region "Publications"
        private string CreateTableScript_Publications()
        {
            StringBuilder script = new StringBuilder();
            script.AppendLine("CREATE TABLE IF NOT EXISTS publications");
            script.AppendLine("(");
            script.AppendLine("id INTEGER PRIMARY KEY,");
            script.AppendLine("name TEXT NOT NULL,");
            script.AppendLine("publisher INTEGER NOT NULL");
            script.AppendLine(");");
            script.AppendLine("CREATE UNIQUE INDEX ix_publisher_name");
            script.Append("ON publications (publisher, name);");
            return script.ToString();
        }

        private string SelectPublicationsScript()
        {
            StringBuilder script = new StringBuilder();
            script.AppendLine("SELECT id, name, publisher FROM publications;");
            return script.ToString();
        }
        private string SelectPublisherPublicationsScript()
        {
            StringBuilder script = new StringBuilder();
            script.AppendLine("SELECT id, name, publisher FROM publications WHERE publisher = @publisher;");
            return script.ToString();
        }
        private string SelectPublicationScript()
        {
            StringBuilder script = new StringBuilder();
            script.AppendLine("SELECT id, name, publisher FROM publications WHERE id = @id;");
            return script.ToString();
        }
        private string CreatePublicationScript()
        {
            StringBuilder script = new StringBuilder();
            script.AppendLine("INSERT INTO publications (name, publisher) VALUES (@name, @publisher);");
            script.AppendLine("SELECT LAST_INSERT_ROWID();");
            return script.ToString();
        }
        private string UpdatePublicationScript()
        {
            StringBuilder script = new StringBuilder();
            script.AppendLine("UPDATE publications SET name = @name, publisher = @publisher WHERE id = @id;");
            return script.ToString();
        }
        private string DeletePublicationScript()
        {
            StringBuilder script = new StringBuilder();
            script.AppendLine("DELETE FROM publications WHERE id = @id;");
            return script.ToString();
        }

        public List<Publication> SelectPublications()
        {
            List<Publication> list = new List<Publication>();
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = SelectPublicationsScript();
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Publication()
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Publisher = reader.GetInt32(2)
                            });
                        }
                        reader.Close();
                    }
                }
            }
            return list;
        }
        public List<Publication> SelectPublications(int publisher)
        {
            List<Publication> list = new List<Publication>();
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = SelectPublisherPublicationsScript();
                    command.Parameters.AddWithValue("publisher", publisher);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Publication()
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Publisher = reader.GetInt32(2)
                            });
                        }
                        reader.Close();
                    }
                }
            }
            return list;
        }
        public Publication SelectPublication(int id)
        {
            Publication publication = null;
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = SelectPublicationScript();
                    command.Parameters.AddWithValue("id", id);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            publication = new Publication()
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Publisher = reader.GetInt32(2)
                            };
                        }
                        reader.Close();
                    }
                }
            }
            return publication;
        }
        public void CreatePublication(Publication publication)
        {
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = CreatePublicationScript();
                    command.Parameters.AddWithValue("name", publication.Name);
                    command.Parameters.AddWithValue("publisher", publication.Publisher);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            publication.Id = reader.GetInt32(0);
                        }
                        reader.Close();
                    }
                }
            }
        }
        public void UpdatePublication(Publication publication)
        {
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = UpdatePublicationScript();
                    command.Parameters.AddWithValue("id", publication.Id);
                    command.Parameters.AddWithValue("name", publication.Name);
                    command.Parameters.AddWithValue("publisher", publication.Publisher);
                    int affected = command.ExecuteNonQuery();
                    // TODO: if (affected == 0) raise error ?
                }
            }
        }
        public void DeletePublication(Publication publication)
        {
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = DeletePublicationScript();
                    command.Parameters.AddWithValue("id", publication.Id);
                    int affected = command.ExecuteNonQuery();
                }
            }
        }
        #endregion
    }
}