using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WordFinderConsole;

try
{
    var stayin = true;
    
    while (stayin)
    {
        Console.Clear();
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.WriteLine("###################################################");
        Console.WriteLine("####### Word finder application ###########");
        Console.WriteLine("###################################################");
        Console.WriteLine("Please provide the path to the file that contains the board:");
        var pathToFile = Console.ReadLine();
        Console.WriteLine($"Wait until we get the file from {pathToFile}");
        if (File.Exists(pathToFile))
        {
            var lines = File.ReadAllLines(pathToFile);
            var finder = new FinderHelper(lines);
            Console.WriteLine($"The file {pathToFile} has been processed successfully");
            Console.WriteLine("Please provide the path to the file that contains the words stream:");
            pathToFile = Console.ReadLine();
            if (File.Exists(pathToFile))
            {
                lines = File.ReadAllLines(pathToFile);
                var tokens = lines[0].Split(' ');
                Console.WriteLine($"The file {pathToFile} has been processed successfully");
                var results = finder.Find(tokens);
                Console.WriteLine("Starting to find the words.. let's have fun");
                Console.WriteLine("Those are the top 10 most repeated words:");
                foreach (var item in results)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine("Thanks for using the word finder tool");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"The file does not exists: {pathToFile}. Please provide a valid path.");
                Console.ResetColor();
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"The file does not exists: {pathToFile}. Please provide a valid path.");
            Console.ResetColor();
        }

        Console.WriteLine("Do you want to finish (Y/N):");
        if (Console.ReadLine().Equals("Y", StringComparison.CurrentCultureIgnoreCase))
            stayin = false;
    }

}
catch (ArgumentNullException ex)
{
    Console.WriteLine(ex.Message);
}
catch (ArgumentException ex)
{
    Console.WriteLine(ex.Message);
}
catch (Exception ex)
{
    Console.WriteLine("Ups, something went extremely wrong. Please try again");
}
