using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ClassLibrarayModel;
using DataAccessLayerDAL;



namespace ClassLibrarayDB
{
    public class teacherClass
    {
        public static async Task SaveTeacherInformation(ModelTeacher mt)
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("InsertTeacher", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FirstName", mt.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", mt.LastName);
                    cmd.Parameters.AddWithValue("@Email", mt.Email);
                    cmd.Parameters.AddWithValue("@Phone", mt.Phone);
                    cmd.Parameters.AddWithValue("@DepartmentID", mt.DepartmentID);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }


        public static List<ModelTeacher> GetAllTeachers()
        {
            List<ModelTeacher> teachers = new List<ModelTeacher>();
            
                using (SqlConnection con = DBHelper.GetConnection())
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("infoofallteacher", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ModelTeacher teacher = new ModelTeacher
                                {
                                    TeacherID = Convert.ToInt32(reader["TeacherID"]),
                                    FirstName = Convert.ToString(reader["FirstName"]),
                                    LastName = Convert.ToString(reader["LastName"]),
                                    Email = Convert.ToString(reader["Email"]),
                                    Phone = Convert.ToString(reader["Phone"]),
                                    DepartmentID = Convert.ToInt32(reader["DepartmentID"])
                                };
                                teachers.Add(teacher);
                            }
                        }
                    }
                }
            
           
            return teachers;
        }

        public static void DeleteTeacher(int teacherID)
        {
            
                using (SqlConnection con = DBHelper.GetConnection())
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("infodeleteteacher", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TeacherID", teacherID);
                        cmd.ExecuteNonQuery();
                    }
                }
       
        }
    }
}