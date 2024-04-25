namespace TooDoo;


public class Writer
{
    public void WriteReadedTodos(List<Todo> todos)
    {
        for (int i = 0; i < todos.Count; i++)
        {
            Todo todo = todos[i];
            Console.WriteLine(todo.ToString());
        }
    }
    public void WriteUpdatedIndexes(List<Todo> todos, string pathToFile)
    {
        for (int i = 0; i < todos.Count; i++)
        {
            Todo todo = todos[i];
            todo.UpdateIndex(i + 1);
        }
        WriteTodosToFile(todos, pathToFile);
    }
    
    public void WriteTodosToFile(List<Todo> todos, string pathToFile)
    {
        using StreamWriter writer = new StreamWriter(pathToFile);

        foreach (var todo in todos)
        {
            writer.WriteLine(todo.ToStringToCsv());
        }
    }
}