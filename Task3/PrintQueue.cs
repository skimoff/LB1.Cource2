namespace Task3;

public class PrintQueue
{
    private List<PrintJob> _queue = new();
    private List<string> _printLog = new();

    public void AddJob(PrintJob job)
    {
        _queue.Add(job);
        Console.WriteLine("Завдання додано до черги.");
    }

    public void PrintNext()
    {
        if (_queue.Count == 0)
        {
            Console.WriteLine("Черга порожня!");
            return;
        }

        
        var nextJob = _queue.OrderBy(j => j.Priority).First();
        _queue.Remove(nextJob);

        Console.WriteLine($"\n Друкується: {nextJob.DocumentName} (користувач: {nextJob.User})...");
        System.Threading.Thread.Sleep(1000); 
        Console.WriteLine(" Друк завершено.");

        string logEntry = $"{DateTime.Now:G} | {nextJob.User} | {nextJob.DocumentName}";
        _printLog.Add(logEntry);
    }

    public void PrintAll()
    {
        while (_queue.Count > 0)
        {
            PrintNext();
        }
    }

    public void ShowQueue()
    {
        if (_queue.Count == 0)
        {
            Console.WriteLine("Черга порожня.");
            return;
        }

        Console.WriteLine("\nПоточна черга друку:");
        foreach (var job in _queue.OrderBy(j => j.Priority))
        {
            Console.WriteLine(job);
        }
    }

    public void ShowLog()
    {
        if (_printLog.Count == 0)
        {
            Console.WriteLine("Статистика поки що порожня.");
            return;
        }

        Console.WriteLine("\n Статистика друку:");
        foreach (var entry in _printLog)
            Console.WriteLine(entry);
    }

    public void SaveLogToFile(string filename = "PrintLog.txt")
    {
        File.WriteAllLines(filename, _printLog);
        Console.WriteLine($" Статистика збережена у файл: {filename}");
    }
}