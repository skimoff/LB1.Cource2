using System.Text.RegularExpressions;

namespace Task2;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        while (true)
        {
            string firstFile = "firstFile.txt";
            if (!File.Exists(firstFile))
            {
                Console.WriteLine("Файл firstFile.txt не знайдено!");
                return;
            }

            var files = File.ReadAllLines(firstFile)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .ToList();

            if (files.Count == 0)
            {
                Console.WriteLine("У файлі firstFile.txt немає назв файлів!");
                return;
            }


            Console.WriteLine("\nДоступні файли:");
            for (int i = 0; i < files.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {files[i]}");
            }


            Console.Write("\nОберіть номер файлу для аналізу: ");
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > files.Count)
            {
                Console.WriteLine("Некоректний вибір!");
                continue;
            }

            string selectedFile = files[choice - 1];
            if (!File.Exists(selectedFile))
            {
                Console.WriteLine($"Файл \"{selectedFile}\" не знайдено!");
                continue;
            }


            string text = File.ReadAllText(selectedFile);


            var words = Regex.Matches(text.ToLower(), @"\b[\p{L}']+\b")
                .Select(m => m.Value)
                .ToList();

            if (words.Count == 0)
            {
                Console.WriteLine("У цьому файлі немає слів.");
                continue;
            }


            var wordCount = words
                .GroupBy(w => w)
                .ToDictionary(g => g.Key, g => g.Count());


            Console.WriteLine($"\nСтатистика для файлу: {selectedFile}");
            foreach (var pair in wordCount.OrderByDescending(p => p.Value))
            {
                Console.WriteLine($"{pair.Key,-15} : {pair.Value}");
            }


            Console.Write("\nЗберегти статистику у файл? (так/ні): ");
            string saveChoice = Console.ReadLine()?.Trim().ToLower();

            if (saveChoice == "так" || saveChoice == "y" || saveChoice == "yes")
            {
                string resultFile = $"stat_{Path.GetFileNameWithoutExtension(selectedFile)}.txt";
                using (StreamWriter sw = new StreamWriter(resultFile))
                {
                    foreach (var pair in wordCount.OrderByDescending(p => p.Value))
                    {
                        sw.WriteLine($"{pair.Key,-15} : {pair.Value}");
                    }
                }

                Console.WriteLine($"Статистика збережена у файл: {resultFile}");
            }


            Console.Write("\nПроаналізувати інший файл? (так/ні): ");
            string again = Console.ReadLine()?.Trim().ToLower();
            if (again != "так" && again != "y" && again != "yes")
            {
                Console.WriteLine("Роботу завершено.");
                break;
            }
        }
    }
}