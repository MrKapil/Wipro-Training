namespace StoredProcedure
{
    class StoredProcedure
    {
        static void Main(string[] args)
        { 
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WiproDatabase;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // SqlCommand cmd = new SqlCommand("dbo.z", connection);
                // cmd.CommandType = CommandType.StoredProcedure;


                // SqlDataReader reader = cmd.ExecuteReader();
                // while (reader.Read())
                // { 
                //     COnsole.WriteLine($"")
                // } 



                //demo for stired Procedure with Parameter
                SqlCommand cmd = new SqlCommand("dbo.a", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", 1002);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                { 
                    Console.WriteLine($"ID: {reader["id"]}, Name: {}")
                } 
            }
        }
    }
}