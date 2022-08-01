using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortAlgorithms
{
    internal class BubbleSort
    {
        public static void Sort(int[] arr)
        {
			int i, j;
			for (i = 0; i < arr.Length; i++)
				for (j = 0; j < arr.Length - 1; j++)
					if (arr[i] > arr[j + 1])
					{
						int tmp = arr[j + 1];
						arr[j + 1] = arr[j];
						arr[j] = tmp;
					}
        }
    }
}
