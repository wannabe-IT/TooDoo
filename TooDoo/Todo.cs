namespace TooDoo;

public class Todo
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }
    public int Priority { get; set; }
    public int Index { get; set; }

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
        //IsDone = false;
        Index = 0;
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

    public string ToString()
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