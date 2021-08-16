using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Text;

namespace DaJet.Benchmarks
{
    public sealed class DatabaseMessageProducer
    {
        private SqlCommand Command;
        private SqlConnection Connection;
        private readonly string ConnectionString;
        private const string InsertCommandText = "INSERT [_InfoRg58] ([_Fld59], [_Fld60], [_Fld65], [_Fld61], [_Fld62], [_Fld63], [_Fld64], [_Fld67], [_Fld66]) VALUES (@p1, CAST(@p2 AS binary(16)), @p3, @p4, @p5, @p6, @p7, @p8, @p9);";

        private const string InsertCommandTextStream = "INSERT [_InfoRg58] ([_Fld59], [_Fld60], [_Fld65], [_Fld61], [_Fld62], [_Fld63], [_Fld64], [_Fld67], [_Fld66]) VALUES (@p1, CAST(@p2 AS binary(16)), @p3, @p4, @p5, @p6, @p7, @p8, @p9);";

        public DatabaseMessageProducer(string connectionString)
        {
            ConnectionString = connectionString;
            InitializeCommand();
        }
        private SqlCommand InitializeCommand()
        {
            if (Connection == null)
            {
                Connection = new SqlConnection(ConnectionString);
                Connection.Open();
            }
            if (Command == null)
            {
                Command = Connection.CreateCommand();
                Command.Parameters.Add(new SqlParameter("p1", SqlDbType.BigInt));
                Command.Parameters.Add(new SqlParameter("p2", SqlDbType.UniqueIdentifier));
                Command.Parameters.Add(new SqlParameter("p3", SqlDbType.DateTime2));
                Command.Parameters.Add(new SqlParameter("p4", SqlDbType.NVarChar, 36));
                Command.Parameters.Add(new SqlParameter("p5", SqlDbType.NVarChar, 6));
                Command.Parameters.Add(new SqlParameter("p6", SqlDbType.NVarChar, 1024));
                Command.Parameters.Add(new SqlParameter("p7", SqlDbType.NVarChar, -1));
                Command.Parameters.Add(new SqlParameter("p8", SqlDbType.Int));
                Command.Parameters.Add(new SqlParameter("p9", SqlDbType.NVarChar, 1024));
            }
            return Command;
        }
        public bool InsertMessage(string messageBody)
        {
            Command.CommandType = CommandType.Text;
            Command.CommandText = InsertCommandText;
            Command.CommandTimeout = 10; // seconds

            Command.Parameters[0].Value = GetUtcTimeStamp(); // long (МоментВремени)
            Command.Parameters[1].Value = Guid.NewGuid(); // Guid (Идентификатор)
            Command.Parameters[2].Value = DateTime.Now.AddYears(2000); // DateTime (ДатаВремя)
            Command.Parameters[3].Value = "TEST"; // string (Отправитель)
            Command.Parameters[4].Value = "INSERT"; // string (ТипОперации)
            Command.Parameters[5].Value = "Справочник.Тест"; // string (ТипСообщения)
            Command.Parameters[6].Value = messageBody; // string (ТелоСообщения)
            Command.Parameters[7].Value = 0; // int (КоличествоОшибок)
            Command.Parameters[8].Value = string.Empty; // string (ОписаниеОшибки)

            int recordsAffected = Command.ExecuteNonQuery();

            Command.Parameters[6].Value = null;

            return (recordsAffected != 0);
        }
        public bool InsertMessage(byte[] message)
        {
            Command.CommandType = CommandType.Text;
            Command.CommandText = InsertCommandTextStream;
            Command.CommandTimeout = 10; // seconds

            Command.Parameters[0].Value = GetUtcTimeStamp(); // long (МоментВремени)
            Command.Parameters[1].Value = Guid.NewGuid(); // Guid (Идентификатор)
            Command.Parameters[2].Value = DateTime.Now.AddYears(2000); // DateTime (ДатаВремя)
            Command.Parameters[3].Value = "TEST"; // string (Отправитель)
            Command.Parameters[4].Value = "INSERT"; // string (ТипОперации)
            Command.Parameters[5].Value = "Справочник.Тест"; // string (ТипСообщения)
            Command.Parameters[6].SqlDbType = SqlDbType.VarBinary;
            Command.Parameters[6].Size = -1;
            Command.Parameters[6].Value = message; // string (ТелоСообщения)
            Command.Parameters[7].Value = 0; // int (КоличествоОшибок)
            Command.Parameters[8].Value = string.Empty; // string (ОписаниеОшибки)

            int recordsAffected = Command.ExecuteNonQuery();

            Command.Parameters[6].Value = null;

            return (recordsAffected != 0);
        }
        public bool InsertMessage(ReadOnlySpan<byte> message)
        {
            Command.CommandType = CommandType.Text;
            Command.CommandText = InsertCommandText;
            Command.CommandTimeout = 10; // seconds

            Command.Parameters[0].Value = GetUtcTimeStamp(); // long (МоментВремени)
            Command.Parameters[1].Value = Guid.NewGuid(); // Guid (Идентификатор)
            Command.Parameters[2].Value = DateTime.Now.AddYears(2000); // DateTime (ДатаВремя)
            Command.Parameters[3].Value = "TEST"; // string (Отправитель)
            Command.Parameters[4].Value = "INSERT"; // string (ТипОперации)
            Command.Parameters[5].Value = "Справочник.Тест"; // string (ТипСообщения)
            Command.Parameters[6].Value = Encoding.UTF8.GetString(message); // string (ТелоСообщения)
            Command.Parameters[7].Value = 0; // int (КоличествоОшибок)
            Command.Parameters[8].Value = string.Empty; // string (ОписаниеОшибки)
            
            int recordsAffected = Command.ExecuteNonQuery();

            Command.Parameters[6].Value = null;

            return (recordsAffected != 0);
        }
        private long GetUtcTimeStamp()
        {
            return DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;
        }
    }
}