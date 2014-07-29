using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace MyPlanner
{

    class SQLManager
    {
        string strConn = "Data Source=D306;Initial Catalog=MyTestDB;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;";

        public DataSet SortResult(int method)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();
                string sqlCmd = null;
                DataSet ds = null;
                switch (method)
                {
                    case 0: //날짜 내림차순
                        sqlCmd = "SELECT Id,Date,Subject,Important FROM ToDoList ORDER BY DATE DESC";
                        break;
                    case 1: //날짜 오름차순
                        sqlCmd = "SELECT Id,Date,Subject,Important FROM ToDoList ORDER BY DATE ASC";
                        break;
                    case 2: // 중요도 내림차순
                        sqlCmd = "SELECT Id,Date,Subject,Important FROM ToDoList ORDER BY Important ASC";
                        break;
                    case 3: //중요도 오름차순
                        sqlCmd = "SELECT Id,Date,Subject,Important FROM ToDoList ORDER BY Important DESC";
                        break;

                }
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd, conn);
                ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                return ds;
            }
        }

        public bool InsertQuery(string subject, string plan, DateTime date, string important)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO ToDoList VALUES (@plan, @date,@important,@subject);";
                SqlParameter paramPlan = new SqlParameter("@plan", SqlDbType.Text);
                SqlParameter paramDate = new SqlParameter("@date", SqlDbType.DateTime);
                SqlParameter paramImportant = new SqlParameter("@important", SqlDbType.NVarChar, 2);
                SqlParameter paramSubject = new SqlParameter("@subject", SqlDbType.NVarChar, 30);
                paramPlan.Value = plan;
                paramDate.Value = date;
                paramImportant.Value = important;
                paramSubject.Value = subject;
                cmd.Parameters.Add(paramPlan);
                cmd.Parameters.Add(paramDate);
                cmd.Parameters.Add(paramImportant);
                cmd.Parameters.Add(paramSubject);
                if (cmd.ExecuteNonQuery() != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public string getPlan(int id)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM ToDoList WHERE Id=@id";
                SqlParameter paramId = new SqlParameter("@id", SqlDbType.Int);
                paramId.Value = id;
                cmd.Parameters.Add(paramId);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    string s = rdr["plan"].ToString();
                    rdr.Close();
                    conn.Close();
                    return s;
                }
                else
                {
                    rdr.Close();
                    conn.Close();
                    return null;
                }


            }
        }

        public bool deletePlan(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConn))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM ToDoList WHERE Id=@id";
                    SqlParameter paramId = new SqlParameter("@id", SqlDbType.Int);
                    paramId.Value = id;
                    cmd.Parameters.Add(paramId);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
