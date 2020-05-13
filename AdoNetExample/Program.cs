using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdoNetExample
{

    class Program
    {
        static string ConString = @"Data Source=DESKTOP-AAJUOB7;Initial Catalog=MyDatabaseDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //string ConString = @"Data Source=DESKTOP-AAJUOB7;Initial Catalog=AdoNetExampleDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            //CreateLocalDB();

            //CreateTable();

            RunProgram();

        }

        private static void RunProgram()
        {
            SqlConnection con = new SqlConnection(ConString);
            string queryString;
            for (int i = 1; i <= 10; i++)
            {
                queryString = $@"INSERT INTO dbo.Orders VALUES({i},{i},{i}, '{DateTime.Now}');";
                con.Open();
                SqlCommand cmd = new SqlCommand(queryString, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            




        }

        private static void CreateTable()
        {


            SqlConnection con = new SqlConnection(ConString);
            string querystring = @"CREATE TABLE dbo.Orders3
                (
                    OrderId INT NOT NULL PRIMARY KEY,
                    EmployeeId INT NOT NULL,
                    CustomerId INT NOT NULL,
                    Date datetime NULL,
                    orderDate DATE NOT NULL
            );";
            con.Open();
            SqlCommand cmd = new SqlCommand(querystring, con);
            con.Close();


        }

        private static void CreateLocalDB()
        {
            string str;
            SqlConnection myConn = new SqlConnection("Server=localhost;Integrated security=SSPI;database=master");
            str = "CREATE DATABASE MyDatabaseDB ON PRIMARY " +
               "(NAME = MyDatabase_DB, " +
               "FILENAME = 'C:\\SQlDatebases\\MyDatabaseDataDB.mdf', " +
               "SIZE = 2MB, MAXSIZE = 10MB, FILEGROWTH = 10%) " +
               "LOG ON (NAME = MyDatabaseDB_Log, " +
               "FILENAME = 'C:\\SQlDatebases\\MyDatabaseDataDB.ldf', " +
               "SIZE = 1MB, " +
               "MAXSIZE = 5MB, " +
               "FILEGROWTH = 10%)";
            SqlCommand myCommand = new SqlCommand(str, myConn);
            try
            {
                myConn.Open();
                myCommand.ExecuteNonQuery();
                Console.WriteLine("DataBase is Created Successfully");
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                myConn.Close();
                Console.ReadKey();
            }
        }

        private static void RunProgramOld()
        {
            var ConString = ConfigurationManager.ConnectionStrings["AdoNetExample"].ConnectionString;
            Console.WriteLine(ConString);
            SqlConnection con = new SqlConnection(ConString);
            string querystring = "SELECT * FROM Student";
            con.Open();
            SqlCommand cmd = new SqlCommand(querystring, con);
            SqlDataReader reader = cmd.ExecuteReader();
            //reader;
            while (reader.Read())
            {
                Console.WriteLine(reader[0].ToString() + " " +
                    reader[1].ToString() + " " + reader[2].ToString());
            }

        }
    }   
}
