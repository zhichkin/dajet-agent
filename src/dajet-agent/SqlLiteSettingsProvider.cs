//using Microsoft.Data.Sqlite;
using System;
using System.IO;

namespace DaJet.Sqlite
{
    //public sealed class SqlLiteSettingsProvider
    //{
    //    private const string DATABASE_FILENAME = "dajet-exchange.db";

    //    private const string CREATE_SETTINGS_TABLE_SCRIPT = "CREATE TABLE IF NOT EXISTS settings (name TEXT PRIMARY KEY, value TEXT NOT NULL);";
    //    private const string INSERT_SETTING_SCRIPT = "INSERT INTO settings (name, value) VALUES (@name, @value);";
    //    private const string SELECT_SETTING_SCRIPT = "SELECT value FROM settings WHERE name = @name;";
    //    private const string UPDATE_SETTING_SCRIPT = "UPDATE settings SET value = @value WHERE name = @name;";

    //    private const string LAST_UPDATED_SETTING_NAME = "LastUpdated";

    //    public string CatalogPath { get; private set; }

    //    public void UseCatalogPath(string catalogPath)
    //    {
    //        CatalogPath = catalogPath;
    //        InitializeDatabase();
    //    }
    //    private void InitializeDatabase()
    //    {
    //        using (SqliteConnection connection = new SqliteConnection(ConnectionString))
    //        {
    //            connection.Open();
    //            using (SqliteCommand command = connection.CreateCommand())
    //            {
    //                command.CommandText = CREATE_SETTINGS_TABLE_SCRIPT;
    //                _ = command.ExecuteNonQuery();
    //            }
    //        }

    //        if (!SettingExists(LAST_UPDATED_SETTING_NAME))
    //        {
    //            DateTime current = DateTime.Now;
    //            DateTime initValue = new DateTime(current.Year, current.Month, current.Day, 0, 0, 0, DateTimeKind.Utc);
    //            CreateSetting(LAST_UPDATED_SETTING_NAME, initValue.ToString("yyyy-MM-ddTHH:mm:ss"));
    //        }
    //    }
        
    //    private string DatabaseFilePath
    //    {
    //        get
    //        {
    //            return Path.Combine(CatalogPath, DATABASE_FILENAME);
    //        }
    //    }
    //    private string ConnectionString
    //    {
    //        get
    //        {
    //            return new SqliteConnectionStringBuilder()
    //            {
    //                DataSource = DatabaseFilePath,
    //                Mode = SqliteOpenMode.ReadWriteCreate
    //            }
    //            .ToString();
    //        }
    //    }
        
    //    public bool SettingExists(string name)
    //    {
    //        bool exists = false;

    //        using (SqliteConnection connection = new SqliteConnection(ConnectionString))
    //        {
    //            connection.Open();
    //            using (SqliteCommand command = connection.CreateCommand())
    //            {
    //                command.CommandText = SELECT_SETTING_SCRIPT;
    //                command.Parameters.AddWithValue("name", name);

    //                using (SqliteDataReader reader = command.ExecuteReader())
    //                {
    //                    if (reader.Read())
    //                    {
    //                        exists = true;
    //                    }
    //                    reader.Close();
    //                }
    //            }
    //        }

    //        return exists;
    //    }
    //    public void CreateSetting(string name, string value)
    //    {
    //        using (SqliteConnection connection = new SqliteConnection(ConnectionString))
    //        {
    //            connection.Open();
    //            using (SqliteCommand command = connection.CreateCommand())
    //            {
    //                command.CommandText = INSERT_SETTING_SCRIPT;
    //                command.Parameters.AddWithValue("name", name);
    //                command.Parameters.AddWithValue("value", value);
    //                int affected = command.ExecuteNonQuery();
    //            }
    //        }
    //    }
    //    public T GetSetting<T>(string name)
    //    {
    //        object setting = default(T);

    //        using (SqliteConnection connection = new SqliteConnection(ConnectionString))
    //        {
    //            connection.Open();
    //            using (SqliteCommand command = connection.CreateCommand())
    //            {
    //                command.CommandText = SELECT_SETTING_SCRIPT;
    //                command.Parameters.AddWithValue("name", name);

    //                using (SqliteDataReader reader = command.ExecuteReader())
    //                {
    //                    if (reader.Read())
    //                    {
    //                        string value = reader.GetString(0);

    //                        if (typeof(T) == typeof(int))
    //                        {
    //                            setting = int.Parse(value);
    //                        }
    //                        else if (typeof(T) == typeof(bool))
    //                        {
    //                            setting = bool.Parse(value);
    //                        }
    //                        else if (typeof(T) == typeof(DateTime))
    //                        {
    //                            setting = DateTime.Parse(value);
    //                        }
    //                    }
    //                    reader.Close();
    //                }
    //            }
    //        }

    //        return (T)setting;
    //    }
    //    public void SetSetting(string name, object value)
    //    {
    //        if (value == null) throw new ArgumentNullException(nameof(value));

    //        string setting;

    //        Type type = value.GetType();

    //        if (type == typeof(int))
    //        {
    //            setting = ((int)value).ToString();
    //        }
    //        else if (type == typeof(bool))
    //        {
    //            setting = ((bool)value).ToString();
    //        }
    //        else if (type == typeof(string))
    //        {
    //            setting = (string)value;
    //        }
    //        else if (type == typeof(DateTime))
    //        {
    //            setting = ((DateTime)value).ToString("yyyy-MM-ddTHH:mm:ss");
    //        }
    //        else
    //        {
    //            setting = value.ToString();
    //        }

    //        using (SqliteConnection connection = new SqliteConnection(ConnectionString))
    //        {
    //            connection.Open();
    //            using (SqliteCommand command = connection.CreateCommand())
    //            {
    //                command.CommandText = UPDATE_SETTING_SCRIPT;
    //                command.Parameters.AddWithValue("name", name);
    //                command.Parameters.AddWithValue("value", setting);
    //                int affected = command.ExecuteNonQuery();
    //            }
    //        }
    //    }
    //}
}