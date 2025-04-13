using System.Globalization;
namespace TooDoo;
public class DateOperations
{
    int hours, minutes;
    public TimeSpan CalculateDaysToFinishTodo(Todo todo)
    {
        if (todo == null)
        {
            throw new ArgumentNullException(nameof(todo), "Todo can not be null.");
        }
        if (string.IsNullOrEmpty(todo.Date))
        {
            return TimeSpan.Zero;
        }
        if (DateTime.TryParseExact(todo.Date, "d.M.yyyy", CultureInfo.InvariantCulture, 
                DateTimeStyles.None, out DateTime todoDate))
        {
            if (!string.IsNullOrEmpty(todo.Hours) && int.TryParse(todo.Hours, out hours))
            {
                todoDate = todoDate.AddHours(hours);
            }
            if (!string.IsNullOrEmpty(todo.Minutes) && int.TryParse(todo.Minutes, out minutes))
            {
                todoDate = todoDate.AddMinutes(minutes);
            }
            DateTime now = DateTime.Now;
            TimeSpan difference = todoDate - now;
            return difference;
        }
        return TimeSpan.Zero;
    }
}