namespace TooDoo;

public class Todo
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }
    public int Priority { get; set; }

    public Todo (string title, string description, bool isDone, int priority)
    {
        Title = title;
        Description = description;
        IsDone = isDone;
        Priority = priority;
    }
    public Todo()
    {
        Priority = 0;
        Title = string.Empty;
        Description = string.Empty;
        IsDone = false;
    }
    
    public void FromString(string readedString)
    { 
        string[] splitedString = readedString.Split(";");
        Priority = Int32.Parse(splitedString[0]);
        Title = splitedString[1];
        Description = splitedString[2];
        IsDone = bool.Parse(splitedString[3]);
    }

    public string ToString()
    {
        return $"Priority: {Priority}\nTitle: {Title}\nDescription: {Description}\n";
    }
    
    public string ToStringToCsv()
    {
        return Priority + ";" + Title + ";" + Description + ";" + IsDone;
    }
}