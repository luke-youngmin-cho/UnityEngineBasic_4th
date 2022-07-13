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

            List<int> list = new List<int>();
            list.Add(5);
            list.Remove(5);
            list.RemoveAt(0);
        }
    }
}
