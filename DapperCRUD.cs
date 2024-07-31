

using System.Data;
using System.Data.SqlClient;
using Dapper;

class DapperCrud()
{
    static readonly string connString = """Server=DESKTOP-5NUP6OJ\SQLEXPRESS; User = sa; Password = sa; Database=PMSAPI;""";
    public static void Main()
    {
        foreach (var project in ReadData(10, connString))
        {
            Console.WriteLine($"{project.Name}");
        }
    }

    public static List<Project> ReadData(int limit, string conn)
    {
        using (IDbConnection db = new SqlConnection(conn))
        {
            string query = """
                SELECT name as Name, description as Description,
                start_date as StartDate, end_date as EndDate, 
                category_id as CategoryId, manager_id as ManagerID 
                FROM projects LIMIT @limit
            """;
            return db.Query<Project>(query, new { limit }).ToList();
        }
    }

    public void AddData()
    {

    }

    public void ModifyData()
    {

    }

    public void DeleteData()
    {

    }
}