using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DataAccessLayerDAL;

namespace DataAccessLayer
{
    public class Loginclass
    {
        private readonly string _connectionString;

        public Loginclass(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<bool> ValidateCredentials(string email, string password)
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("ValidateCredentials", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);
                    SqlParameter outputParam = new SqlParameter("@IsValid", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParam);

                    await cmd.ExecuteNonQueryAsync();

                    return (bool)outputParam.Value;
                }
            }
        }
    }
}
