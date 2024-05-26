using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ClassLibrarayModel;
using DataAccessLayerDAL;

namespace ClassLibrarayDB
{
    public class StudentClass
    {
        public static void SaveStudentInformation(ModelStudent ms)
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("InsertStudent", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FirstName", ms.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", ms.LastName);
                    cmd.Parameters.AddWithValue("@DateOfBirth", ms.DateOfBirth);
                    cmd.Parameters.AddWithValue("@Gender", ms.Gender);
                    cmd.Parameters.AddWithValue("@Email", ms.Email);
                    cmd.Parameters.AddWithValue("@Phone", ms.Phone);
                    cmd.Parameters.AddWithValue("@Address", ms.Address);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<ModelStudent> GetAllStudents()
        {
            List<ModelStudent> students = new List<ModelStudent>();
            using (SqlConnection con = DBHelper.GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("infoofallstudent", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ModelStudent student = new ModelStudent
                            {
                                StudentID = Convert.ToInt32(reader["StudentID"]),
                                FirstName = Convert.ToString(reader["FirstName"]),
                                LastName = Convert.ToString(reader["LastName"]),
                                DateOfBirth = Convert.ToString(reader["DateOfBirth"]),
                                Gender = Convert.ToString(reader["Gender"]),
                                Email = Convert.ToString(reader["Email"]),
                                Phone = Convert.ToString(reader["Phone"]),
                                Address = Convert.ToString(reader["Address"])
                            };
                            students.Add(student);
                        }
                    }
                }
            }
            return students;
        }

        public static void DeleteStudent(int studentID)
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("infodeletestudent", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StudentID", studentID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // New method to get student by ID
        public static ModelStudent GetStudentByID(int studentID)
        {
            ModelStudent student = null;
            using (SqlConnection con = DBHelper.GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("GetStudentByID", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StudentID", studentID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            student = new ModelStudent
                            {
                                StudentID = Convert.ToInt32(reader["StudentID"]),
                                FirstName = Convert.ToString(reader["FirstName"]),
                                LastName = Convert.ToString(reader["LastName"]),
                                DateOfBirth = Convert.ToString(reader["DateOfBirth"]),
                                Gender = Convert.ToString(reader["Gender"]),
                                Email = Convert.ToString(reader["Email"]),
                                Phone = Convert.ToString(reader["Phone"]),
                                Address = Convert.ToString(reader["Address"])
                            };
                        }
                    }
                }
            }
            return student;
        }
    }
}
