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
        }
    }
}
