using System.Data;
using System.Data.SqlClient;
using Dapper;

class DapperCrud
{
    static readonly string connString = """Server=DESKTOP-5NUP6OJ\SQLEXPRESS; User = sa; Password = sa; Database=PMSAPI;""";
    public static void Main()
    {
        // var id = AddData(connString, "C# Project", "Project made with C# and Dapper", DateTime.Now, DateTime.Now.AddDays(30));
        var id = ModifyData(connString, 5, "C# Project Updated", "Project updated with C# and Dapper", DateTime.Now, DateTime.Now.AddDays(30));
        Console.WriteLine(id);
        foreach (var project in ReadData(0, 30, connString))
        {
            Console.WriteLine($"{project.Name}");
        }
    }

    public static List<Project> ReadData(int offset, int limit, string conn)
    {
        // List<Project> fetch = new();
        try
        {
            using (IDbConnection db = new SqlConnection(conn))
            {
                string query = """
                SELECT name as Name, description as Description,
                start_date as StartDate, end_date as EndDate, 
                category_id as CategoryId, manager_id as ManagerID 
                FROM projects 
                ORDER BY id
                OFFSET @offset ROWS
                FETCH NEXT @limit ROWS ONLY
            """;
                return db.Query<Project>(query, new { offset, limit }).ToList();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return new List<Project>();
        }
        // return fetch;
    }

    public static long AddData(string conn, string name, string desc, DateTime start, DateTime end)
    {
        long lastId = 0;
        try
        {
            string query = """
        INSERT INTO projects (name, description, start_date, end_date)
        VALUES (@name, @description, @start_date, @end_date)
        SELECT SCOPE_IDENTITY();
        """;
            using (IDbConnection db = new SqlConnection(conn))
            {
                // var result = db.Execute(query, new { projName = name, description = desc, start_date = start, end_date = end });
                lastId = db.QuerySingle<long>(query, new { name, description = desc, start_date = start, end_date = end }); //returns the last inserted id
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return lastId;
    }

    public static int ModifyData(string conn, int id, string name, string desc, DateTime start, DateTime end)
    {
        int result = 0;
        try
        {
            string query = """
        UPDATE projects SET name = @projName, description = @desc, start_date = @start, end_date = @end
        WHERE id = @id        
        """;
            using (IDbConnection db = new SqlConnection(conn))
            {
                result = db.Execute(query, new { id, projName = name, desc, start, end });
                // db.QuerySingle<long>(query, new { id, projName = name, description = desc, start_date = start, end_date = end }); //returns the last inserted id
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return result;
    }

    public static void DeleteData()
    {

    }
}