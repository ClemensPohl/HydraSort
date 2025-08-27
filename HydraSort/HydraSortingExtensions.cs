using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HydraSort;

internal static class HydraSortingExtensions
{


#region SortingKowledge
    public static void QueueSort(this int[] array)
    {

        var unsorted = new Queue<int>(array);
        var sorted = new Queue<int>();

        bool isSorted = false;
        while (!isSorted)
        {
            while (unsorted.Count > 1)
            {
                var num1 = unsorted.Dequeue();
                var num2 = unsorted.Dequeue();

                if (num1 < num2)
                {
                    sorted.Enqueue(num1);
                    unsorted.Enqueue(num2);
                }
                else
                {
                    sorted.Enqueue(num2);
                    unsorted.Enqueue(num1);
                    isSorted = false;
                }
            }

            if (unsorted.Count == 1)
            {
                sorted.Enqueue(unsorted.Dequeue());
            }


        }

        array = sorted.ToArray();


    } // bullshit

    public static bool BubbleSort(this int[] array)
    {
        bool isSorted = true;

        for (int i = 0; i < array.Length - 1; i++)
        {
            if (array[i] > array[i + 1])
                isSorted = false;
        }

        if (isSorted)
            return true;

        for (int i = 0; i < array.Length - 1; i++)
        {
            if (array[i] > array[i + 1])
            {
                (array[i + 1], array[i]) = (array[i], array[i + 1]);

                if (BubbleSort(array))
                    return true;
            }

        }

        return false;
    }
#endregion SortingKowledge

    public static bool HydraSort(this int[] array, int headCount = 3)
    {
        bool isSorted = true;
        // == Exit condition ===
        for (int i = 0; i < array.Length - 1; i++)
        {
            if (array[i] > array[i + 1])
            {
                isSorted = false;
                break;
            }
        }
        if (isSorted)
            return true;

        var heads = CreateHeads(array, headCount);
        // == Recursive sorting ==
        foreach (var head in CreateHeads(array, headCount))
        {
            for (int i = 0; i < head.Length - 1; i++)
            {
                if (head[i] > head[i + 1])
                {
                    (head[i + 1], head[i]) = (head[i], head[i + 1]);
                }

            }
        }

        var mergedHeads = MergeHeads(heads);
        Array.Copy(mergedHeads, array, array.Length);

        // after merge sort again
        for (int i = 0; i < array.Length - 1; i++)
        {
            if (array[i] > array[i + 1])
            {
                (array[i + 1], array[i]) = (array[i], array[i + 1]);
            }
        }

        // check if the array is sorted now, its not? then we increase the hydra head count
        if (array.HydraSort(headCount + headCount * 4))
            return true;




        return false;
    }

    static List<int[]> CreateHeads(int[] array, int count)
    {
        var heads = new List<int[]>();
        int headSize = array.Length / count;
        int rest = array.Length % count;

        int start = 0;
        for (int i = 0; i < count; i++)
        {
            int currentSize = headSize + (i < rest ? 1 : 0);
            int[] head = array[start..(start + currentSize)];
            heads.Add(head);
            start += currentSize;
        }
        return heads;
    }

    static int[] MergeHeads(List<int[]> heads)
    {
        int[] result = heads[0];
        for (int i = 1; i < heads.Count; i++)
        {
            result = result.Concat(heads[i]).ToArray();
        }
        return result;
    }
}
