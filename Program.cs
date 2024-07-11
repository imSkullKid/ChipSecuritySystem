using System;
using System.Collections.Generic;
using System.Linq;

namespace ChipSecuritySystem
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 0;
            while(i < 3) { 
            List<ColorChip> chips = GenerateRandomChips(6); // Generate 6 random chips

            var result = FindLongestSequence(chips, Color.Blue, Color.Green);

            if (result.Sequence != null && result.Sequence.Any())
            {
                Console.WriteLine("\nSequence found:");
                foreach (var chip in result.Sequence)
                {
                    Console.WriteLine(chip);
                }
            }
            else
            {
                Console.WriteLine(Constants.ErrorMessage + Environment.NewLine);
            }

            Console.WriteLine("\nUnused chips:");
            foreach (var chip in result.UnusedChips)
            {
                Console.WriteLine($"{chip.StartColor}, {chip.EndColor}");
            }
                i++;
            
        }
            Console.WriteLine("\nPress Enter to exit...");
            Console.ReadLine();
        }

        static List<ColorChip> GenerateRandomChips(int count)
        {
            Random random = new Random();
            List<ColorChip> chips = new List<ColorChip>();

            for (int i = 0; i < count; i++)
            {
                Color startColor = (Color)random.Next(Enum.GetValues(typeof(Color)).Length);
                Color endColor = (Color)random.Next(Enum.GetValues(typeof(Color)).Length);
                ColorChip chip = new ColorChip(startColor, endColor);
                chips.Add(chip);
            }

            return chips;
        }

        static (List<ColorChip> Sequence, List<ColorChip> UnusedChips) FindLongestSequence(List<ColorChip> chips, Color startColor, Color endColor)
        {
            var allSequences = new List<List<ColorChip>>();
            FindSequences(new List<ColorChip>(), chips, startColor, allSequences);

            var longestSequence = allSequences
                .Where(seq => seq.LastOrDefault()?.EndColor == endColor)
                .OrderByDescending(seq => seq.Count)
                .FirstOrDefault();

            var unusedChips = new List<ColorChip>(chips);
            if (longestSequence != null)
            {
                foreach (var chip in longestSequence)
                {
                    unusedChips.Remove(chip);
                }
            }

            return (longestSequence, unusedChips);
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
