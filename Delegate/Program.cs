using System;

namespace Delegate
{
    internal class Program
    {
        public delegate int MyDelegate(int a, int b);
        public static MyDelegate opDelegate;
        //public static event MyDelegate opDelegate;
        // event 한정자 
        // 이벤트가 선언된 클래스 내에서만 해당 대리자를 호출할 수 있으며 
        // 외부 클래스에서는 구독및 구독취소 ( += 또는 -= ) 만 가능하다. 
        public static Action<int, int> opAction;
        public static Func<int, int> opFunc;
        static void Main(string[] args)
        {
            OPs.RefreshOP(OPs.OP.SUB, ref opDelegate);

            Console.WriteLine(opDelegate(3, 5));

            OPs.AddOP(OPs.OP.MUL, ref opDelegate);

            Console.WriteLine(opDelegate(3, 6));

            // 대리자에 익명메소드 추가
            opAction += delegate (int a, int b)
            {
                Console.WriteLine(a + b);
            };

            // 대리자에 람다식 추가
            opAction += (a, b) => { Console.WriteLine(a + b); };
            
        }

    }
}
