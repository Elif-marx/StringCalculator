using System;
using System.Linq;

namespace StringCal
{
    class Program
    {
        public static double processOperation(double num1,double num2,string op)
        {           
            switch (op)
            {
                case "+":
                    return num1 + num2;
                    
                case "-":
                    return num1 - num2;
                    
                case "x":
                    return num1 * num2;
                    
                case "/":
                    return num1 / num2;
                    
                default:
                    return 0;                    
            }
        }
        public static string checkOperation(string text)
        {
            if (text.Contains('+'))
            {
                return "+";
            }
            else if (text.Contains('-'))
            {
                return "-";
            }
            else if (text.Contains('x'))
            {
                return "x";
            }
            else if (text.Contains('/'))
            {
                return "/";
            }
            else
            {
                return "text does not contain operator";
            }
        }
        public static bool checkParanthesis(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i]=='(')
                {
                    return false;
                }
                else if(text[i] == ')')
                {
                    return true;
                }
            }
            return false;
        }

        public static int findIndex(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i]==')')
                {
                    return i;
                }
            }
            return 0;
        }
        public static string getDeeptestParanthesis(string text)
        {
            //1- İlk acık parantezi bul.
            int firstOpenParanthesisIndex = text.IndexOf("(");

            //2- ilk acık parantezden sonra kalan string icerisinde ikinci herhangi parantez isaretini bul.  (8+(6x3))
            int firstCloseParanthesisIndex = text.IndexOf(")", firstOpenParanthesisIndex + 1);
            int secondOpenParanthesisIndex = text.IndexOf("(", firstOpenParanthesisIndex + 1);

            //3- Eger ikinci parantez isareti kapat parantez ise dogru parantezi bulduk. birinci ve ikinci indexler arasını isle.
            if (firstCloseParanthesisIndex < secondOpenParanthesisIndex || secondOpenParanthesisIndex < 0)
            {
                return text.Substring(firstOpenParanthesisIndex , firstCloseParanthesisIndex - firstOpenParanthesisIndex +1 );
            }

            //4 -Eger ikinci parantez isareti ac parantez ise, ikinci parantez isaretinden itibaren olan stringi recursive kendine gondermeli.
            else {
                return getDeeptestParanthesis(text.Substring(secondOpenParanthesisIndex));
            }
                                
        }

        public static double processCalculation(string text) {
            string leftStr, rightStr;
            double leftNum, rightNum;
            bool isLeftNumber, isRightNumber;

            if (text.Contains("("))
            {
                string deeptestParanthesesInside = getDeeptestParanthesis(text);
                string deeptestParantheses =deeptestParanthesesInside.Substring(1,deeptestParanthesesInside.Length-2);
                double value = processCalculation(deeptestParantheses);
                deeptestParantheses = "(" + deeptestParantheses + ")";
                string newstr = text.Replace(deeptestParantheses, value.ToString());
                return processCalculation(newstr);
            }

            if (text.IndexOf('+') > 0)
            {
                leftStr = text.Substring(0, text.IndexOf('+'));
                rightStr = text.Substring(text.IndexOf('+') + 1);
                isLeftNumber = double.TryParse(leftStr, out leftNum);
                isRightNumber = double.TryParse(rightStr, out rightNum);

                if (isLeftNumber == true && isRightNumber == true)
                {
                    return leftNum + rightNum;
                }
                else if (isLeftNumber == true && isRightNumber == false)
                {
                    return leftNum + processCalculation(rightStr);
                }
                else if (isLeftNumber == false && isRightNumber == true)
                {
                    return processCalculation(leftStr) + rightNum;
                }
                else
                {
                    return processCalculation(leftStr) + processCalculation(rightStr);
                }
            }
            if (text.IndexOf('-') > 0)
            {
                leftStr = text.Substring(0, text.IndexOf('-'));
                rightStr = text.Substring(text.IndexOf('-') + 1);
                isLeftNumber = double.TryParse(leftStr, out leftNum);
                isRightNumber = double.TryParse(rightStr, out rightNum);

                if (isLeftNumber == true && isRightNumber == true)
                {
                    return leftNum - rightNum;
                }
                else if (isLeftNumber == true && isRightNumber == false)
                {
                    return leftNum - processCalculation(rightStr);
                }
                else if (isLeftNumber == false && isRightNumber == true)
                {
                    return processCalculation(leftStr) - rightNum;
                }
                else
                {
                    return processCalculation(leftStr) - processCalculation(rightStr);
                }
            }
            if (text.IndexOf('x') > 0)
            {
                leftStr = text.Substring(0, text.IndexOf('x'));
                rightStr = text.Substring(text.IndexOf('x') + 1);
                isLeftNumber = double.TryParse(leftStr, out leftNum);
                isRightNumber = double.TryParse(rightStr, out rightNum);

                if (isLeftNumber == true && isRightNumber == true)
                {
                    return leftNum * rightNum;
                }
                else if (isLeftNumber == true && isRightNumber == false)
                {
                    return leftNum * processCalculation(rightStr);
                }
                else if (isLeftNumber == false && isRightNumber == true)
                {
                    return processCalculation(leftStr) * rightNum;
                }
                else
                {
                    return processCalculation(leftStr) * processCalculation(rightStr);
                }
            }
            if (text.IndexOf('/') > 0)
            {
                leftStr = text.Substring(0, text.IndexOf('/'));
                rightStr = text.Substring(text.IndexOf('/') + 1);
                isLeftNumber = double.TryParse(leftStr, out leftNum);
                isRightNumber = double.TryParse(rightStr, out rightNum);

                if (isLeftNumber == true && isRightNumber == true)
                {
                    return leftNum / rightNum;
                }
                else if (isLeftNumber == true && isRightNumber == false)
                {
                    return leftNum / processCalculation(rightStr);
                }
                else if (isLeftNumber == false && isRightNumber == true)
                {
                    return processCalculation(leftStr) / rightNum;
                }
                else
                {
                    return processCalculation(leftStr) / processCalculation(rightStr);
                }
            }
            double a = double.Parse(text);
            return a;
            //İkiye bol
            //SOl ve sag kısımlarda sayı ise operasyonu yap ve don.
            //degilse, sol taraf sayı ise, sag tarafı kendine gonderip operasyonu don.
        }
        public static void Test()
        {
            Console.WriteLine(processCalculation("(4x6)+(18/6x(7+3))"));
            Console.WriteLine(processCalculation("(4x6+(18/6x(7+3)))"));
            Console.WriteLine(processCalculation("(7x6/3+10)"));

        }
            
        static void Main(string[] args)
        {
            //while (1 == 1)
            //{
            //    Console.WriteLine("calculate somethinggggggg");
            //    string txt = Convert.ToString(Console.ReadLine());
            //    Console.Writeline(processCalculation(txt);
            //}
            Test();
        }
    }
}