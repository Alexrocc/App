
using System.Data.Common;
using System.Data.SqlClient;

// try
// {
//     string connectionString = """Server=DESKTOP-5NUP6OJ\SQLEXPRESS;User=sa;Password=sa;Database=PMSAPI;""";
//     using SqlConnection connection = new(connectionString);
//     connection.Open();
//     string query = "SELECT * FROM test";
//     SqlCommand cmd = new(query, connection);
//     SqlDataReader reader = cmd.ExecuteReader();

//     while (reader.Read())
//     {
//         var id = reader.GetGuid(0);
//         var name = reader.GetString(1);
//         Console.WriteLine($"{id}: {name}");
//     }
// }
// catch (Exception ex)
// {
//     Console.WriteLine(ex.Message);
// }

class QueryHandler
{
    public static void Main()
    {
        Int32 lastid = InsertData();
        if (lastid > 0)
        {
            ModifyData(lastid);
        }
        ReadData();
    }
    public static void ReadData()
    {
        try
        {
            string connString = """Server=DESKTOP-5NUP6OJ\SQLEXPRESS; User = sa; Password = sa; Database=PMSAPI;""";
            using SqlConnection conn = new(connString);
            conn.Open();
            string query = "SELECT * FROM projects";
            SqlCommand cmd = new(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var id = reader.GetInt32(0);
                var name = reader.GetString(1);
                Console.WriteLine($"{id}: {name}");
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"{ex}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex}");
        }
    }
    //INSERT query to write data in database, returning the last inserted id
    public static Int32 InsertData()
    {
        Int32 id = 1;
        string connString = """Server=DESKTOP-5NUP6OJ\SQLEXPRESS; User = sa; Password = sa; Database=PMSAPI;""";
        string insertQuery = """
        INSERT INTO projects (name, description, start_date, end_date)
        VALUES (@name, @description, @start_date, @end_date)
        SELECT SCOPE_IDENTITY();
        """;
        try
        {
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                using (var command = new SqlCommand(insertQuery, conn))
                {
                    command.Parameters.AddWithValue("@name", "New project");
                    command.Parameters.AddWithValue("@description", "It's a new project");
                    command.Parameters.AddWithValue("@start_date", DateTime.Now);
                    command.Parameters.AddWithValue("@end_date", DateTime.Now.AddDays(30));

                    id = Convert.ToInt32(command.ExecuteScalar());

                    return id;
                }
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"{ex}");
            return 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex}");
            return 0;
        }

    }

    //modifiyng the last inserted data
    public static void ModifyData(int id)
    {
        string connString = """Server=DESKTOP-5NUP6OJ\SQLEXPRESS; User = sa; Password = sa; Database=PMSAPI;""";
        string insertQuery = """
        UPDATE projects SET name = @name, description = @description
        WHERE id = @id
        """;
        try
        {
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                using (var command = new SqlCommand(insertQuery, conn))
                {
                    command.Parameters.AddWithValue("@name", "Updated project");
                    command.Parameters.AddWithValue("@description", "Updated project description");
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"{ex}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex}");
        }
    }
}

