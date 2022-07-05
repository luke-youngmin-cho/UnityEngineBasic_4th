using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegate
{
    internal class OPs
    {
        public enum OP
        {
            SUM,
            SUB,
            MUL,
            DIV,
            MOD
        }

        public static void Test()
        {
            Program.opDelegate += Sum;
            Program.opDelegate(3, 5);
        }

        public static bool RefreshOP(OP op, ref Program.MyDelegate opDel)
        {
            switch (op)
            {
                case OP.SUM:
                    opDel = Sum;
                    break;
                case OP.SUB:
                    opDel = Sub;
                    break;
                case OP.MUL:
                    opDel = Mul;
                    break;
                case OP.DIV:
                    opDel = Div;
                    break;
                case OP.MOD:
                    opDel = Mod;
                    break;
                default:
                    return false;
            }
            return true;
        }

        public static bool AddOP(OP op, ref Program.MyDelegate opDel)
        {
            switch (op)
            {
                case OP.SUM:
                    opDel += Sum;
                    break;
                case OP.SUB:
                    opDel += Sub;
                    break;
                case OP.MUL:
                    opDel += Mul;
                    break;
                case OP.DIV:
                    opDel += Div;
                    break;
                case OP.MOD:
                    opDel += Mod;
                    break;
                default:
                    return false;
            }
            return true;
        }

        public static bool SubOP(OP op, ref Program.MyDelegate opDel)
        {
            switch (op)
            {
                case OP.SUM:
                    opDel -= Sum;
                    break;
                case OP.SUB:
                    opDel -= Sub;
                    break;
                case OP.MUL:
                    opDel -= Mul;
                    break;
                case OP.DIV:
                    opDel -= Div;
                    break;
                case OP.MOD:
                    opDel -= Mod;
                    break;
                default:
                    return false;
            }
            return true;
        }

        static int Sum(int a, int b)
        {
            Console.WriteLine($"OP : Sum(), result : {a + b}");
            return a + b;
        }

        static int Sub(int a, int b)
        {
            Console.WriteLine($"OP : Sub(), result : {a - b}");
            return a - b;
        }
        static int Mul(int a, int b)
        {
            Console.WriteLine($"OP : Mul(), result : {a * b}");
            return a * b;
        }
        static int Div(int a, int b) => a / b;

        static int Mod(int a, int b) => a % b;
    }
}
