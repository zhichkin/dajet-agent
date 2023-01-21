using DaJet.Agent.Service.Security;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DaJet.Agent.Service
{
    public sealed class SqliteAppSettingsProvider
    {
        private const string DEFAULT_USER_NAME = "admin";
        private const string DEFAULT_USER_PSWD = "admin";
        private const string DATABASE_FILE_NAME = "dajet-agent.db";
        
        #region "SQL SCRIPTS"

        private const string TABLE_EXISTS_SCRIPT = "SELECT 1 FROM sqlite_master WHERE type = 'table' AND name = @table_name;";

        private const string CREATE_USERS_TABLE = "CREATE TABLE IF NOT EXISTS " +
            "users (name TEXT NOT NULL, pswd TEXT NOT NULL, PRIMARY KEY (name)) WITHOUT ROWID;";
        private const string EXISTS_USER = "SELECT 1 FROM users WHERE name = @name;";
        private const string SELECT_USER = "SELECT 1 FROM users WHERE name = @name AND pswd = @hash;";
        private const string INSERT_USER = "INSERT INTO users (name, pswd) VALUES (@name, @hash);";
        private const string UPDATE_USER = "UPDATE users SET pswd = @pswd WHERE name = @name AND pswd = @hash;";
        private const string DELETE_USER = "DELETE FROM users WHERE name = @name;";

        private const string CREATE_SETTINGS_TABLE = "CREATE TABLE IF NOT EXISTS " +
            "settings (key TEXT NOT NULL, value TEXT NOT NULL, PRIMARY KEY (key)) WITHOUT ROWID;";
        private const string SELECT_ALL_SETTINGS = "SELECT key, value FROM settings ORDER BY key ASC;";
        private const string EXISTS_SETTING_VALUE = "SELECT 1 FROM settings WHERE key = @key;";
        private const string SELECT_SETTING_VALUE = "SELECT value FROM settings WHERE key = @key;";
        private const string INSERT_SETTING_VALUE = "INSERT INTO settings (key, value) VALUES (@key, @value);";
        private const string UPDATE_SETTING_VALUE = "UPDATE settings SET value = @value WHERE key = @key;";
        private const string DELETE_SETTING_VALUE = "DELETE FROM settings WHERE key = @key;";

        #endregion

        private readonly AppSettings _settings;
        private readonly string _connectionString;
        private readonly IDataProtector _protector;
        public SqliteAppSettingsProvider(IDataProtectionProvider protector, IOptions<AppSettings> settings)
        {
            _settings = settings.Value;
            _protector = protector.CreateProtector(DEFAULT_USER_NAME);

            string databaseFileFullPath = Path.Combine(_settings.AppCatalog, DATABASE_FILE_NAME);

            _connectionString = new SqliteConnectionStringBuilder()
            {
                DataSource = databaseFileFullPath,
                Mode = SqliteOpenMode.ReadWriteCreate
            }
            .ToString();
        }
        public void Configure()
        {
            InitializeDatabase();
        }
        private void InitializeDatabase()
        {
            if (!TableExists("users"))
            {
                CreateUsersTable();
            }

            if (!UserExists(DEFAULT_USER_NAME))
            {
                CreateDefaultUser();
            }
        }
        public bool TableExists(string name)
        {
            using (SqliteConnection connection = new(_connectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = TABLE_EXISTS_SCRIPT;
                    command.Parameters.AddWithValue("table_name", name);

                    object result = command.ExecuteScalar();

                    if (result == null)
                    {
                        return false;
                    }
                    else
                    {
                        return ((long)command.ExecuteScalar() == 1L);
                    }
                }
            }
        }
        private void CreateUsersTable()
        {
            using (SqliteConnection connection = new(_connectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = CREATE_USERS_TABLE;

                    _ = command.ExecuteNonQuery();
                }
            }
        }
        private void CreateDefaultUser()
        {
            _ = TryInsertUser(new AppUser()
            {
                Name = DEFAULT_USER_NAME,
                Pswd = DEFAULT_USER_PSWD
            });
        }

        public bool UserExists(in string name)
        {
            using (SqliteConnection connection = new(_connectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = EXISTS_USER;
                    command.Parameters.AddWithValue("name", name);

                    object result = command.ExecuteScalar();

                    if (result == null)
                    {
                        return false;
                    }
                    else
                    {
                        return ((long)command.ExecuteScalar() == 1L);
                    }
                }
            }
        }
        public bool TrySelectUser(in AppUser user)
        {
            byte[] hash = SHA256.HashData(Encoding.UTF8.GetBytes(user.Pswd));
            string test = Convert.ToHexString(hash);

            using (SqliteConnection connection = new(_connectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = SELECT_USER;
                    command.Parameters.AddWithValue("name", user.Name);
                    command.Parameters.AddWithValue("hash", test);

                    object result = command.ExecuteScalar();

                    if (result == null)
                    {
                        return false;
                    }
                    else
                    {
                        return ((long)command.ExecuteScalar() == 1L);
                    }
                }
            }
        }
        public bool TryInsertUser(in AppUser user)
        {
            int result;

            byte[] hash = SHA256.HashData(Encoding.UTF8.GetBytes(user.Pswd));
            string pswd = Convert.ToHexString(hash);

            using (SqliteConnection connection = new(_connectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = INSERT_USER;
                    command.Parameters.AddWithValue("name", user.Name);
                    command.Parameters.AddWithValue("hash", pswd);

                    result = command.ExecuteNonQuery();
                }
            }

            return (result == 1);
        }
        public bool TryUpdateUser(in AppUser user, in string password)
        {
            int result;

            byte[] hash = SHA256.HashData(Encoding.UTF8.GetBytes(user.Pswd));
            string test = Convert.ToHexString(hash);

            hash = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            string pswd = Convert.ToHexString(hash);

            using (SqliteConnection connection = new(_connectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = UPDATE_USER;

                    command.Parameters.AddWithValue("name", user.Name);
                    command.Parameters.AddWithValue("hash", test);
                    command.Parameters.AddWithValue("pswd", pswd);

                    result = command.ExecuteNonQuery();

                    if (result == 1)
                    {
                        user.Pswd = string.Empty;
                    }
                }
            }

            return (result == 1);
        }
        public void DeleteUser(in string name)
        {
            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = DELETE_USER;
                    command.Parameters.AddWithValue("name", name);
                    _ = command.ExecuteNonQuery();
                }
            }
        }

        public void CreateAppSettingsTable()
        {
            using (SqliteConnection connection = new(_connectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = CREATE_SETTINGS_TABLE;
                    
                    _ = command.ExecuteNonQuery();
                }
            }
        }
        public Dictionary<string, string> SelectAppSettings()
        {
            Dictionary<string, string> settings = new Dictionary<string, string>();

            using (SqliteConnection connection = new(_connectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = SELECT_ALL_SETTINGS;

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            settings.Add(reader.GetString(0), reader.GetString(1));
                        }
                        reader.Close();
                    }
                }
            }

            return settings;
        }
        public bool AppSettingExists(in string key)
        {
            using (SqliteConnection connection = new(_connectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = EXISTS_SETTING_VALUE;
                    command.Parameters.AddWithValue("key", key);

                    object result = command.ExecuteScalar();

                    if (result == null)
                    {
                        return false;
                    }
                    else
                    {
                        return ((long)command.ExecuteScalar() == 1L);
                    }
                }
            }
        }
        public bool TrySelectAppSetting(in string key, out string value, bool decrypt = false)
        {
            value = null;

            using (SqliteConnection connection = new(_connectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = SELECT_SETTING_VALUE;
                    command.Parameters.AddWithValue("key", key);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            value = reader.GetString(0);

                            if (decrypt)
                            {
                                value = _protector.Unprotect(value);
                            }
                        }
                        reader.Close();
                    }
                }
            }

            return (value != null);
        }
        public bool TryInsertAppSetting(in string key, in string value, bool encrypt = false)
        {
            int result;

            using (SqliteConnection connection = new(_connectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = INSERT_SETTING_VALUE;
                    command.Parameters.AddWithValue("key", key);
                    if (encrypt)
                    {
                        command.Parameters.AddWithValue("value", _protector.Protect(value));
                    }
                    else
                    {
                        command.Parameters.AddWithValue("value", value);
                    }
                    result = command.ExecuteNonQuery();
                }
            }

            return (result == 1);
        }
        public bool TryUpdateAppSetting(in string key, in string value, bool encrypt = false)
        {
            int result;

            using (SqliteConnection connection = new(_connectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = UPDATE_SETTING_VALUE;
                    command.Parameters.AddWithValue("key", key);
                    if (encrypt)
                    {
                        command.Parameters.AddWithValue("value", _protector.Protect(value));
                    }
                    else
                    {
                        command.Parameters.AddWithValue("value", value);
                    }
                    result = command.ExecuteNonQuery();
                }
            }

            return (result == 1);
        }
        public void DeleteAppSetting(in string key)
        {
            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = DELETE_SETTING_VALUE;
                    command.Parameters.AddWithValue("key", key);
                    _ = command.ExecuteNonQuery();
                }
            }
        }
    }
}