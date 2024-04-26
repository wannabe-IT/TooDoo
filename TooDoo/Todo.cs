namespace TooDoo;

public class Todo
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public bool IsDone { get; private set; }
    public int Priority { get; private set; }
    public int Index { get; private set; }

    public Todo (string title, string description, bool isDone, int priority, int index)
    {
        Title = title;
        Description = description;
        IsDone = isDone;
        Priority = priority;
        Index = index;
    }
    public Todo()
    {
        Priority = 0;
        Title = string.Empty;
        Description = string.Empty;
        Index = 0;
    }

    public void UpdateTitle(string title)
    {
        if (!string.IsNullOrEmpty(title))
        {
            Title = title;
        }
        else
        {
            throw new ArgumentException("Title cannot be empty", nameof(title));
        }
    }
    
    public void UpdateDescription(string description)
    {
        if (!string.IsNullOrEmpty(description))
        {
            Description = description;
        }
        else
        {
            throw new ArgumentException("Description cannot be empty", nameof(description));
        }
    }
    
    public void UpdatePriority(int priority)
    {
        if (priority > 0 && priority <= 5)
        {
            Priority = priority;
        }
        else
        {
            throw new ArgumentException("Priority cannot be negative", nameof(priority));
        }
    }
    
    public void UpdateIsDone(bool isDone)
    {
        IsDone = isDone;
    }
    public int UpdateIndex(int index)
    {
        Index = index;
        return Index;
    }
    
    public void FromString(string readedString)
    {
        string[] splitedString = readedString.Split(";");
        Index = Int32.Parse(splitedString[0]);
        Priority = Int32.Parse(splitedString[1]);
        Title = splitedString[2];
        Description = splitedString[3];
        IsDone = bool.Parse(splitedString[4]);
    }

    public override string ToString()
    {
        return $"{Index}) Title: {Title}\n   Description: {Description}\n   Priority: {Priority}\n";
    }

    public string ToStringWithoutIndex()
    {
        return $"Title: {Title}\n   Description: {Description}\n   Priority: {Priority}\n";
    }
    
    public string ToStringToCsv()
    {
        return Index + ";" + Priority + ";" + Title + ";" + Description + ";" + IsDone;
    }
}