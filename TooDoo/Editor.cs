namespace TooDoo;

public class Editor
{
    public void EditTodoTitle(Todo todo)
    {
        Console.Clear();
        string newTitle;
        Console.WriteLine("Current title is: {0}", todo.Title);
        Console.Write("Enter new title: ");
        newTitle = Console.ReadLine();
        todo.UpdateTitle(newTitle);
    }
    public void EditTodoDescription(Todo todo)
    {
        string newDescription;
        Console.Clear();
        Console.WriteLine("Current description is: {0}", todo.Description);
        Console.Write("Enter new description: ");
        newDescription = Console.ReadLine();
        todo.UpdateDescription(newDescription);
    }
    public void EditTodoPriority(Todo todo)
    {
        int newPriority;
        Console.Clear();
        Console.WriteLine("Current priority is: {0}", todo.Priority);
        Console.Write("Enter new priority: ");
        newPriority = int.Parse(Console.ReadLine());
        todo.UpdatePriority(newPriority);
    }
    public void EditTodoIsDone(Todo todo)
    {
        Console.Clear();
        Console.Write("Mark as done? (y/n)");
        todo.UpdateIsDone(Console.ReadLine().ToLower() == "y");
    }
}