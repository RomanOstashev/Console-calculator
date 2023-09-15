using System.Globalization;

namespace ConsoleCalculator
{
    public class Program
    {
        public static double previousNubmber = 0;
        public static double curentNumber = 0;
        public static int roundValue = 6;
        public static void Main()
        {
            var str = "";

            Console.WriteLine("It is Console calculator 1.0 (for informaion enter '-h' or '--help')\n");

            for (; ;)
            {
                str = GetString();

                //cancel memory
                if (str == "-c" || str == "--cancel")
                {
                    previousNubmber = 0d;
                    continue;
                }

                //help table
                if (str == "-h" || str == "--help")
                {
                    HelpTable();
                    continue;
                }

                //application information
                if (str == "-i" || str == "--info")
                {
                    Console.WriteLine("[Info]\n" +
                                      "Application \t Console calculator\n" +
                                      "Version \t 1.0\n" +
                                      "Author \t\t Roman Ostashev\n" + //dont know why tho \t
                                      "Contact \t lemon-noman@yandex.ru\n");
                    continue;
                }

                //input round value
                if (str.StartsWith("-r") || str.StartsWith("--round"))
                {
                    roundValue = RoundForAnswer(str);
                    Console.WriteLine("Now rounds the value to " + roundValue + " decimal places, (in memory " + previousNubmber + ")");
                    continue;
                }

                char[] arrOperation = { '+', '-', '*', '/', '^' };
                char[] arrBanSymbol = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ~!@#$%&()_=`';:'".ToCharArray(); //for check

                //str check
                if (arrOperation.Any(str.Contains) == false) //check for arrOperation in str
                {
                    Console.WriteLine("WARNING: String without operation (in memory " + previousNubmber + ")");
                    continue;
                }
                else if (str.Contains("/0") && str.Contains("/0.") != true) //check for divide by zero
                {
                    Console.WriteLine("WARNING: Divide by zero (in memory " + previousNubmber + ")");
                    continue;
                }
                else if (arrBanSymbol.Any(str.Contains)) //check for arrBanSymbol in str
                {
                    Console.WriteLine("WARNING: String contains ban symbols (in memory " + previousNubmber + ")");
                    continue;
                }
                else if (arrOperation.Any(str.Contains) && str.Length == 1) //check for str with arrOperation, but without numbers
                {
                    Console.WriteLine("WARNING: String without numbers (in memory " + previousNubmber + ")");
                    continue;
                }

                //find operation
                var operationIndex = str.IndexOfAny(arrOperation);
                var operation = str.Substring(operationIndex, 1);

                if (operationIndex == 0)
                {
                    //not first enter
                    curentNumber = double.Parse(str.Substring(operationIndex + 1), CultureInfo.InvariantCulture);

                    previousNubmber = MathMagic(previousNubmber, curentNumber, operation);
                    Console.WriteLine(previousNubmber);
                }
                else
                {
                    //first enter
                    previousNubmber = double.Parse(str.Substring(0, operationIndex), CultureInfo.InvariantCulture);
                    curentNumber = double.Parse(str.Substring(operationIndex + 1), CultureInfo.InvariantCulture);

                    var output = MathMagic(previousNubmber, curentNumber, operation);
                    previousNubmber = output;
                    Console.WriteLine(previousNubmber);
                }
            }
        }
        /// <summary>
        /// Return string, check for emtpy string
        /// </summary>
        /// <returns>Not null string value</returns>
        public static string GetString()
        {
            var imputString = Console.ReadLine();
            while (String.IsNullOrEmpty(imputString))
            {
                Console.WriteLine("WARNING: Strig is empty (in memory " + previousNubmber + ")");
                imputString = Console.ReadLine();
            }
            return imputString;
        }
        /// <summary>
        /// Make simple mathematical operation
        /// </summary>
        /// <param name="x">First number</param>
        /// <param name="y">Second number</param>
        /// <param name="o">Operation</param>
        /// <returns>Answer of math mgaic</returns>
        public static double MathMagic(double x, double y, string o)
        {
            var ans = 0d;

            if (o == "+")
                ans = x + y;
            else if (o == "-")
                ans = x - y;
            else if (o == "*")
                ans = x * y;
            else if (o == "/")
                ans = x / y;
            else
                ans = Math.Pow(x, y);
            
            return Math.Round(ans, roundValue);
        }
        public static void HelpTable()
        {
            Console.WriteLine("[Help]");
            Console.WriteLine("Comands\n" + 
                              "-c \t --cancel \t Cancel/clear memory\n" +
                              "-h \t --help \t Help\n" +
                              "-i \t --info \t Application information\n" +
                              "-r \t --round \t Round value (default is 6 decimal places) [-r 4 -> 4 decimal places]\n" +
                              "\nOperation\n" +
                              "+ \t Plus operation\n" +
                              "- \t Minus operation\n" +
                              "* \t Multiplication operation\n" +
                              "/ \t Divide operation\n" +
                              "^ \t Exponentiation operation\n");
        }
        /// <summary>
        /// Changes the number of decimal places 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int RoundForAnswer(string s)
        {
            var value = roundValue;

            if (s.StartsWith("-r"))
                value = int.Parse(s.Substring(3));
            else if (s.StartsWith("--round"))
                value = int.Parse(s.Substring(8));
            
            return value;
        }
    }
}