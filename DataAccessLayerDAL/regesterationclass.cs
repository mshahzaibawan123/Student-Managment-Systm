using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ClassLibrarayModel;
using DataAccessLayerDAL;

namespace DataAccessLayer
{
    public class Registrationclass
    {
        public static async Task SaveRegistrationInformation(ModelRegistration mr)
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("AddRegistration", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FirstName", mr.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", mr.LastName);
                    cmd.Parameters.AddWithValue("@Email", mr.Email);
                    cmd.Parameters.AddWithValue("@Username", mr.Username);
                    cmd.Parameters.AddWithValue("@Password", mr.Password);
                    cmd.Parameters.AddWithValue("@UserType", mr.UserType);
                    cmd.Parameters.AddWithValue("@RegistrationDate", mr.RegistrationDate);

                    await cmd.ExecuteNonQueryAsync();

                }
            }
        }

        public static List<ModelRegistration> GetAllRegistrations()
        {
            List<ModelRegistration> registrations = new List<ModelRegistration>();
            using (SqlConnection con = DBHelper.GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("GetAllRegistrations", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ModelRegistration registration = new ModelRegistration
                            {
                                RegistrationID = Convert.ToInt32(reader["RegistrationID"]),
                                FirstName = Convert.ToString(reader["FirstName"]),
                                LastName = Convert.ToString(reader["LastName"]),
                                Email = Convert.ToString(reader["Email"]),
                                Username = Convert.ToString(reader["Username"]),
                                Password = Convert.ToString(reader["Password"]),
                                UserType = Convert.ToString(reader["UserType"]),
                                RegistrationDate = Convert.ToDateTime(reader["RegistrationDate"])
                            };
                            registrations.Add(registration);

                        }
                    }
                }
            }
            return registrations;
        }
    }
}