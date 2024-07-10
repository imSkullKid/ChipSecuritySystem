using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChipSecuritySystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var chips = new List<ColorChip>
            {
                new ColorChip(Color.Blue, Color.Yellow),
                new ColorChip(Color.Red, Color.Green),
                new ColorChip(Color.Yellow, Color.Red),
                new ColorChip(Color.Orange, Color.Purple)
            };

            var result = FindChipSequence(chips);

            if (result != null)
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

        static List<ColorChip> FindChipSequence(List<ColorChip> chips)
        {
            var sequence = new List<ColorChip>();
            bool found = FindSequence(chips, Color.Blue, sequence, new HashSet<ColorChip>());

            if (found && sequence.Last().EndColor == Color.Green)
            {
                return sequence;
            }
            return null;
        }

        static bool FindSequence(List<ColorChip> chips, Color currentColor, List<ColorChip> sequence, HashSet<ColorChip> usedChips)
        {
            if (currentColor == Color.Green && sequence.Count > 0)
            {
                return true;
            }

            foreach (var chip in chips)
            {
                if (!usedChips.Contains(chip) && chip.StartColor == currentColor)
                {
                    sequence.Add(chip);
                    usedChips.Add(chip);

                    if (FindSequence(chips, chip.EndColor, sequence, usedChips))
                    {
                        return true;
                    }

                    sequence.RemoveAt(sequence.Count - 1);
                    usedChips.Remove(chip);
                }
            }

            return false;
        }
    }
}