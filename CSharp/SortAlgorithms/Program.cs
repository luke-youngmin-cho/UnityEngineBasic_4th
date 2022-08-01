using System;

namespace SortAlgorithms
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 3, 10, 6, 7, 7, 5, 11 };

            BubbleSort.Sort(arr);
            SelectionSort.Sort(arr);
            InsertionSort.Sort(arr);
            MergeSort.Sort(arr, 0, arr.Length - 1);
            QuickSort.Sort(arr, 0, arr.Length - 1);
            HeapSort.Sort(arr);

            for (int i = 0; i < arr.Length; i++)
                Console.Write($"{arr[i]}, ");
        }
    }
}
