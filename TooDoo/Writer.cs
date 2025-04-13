namespace TooDoo;
using System.Text.Json;

public class Writer
{
    public void WriteReadTodos(List<Todo> todos)
    {
        var dateOps = new DateOperations();
    
        if (todos.Count == 0)
        {
            Console.WriteLine("Any todo added yet!");
        }
        else
        {
            for (int i = 0; i < todos.Count; i++)
            {
                Todo todo = todos[i];
                Console.WriteLine(todo.TodoToString());
                
                TimeSpan timeLeft = dateOps.CalculateDaysToFinishTodo(todo);
                
                if (timeLeft.TotalMinutes > 0)
                {
                    string timeLeftStr = FormatTimeSpan(timeLeft);
                    Console.WriteLine($"   {timeLeftStr} left");
                }
                else if (timeLeft.TotalMinutes < 0)
                {
                    TimeSpan overdue = timeLeft.Duration();
                    string overdueStr = FormatTimeSpan(overdue);
                    Console.WriteLine($"   {overdueStr} past due");
                }
                else
                {
                    Console.WriteLine("   Due today");
                }
                Console.WriteLine();
            }
        }
    }
    private string FormatTimeSpan(TimeSpan timeSpan)
    {
        List<string> parts = new List<string>();
        
        // Days
        if (timeSpan.Days > 0)
            parts.Add($"{timeSpan.Days} {(timeSpan.Days == 1 ? "day" : "days")}");
        
        // Hours
        if (timeSpan.Hours > 0)
            parts.Add($"{timeSpan.Hours} {(timeSpan.Hours == 1 ? "hour" : "hours")}");
        
        // Minutes
        if (timeSpan.Minutes > 0 || (parts.Count == 0 && timeSpan.Seconds > 0))
            parts.Add($"{timeSpan.Minutes} {(timeSpan.Minutes == 1 ? "minute" : "minutes")}");
        
        // If very short time
        if (parts.Count == 0)
            return "less than a minute";
        
        return string.Join(", ", parts);
    }

    public void WriteUpdatedIndexes(List<Todo> todos)
    {
        for (int i = 0; i < todos.Count; i++)
        {
            Todo todo = todos[i];
            todo.UpdateIndex(i + 1);
        }
    }

    public void WriteTodosToFile(List<Todo> todos, string pathToFile)
    {
        string jsonString = JsonSerializer.Serialize(todos, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(pathToFile, jsonString);
    }
    
    public void CreateTodoFile(string pathToFile)
    {
        if (!File.Exists(pathToFile))
        {
            File.WriteAllText(pathToFile, "[]");
        }
    }


}
