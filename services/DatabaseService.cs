using Magna_TestApplication.Models;

namespace Magna_TestApplication.services
{
    public class DatabaseService
    {
        // Replace with your actual connection string
        private readonly string _connectionString = "Data Source=.;Initial Catalog=MagnaDB;Integrated Security=True";

        public List<PlcRegisterMap> GetPlcMappings()
        {
            var list = new List<PlcRegisterMap>();

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT Id, Category, ParameterName, RegisterAddress, ValueType, UiControlName FROM PlcRegisterMappings";

                using (var cmd = new SqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PlcRegisterMap
                        {
                            Id = (int)reader["Id"],
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