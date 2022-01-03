using System;
using System.Linq;

namespace Ejudge_92_QuickSort
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = int.Parse(Console.ReadLine());
            int[] array = Console.ReadLine().Trim().Split().Select(int.Parse).ToArray();

            //QuickSort.Sort(array);
            MergeSortClass.Sort(array);

            Console.WriteLine(string.Join(" ", array));
        }
    }
    static class QuickSort
    {
        public static int[] Sort(int[] data)
        {
            return Quicksort(data, 0, data.Length - 1);
        }

        static int[] Quicksort(int[] elements, int left, int right)
        {
            int i = left, j = right;
            var pivot = elements[(left + right) / 2];
            while (i <= j)
            {
                while (elements[i].CompareTo(pivot) < 0)
                {
                    i++;
                }

                while (elements[j].CompareTo(pivot) > 0)
                {
                    j--;
                }

                if (i <= j)
                {
                    // Поменять элементы местами
                    var temp = elements[i];
                    elements[i] = elements[j];
                    elements[j] = temp;

                    i++;
                    j--;
                }
            }

            // Рекурсивные вызовы
            if (left < j)
            {
                Quicksort(elements, left, j);
            }

            if (i < right)
            {
                Quicksort(elements, i, right);
            }

            return elements;
        }
    }

    static class MergeSortClass
    {
        public static int[] Sort(int[] data)
        {
            return MergeSort(data, 0, data.Length - 1);
        }


        static void Merge(int[] array, int lowIndex, int middleIndex, int highIndex)
        {
            var left = lowIndex;
            var right = middleIndex + 1;
            var tempArray = new int[highIndex - lowIndex + 1];
            var index = 0;

            while ((left <= middleIndex) && (right <= highIndex))
            {
                if (array[left] < array[right])
                {
                    tempArray[index] = array[left];
                    left++;
                }
                else
                {
                    tempArray[index] = array[right];
                    right++;
                }

                index++;
            }

            for (var i = left; i <= middleIndex; i++)
            {
                tempArray[index] = array[i];
                index++;
            }

            for (var i = right; i <= highIndex; i++)
            {
                tempArray[index] = array[i];
                index++;
            }

            for (var i = 0; i < tempArray.Length; i++)
            {
                array[lowIndex + i] = tempArray[i];
            }
        }

        //Сортировка слиянием
        static int[] MergeSort(int[] array, int lowIndex, int highIndex)
        {
            if (lowIndex < highIndex)
            {
                var middleIndex = (lowIndex + highIndex) / 2;
                MergeSort(array, lowIndex, middleIndex);
                MergeSort(array, middleIndex + 1, highIndex);
                Merge(array, lowIndex, middleIndex, highIndex);
            }

            return array;
        }
    }
}
