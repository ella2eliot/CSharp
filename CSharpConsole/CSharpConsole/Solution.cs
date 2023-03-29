using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpConsole
{
    internal class Solution
    {
        public int[] Question1(int[] nums, int target)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = i + 1; j < nums.Length; j++)
                {
                    if (nums[j] == target - nums[i])
                    {
                        return new int[] { i, j };
                    }
                }
            };
            return null;
        }
        public int Question2(string s)
        {
            s += "#";
            Dictionary<string, int> mapping = new Dictionary<string, int>();
            mapping.Add("I", 1);
            mapping.Add("IV", 4);
            mapping.Add("V", 5);
            mapping.Add("X", 10);
            mapping.Add("L", 50);
            mapping.Add("C", 100);            
            mapping.Add("D", 500);            
            mapping.Add("M", 1000);

            int result = 0;

            for (int i = 0; i < s.Length; i++)
            {
                if(i+1 <s.Length && mapping.ContainsKey(s.Substring(i, i+2))){
                    result = result + mapping[s.Substring(i, i + 2)];
                    i++;
                }
                if(i<s.Length && mapping.ContainsKey(s.Substring(i, i + 1)))
                {
                    result = result + mapping[s.Substring(i, i + 1)];
                }
            }
            return result;
        }
    }
}
