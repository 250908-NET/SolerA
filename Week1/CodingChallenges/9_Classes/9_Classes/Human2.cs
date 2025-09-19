using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("9_ClassesChallenge.Tests")]
namespace _9_ClassesChallenge
{
    internal class Human2
    {
        private string lastName = "Smyth";
        private string firstName = "Pat";
        //2.1
        private string eyeColor;
        private int? age = null;
        //2.6
        private int weight;

        public Human2()
        {

        }

        public Human2(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }

        //2.2
        public Human2(string firstName, string lastName, string eyeColor, int age)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.eyeColor = eyeColor;
            this.age = age;
        }

        public Human2(string firstName, string lastName, int age)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.age = age;
        }

        public Human2(string firstName, string lastName, string eyeColor)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.eyeColor = eyeColor;
        }

        public string AboutMe()
        {
            return $"My name is {firstName} {lastName}.";
        }

        //2.3
        public string AboutMe2()
        {
            if (string.IsNullOrWhiteSpace(eyeColor))
                if (!age.HasValue)
                    return $"My name is {firstName} {lastName}.";
                else
                    return $"My name is {firstName} {lastName}. My age is {age}";
            else
                if (!age.HasValue)
                return $"My name is {firstName} {lastName}. My eye color is {eyeColor}.";
            else
                return $"My name is {firstName} {lastName}. My age is {age}. My eye color is {eyeColor}.";
        }
        
        //2.6 Continued
        public int Weight
        {
            get { return weight; }
            set
            {
                if (value < 0 || value > 400)
                    weight = 0;
                else
                    weight = value;
            }
        }
    }
}