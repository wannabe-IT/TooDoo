namespace TooDoo;

public class DateOperations
{
    private Todo _todo = new Todo();

    public int CalculateDaysToFinishTodo(Todo todo)
    {
        if (DateTime.TryParse(todo.Date, out DateTime todoDate))
        {
            // Používáme pouze datum bez času pro přesné porovnání dnů
            DateTime todayDate = DateTime.Today;
        
            // Rozdíl ve dnech (todoDate - todayDate)
            TimeSpan difference = todoDate - todayDate;
            int daysToFinish = difference.Days;
        
            return daysToFinish;
        }
        return 0;
    }

}