namespace TooDoo;

public class DateOperations
{
    public System.DateTime GetCurrentDate()
    {
        var currentDate = System.DateTime.Today;
        
        return currentDate;
    }

    public string GetDateFromTodoFile(Todo todo)
    {
        var dates = todo.Date;
        Console.WriteLine(dates);
        return dates;
    }

    public void CalculateDaysToFInishTodo(List<Todo> todos)
    {
        var currentDate = GetCurrentDate().ToShortDateString();
        foreach (var todo in todos)
        {
            var dateFromTodo = DateTime.Parse(todo.Date).ToShortDateString();
            string datum = dateFromTodo;
            Console.WriteLine(datum);
            Console.ReadKey();
            if (datum == "10-03-2022")
            {
                Console.ReadLine();
            }
            Console.WriteLine(datum);
        }
    }
}