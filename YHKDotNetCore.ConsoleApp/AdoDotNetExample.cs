using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace YHKDotNetCore.ConsoleApp
{
    internal class AdoDotNetExample
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "DESKTOP-RIV6QC5",
            InitialCatalog = "DotNetTraining",
            UserID = "sa",
            Password = "password"
        };
        public void Read()
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);

            connection.Open();
            Console.WriteLine("Connection Open");

            string query = "SELECT * FROM Tbl_Blog";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();
            Console.WriteLine("Connection Close");

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine("BlogId => " + dr["BlogId"]);
                Console.WriteLine("BlogTitle => " + dr["BlogTitle"]);
                Console.WriteLine("BlogAuthor => " + dr["BlogAuthor"]);
                Console.WriteLine("BlogContent => " + dr["BlogContent"]);
            }
        }

        public void ReadSingle(int id)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);

            connection.Open();

            string query = "SELECT * FROM Tbl_Blog WHERE BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            DataRow dr = dt.Rows[0];
        
            Console.WriteLine("BlogId => " + dr["BlogId"]);
            Console.WriteLine("BlogTitle => " + dr["BlogTitle"]);
            Console.WriteLine("BlogAuthor => " + dr["BlogAuthor"]);
            Console.WriteLine("BlogContent => " + dr["BlogContent"]);
        }

        public void Create(string title, string author, string content)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);

            connection.Open();

            string query = @"
            INSERT INTO [dbo].[Tbl_Blog]
                   ([BlogTitle]
                   ,[BlogAuthor]
                   ,[BlogContent])
             VALUES
                   (@BlogTitle
                   ,@BlogAuthor
                   ,@BlogContent)";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("BlogTitle", title);
            cmd.Parameters.AddWithValue("BlogAuthor", author);
            cmd.Parameters.AddWithValue("BlogContent", content);

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Created successfully" : "Failed to create data";

            Console.WriteLine(message);
        }

        public void Update(int id, string title, string author , string content )
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);

            connection.Open();

            string query = @"
            UPDATE [dbo].[Tbl_Blog]
                     SET [BlogTitle] = @BlogTitle
                        ,[BlogAuthor] = @BlogAuthor
                        ,[BlogContent] = @BlogContent
                     WHERE BlogId = @BlogId
            ";

            SqlCommand cmd = new SqlCommand (query, connection);
            cmd.Parameters.AddWithValue("BlogId", id);
            cmd.Parameters.AddWithValue("BlogTitle", title);
            cmd.Parameters.AddWithValue("BlogAuthor", author);
            cmd.Parameters.AddWithValue("BlogContent", content);

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Updated successfully" : "Failed to update data";

            Console.WriteLine(message);
        }

        public void Delete(int id) 
        { 
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);

            connection.Open();

            string query = "DELETE FROM [dbo].[Tbl_Blog] WHERE BlogId = @BlogId ";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("BlogId", id);
            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Deleted successfully" : "Failed to delete data";

            Console.WriteLine(message);
        }

    }
}
