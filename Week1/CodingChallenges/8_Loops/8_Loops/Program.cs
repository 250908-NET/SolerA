

using System;
using System.Collections.Generic;

namespace _8_LoopsChallenge
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /* Your code here */
            List<object> oddsAndEvensDiffTypes = new List<object>()
            {
            -128,127,0,255,'h','e','y',-2147483648,"hey",21474836470,4294967295,
            "hey",-9223372036854775808,9223372036854775807,"hey",18446744073709551615,"hey"
        };//5 even

            UseForEach(oddsAndEvensDiffTypes);
        }

        /// <summary>
        /// Return the number of elements in the List<int> that are odd.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int UseFor(List<int> x)
        {
            //throw new NotImplementedException("UseFor() is not implemented yet.");
            int odds = 0;
            for(int i = 0; i < x.Count;i++)
            {
                if (x[i] % 2 != 0)
                    odds++;
            }
            return odds;
        }

        /// <summary>
        /// This method counts the even entries from the provided List<object> 
        /// and returns the total number found.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int UseForEach(List<object> x)
        {
            int evens = 0;

            foreach (object obj in x)
            {
                string str = obj.ToString();

                if (char.IsDigit(str[^1]))
                {
                    char lastChar = str[^1];

                    if (lastChar == '0' || lastChar == '2' || lastChar == '4' || lastChar == '6' || lastChar == '8')
                    {
                        evens++;
                    }
                }
            }

            return evens;
        }




        public static int UseDoWhile(List<int> x)
        {
            //throw new NotImplementedException("UseFor() is not implemented yet.");
            int i = -1;
            int multiples = 0;
            do
            {
                i++;
                if (x[i] % 4 == 0)
                    multiples++;
            } while (x[i] != 1234);

            return multiples;
        }

        /// <summary>
        /// This method counts the multiples of 4 from the provided List<int>. 
        /// Exit the loop when the integer 1234 is found.
        /// Return the total number of multiples of 4.
        /// </summary>
        /// <param name="x"></param>
        public static int UseWhile(List<int> x)
        {
            //throw new NotImplementedException("UseFor() is not implemented yet.");
            int i = 0;
            int multiples = 0;

            while (x[i] != 1234)
            {
                if (x[i] % 4 == 0)
                    multiples++;

                i++;
            }

            return multiples;
        }

        /// <summary>
        /// This method will evaluate the Int Array provided and return how many of its 
        /// values are multiples of 3 and 4.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int UseForThreeFour(int[] x)
        {
            //throw new NotImplementedException("UseForThreeFour() is not implemented yet.");
            int multiples = 0;
            for(int i = 0; i < x.Length;i++)
            {
                if (x[i] % 3 == 0 && x[i] % 4 == 0)
                    multiples++;
            }
            return multiples;
            
        }

        /// <summary>
        /// This method takes an array of List<string>'s. 
        /// It concatenates all the strings, with a space between each, in the Lists and returns the resulting string.
        /// </summary>
        /// <param name="stringListArray"></param>
        /// <returns></returns>
        public static string LoopdyLoop(List<string>[] stringListArray)
        {
            //throw new NotImplementedException("LoopdyLoop() is not implemented yet.");
            List<string> allStrings = new List<string>();
            
            foreach (List<string> list in stringListArray)
                foreach (string str in list)
                    allStrings.Add(str);
            
            return string.Join(" ", allStrings);
        }
    }
}

//using System;
//using System.Collections.Generic;

//namespace _8_LoopsChallenge
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            /* Your code here */

//        }

//        /// <summary>
//        /// Return the number of elements in the List<int> that are odd.
//        /// </summary>
//        /// <param name="x"></param>
//        /// <returns></returns>
//        public static int UseFor(List<int> x)
//        {
//            throw new NotImplementedException("UseFor() is not implemented yet.");
//        }

//        /// <summary>
//        /// This method counts the even entries from the provided List<object> 
//        /// and returns the total number found.
//        /// </summary>
//        /// <param name="x"></param>
//        /// <returns></returns>
//        public static int UseForEach(List<object> x)
//        {
//            throw new NotImplementedException("UseForEach() is not implemented yet.");
//        }

//        /// <summary>
//        /// This method counts the multiples of 4 from the provided List<int>. 
//        /// Exit the loop when the integer 1234 is found.
//        /// Return the total number of multiples of 4.
//        /// </summary>
//        /// <param name="x"></param>
//        public static int UseWhile(List<int> x)
//        {
//            throw new NotImplementedException("UseFor() is not implemented yet.");
//        }

//        /// <summary>
//        /// This method will evaluate the Int Array provided and return how many of its 
//        /// values are multiples of 3 and 4.
//        /// </summary>
//        /// <param name="x"></param>
//        /// <returns></returns>
//        public static int UseForThreeFour(int[] x)
//        {
//            throw new NotImplementedException("UseForThreeFour() is not implemented yet.");
//        }

//        /// <summary>
//        /// This method takes an array of List<string>'s. 
//        /// It concatenates all the strings, with a space between each, in the Lists and returns the resulting string.
//        /// </summary>
//        /// <param name="stringListArray"></param>
//        /// <returns></returns>
//        public static string LoopdyLoop(List<string>[] stringListArray)
//        {
//            throw new NotImplementedException("LoopdyLoop() is not implemented yet.");
//        }
//    }
//}