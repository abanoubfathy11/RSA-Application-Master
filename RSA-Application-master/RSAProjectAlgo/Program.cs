using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RSAProjectAlgo
{
    class bigdiv
    {
        public string q;
        public string r;

    }
    class BigInteger
    {
        public static bigdiv div(string a, string b)
        {
            bigdiv s = new bigdiv();
            if (isSmaller(a, b))
            {
                s.q = "0";
                s.r = a;
                return s;
            }
            string m = Add(b, b);
            s = div(a, m);
            s.q = Add(s.q, s.q);
            if (isSmaller(s.r, b))
            {
                return s;
            }
            else
            {
                s.q = Add(s.q, "1");
                s.r = Sub(s.r, b);
                return s;
            }

        }
        public static string Add(string number1, string number2)  //O(N)
        {
            int maxLength;
            if (number1.Length < number2.Length)  //O(1)
                maxLength = number2.Length; //O(1)
            else maxLength = number1.Length;  //O(1)

            int[] number1Integer = new int[maxLength]; // O(1)
            int[] number2Integer = new int[maxLength]; // O(1)
            int Convertion = 48; // O(1)

            for (int i = 0; i < number1.Length; i++) // O(N)
                number1Integer[i] = number1[number1.Length - 1 - i] - Convertion;//O(1)
            for (int i = 0; i < number2.Length; i++)  // O(N)
                number2Integer[i] = number2[number2.Length - 1 - i] - Convertion;//O(1)

            int Carry = 0;  // O(1)
            int[] sumOfNumbers = new int[maxLength + 1]; // O(1)
            for (int i = 0; i < maxLength; i++)   // O(N) * O(1) = O(N)
            {
                sumOfNumbers[i] = (number1Integer[i] + number2Integer[i] + Carry) % 10; // O(1)
                int Res = number1Integer[i] + number2Integer[i] + Carry;// O(1)
                if (Res < 10) // O(1)
                { Carry = 0; }// O(1)
                else
                    Carry = 1;// O(1)
            }
            sumOfNumbers[maxLength] = Carry; // O(1)
            if (Carry == 0) // O(1)
            { maxLength = maxLength - 1; }// O(1)

            StringBuilder resultOfSum = new StringBuilder(); // O(1)
            for (int i = maxLength; i >= 0; i--)   // O(N)
                resultOfSum.Append(sumOfNumbers[i]); // O(1) 
            return resultOfSum.ToString();   // O(1)
        }



        public static string Sub(string number1, string number2)  // O(N)
        {
            if (isSmaller(number1, number2)) //O(N)
            {
                string temp = number1;// O(1)
                number1 = number2;// O(1)
                number2 = temp;// O(1)
            }
            string resultStr = "";  //O(1)
            int lengthOfNumber1 = number1.Length;  //O(1)
            int lengthOfNumber2 = number2.Length;  //O(1)
            int diffBetweenLen = lengthOfNumber1 - lengthOfNumber2;  //O(1)
            int Carry = 0;  //O(1)
            for (int i = lengthOfNumber2 - 1; i >= 0; i--)  //O(N)
            {
                int subtraction = ((number1[i + diffBetweenLen] - '0') - (number2[i] - '0') - Carry);  // O(1)
                if (subtraction < 0)  //O(1)
                {
                    subtraction = subtraction + 10;// O(1)
                    Carry = 1;// O(1)
                }
                else //O(1)
                    Carry = 0;//O(1)
                resultStr = subtraction + resultStr;//O(1)
            }

            for (int i = lengthOfNumber1 - lengthOfNumber2 - 1; i >= 0; i--)    //O(N)
            {
                if (number1[i] == '0' && Carry != 0)//O(1)
                { resultStr = "9" + resultStr; }//O(1)
                int subtraction2 = ((number1[i] - '0') - Carry); //O(1)
                if (i > 0 || subtraction2 > 0)      //O(1)
                { resultStr = subtraction2 + resultStr; } //O(1)
                Carry = 0; //O(1)
            }
            StringBuilder finalResult = new StringBuilder();                           //O(1)
            if (resultStr[0] == '0')                       //O(N)
            {
                for (int i = 0; i < resultStr.Length - 1; i++) //O(N)
                { finalResult.Append(resultStr[i + 1]); }
            }
            else finalResult = new StringBuilder( resultStr);
            return finalResult.ToString(); ;         //O(1)
        }
        
        public static string mul(string number1, string number2) // O(N^1.58)
        {
            int lengthOf1 = number1.Length;    //O(1)
            int lengthOf2 = number2.Length;    //O(1)
            int n = lengthOf1;   //O(1)
            if (lengthOf1 > lengthOf2)  //O(N)
            {
                for (int i = 0; i < lengthOf1 - lengthOf2; i++) { number2 = '0' + number2; n = lengthOf1; } // O(N)
            }
            else if (lengthOf1 < lengthOf2)// O(N)
                for (int i = 0; i < lengthOf2 - lengthOf1; i++) { number1 = '0' + number1; n = lengthOf2; }//O(N)
            if (n == 0) return "0";  //O(1)
            if (n == 1) return (int.Parse(number1) * int.Parse(number2)).ToString();  //O(1)

            int fristHalf = n / 2;
            int secondHalf = (n - fristHalf);

            string number1Left = number1.Substring(0, fristHalf); //O(N)
            string number1Right = number1.Substring(fristHalf, secondHalf); //O(N)

            string number2Left = number2.Substring(0, fristHalf); //O(N)
            string number2Right = number2.Substring(fristHalf, secondHalf); //O(N)

            string C1 = mul(number1Left, number2Left); //O(N^1.58)
            string C2 = mul(number1Right, number2Right); //O(N^1.58)
            string C3 = mul(Add(number1Left, number1Right), Add(number2Left, number2Right)); //O(N^1.58)

            string C = power10(Sub(C3, Add(C1, C2)), secondHalf); //O(N)

            return Add(Add(power10(C1, 2 * secondHalf), C), C2); //O(N)


        }
        public static string Mul(string number1, string number2)  // O(N^1.58)
        {
            string firstMultiplication = mul(number1, number2); // O(N^1.58)
            int numOfZerosFirst = 0;// O(1)
            for (int i = 0; i < firstMultiplication.Length; i++)// O(N)
            {
                if (firstMultiplication[i] == '0')// O(1)
                    numOfZerosFirst++;// O(1)
                else break;// O(1)
            }
            string final = firstMultiplication.Substring(numOfZerosFirst, firstMultiplication.Length - numOfZerosFirst);// O(N)
            return final;// O(1)
        }
        public static string power10(string str, int n) // O(N)
        {
            StringBuilder strPower =  new StringBuilder( str);// O(1)
            for (int i = 0; i < n; i++)// O(N)
            {
                strPower.Append('0');// O(1)
            }
            return strPower.ToString();// O(1)
        }

        public static bool isSmaller(string Number1, string Number2) //O(N)
        {
            int lengthOfNumber1 = Number1.Length;  //O(1)
            int lengthOfNumber2 = Number2.Length;  //O(1)
            if (lengthOfNumber1 < lengthOfNumber2)  //O(1)
            { return true; }
            if (lengthOfNumber2 < lengthOfNumber1)  //O(1)
            { return false; }
            for (int i = 0; i < lengthOfNumber1; i++)  //O(N)
                if (Number1[i] < Number2[i]) return true; // O(1)
                else if (Number1[i] > Number2[i]) return false;// O(1)
            return false;// O(1)
        }
        public static void ReadFileMultiplication()
        {
            FileStream filew;
            if (File.Exists("MultiplyTestCases_Output.txt")) filew = new FileStream("MultiplyTestCases_Output.txt", FileMode.Open, FileAccess.Write);
            else filew = new FileStream("MultiplyTestCases_Output.txt", FileMode.OpenOrCreate, FileAccess.Write);
            FileStream file = new FileStream("Mul.txt", FileMode.Open, FileAccess.Read);
            StreamWriter sw = new StreamWriter(filew);// O(1)
            int cases;
            StreamReader sr = new StreamReader(file);
            string line = sr.ReadLine();
            cases = int.Parse(line);
            int wrongAnswers = 0;
            for (int i = 0; i < cases; i++)
            {
                line = sr.ReadLine();
                string num1 = sr.ReadLine();
                string num2 = sr.ReadLine();
                string result = sr.ReadLine();
                string ReceivedAnswer = BigInteger.Mul(num1, num2), expectedAnswer = result;
                sw.WriteLine(ReceivedAnswer);// O(1)
                sw.WriteLine();// O(1)
                if (ReceivedAnswer != expectedAnswer)
                {
                    Console.WriteLine("wrong answer,case #" + (i + 1) + " expected answer = " + expectedAnswer + " , received answer= " + ReceivedAnswer);
                    wrongAnswers++;
                }
            }
            sw.Close();// O(1)
            filew.Close();// O(1)
            sr.Close();
            file.Close();
            if (wrongAnswers == 0)
                Console.WriteLine("Congratulation, Multiplication Done");
            else
                Console.WriteLine("Wrong at Multiplication");
        }
        public static void ReadFileAddition()
        {
            FileStream fileW;
            if (File.Exists("AddTestCases_Output.txt")) fileW = new FileStream("AddTestCases_Output.txt", FileMode.Open, FileAccess.Write);
            else fileW = new FileStream("AddTestCases_Output.txt", FileMode.Append, FileAccess.Write);
            FileStream file = new FileStream("Add.txt", FileMode.Open, FileAccess.Read);
            StreamWriter SW = new StreamWriter(fileW);
            int cases;
            StreamReader sr = new StreamReader(file);
            string line = sr.ReadLine();
            cases = int.Parse(line);
            int wrongAnswers = 0;
            for (int i = 0; i < cases; i++)
            {
                line = sr.ReadLine();
                string num1 = sr.ReadLine();
                string num2 = sr.ReadLine();
                string result = sr.ReadLine();
                string ReceivedAnswer = BigInteger.Add(num1, num2), expectedAnswer = result;

                SW.WriteLine(ReceivedAnswer); // O(1)
                SW.WriteLine(); // n(1)
                
                if (ReceivedAnswer != expectedAnswer)
                {
                    Console.WriteLine("wrong answer,case #" + (i + 1) + " expected answer = " + expectedAnswer + " , received answer= " + ReceivedAnswer);
                    wrongAnswers++;
                }
            }
            SW.Close(); // O(1)
            fileW.Close(); // O(1)
            sr.Close();
            file.Close();
            if (wrongAnswers == 0)
                Console.WriteLine("Congratulation, Addition Done");
            else
                Console.WriteLine("Wrong at Addition");
        }
        public static void ReadFileSubtraction()
        {
            FileStream filew;
            if (File.Exists("SubtractTestCases_Output.txt")) filew = new FileStream("SubtractTestCases_Output.txt", FileMode.Open, FileAccess.Write);
            else filew = new FileStream("SubtractTestCases_Output.txt", FileMode.Append, FileAccess.Write);
            FileStream file = new FileStream("Sub.txt", FileMode.Open, FileAccess.Read);
            StreamWriter SW = new StreamWriter(filew);          //O(1)
            int cases;
            StreamReader sr = new StreamReader(file);
            string line = sr.ReadLine();
            cases = int.Parse(line);
            int wrongAnswers = 0;
            for (int i = 0; i < cases; i++)
            {
                line = sr.ReadLine();
                string num1 = sr.ReadLine();
                string num2 = sr.ReadLine();
                string result = sr.ReadLine();
                string ReceivedAnswer = BigInteger.Sub(num1, num2), expectedAnswer = result;
                SW.WriteLine(ReceivedAnswer);   //O(1)
                SW.WriteLine();              //O(1)
                if (ReceivedAnswer != expectedAnswer)
                {
                    Console.WriteLine("wrong answer,case #" + (i + 1) + " expected answer = " + expectedAnswer + " , received answer= " + ReceivedAnswer);
                    wrongAnswers++;
                }
            }
            SW.Close();                  //O(1)
            filew.Close();              //O(1)
            sr.Close();
            file.Close();
            if (wrongAnswers == 0)
                Console.WriteLine("Congratulation, Subtraction Done ");
            else
                Console.WriteLine("Wrong at Subtraction");
        }
        public static string pow(string num, string n)
        {
            if (n == "1") return num;
            return mul(num, pow(num, Sub(n, "1")));
        }

    }
    class Program
    {
        static void Main(string[] args)
        {    
            BigInteger.ReadFileAddition();
            BigInteger.ReadFileMultiplication();
            BigInteger.ReadFileSubtraction();
            bigdiv d = BigInteger.div("72568965964896", "2455");
            Console.WriteLine(d.q + "\t\t\t" + d.r);
        }
        
    }
}

