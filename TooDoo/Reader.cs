using System.Text.Json;
namespace TooDoo;

public class Reader
{
    private Todo _todo = new();
    

    public int TodoLineToEdit(string pathToFile)
    {
        int lineToEdit;
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out lineToEdit) && lineToEdit > 0 &&
                lineToEdit <= TodosCounter(pathToFile))
            {
                break;    
            }
            if (lineToEdit != 0)
            {
                Console.Write("Invalid number. Please enter a number between 1 and " + TodosCounter(pathToFile)
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
    private int TodosCounter(string pathToFile)
    {
        int todosCount = 0;
        using StreamReader reader = new StreamReader(Path.Combine(pathToFile));
        while (reader.ReadLine() != null)
        {
            todosCount++;
        }
        _todo.UpdateIndex(todosCount);
        return _todo.Index;
    }
    
    public List<Todo> ReadTodosFromFile(string pathToFile)
    {
        if (!File.Exists(pathToFile))
        {
            Console.WriteLine("Soubor not found!");
            return new List<Todo>();
        }

        try
        {
            string jsonContent = File.ReadAllText(pathToFile);
            if (string.IsNullOrWhiteSpace(jsonContent))
            {
                return new List<Todo>();
            }
            
            return JsonSerializer.Deserialize<List<Todo>>(jsonContent) ?? new List<Todo>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Chyba při čtení souboru: {ex.Message}");
            return new List<Todo>();
        }
    }

    int hour, minute = 0;
    string month;
    string year;
    string date;
    public void ReadTodosFromConsole(List<Todo> todos)
    {
        do
        {
            bool flag = false;
            bool checkifTimeIsValid = false;
            bool checkifDateIsValid = false;
            string title = ReadTitleFromConsole();
            string description = ReadDescriptionFromConsole();
            string day = "";
            string stringHour, stringMinute = "";
            
            
            while (!flag)
            {
                while (!checkifDateIsValid)
                {
                    Console.WriteLine("Write a date till you want to done TODO");
                    day = ReadDayFromConsole().ToString();
                    month = ReadMonthFromConsole().ToString();
                    year = ReadYearFromConsole().ToString();
                    Console.Clear();
                    Console.Write("Entered TODO deadline is {0}.{1}.{2}. Let me validate this date...",
                        day, month, year);
                    Thread.Sleep(1500);
                    bool checkIfDateIsValid = CheckValidDate(day, month, year);
                    Console.WriteLine(checkIfDateIsValid);
                    if (checkIfDateIsValid)
                    {
                        Console.Clear();
                        Console.Write("Correct date.");
                        Thread.Sleep(1500);
                        checkifDateIsValid = true;
                        Console.Clear();
                    }
                }
                
                while (!checkifTimeIsValid)
                {
                    Console.Write("Write hour when you want to finish todo: ");
                    string? input = Console.ReadLine();
                    if (string.IsNullOrEmpty(input) || !int.TryParse(input, out hour))
                    {
                        Console.WriteLine("Please enter a valid number for hour.");
                        continue;
                    }
                   
                    Console.WriteLine("Write minute when you want to finish todo: ");
                    string? input2 = Console.ReadLine();
                    if (string.IsNullOrEmpty(input2) || !int.TryParse(input2, out minute))
                    {
                        Console.WriteLine("Please enter a valid number for minute.");
                        continue;
                    }
                    if ((hour > 24 || hour < 0) || (minute > 60 || minute < 0))
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid time. Please enter a valid time.");
                        checkifTimeIsValid = false;
                        Thread.Sleep(1500);
                        Console.Clear();
                    }
                    else
                    {
                        Console.Clear();
                        Console.Write("Correct time.");
                        checkifTimeIsValid = true;
                        Thread.Sleep(1500);
                        Console.Clear();
                    }
                }
                bool dateAndTimeAreValid = checkifDateIsValid && checkifTimeIsValid;
                if (dateAndTimeAreValid)
                {
                    if (hour < 10)
                    { 
                        stringHour = $"0{hour}";
                    }
                    else
                    {
                        stringHour = hour.ToString();
                    }
                    if (minute < 10)
                    {
                        stringMinute = $"0{minute}";
                    }
                    else
                    {
                        stringMinute = minute.ToString();
                    }
                    
                    date = day + "." + month + "." + year;
                    int priority = ReadPriorityFromConsole();
                    Todo newTodo = new Todo(title, description, false, priority, todos.Count + 1, date, 
                        stringHour, stringMinute);
                    todos.Add(newTodo);
                    flag = true;
                }
            }
            Console.Write("Want to add another TODO? Y/N: ");
            Console.ReadKey();
        } while (string.Equals(Console.ReadLine(), "y", StringComparison.OrdinalIgnoreCase));
    }

    public bool CheckValidDate(string dayFromString, string monthFromString, string yearFromString)
    {
        bool isPossible = false;
        
        int day = int.Parse(dayFromString);
        int month = int.Parse(monthFromString);
        int year = int.Parse(yearFromString);
        
        if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8
            || month == 10 || month == 12)
        {
            isPossible = true;
        }
        else if (month == 2 && day == 29 && IsLeapYear(year))
        {
            isPossible = true;
        }
        else if (month == 2 && day > 28)
        {
            Console.Write("February cannot have more than 28 days and {0} is not a leap year!", year);
            Console.ReadKey();
            Console.Clear();
        }
        else if (month == 2 && day <= 28 && !IsLeapYear(year))
        {
            isPossible = true;
        }
        else if ((month == 4 || month == 6 || month == 9 || month == 11) && day <= 30)
        {
            isPossible = true;
        }
        else if ((month == 4 || month == 6 || month == 9 || month == 11) && day >= 31)
        {
            Console.Clear();
            Console.WriteLine("This month cannot have more than 30 days.");
            Console.ReadKey();
        }
        return isPossible;
    }
    private string ReadNonEmptyInput(string prompt, string errorMessage)
    {
        string? input;
        do
        {
            Console.Write(prompt);
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                break;
            }
            Console.WriteLine(errorMessage);
        } while (true);
        return input;
    }

    private bool IsLeapYear(int year)
    {
        return DateTime.IsLeapYear(year);
    }
    private string ReadTitleFromConsole()
    {
        return ReadNonEmptyInput("Write a TODO title: ", 
            "Title cannot be empty. Please enter a valid title...");
    }

    private string ReadDescriptionFromConsole()
    {
        return ReadNonEmptyInput("Write a TODO description: ",
            "Description cannot be empty. Please enter a valid description...");
    }
    
    private int ReadPriorityFromConsole()
    {
        return ReadValidPriority();
    }

    public int ReadDayFromConsole()
    {
        int day;
        bool validDay;
        do
        {
            Console.Write("Please enter a day: ");
            bool isDay = int.TryParse(Console.ReadLine(), out day);
            if (day > 0 && day < 32)
            {
                validDay = true;
            }
            else
            {
                validDay = false;
                Console.WriteLine("Invalid day. Please enter a valid day.");
                Console.ReadKey();
                Console.Clear();
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
            Console.Write("Please enter a month: ");
            bool isMonth = int.TryParse(Console.ReadLine(), out month);
            if (0 < month && month < 13)
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
            Console.Write("Please enter a year: ");
            bool isYear = int.TryParse(Console.ReadLine(), out year);
            if (isYear && DateTime.Today.Year <=year && year < DateTime.Today.Year + 100)
            {
                validYear = true; 
            }
            else
            {
                Console.WriteLine("Invalid year. You can enter only year between {0} and {1}.",
                    currentYear, DateTime.Today.Year + 100);
            }
        } while (!validYear);
        return year;
    }
    private int ReadValidPriority()
    {
        int priority;
        while (true)
        {
            Console.Write("Write a TODO priority (1-5): ");
            if (int.TryParse(Console.ReadLine(), out priority) && priority >= 1 && priority <= 5)
            {
                return priority;
            }
            Console.Write("Invalid priority. Please enter a number between 1 and 5.");
            Console.ReadKey();
            Console.Clear();
        }
    }

    public bool IsFile(string path)
    {
        return File.Exists(path);
    }
}