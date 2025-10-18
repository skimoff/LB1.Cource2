namespace Task3;

public class PrintJob
{
    public string User { get; set; }
    public string DocumentName { get; set; }
    public int Priority { get; set; }
    public DateTime TimeAdded { get; set; }

    public PrintJob(string user, string documentName, int priority)
    {
        User = user;
        DocumentName = documentName;
        Priority = priority;
        TimeAdded = DateTime.Now;
    }

    public override string ToString()
    {
        return $"{User,-10} | {DocumentName,-20} | Priority: {Priority} | Added: {TimeAdded:T}";
    }
}