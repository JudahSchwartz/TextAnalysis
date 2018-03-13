using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreLinq;

namespace TextAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(@"C:\Users\SchwartzPC\source\repos\TextAnalysis\TextAnalysis\Results.txt"))
            {
                var bibleText =
                    File.ReadAllText(@"C:\Users\SchwartzPC\source\repos\TextAnalysis\TextAnalysis\kfira.txt");
                PrintLetterFreq(bibleText, file);
                PrintWordLengthFreq(bibleText, file);
                PrintCommonWordsToFile(bibleText, file);
                Console.WriteLine("Done");
                Console.ReadLine();
            }

        }

        private static void PrintCommonWordsToFile(string bibleText, StreamWriter file)
        {
            file.WriteLine();
            file.WriteLine("100 most common words:");

            var words = bibleText.Split(' ').ToList().Where(w => !int.TryParse(w, out int throwAway));
            var wordCount = words.GroupBy(w => w.ToLower()).ToDictionary(w => w.Key, w => w.Count())
                .OrderByDescending(w => w.Value);
            int i = 0;

           
                foreach (var item in wordCount)
                {
                    
                    file.WriteLine($"Number {++i}: {item.Key} Shows up {item.Value} times. Percentage: %{100 * Math.Round((double) item.Value / words.Count(), 5)}");
                    if (i == 100)
                        break;
                }


            
        }

        private static void PrintWordLengthFreq(string bibleText, StreamWriter file)
        {
            file.WriteLine();
            file.WriteLine("Word Statistics");
            var words = bibleText.Split(' ').ToList().Where(w => !int.TryParse(w, out int throwAway)).Where(w => w != "");//in cases of two spaces in a row
            var wordGroups = words.GroupBy(w => w.Length).OrderBy(w=>w.Key);
            
                foreach (var wordGroup in wordGroups)
                {
                    file.WriteLine(
                        $"Words of Legnth {wordGroup.Key}: {wordGroup.Count()} Percentage: %{100 * Math.Round((double) wordGroup.Count() / words.Count(), 5)}");
                }
            
        }

        private static void PrintLetterFreq(string bibleText, StreamWriter file)
        {
            file.WriteLine();
            file.WriteLine("Letter Statistics:");
            var lettersUsed =
                bibleText
                    .GroupBy(Char.ToUpper)
                    .ToDictionary(g => g.Key, g => g.Count());

           
                for (int i = 'A'; i <= 'Z'; i++)
                {
                    file.WriteLine(
                        $"{(char) i}: Shows up {lettersUsed[(char) i]} times. Percentage: %{100 * Math.Round((double) lettersUsed[(char) i] / bibleText.Length, 5)}");
                }
            
        }

    }
}
