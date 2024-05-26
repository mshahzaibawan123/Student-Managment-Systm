using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ClassLibrarayModel;
using DataAccessLayerDAL;

namespace DataAccessLayer
{
    public class CourseDataAccess
    {
        public static async Task<List<ModelCourses>> ViewAllCourses()
        {
            List<ModelCourses> courses = new List<ModelCourses>();
            using (SqlConnection con = DBHelper.GetConnection())
            {
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("ViewAllCourses", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            courses.Add(new ModelCourses
                            {
                                CourseID = (int)reader["CourseID"],
                                CourseName = reader["CourseName"].ToString(),
                                CourseDescription = reader["CourseDescription"].ToString(),
                                Credits = (int)reader["Credits"]
                            });
                        }
                    }
                }
            }
            return courses;
        }

            public static async Task AddCourse(ModelCourses course)
        {
                                
            using (SqlConnection con = DBHelper.GetConnection())
            {
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("AddCourse", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CourseName", course.CourseName);
                    cmd.Parameters.AddWithValue("@CourseDescription", course.CourseDescription);
                    cmd.Parameters.AddWithValue("@Credits", course.Credits);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public static async Task UpdateCourse(ModelCourses course)
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("UpdateCourse", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CourseID", course.CourseID);
                    cmd.Parameters.AddWithValue("@CourseName", course.CourseName);
                    cmd.Parameters.AddWithValue("@CourseDescription", course.CourseDescription);
                    cmd.Parameters.AddWithValue("@Credits", course.Credits);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public static async Task DeleteCourse(int courseId)
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("DeleteCourse", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CourseID", courseId);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }


    }
}
