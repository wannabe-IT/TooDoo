namespace TooDoo;

public class DateOperations
{
    private Todo _todo = new Todo();
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
    public void CalculateDaysToFInishTodo(Todo todo)
    {
        int currentDate = System.DateTime.Today.Day;
        int currentMonth = System.DateTime.Today.Month;
        int currentYear = System.DateTime.Today.Year;
        int day;

    }
}