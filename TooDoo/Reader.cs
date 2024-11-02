namespace TooDoo;

public class Reader
{
    Todo todo = new Todo();

    public int todoLineToEdit(string pathToFile)
    {
        int lineToEdit;
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out lineToEdit) && lineToEdit > 0 &&
                lineToEdit <= todosCounter(pathToFile))
            {
                break;    
            }
            if (lineToEdit != 0)
            {
                Console.WriteLine("Invalid number. Please enter a number between 1 and " + todosCounter(pathToFile)
                    + " or press enter to go back.");
            }
            if (lineToEdit == 0)
            {
                break;
            }
        }
        Console.WriteLine(lineToEdit);
        return lineToEdit;
    }

    public int todosCounter(string pathToFile)
    {
        int todosCount = 0;
        using StreamReader reader = new StreamReader(Path.Combine(pathToFile));
        while (reader.ReadLine() != null)
        {
            todosCount++;
        }
        todo.UpdateIndex(todosCount);
        return todo.Index;
    }
    
    public List<Todo> ReadTodosFromFile(string pathToFile)
    {
        string? line;

        List<Todo> todosFromFile = new List<Todo>(); // list of objects
        using StreamReader reader = new StreamReader(Path.Combine(pathToFile));
        line = reader.ReadLine();
        while (line != null)
        {
            todo = new Todo();
            todo.FromString(line);
            todosFromFile.Add(todo); //object is added into list of objects
            line = reader.ReadLine();
        }
        return todosFromFile;
    }
    public void ReadTodosFromConsole(List<Todo> todos)
    {
        do
        {
            string title = ReadTitleFromConsole();
            string description = ReadDescriptionFromConsole();
            Console.WriteLine("Write a date till you want to done TODO");
            string day = ReadDayFromConsole().ToString();
            string month = ReadMonthFromConsole().ToString();
            string year = ReadYearFromConsole().ToString();
            string date = day + "." + month + "." + year;
            int priority = ReadPriorityFromConsole();
            
            Todo newTodo = new Todo(title, description, false, priority, todos.Count + 1, date);
            todos.Add(newTodo);

            Console.WriteLine("Want to add another TODO? Y/N ");
        } while (string.Equals(Console.ReadLine(), "y", StringComparison.OrdinalIgnoreCase));
    }

    private string ReadNonEmptyInput(string prompt, string errorMessage)
    {
        string? input;
        do
        {
            Console.WriteLine(prompt);
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                break;
            }
            Console.WriteLine(errorMessage);
        } while (true);
        return input;
    }

    public string ReadTitleFromConsole()
    {
        return ReadNonEmptyInput("Write a TODO title: ", 
            "Title cannot be empty. Please enter a valid title...");
    }

    public string ReadDescriptionFromConsole()
    {
        return ReadNonEmptyInput("Write a TODO description: ",
            "Description cannot be empty. Please enter a valid description...");
    }
    
    public int ReadPriorityFromConsole()
    {
        return ReadValidPriority();
    }

    public int ReadDayFromConsole()
    {
        int day;
        bool validDay = false;
        do
        {
            Console.WriteLine("Please enter a day: ");
            bool isDay = int.TryParse(Console.ReadLine(), out day);
            if (isDay && day > 0 && day < 32)
            {
                validDay = true;
            }
            else
            {
                validDay = false;
                Console.WriteLine("Invalid day. Please enter a valid day.");
            }
        } while (!validDay);
        return day;
    }
    public int ReadMonthFromConsole()
    {
        int month;
        bool validMonth = false;
        do
        {
            Console.WriteLine("Please enter a month: ");
            bool isMonth = int.TryParse(Console.ReadLine(), out month);
            if (isMonth && 0 < month && month < 13)
            {
                validMonth = true;
            }
            else
            {
                Console.WriteLine("Invalid month. Please enter a valid month...");
            }
        } while (!validMonth);
        return month;
    }

    public int ReadYearFromConsole()
    {
        int year;
        int currentYear = DateTime.Now.Year;
        bool validYear = false;
        do
        {
            Console.WriteLine("Please enter a year: ");
            bool isYear = int.TryParse(Console.ReadLine(), out year);
            if (isYear && System.DateTime.Today.Year <=year && year < System.DateTime.Today.Year + 100)
            {
                validYear = true; 
            }
            else
            {
                Console.WriteLine("Invalid year. You can enter only years between {0} and {1}.",currentYear, System.DateTime.Today.Year + 100);
            }
        } while (!validYear);
        return year;
    }
    private int ReadValidPriority()
    {
        int priority;
        while (true)
        {
            Console.WriteLine("Write a TODO priority (1-5): ");
            if (int.TryParse(Console.ReadLine(), out priority) && priority >= 1 && priority <= 5)
            {
                return priority;
            }
            Console.WriteLine("Invalid priority. Please enter a number between 1 and 5.");
        }
    }
}