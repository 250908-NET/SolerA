using System;

namespace _9_ClassesChallenge
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //1.3
            Human being1 = new Human();
            //1.4
            Human being2 = new Human("Will", "Turner");

            //1.6
            Console.WriteLine(being1.AboutMe());
            Console.WriteLine(being2.AboutMe());

            //2.4
            Human2 being3 = new Human2("Jack", "Sparrow", "Brown");
            Human2 being4 = new Human2("Hector", "Barbosa", 63);
            Human2 being5 = new Human2("Elizabeth", "Swann", "Brown", 18);

            //2.5
            Console.WriteLine(being3.AboutMe2());
            Console.WriteLine(being4.AboutMe2());
            Console.WriteLine(being5.AboutMe2());

            //2.7
            Human2 being6 = new Human2("Davey", "Jones", "blue-green", 1045);
            being6.Weight = 240;
            Console.WriteLine($"When putting a valid weight(240), they weigh {being6.Weight}.");
            being6.Weight = 666;
            Console.WriteLine($"When putting an invalid weight(666), they weigh {being6.Weight}.");
        }
    }
}
