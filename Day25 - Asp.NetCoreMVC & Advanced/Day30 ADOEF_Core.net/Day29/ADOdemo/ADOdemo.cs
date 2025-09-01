using System;
using System.Data;
using System.Data.SqlClient;

namespace ADOdemo
{
    class ADOdemo
    {
        static void Main(string[] args)
        {
            // for establishing a connection to the database


            // string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WiproDatabase;Integrated Security=True";

            // using (SqlConnection connection = new SqlConnection(connectionString)) 
            // {
            //     connection.Open();
            //     Console.WriteLine("Connection opened successfully.");

            //     connection.Close();
            //     Console.WriteLine("Connection closed successfully.");
            //}

            // string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WiproDatabase;Integrated Security=True";
            // using (SqlConnection connection = new SqlConnection(connectionString)) 
            // {
            //     connection.Open();
            //     SqlCommand cmd = new SqlCommand("select *  from employee", connection);
            //     SqlDataReader reader = cmd.ExecuteReader();
            //     while (reader.Read())
            //     {
            //         Console.WriteLine($"ID:{reader["empid"]}, name:{reader["city"]}, salary:{reader["salary"]}");
            //     }
            // }

            // string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WiproDatabase;Integrated Security=True";
            // using (SqlConnection connection = new SqlConnection(connectionString))
            // {
            //     connection Open();
            //     SqlCommand cmd = new SqlCommand
            //     ("select * from employee where empid>103 order by name", connection);

            //     SqlDataReader reader = cmd.ExecuteReader();

            //     while (reader.Read())
            //     {
            //         Console.WriteLine($"ID:{reader["empid"]}, name:{reader["city"]}, salary:{reader["salary"]}");
            //     }
            // }

            // string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WiproDatabase;Integrated Security=True";
            // using (SqlConnection connection = new SqlConnection(connectionString))
            // {
            //     connection.Open();
            //     SqlCommand cmd = new SqlCommand
            //     ("insert into employee (employeeID, name, salary, email) values (9, 'Ravi', 550000, 'ravi123@gamil.com')", connection);

            //     SqlDataReader reader = cmd.ExecuteReader();

            //     while (reader.Read())
            //     {
            //         Console.WriteLine($"ID:{reader["employeeID"]}, name:{reader["Name"]}, salary:{reader["Salary"]}, Email{reader["Email"]}");
            //     }
            //     int rowsAffected = cmd.ExecuteNonQuery();
            //     Console.WriteLine($"{rowsAffected} row(s) inserted.");
            // }





            //SqlDataAdapter

            // string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WiproDatabase;Integrated Security=True";
            // using (SqlConnection connection = new SqlConnection(connectionString))
            // {
            //     connection Open();
            //     string sql = "select * from employee";

            //     using (SqlCommand command = new SqlCommand(sql, connection))
            //     {
            //         SqlDataAdapter adapter = new SqlDataAdapter(command);
            //         Dataset ds = new Dataset();

            //         adapter.Fill(ds, "Employee");

            //         Console.WriteLine("Employee Data:");
            //         foreach (DataRow row in ds.Table["Employee"].Rows)
            //         {
            //             Console.WriteLine($"ID:{reader["employeeID"]}, name:{reader["Name"]}, salary:{reader["Salary"]}, Email{reader["Email"]}");

            //         }
            //     }
            // }


            DataTable t1 = new DataTable("cusotmer");
            t1.Columns.Add("custid", typeof(int));
            t1.Columns.Add("custName", typeof(string));
            t1.Columns.Add("custSalary", typeof(int));
            t1.Rows.Add(101, "Raju", 5000);
            t1.Rows.Add(102, "Popat", 2500);
            t1.Rows.Add(103, "jaggu", 2300);


            DataTable t2 = new DataTable("orders");
            t1.Columns.Add("orderid", typeof(int));
            t2.Columns.Add("custid", typeof(int));
            t2.Columns.Add("orderdate", typeof(DateTime));
            t2.Rows.Add(1, 101, DateTime.Now.AddDays(-1));
            t2.Rows.Add(2, 102, DateTime.Now.AddDays(-2));
            t2.Rows.Add(3, 103, DateTime.Now.AddDays(-3));
            t2.Rows.Add(4, 104, DateTime.Now.AddDays(-4));


            DataSet ds = new DataSet();
            ds.Tables.Add(t1);
            ds.Tables.Add(t2);

            foreach (DataTable t in ds.Tables)
            {
                Console.WriteLine($"---Table: {t.TableName}---");
            }

            foreach (DataColumn column in t1.Columns)
            {
                Console.Write($"{column.ColumnName}\t");
            }
            Console.WriteLine();

            foreach (DataRow row in t1.Rows)
            {
                Console.Write($"{item}\t");
            }
            Console.WriteLine(); //add Spaces between tables
        }

    }
}