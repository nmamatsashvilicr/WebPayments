using System.Data;
using System.Data.SqlClient;

namespace WebPaymentsApp
{
    public class DB
    {
        private static string SqlString = Shared.getConf("ConnectionStrings.DatabaseConnection");
        public static int Run(string query, Dictionary<string, string> Params)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(SqlString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = query;

                        foreach (var item in Params.Keys)
                            cmd.Parameters.AddWithValue($"@{item}", Params[item]);

                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = connection;
                        connection.Open();
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
