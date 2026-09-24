using System.Data.SqlClient;

namespace Magna_TestApplication.services
{
    public class DatabaseService
    {
        private readonly string _connectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;
              Initial Catalog=MagnaDB;
              Integrated Security=True;
              Encrypt=False;
              TrustServerCertificate=True;";

        // CHANGED: No longer shows MessageBox. Returns success and message.
        public (bool Success, string Message) TestDatabaseConnection()
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    return (true, $"DB Connected: {conn.Database}");
                }
            }
            catch (Exception ex)
            {
                return (false, "DB Error: " + ex.Message);
            }
        }
        // ============================================================
        // GENERIC SAVE — works for any log type
        // ============================================================
        public void SaveLog<T>(T log, string tableName)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                var props = typeof(T)
                    .GetProperties()
                    .Where(p => p.CanRead && p.CanWrite)
                    .Where(p => p.Name != "Id")
                    .Where(p => p.Name != "Date" && p.Name != "Time")
                    .ToList();

                string columns = string.Join(", ", props.Select(p => p.Name));
                string parameters = string.Join(", ", props.Select(p => "@" + p.Name));
                string query = $"INSERT INTO {tableName} ({columns}) VALUES ({parameters})";

                using (var cmd = new SqlCommand(query, conn))
                {
                    foreach (var prop in props)
                    {
                        object value = prop.GetValue(log) ?? DBNull.Value;
                        cmd.Parameters.AddWithValue("@" + prop.Name, value);
                    }
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ============================================================
        // GENERIC LOAD — works for any log type
        // ============================================================
        public List<T> GetLogs<T>(DateTime fromDate, DateTime toDate, string tableName) where T : new()
        {
            var list = new List<T>();

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string query = $@"
            SELECT * FROM {tableName}
            WHERE LoggedAt >= @From AND LoggedAt <= @To
            ORDER BY LoggedAt DESC";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@From", fromDate);
                    cmd.Parameters.AddWithValue("@To", toDate);

                    using (var reader = cmd.ExecuteReader())
                    {
                        var cols = new Dictionary<string, int>();
                        for (int i = 0; i < reader.FieldCount; i++)
                            cols[reader.GetName(i)] = i;

                        while (reader.Read())
                        {
                            var log = new T();

                            foreach (var prop in typeof(T).GetProperties())
                            {
                                if (!prop.CanWrite) continue;
                                if (!cols.ContainsKey(prop.Name)) continue;

                                object value = reader.GetValue(cols[prop.Name]);
                                if (value == DBNull.Value) continue;

                                try
                                {
                                    if (prop.PropertyType == typeof(double))
                                        prop.SetValue(log, Convert.ToDouble(value));
                                    else if (prop.PropertyType == typeof(int))
                                        prop.SetValue(log, Convert.ToInt32(value));
                                    else if (prop.PropertyType == typeof(DateTime))
                                        prop.SetValue(log, Convert.ToDateTime(value));
                                    else if (prop.PropertyType == typeof(string))
                                        prop.SetValue(log, value.ToString());
                                }
                                catch { /* skip bad conversion */ }
                            }

                            list.Add(log);
                        }
                    }
                }
            }

            return list;
        }

        public List<PlcRegisterMap> GetPlcMappings()
        {
            var list = new List<PlcRegisterMap>();

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string query = @"
            SELECT Id, Category, ParameterName, RegisterAddress, 
                   ValueType, UiControlName, LogPropertyName, ShowInReport
            FROM dbo.PlcRegisterMappings
            ORDER BY Id";

                using (var cmd = new SqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PlcRegisterMap
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Category = reader["Category"].ToString(),
                            ParameterName = reader["ParameterName"].ToString(),
                            RegisterAddress = reader["RegisterAddress"].ToString(),
                            ValueType = reader["ValueType"].ToString(),
                            UiControlName = reader["UiControlName"].ToString(),
                            LogPropertyName = reader["LogPropertyName"] == DBNull.Value
                                ? null
                                : reader["LogPropertyName"].ToString(),
                            ShowInReport = reader["ShowInReport"] != DBNull.Value
                                && Convert.ToBoolean(reader["ShowInReport"])
                        });
                    }
                }
            }

            return list;
        }
    }
}