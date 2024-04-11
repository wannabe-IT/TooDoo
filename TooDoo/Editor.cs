namespace TooDoo;

public class Editor
{
    public void EditTodoTitle(Todo todo)
    {
        string newTitle;
        Console.WriteLine("Enter new title: ");
        newTitle = Console.ReadLine();
        todo.UpdateTitle(newTitle);
    }
    public void EditTodoDescription(Todo todo)
    {
        string newDescription;
        Console.WriteLine("Enter new description: ");
        newDescription = Console.ReadLine();
        todo.UpdateDescription(newDescription);
    }
    public void EditTodoPriority(Todo todo)
    {
        int newPriority;
        Console.WriteLine("Enter new priority: ");
        newPriority = int.Parse(Console.ReadLine());
        todo.UpdatePriority(newPriority);
    }
    public void EditTodoIsDone(Todo todo)
    {
        Console.WriteLine("Mark as done? (y/n)");
        todo.UpdateIsDone(Console.ReadLine().ToLower() == "y");
    }
}