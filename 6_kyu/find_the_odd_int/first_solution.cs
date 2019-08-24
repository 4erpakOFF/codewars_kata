using System.Collections.Generic;

namespace Solution
{
    class Kata
    {
        public static int find_it(int[] seq)
        {
            var dict = new Dictionary<int, int>();
            foreach (var s in seq)
            {
                if (dict.ContainsKey(s))
                    dict[s]++;
                else
                    dict.Add(s, 1);
            }

            foreach (var pair in dict)
                if (pair.Value % 2 == 1)
                    return pair.Key;

            return -1;
        }

    }
}
