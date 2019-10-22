using System;

public class Kata
{
    public static long NextBiggerNumber(long n)
    {
        var charArr = n.ToString().ToCharArray();
        var digits = new byte[charArr.Length];
        for (int i = 0; i < digits.Length; i++)
            digits[i] = (byte)(charArr[i] - '0');

        for(int curr = digits.Length-2; curr >= 0; curr--)
        {
            if (digits[curr] == 9) 
                continue;

            var minPos = -1;
            for (int iterator = digits.Length - 1; iterator > curr; iterator--)
            {
                if (digits[iterator] > digits[curr])
                    if (minPos >= 0)
                        minPos = digits[iterator] < digits[minPos] ? iterator : minPos;
                    else
                        minPos = iterator;
            }

            if (minPos != -1)
            {
                var tmp = digits[curr];
                digits[curr] = digits[minPos];
                digits[minPos] = tmp;

                Array.Sort(digits, ++curr, digits.Length - curr);
                long result = 0;
                foreach (var d in digits)
                    result = result * 10 + d;

                return result;
            }
        }
        return -1;
    }
}