public class Kata
{
    public static int FindEvenIndex(int[] arr)
    {
        if (arr.Length == 0) return -1;
        var rSum = 0;
        var lSum = 0;
        foreach (var a in arr) rSum += a;
        for (var i = 0; i < arr.Length; i++)
        {
            lSum += arr[i];
            if (lSum == rSum) return i;
            rSum -= arr[i];
        }

        return -1;
    }
}