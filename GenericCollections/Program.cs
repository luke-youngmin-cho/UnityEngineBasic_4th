using System;
using System.Collections.Generic;

// Generic : 일반화
// 다양한 자료형에 대해서 유동적으로 갖다쓸수 있도록 만드는 형태
// 1 + 1 + 1 + 1 + 1
// 2 + 2 + 2 + 2 + 2 
// 3 + 3 + 3 + 3 + 3 
// 4 + 4 + 4 + 4 + 4 

// n + n + n + n + n
namespace GenericCollections
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // List
            //-------------------------------------------------
            List<int> list_int = new List<int>();
            List<float> list_float = new List<float>();
            List<List<int>> list_list_int = new List<List<int>>();

            // 추가
            list_int.Add(0);
            list_float.Add(1.0f);
            list_list_int.Add(list_int);
            list_list_int.Add(new List<int>());

            // 삭제
            list_int.Remove(0);
            list_list_int.RemoveAt(1);

            // 검색
            //list_int.Find(x => x == 0);
            list_int.Contains(0);

            // LinkedList
            LinkedList<int> linkedList = new LinkedList<int>();
            linkedList.AddLast(0);
            linkedList.AddFirst(1);
            linkedList.AddBefore(linkedList.Find(0), 3);
            Console.WriteLine(linkedList.First);
            linkedList.RemoveLast();
            
            
            // Dictionary

            // Queue

            // Stack
        }
    }
}
