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
                string query = @"
            INSERT INTO TestLogs (
                LoggedAt, Shift, Variant, SerialNumber, Result,
                SealLoad_Min, SealLoad_Max, SealLoad_Actual,
                PowerLockCurrent_Min, PowerLockCurrent_Max, PowerLockCurrent_Actual,
                InsideLockEffort_Min, InsideLockEffort_Max, InsideLockEffort_Actual
                -- Add all other columns here
            ) VALUES (
                @LoggedAt, @Shift, @Variant, @SerialNumber, @Result,
                @SealLoad_Min, @SealLoad_Max, @SealLoad_Actual,
                @PowerLockCurrent_Min, @PowerLockCurrent_Max, @PowerLockCurrent_Actual,
                @InsideLockEffort_Min, @InsideLockEffort_Max, @InsideLockEffort_Actual
                -- Add all other parameters here
            )";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LoggedAt", log.LoggedAt);
                    cmd.Parameters.AddWithValue("@Shift", log.Shift ?? "");
                    cmd.Parameters.AddWithValue("@Variant", log.Variant ?? "");
                    cmd.Parameters.AddWithValue("@SerialNumber", log.SerialNumber ?? "");
                    cmd.Parameters.AddWithValue("@Result", log.Result ?? "");

                    // Add all other parameters...
                    cmd.Parameters.AddWithValue("@SealLoad_Min", log.SealLoad_Min);
                    // ... etc

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
                        while (reader.Read())
                        {
                            list.Add(new TestLog
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                LoggedAt = Convert.ToDateTime(reader["LoggedAt"]),
                                Shift = reader["Shift"].ToString(),
                                Variant = reader["Variant"].ToString(),
                                Result = reader["Result"].ToString(),
                                SealLoad_Actual = reader["SealLoad_Actual"] == DBNull.Value ? 0 : Convert.ToDouble(reader["SealLoad_Actual"])
                                // Map other fields...
                            });
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