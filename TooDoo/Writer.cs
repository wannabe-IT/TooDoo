namespace TooDoo;

public class Writer
{
    public void WriteReadedTodos(List<Todo> todos)
    {
        foreach (Todo todo in todos)
        {
            if (!todo.IsDone)
            {
                Console.WriteLine(todo.ToString());
            }
        }
    }
    
    public void WriteReadedTodosDone(List<Todo> todos)
    {
        foreach (Todo todo in todos)
        {
            if (todo.IsDone)
            {
                Console.WriteLine(todo.ToString());
            }
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
}