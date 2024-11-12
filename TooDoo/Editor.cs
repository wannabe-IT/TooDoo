namespace TooDoo;

public class Editor
{
    Reader reader = new Reader();

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
    public void EditDate(Todo todo)
    {
        string newDate;
        bool flag = true;
        while (flag)
        {
            Console.Clear();
            Console.WriteLine("Current day is: {0}", todo.Date);
            string newDay = reader.ReadDayFromConsole().ToString();
            string newMonth = reader.ReadMonthFromConsole().ToString();
            string newYear = reader.ReadYearFromConsole().ToString();
            if (reader.CheckValidDate(newDay, newMonth, newYear))
            {
                newDate = newDay + "." + newMonth + "." + newYear;
                Console.Write("New date for this todo is {0}", newDate);
                todo.UpdateDate(newDate);
                flag = false;
            }
        }
        Console.ReadKey();
    }
}