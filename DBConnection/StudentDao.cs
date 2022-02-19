using StudentWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace StudentWebApi.DBConnection
{
    public class StudentDao
    {
        SqlConnection conn;
        private void open() {
            conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StudentDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            conn.Open();
        }
        public bool Insert(Student s)
        {
            open();
            SqlCommand cmdI = new SqlCommand();

            cmdI.Connection = conn;
            cmdI.CommandType = System.Data.CommandType.StoredProcedure;
            cmdI.CommandText = "InsertStudent";

            cmdI.Parameters.AddWithValue("@Name",s.Name);
            cmdI.Parameters.AddWithValue("@Marks",s.Marks);
            cmdI.Parameters.AddWithValue("@Password",s.Password);

            try
            {
                cmdI.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }
           
            return true;
        }

        public List<Student> GetAllStudents()
        {
            open();
            SqlCommand cmdSelct = new SqlCommand();
            cmdSelct.Connection = conn;
            cmdSelct.CommandType = System.Data.CommandType.StoredProcedure;
            cmdSelct.CommandText = "SelectAllStudents";

            SqlDataReader dr = null;
            List<Student> ls = new List<Student>();
            dr = cmdSelct.ExecuteReader();

            while (dr.Read())
            {
                ls.Add(new Student
                {
                    RollNo = (int)dr["RollNo"],
                    Name = (string)dr["Name"],
                    Marks = (double)dr["Marks"],
                    Password = (string)dr["Password"]
                });
                /*                Student s = new Student();
                                s.Name = (string) dr["Name"];
                                s.RollNo = (int)dr["RollNo"];
                                s.Marks = (double)dr["Marks"];
                                s.Password = (string)dr["Password"];
                                ls.Add(s);*/
            }

            conn.Close();
            return ls;
        }

        public Student GetStudentById(int id)
        {
            open();
            SqlCommand cmdSelectId = new SqlCommand();
            cmdSelectId.Connection = conn;
            cmdSelectId.CommandType = System.Data.CommandType.StoredProcedure;
            cmdSelectId.CommandText = "SelectStudentById";
            cmdSelectId.Parameters.AddWithValue("@Id", id);

            SqlDataReader dr = cmdSelectId.ExecuteReader();

            Student s = null;
            while (dr.Read())
            {
                s = new Student 
                {
                    RollNo = (int)dr["RollNo"],
                    Name = (string)dr["Name"],
                    Marks = (double)dr["Marks"],
                    Password = (string)dr["Password"]
                };
            }

            conn.Close();

            return s;

        }

        public bool UpdateData(Student s)
        {
            open();

            SqlCommand cmdUpdate = new SqlCommand();
            cmdUpdate.Connection = conn;
            cmdUpdate.CommandType = System.Data.CommandType.StoredProcedure;
            cmdUpdate.CommandText = "UpdateStudent";


            cmdUpdate.Parameters.AddWithValue("@RollNo", s.RollNo);
            cmdUpdate.Parameters.AddWithValue("@Name", s.Name);
            cmdUpdate.Parameters.AddWithValue("@Marks", s.Marks);
            cmdUpdate.Parameters.AddWithValue("@Password", s.Password);


            cmdUpdate.ExecuteNonQuery();
            conn.Close();

            return true;

        }


        public bool DeleteStudent(int id)
        {
            open();
            SqlCommand cmdDelete = new SqlCommand();
            cmdDelete.Connection = conn;
            cmdDelete.CommandType = System.Data.CommandType.StoredProcedure;
            cmdDelete.CommandText = "DeleteStudentById";
            cmdDelete.Parameters.AddWithValue("@Id", id);
            cmdDelete.ExecuteNonQuery();

            conn.Close();
            return true;

        }
    }

}