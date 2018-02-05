using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cannibalsSolution
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> allnumbers = new List<int> { 21, 9, 5, 8, 10, 1, 3 };
            List<int> targets = new List<int> { 10, 15 };

            foreach (var target in targets)
            {
                Console.WriteLine($"There are {getCannibalCount(allnumbers, target)} total cannibals.");
            }
        }

        public static int getCannibalCount(List<int> AllNumbers, int Target)
        {
            List<int> cannibals = (from number in AllNumbers where number >= Target select number).ToList();
            List<int> rejects = ((from number in AllNumbers where ((number + AllNumbers.Count() - 1 - cannibals.Count()) >= Target) && number < Target select number).OrderByDescending(x => x)).ToList();
            List<int> possibles = ((from number in AllNumbers where ((number + AllNumbers.Count() - 1 - cannibals.Count()) < Target) select number).OrderBy(x => x)).ToList();

            foreach (var possible in possibles)
            {
                var testAmount = possible;
                for (int i = 0; i < rejects.Count(); i++)
                {
                    testAmount += rejects[i];
                    if (testAmount >= Target)
                    {
                        cannibals.Add(testAmount);
                        rejects = rejects.GetRange(i + 1, rejects.Count() - i - 1);
                        break;
                    }
                }
            }

            return cannibals.Count();
        }
    }
}
