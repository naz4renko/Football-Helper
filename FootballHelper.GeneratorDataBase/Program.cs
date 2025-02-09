using FootballHelper.DataBase;
using FootballHelper.Logic;
using FootballHelper.Logic.BuilderPattern;
using System.Reflection.Metadata;

public class Program
{
    private static void Main(string[] args)
    {
        DataAccess myDataBase = new DataAccess();

        DatabaseSeeder dbSeeder = new DatabaseSeeder();
        dbSeeder.SeedDatabase(myDataBase);


        Console.ReadKey(true);
    }
}