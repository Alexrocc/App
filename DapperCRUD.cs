

using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.VisualBasic;

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
                OFFSET @limit ROWS
                FETCH NEXT @limit ROWS ONLY
            """;
                return db.Query<Project>(query, new { limit }).ToList();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return new List<Project>();
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