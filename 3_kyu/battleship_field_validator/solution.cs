using System;
using System.Collections.Generic;

namespace Solution
{
    public class BattleshipField
    {
        private class Battleship
        {
            public readonly int RequiredNum = 0;
            public readonly int Length = 0;

            public Battleship(int length, int maxNum)
            {
                RequiredNum = maxNum;
                Length = length;

                Counter = 0;
            }

            public int Counter { get; set; }
        }

        private static readonly Battleship[] battleships = new[]
        {
            new Battleship(1, 4),
            new Battleship(3, 2),
            new Battleship(2, 3),
            new Battleship(4, 1)
        };

        private static Dictionary<int, Battleship> GetDictionary(Battleship[] ships, out int maxLength)
        {
            var result = new Dictionary<int, Battleship>();
            var currMaxLength = 0;
            foreach (var s in ships)
            {
                result.Add(s.Length, s);
                currMaxLength = s.Length > currMaxLength ? s.Length : currMaxLength;
            }

            maxLength = currMaxLength;
            return result;
        }

        /// <summary>
        /// Returns -1 if battleship is incorrect
        /// </summary>
        private static int GettBattleshipLength(int i, int j, int maxLength, 
                                                int[,] field, bool[,] alreadyVisited)
        {
            alreadyVisited[i, j] = true;
            byte dx = 0, dy = 0;

            if (i < field.GetLength(0) - 1)
                if (field[i + 1, j] == 1)
                    dx = 1;
            if (j < field.GetLength(1)-1)
                if (field[i, j + 1] == 1)
                    dy = 1;

            if(dx == 1 && dy == 1)
                return -1;

            var currLength = 0;
            if (dx != 0 || dy != 0)
            {
                for (int x = i, y = j; x < field.GetLength(0) && y < field.GetLength(1); x += dx, y += dy)
                {
                    alreadyVisited[x, y] = true;

                    if (currLength > maxLength)
                        return -1;
                    if (y < field.GetLength(1) - dx && x < field.GetLength(0) - dy)
                        if (field[x + dy, y + dx] == 1)
                            return -1;

                    if (field[x, y] != 1)
                        break;

                    currLength++;
                }
            }
            else
            {
                currLength = 1;
                if (i < field.GetLength(0) - 1 && j < field.GetLength(1) - 1)
                {
                    if (field[i + 1, j + 1] == 1)
                        return -1;
                    else
                        alreadyVisited[i, j] = true;
                }
            }
            return currLength;
        }

        private static bool isCorrectNumOfShips(int currLength, Dictionary<int,Battleship> ships)
        {
            if (!ships.ContainsKey(currLength))
                return false;
            ships[currLength].Counter++;
            return ships[currLength].Counter <= ships[currLength].RequiredNum;
        }

        private static bool isCorrectNumOfShips (Dictionary<int, Battleship> ships)
        {
            foreach (var s in ships)
                if (s.Value.Counter != s.Value.RequiredNum)
                    return false;
            return true;
        }

        public static bool ValidateBattlefield(int[,] field)
        {
            if (field.Length == 0) 
                return false;

            int maxLength;
            var ships = GetDictionary(battleships, out maxLength);
            var alreadyVisited = new bool[field.GetLength(0),field.GetLength(1)];
            if (maxLength <= 0) 
                return false;

            for (var i = 0; i < field.GetLength(0); i++)
                for(var j = 0; j < field.GetLength(1); j++)
                {
                    if (alreadyVisited[i, j])
                        continue;
                    if (field[i, j] == 1)
                    {
                        var currLength = GettBattleshipLength(i, j, maxLength, field, alreadyVisited);
                        if (currLength > 0 && isCorrectNumOfShips(currLength, ships))
                            continue;
                        else
                            return false;
                    }
                }
            return isCorrectNumOfShips(ships);
        }
    }
}