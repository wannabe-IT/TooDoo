namespace TooDoo;

public class Remover
{
    public void RemoveTodoByIndex(List<Todo> todos, Todo todo)
    {
        todos.Remove(todo);
    }
}