namespace TooDoo;

public class Writer
{
    Menu menu = new Menu();

    public void WriteReadedTodos(List<Todo> todos)
    {
        for (int i = 0; i < todos.Count; i++)
        {
            Todo todo = todos[i];
            Console.WriteLine(todo.ToString());
        }
    }
    public void WriteUpdatedIndexes(List<Todo> todos)
    {
        for (int i = 0; i < todos.Count; i++)
        {
            Todo todo = todos[i];
            todo.Index = i + 1;
        }
        WriteTodosToFile(todos, menu.fullPath);
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