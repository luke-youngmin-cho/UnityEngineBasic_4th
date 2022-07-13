using System;
using System.Collections.Generic;
namespace Example06_MyLinkedList
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyLinkedList<int> myLinkedList = new MyLinkedList<int>();
            myLinkedList.AddFirst(1);
            myLinkedList.AddFirst(2);
            myLinkedList.AddBefore(1, 3);

            foreach (var sub in myLinkedList.GetAllNodes())
            {
                Console.WriteLine(sub.value);
            }
            LinkedList<int> list = new LinkedList<int>();
            foreach (var item in list)
            {

            }

        }
    }
}
