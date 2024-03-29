using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery
{
    public class Lottery539
    {
        public Lottery539()
        {

        }

        public void GetMiniSets()
        {
            List<int> numbers = Enumerable.Range(1, 13).ToList(); // 生成1到13的數字列表
            List<List<int>> combinations = GenerateCombinations(numbers, 5); // 生成所有的組合
            int minCombinationCount = FindMinCombinationCount(combinations); // 找到最小組合數
            Console.WriteLine($"最小組合數：{minCombinationCount}");

            // 打印具有最小组合数的所有组合中的数字
            Console.WriteLine("具有最小组合数的组合中的数字:");
            foreach (var combination in combinations.Where(c => GetCombinationCount(c, combinations) == minCombinationCount))
            {
                Console.WriteLine(string.Join(", ", combination));
            }
        }
        
        private List<List<int>> GenerateCombinations(List<int> numbers, int k)
        {
            List<List<int>> combinations = new List<List<int>>();
            List<int> currentCombination = new List<int>();
            GenerateCombinationsHelper(numbers, k, 0, currentCombination, combinations);
            return combinations;
        }

        private void GenerateCombinationsHelper(List<int> numbers, int k, int start, List<int> currentCombination, List<List<int>> combinations)
        {
            if (k == 0)
            {
                combinations.Add(new List<int>(currentCombination));
                return;
            }

            for (int i = start; i <= numbers.Count - k; i++)
            {
                currentCombination.Add(numbers[i]);
                GenerateCombinationsHelper(numbers, k - 1, i + 1, currentCombination, combinations);
                currentCombination.Remove(numbers[i]);
            }
        }
        private int FindMinCombinationCount(List<List<int>> combinations)
        {
            int minCombinationCount = int.MaxValue;
            foreach (var combination in combinations)
            {
                int combinationCount = GetCombinationCount(combination, combinations);
                if (combinationCount < minCombinationCount)
                {
                    minCombinationCount = combinationCount;
                }
            }
            return minCombinationCount;
        }
        private int GetCombinationCount(List<int> combination, List<List<int>> combinations)
        {
            int count = 0;
            foreach (var c in combinations)
            {
                if (c.Intersect(combination).Count() > 1)
                {
                    count++;
                }
            }
            return count;
        }
    }
}
