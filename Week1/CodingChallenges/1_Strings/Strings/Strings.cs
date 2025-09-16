using System;

namespace StringManipulationChallenge
{
    public class Program
    {
        static void Main(string[] args)
        {
            /*
            *
            * implement the required code here and within the methods below.
            *
            */
            //when you call a method, you call it with arguments. The args values are held in a variable.
            Console.WriteLine("Please enter a sentence you would like to be transformed: ");
            string sentence = Console.ReadLine();
            int lengthOfSentence = sentence.length();

            Console.WriteLine("Watch the magic happen below:");
            //StrinToUpper 
            Console.WriteLine("\nAbracadabra!");
            Console.WriteLine("All Caps:\t" + StringToUpper(sentence));

            //StringToLower
            Console.WriteLine("\nAlakazam!");
            Console.WriteLine("All Lower case:\t" + StringToLower(sentence));

            //StringTrim
            Console.WriteLine("\nHocus Pocus!");
            Console.WriteLine("Whitespace be gone:\t" + StringTrim(sentence));

            Console.WriteLine("\nFor these next parts, I will need an assistant.");
            Console.WriteLine("We have never met before right? right");

            Console.WriteLine("What is your first name?");
            string firstName = Console.ReadLine();
            Console.WriteLine("What is your last name?");
            string lastName = Console.ReadLine();


            int firstElement = -1;

            while (firstElement < 0 || firstElement > lengthOfSentence)
            {
                Console.WriteLine("What is your favorite number between 0 and " + (lengthOfSentence - 1) + "?");
                firstElement = Convert.ToInt32(Console.ReadLine());
            }

            int lengthOfSubstring = 1;

            if (firstElement < lengthOfSentence - 1)
            {
                do
                {
                    Console.WriteLine("What is your second favorite number between 1 and "(lengthOfSentence - firstElement) + "?");
                    lengthOfSubstring = Convert.ToInt32(Console.ReadLine());
                } while (lengthOfSubstring < 1 || (lengthOfSubstring > (lengthOfSentence - firstElement + 1)));
            }

            Console.WriteLine("Lastly, pick an ascii character, any ascii character, dont let me see!");
            char chr = Console.ReadKey().KeyChar;

            Console.WriteLine("\nAlright, the magic is ready! Rapid Fire mode!");

            //StringSubstring
            Console.WriteLine("\nSubstring using your favorites: " + StringSubstring(sentence, firstElement, lengthOfSubstring));

            //SearchChar
            Console.WriteLine("The character you chose is in position " + SearchChar(sentence, chr));

            //ConcatNames
            Console.WriteLine("Last, but not least, your full name is..." + ConcatNames(firstName, lastName) + "!");


            Console.WriteLine("\n\nThank you! Thank you! I will be here all Training!");
        }

        /// <summary>
        /// This method has one string parameter and will: 
        /// 1) change the string to all upper case and 
        /// 2) return the new string.
        /// </summary>
        /// <param name="usersString"></param>
        /// <returns></returns>
        public static string StringToUpper(string myString)// the method itself has 'parameters'
        {
            // throw new NotImplementedException("StringToUpper method not implemented.");
            return myString.ToUpper();
        }

        /// <summary>
        /// This method has one string parameter and will:
        /// 1) change the string to all lower case,
        /// 2) return the new string into the 'lowerCaseString' variable
        /// </summary>
        /// <param name="usersString"></param>
        /// <returns></returns>       
        public static string StringToLower(string usersString)
        {
            //throw new NotImplementedException("StringToUpper method not implemented.");
            return usersString.ToLower();
        }

        /// <summary>
        /// This method has one string parameter and will:
        /// 1) trim the whitespace from before and after the string, and
        /// 2) return the new string.
        /// HINT: When getting input from the user (you are the user), try inputting "   a string with whitespace   " to see how the whitespace is trimmed.
        /// </summary>
        /// <param name="usersStringWithWhiteSpace"></param>
        /// <returns></returns>
        public static string StringTrim(string usersStringWithWhiteSpace)
        {
            //throw new NotImplementedException("StringTrim method not implemented.");
            usersStringWithWhiteSpace.Trim();
        }

        /// <summary>
        /// This method has three parameters, one string and two integers. It will:
        /// 1) get the substring based on the first integer element and the length 
        /// of the substring desired.
        /// 2) return the substring.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="firstElement"></param>
        /// <param name="lastElement"></param>
        /// <returns></returns>
        public static string StringSubstring(string x, int firstElement, int lengthOfSubsring)
        {
            throw new NotImplementedException("StringSubstring method not implemented.");
        }

        /// <summary>
        /// This method has two parameters, one string and one char. It will:
        /// 1) search the string parameter for first occurrance of the char parameter and
        /// 2) return the index of the char.
        /// HINT: Think about how StringTrim() (above) would be useful in this situation
        /// when getting the char from the user. 
        /// </summary>
        /// <param name="userInputString"></param>
        /// <param name="charUserWants"></param>
        /// <returns></returns>
        public static int SearchChar(string userInputString, char charUserWants)
        {
            throw new NotImplementedException("SearchChar method not implemented.");
        }

        /// <summary>
        /// This method has two string parameters. It will:
        /// 1) concatenate the two strings with a space between them.
        /// 2) return the new string.
        /// HINT: You will need to get the users first and last name in the 
        /// main method and send them as arguments.
        /// </summary>
        /// <param name="fName"></param>
        /// <param name="lName"></param>
        /// <returns></returns>
        public static string ConcatNames(string fName, string lName)
        {
            throw new NotImplementedException("ConcatNames method not implemented.");
        }
    }//end of program
}
