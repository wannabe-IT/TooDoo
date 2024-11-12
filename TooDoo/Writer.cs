namespace TooDoo;
public class Writer
{
    public void WriteReadedTodos(List<Todo> todos)
    {
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