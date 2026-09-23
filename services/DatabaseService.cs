using Magna_TestApplication.Models;
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

        // Inside DatabaseService.cs
        public void SaveTestLog(TestLog log)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                // 1. Get all public properties of TestLog (except Id and helper properties)
                var props = typeof(TestLog)
                    .GetProperties()
                    .Where(p => p.CanRead && p.CanWrite)         // Only settable properties
                    .Where(p => p.Name != "Id")                  // Skip auto-increment
                    .Where(p => p.Name != "Date" && p.Name != "Time") // Skip computed props
                    .ToList();

                // 2. Build the column list and the @parameter list
                string columns = string.Join(", ", props.Select(p => p.Name));
                string parameters = string.Join(", ", props.Select(p => "@" + p.Name));

                string query = $"INSERT INTO TestLogs ({columns}) VALUES ({parameters})";

                using (var cmd = new SqlCommand(query, conn))
                {
                    // 3. Add a parameter for each property
                    foreach (var prop in props)
                    {
                        object value = prop.GetValue(log) ?? DBNull.Value;
                        cmd.Parameters.AddWithValue("@" + prop.Name, value);
                    }

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public List<TestLog> GetTestLogs(DateTime fromDate, DateTime toDate)
        {
            var list = new List<TestLog>();

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string query = @"
            SELECT * FROM TestLogs
            WHERE LoggedAt >= @From AND LoggedAt <= @To
            ORDER BY LoggedAt DESC";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@From", fromDate);
                    cmd.Parameters.AddWithValue("@To", toDate);

                    using (var reader = cmd.ExecuteReader())
                    {
                        // Get column ordinal positions once (fast)
                        var cols = new Dictionary<string, int>();
                        for (int i = 0; i < reader.FieldCount; i++)
                            cols[reader.GetName(i)] = i;

                        while (reader.Read())
                        {
                            var log = new TestLog();

                            // Use reflection to fill every property from its matching column
                            foreach (var prop in typeof(TestLog).GetProperties())
                            {
                                if (!prop.CanWrite) continue;
                                if (!cols.ContainsKey(prop.Name)) continue;

                                object value = reader.GetValue(cols[prop.Name]);

                                if (value == DBNull.Value)
                                {
                                    // Leave default (0 for double, "" for string)
                                    continue;
                                }

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
                                catch
                                {
                                    // Ignore individual column conversion errors
                                }
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
                    SELECT Id, Category, ParameterName, RegisterAddress, ValueType, UiControlName
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
                            UiControlName = reader["UiControlName"].ToString()
                        });
                    }
                }
            }

            return list;
        }
    }
}