namespace TooDoo;

public class Remover
{
    public void RemoveTodoByIndex(List<Todo> todos, Todo todo)
    {
        Console.Clear();
        todos.Remove(todo);
        Console.Write("Todo removed...Press any key to continue.");
        Console.ReadKey();
    }
}