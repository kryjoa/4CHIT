using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<List<char>> eingabe = new List<List<char>>()
        {
            new List<char> { 'K', 'K', 'H', 'H' },
            new List<char> { 'H', 'K', 'K', 'H' },
            new List<char> { 'K', 'K', 'K', 'H' },
            new List<char> { 'K', 'H', 'K', 'H' },
            new List<char> { 'H', 'K', 'H', 'H' },
            new List<char> { 'H', 'H', 'H', 'H' },
            new List<char> { 'K', 'H', 'H', 'H' },
            new List<char> { 'H', 'K', 'K', 'H' },
            new List<char> { 'K', 'H', 'K', 'H' },
            new List<char> { 'K', 'K', 'H', 'H' },
            new List<char> { 'K', 'H', 'H', 'H' },
            new List<char> { 'H', 'H', 'K', 'K' },
            new List<char> { 'H', 'H', 'H', 'K' },
            new List<char> { 'H', 'K', 'H', 'K' },
            new List<char> { 'K', 'H', 'H', 'K' },
            new List<char> { 'K', 'K', 'H', 'K' },
            new List<char> { 'K', 'H', 'H', 'K' },
            new List<char> { 'H', 'H', 'K', 'K' },
            new List<char> { 'H', 'H', 'K', 'K' },
            new List<char> { 'H', 'H', 'K', 'K' },
            new List<char> { 'K', 'H', 'H', 'H' },
            new List<char> { 'H', 'H', 'H', 'K' },
            new List<char> { 'H', 'K', 'H', 'H' },
            new List<char> { 'K', 'K', 'H', 'K' },
            new List<char> { 'H', 'H', 'H', 'H' },
            new List<char> { 'H', 'K', 'K', 'K' },
            new List<char> { 'K', 'K', 'K', 'H' },
            new List<char> { 'H', 'K', 'K', 'H' },
            new List<char> { 'H', 'H', 'K', 'K' },
            new List<char> { 'K', 'H', 'K', 'H' },
            new List<char> { 'K', 'K', 'H', 'H' },
            new List<char> { 'H', 'H', 'H', 'K' },
            new List<char> { 'K', 'H', 'K', 'H' },
            new List<char> { 'K', 'H', 'H', 'H' },
            new List<char> { 'H', 'H', 'K', 'K' },
            new List<char> { 'K', 'H', 'H', 'K' },
            new List<char> { 'K', 'K', 'H', 'H' },
            new List<char> { 'K', 'H', 'H', 'H' },
            new List<char> { 'K', 'H', 'K', 'H' },
            new List<char> { 'K', 'H', 'K', 'K' },
            new List<char> { 'H', 'K', 'H', 'H' },
            new List<char> { 'H', 'K', 'K', 'H' },
            new List<char> { 'K', 'K', 'H', 'K' },
            new List<char> { 'H', 'H', 'H', 'H' },
            new List<char> { 'H', 'K', 'K', 'H' },
            new List<char> { 'H', 'H', 'H', 'K' },
            new List<char> { 'K', 'K', 'H', 'K' },
            new List<char> { 'K', 'K', 'H', 'K' },
            new List<char> { 'H', 'K', 'H', 'H' },
            new List<char> { 'K', 'H', 'K', 'H' },
            new List<char> { 'K', 'K', 'H', 'H' },
            new List<char> { 'K', 'H', 'K', 'K' },
            new List<char> { 'K', 'K', 'H', 'K' },
            new List<char> { 'H', 'H', 'K', 'H' },
            new List<char> { 'K', 'H', 'K', 'H' },
        };

        Dictionary<string, int> countMap = new Dictionary<string, int>();

        foreach (List<char> item in eingabe)
        {
            string key = new string(item.ToArray());
            if (countMap.ContainsKey(key))
            {
                countMap[key]++;
            }
            else
            {
                countMap[key] = 1;
            }
        }

        foreach (KeyValuePair<string, int> pair in countMap)
        {
            Console.WriteLine(pair.Key + " " + pair.Value);
        }
    }
}
