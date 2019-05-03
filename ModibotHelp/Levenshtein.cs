using System;

/// <summary>
/// Modibot help namespace
/// </summary>
namespace ModibotHelp
{
    /// <summary>
    /// Levenshtein class
    /// </summary>
    public static class Levenshtein
    {
        /// <summary>
        /// Get levenshtein distance
        /// Modified code from https://www.dotnetperls.com/levenshtein
        /// </summary>
        /// <param name="left">Left</param>
        /// <param name="right">Right</param>
        /// <returns>Levenshtein distance</returns>
        public static uint GetDistance(string left, string right)
        {
            uint ret = 0U;
            if (left.Length == 0)
            {
                ret = (uint)(right.Length);
            }
            else if (right.Length == 0)
            {
                ret = (uint)(left.Length);
            }
            else
            {
                int[,] distances = new int[left.Length + 1, right.Length + 1];
                for (int i = 0; i <= left.Length; i++)
                {
                    distances[i, 0] = i;
                }
                for (int i = 0; i <= right.Length; i++)
                {
                    distances[0, i] = i;
                }
                for (int i = 1, j; i <= left.Length; i++)
                {
                    for (j = 1; j <= right.Length; j++)
                    {
                        distances[i, j] = Math.Min(Math.Min(distances[i - 1, j] + 1, distances[i, j - 1] + 1), distances[i - 1, j - 1] + ((right[j - 1] == left[i - 1]) ? 0 : 1));
                    }
                }
                ret = (uint)(distances[left.Length, right.Length]);
            }
            return ret;
        }
    }
}
