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
        int hour, minute;
        string stringHour = "", stringMinute = "";
        try
        {
            Console.Clear();
            Console.WriteLine("Current date is: {0}", todo.Date);

            // Day input
            Console.Write("Enter day (1-31): ");
            string newDay = Console.ReadLine();
            int day = int.Parse(newDay);
            if (day < 1 || day > 31)
            {
                Console.WriteLine("Invalid day.");
                Console.ReadKey();
                return;
            }

            // Month input
            Console.Write("Enter month (1-12): ");
            string newMonth = Console.ReadLine();
            int month = int.Parse(newMonth);
            if (month < 1 || month > 12)
            {
                Console.WriteLine("Invalid month.");
                Console.ReadKey();
                return;
            }

            // Year input
            Console.Write("Enter year (e.g. 2024): ");
            string newYear = Console.ReadLine();
            int year = int.Parse(newYear);
            if (year < 2000 || year > 2100)
            {
                Console.WriteLine("Invalid year.");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            Console.WriteLine("Current time is: {0}:{1}", todo.Hours, todo.Minutes);
            Console.WriteLine("Enter hour (24h): ");
            string newHour = Console.ReadLine();
            hour = int.Parse(newHour);
            if (hour < 0 || hour > 23)
            {
                throw new Exception("Invalid hour!");
            }

            Console.WriteLine("Enter minute: ");
            string newMinute = Console.ReadLine();
            minute = int.Parse(newMinute);
            if (minute < 0 || minute > 59)
            {
                throw new Exception("Invalid minute!");
            }

            // Basic date validation
            bool isValid = false;
            if (month == 2)
            {
                // February
                bool isLeapYear = DateTime.IsLeapYear(year);
                if ((isLeapYear && day <= 29) || (!isLeapYear && day <= 28))
                {
                    isValid = true;
                }
            }
            else if ((month == 4 || month == 6 || month == 9 || month == 11) && day <= 30)
            {
                // Months with 30 days
                isValid = true;
            }
            else if (day <= 31)
            {
                // Other months
                isValid = true;
            }

            if (isValid)
            {
                stringHour = hour.ToString();
                stringMinute = minute.ToString();

                if (hour < 10)
                {
                    stringHour = $"0{hour}";
                }

                if (minute < 10)
                {
                    stringMinute = $"0{minute}";
                }

                string newDateToConsole = $"{day}.{month}.{year} - {stringHour}:{stringMinute}";
                string newDate = $"{day}.{month}.{year}";

                Console.WriteLine($"New date for this todo is {newDateToConsole}");
                todo.UpdateDate(newDate);
                todo.UpdateHours(stringHour);
                todo.UpdateMinutes(stringMinute);

            }
            else
            {
                Console.WriteLine("Invalid date!");
            }

            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error entering date: {ex.Message}");
            Console.ReadKey();
        }
    }
}