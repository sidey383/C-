using System.Collections;
using Microsoft.VisualBasic.FileIO;

namespace C_;

public class Program
{
    public static void Main(string[] args)
    {
        TableReader reader = new TableReader();
        reader.ReadJuniors("Juniors5.csv");
        reader.ReadJuniors("Teamleads5.csv");
        reader.GetJuniors().ForEach(Console.WriteLine);
        reader.GetTeamleads().ForEach(Console.WriteLine);
    }

}
