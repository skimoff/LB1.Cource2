namespace Task3;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        PrintQueue printer = new();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n=== МЕНЮ ПРИНТЕРА ===");
            Console.WriteLine("1. Додати завдання до черги");
            Console.WriteLine("2. Переглянути чергу");
            Console.WriteLine("3. Надрукувати наступне завдання");
            Console.WriteLine("4. Надрукувати все");
            Console.WriteLine("5. Показати статистику");
            Console.WriteLine("6. Зберегти статистику у файл");
            Console.WriteLine("0. Вихід");
            Console.Write("Ваш вибір: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Ім'я користувача: ");
                    string user = Console.ReadLine();

                    Console.Write("Назва документа: ");
                    string doc = Console.ReadLine();

                    Console.Write("Пріоритет (1–5, де 1 — найвищий): ");
                    int prio = int.TryParse(Console.ReadLine(), out int p) ? Math.Clamp(p, 1, 5) : 3;

                    printer.AddJob(new PrintJob(user, doc, prio));
                    break;

                case "2":
                    printer.ShowQueue();
                    break;

                case "3":
                    printer.PrintNext();
                    break;

                case "4":
                    printer.PrintAll();
                    break;

                case "5":
                    printer.ShowLog();
                    break;

                case "6":
                    printer.SaveLogToFile();
                    break;

                case "0":
                    running = false;
                    break;

                default:
                    Console.WriteLine(" Невірний вибір!");
                    break;
            }
        }

        Console.WriteLine("\n Програму завершено.");
    }
}