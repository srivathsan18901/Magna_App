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