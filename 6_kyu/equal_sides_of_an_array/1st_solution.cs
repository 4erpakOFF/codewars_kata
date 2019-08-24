public class Kata
{
    public static int FindEvenIndex(int[] arr)
    {
        if (arr.Length == 0) return -1;
        var left = new long[arr.Length];
        var right = new long[arr.Length];
        left[0] = arr[0];
        right[arr.Length - 1] = arr[arr.Length - 1];

        for (int i = 1; i < arr.Length; i++) left[i] = left[i - 1] + arr[i];
        for (int j = arr.Length - 2; j >= 0; j--) right[j] = right[j + 1] + arr[j];

        for (int i = 0; i < arr.Length; i++)
            if (left[i] == right[i])
                return i;

        return -1;
    }
}