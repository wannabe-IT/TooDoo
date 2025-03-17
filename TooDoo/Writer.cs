namespace TooDoo;
public class Writer
{
    public void WriteReadedTodos(List<Todo> todos)
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
                // Nejprve vypíše základní informace o todo
                //Console.WriteLine(todo.TodoToString());
            
                // Potom na další řádek přidá informaci o termínu s odsazením
                int daysLeft = dateOps.CalculateDaysToFinishTodo(todo);
                if (daysLeft > 0)
                {
                    Console.WriteLine(todo.TodoToString());
                    Console.WriteLine($"   {daysLeft} days left");
                    Console.WriteLine();
                }
                else if (daysLeft < 0)
                {
                    Console.WriteLine(todo.TodoToString());
                    Console.WriteLine($"   {Math.Abs(daysLeft)} days past due");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine(todo.TodoToString());
                    Console.WriteLine("   Due today");
                    Console.WriteLine();
                }
            }
        }
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
        using StreamWriter writer = new StreamWriter(pathToFile);

        foreach (Todo todo in todos)
        {
            writer.WriteLine(todo.ToStringToCsv());
        }
    }

    public void CreateTodoFile(string directory)
    {
        using (StreamWriter writer = new StreamWriter(directory));
        Console.WriteLine("TODO file created...");
    }
}