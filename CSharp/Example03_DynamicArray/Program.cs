using System;
using System.Collections.Generic;

namespace Example03_DynamicArray
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DynamicArray<int> dynamicArray = new DynamicArray<int>();
            dynamicArray.Add(5);
            dynamicArray.Add(4);
            dynamicArray.Add(3);
            dynamicArray.Add(2);
            dynamicArray.Add(1);
            Console.WriteLine(dynamicArray.Length);
            Console.WriteLine(dynamicArray.Capacity);
            Console.WriteLine(dynamicArray[3]);
            dynamicArray[3] = 10;
            Console.WriteLine(dynamicArray[3]);

            for (int i = 0; i < dynamicArray.Length; i++)
            {
                Console.WriteLine(dynamicArray[i]);
            }

            Console.WriteLine("My enum test");
            IEnumerator<int> myEnum = dynamicArray.GetEnumerator();
            while (myEnum.MoveNext())
            {
                Console.WriteLine(myEnum.Current);
            }

            myEnum.Reset();
            while (myEnum.MoveNext())
            {
                Console.WriteLine(myEnum.Current);
            }

            foreach (var item in dynamicArray)
            {
                Console.WriteLine(item);
            }

            List<int> list = new List<int>();
            list.Add(5);
            list.Remove(5);

            List<int>.Enumerator enumerator = list.GetEnumerator();
            
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current);
            }

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }

        public List<int> TestReturnList()
        {
            return new List<int> { 1, 2, 3, };
        }

        public IEnumerator<int> TestReturnEnum()
        {
            return new List<int> { 1, 2, 3 }.GetEnumerator();
        }
    }
}
