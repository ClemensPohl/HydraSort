using HydraSort;

var random = new Random();

for (int i = 0; i < 20; i++)
{
    var array = new int[i];
    for (int j = 0; j < i; j++)
    {
        array[j] = random.Next(0, 999);
    }

    array.HydraSort();
    Console.WriteLine($"Count: {i}, Sorted array: [{string.Join(',', array)}]");
}

Console.WriteLine("All your arrays are sorted, tough you wasted a lot of time.");
Console.WriteLine("Press enter to exit.");
Console.ReadKey();

