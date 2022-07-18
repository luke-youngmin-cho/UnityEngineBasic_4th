using System;

namespace OperatorMethods
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(OperatorMethods.Div(7, 3));
            Console.WriteLine(OperatorMethods.Div(7.0f, 3.2f));
            int a = 1;
            Console.WriteLine(OperatorMethods.Increase(a));
            //Console.WriteLine(a++);
            Console.WriteLine(a);
            Console.WriteLine(OperatorMethods.IsSame(1, 2));

        }
    }

}
