using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("9_ClassesChallenge.Tests")]
namespace _9_ClassesChallenge
{
    internal class Human
    {
        //1.1
        private string lastName = "Smyth";
        private string firstName = "Pat";

        //1.3 continued
        public Human()
        {
            
        }
        //1.2
        public Human(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }
        
        //1.5
        public string AboutMe()
        {
            return $"My name is {firstName} {lastName}.";
        }

    }//end of class
}//end of namespace