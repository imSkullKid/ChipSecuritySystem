using System;
using System.Collections.Generic;
using System.Linq;

namespace ChipSecuritySystem
{
    class Program
    {
        static void Main(string[] args)
        {
            List<ColorChip> chips = new List<ColorChip>
            {
                new ColorChip(Color.Blue, Color.Yellow),
                new ColorChip(Color.Red, Color.Green),
                new ColorChip(Color.Yellow, Color.Red),
                new ColorChip(Color.Orange, Color.Purple)
            };

            var result = FindLongestSequence(chips, Color.Blue, Color.Green);

            if (result != null && result.Any())
            {
                Console.WriteLine("Sequence found:");
                foreach (var chip in result)
                {
                    Console.WriteLine(chip);
                }
            }
            else
            {
                Console.WriteLine(Constants.ErrorMessage);
            }

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }

        static List<ColorChip> FindLongestSequence(List<ColorChip> chips, Color startColor, Color endColor)
        {
            var allSequences = new List<List<ColorChip>>();
            FindSequences(new List<ColorChip>(), chips, startColor, allSequences);

            var longestSequence = allSequences
                .Where(seq => seq.LastOrDefault()?.EndColor == endColor)
                .OrderByDescending(seq => seq.Count)
                .FirstOrDefault();

            return longestSequence;
        }

        static void FindSequences(List<ColorChip> current, List<ColorChip> available, Color requiredStart, List<List<ColorChip>> allSequences)
        {
            bool foundAny = false;
            foreach (var chip in available.Where(chip => chip.StartColor == requiredStart))
            {
                foundAny = true;
                var newCurrent = new List<ColorChip>(current) { chip };
                var newAvailable = new List<ColorChip>(available);
                newAvailable.Remove(chip);
                FindSequences(newCurrent, newAvailable, chip.EndColor, allSequences);
            }

            if (!foundAny)
            {
                allSequences.Add(new List<ColorChip>(current));
            }
        }
    }
}
