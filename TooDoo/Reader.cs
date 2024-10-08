using System.Runtime.InteropServices.JavaScript;

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
            string title = ReadNonEmptyInput("Write a TODO title: ", "Title cannot be empty. Please enter a valid title.");
            string description = ReadNonEmptyInput("Write a TODO description: ", "Description cannot be empty. Please enter a valid description.");
            int priority = ReadValidPriority();
            string date = ReadNonEmptyInput("Write a date till you want to done todo: ", "Wrong date!");

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