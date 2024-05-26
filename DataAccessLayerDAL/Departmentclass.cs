using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ClassLibrarayModel;
using DataAccessLayerDAL;

namespace ClassLibrarayDB
{
    public class Departmentclass
    {
        public static void SaveDepartmentInformation(ModelDepartment md)
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("InsertDepartment", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DepartmentName", md.DepartmentName);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<ModelDepartment> GetDepartmentsInfo()
        {
            List<ModelDepartment> departments = new List<ModelDepartment>();

            using (SqlConnection con = DBHelper.GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("GetDepartmentsInfo", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ModelDepartment department = new ModelDepartment
                            {
                                DepartmentID = Convert.ToInt32(reader["DepartmentID"]),
                                DepartmentName = Convert.ToString(reader["DepartmentName"]),
                            };
                            departments.Add(department);
                        }
                    }
                }
            }

            return departments;
        }
    }
}