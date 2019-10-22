using System;
using System.Collections.Generic;

public class SnailSolution
{
    private class Direction
    {
        public int dI { get; set; }
        public int dJ { get; set; }

        public Direction(int i, int j)
        {
            dI = i;
            dJ = j;
        }
    }

    private static readonly Direction[] snailClockwiseDir = new[]
    {
        new Direction(0, 1),
        new Direction(1, 0),
    };

    public static int[] Snail(int[][] array)
    {
        if (array.Length == 0 || array[0].Length == 0) return new int[0];

        var steps = array.Length - 1;
        var result = new List<int>(steps * steps);

        int i = 0, j = 0;
        int dirIndcator = 1;
        bool isNeedToJump = false;
        while (steps >= -1)
        {
            foreach (var dir in snailClockwiseDir)
            {
                if(isNeedToJump)
                {
                    isNeedToJump = false;
                    result.Add(array[i][j]);
                    i += dir.dI * dirIndcator;
                    j += dir.dJ * dirIndcator;
                }
                for (int currSteps = steps; currSteps > 0; currSteps--)
                {
                    result.Add(array[i][j]);
                    i += dir.dI * dirIndcator;
                    j += dir.dJ * dirIndcator;
                }
            }
            steps--;
            dirIndcator *= -1;
            isNeedToJump = true;
        }

        return result.ToArray();
    }
}
