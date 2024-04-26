namespace TooDoo;

public class Reader
{
    Writer writer = new Writer();
    Todo todo = new Todo();
    public int todoLineToEdit(string pathToFile)
    {
        int lineToEdit = -1;
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out lineToEdit) && lineToEdit > 0 && lineToEdit <= todosCounter(pathToFile))
            {
                break;
            }
            if (lineToEdit == 0)
            {
                break;
            }
            Console.Write("Invalid number. Please enter a number between 1 and " + todosCounter(pathToFile) + ": ");
        }
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
        
        List<Todo> todosFromFile = new List<Todo>();
        using StreamReader reader = new StreamReader(Path.Combine(pathToFile));
        line = reader.ReadLine();
        if (line != null)
        {
            while (line != null)
            {
                todo = new Todo();
                todo.FromString(line);
                todosFromFile.Add(todo);
                line = reader.ReadLine();
            }
            return todosFromFile;
        }
        return todosFromFile;
    }
    
    public void ReadTodosFromConsole(List<Todo> todos)
    {
        do
        {
            Console.Clear();
            string title = ReadNonEmptyInput("Write a TODO title: ", "Title cannot be empty. Please enter a valid title...");
            Console.Clear();
            string description = ReadNonEmptyInput("Write a TODO description: ", "Description cannot be empty. Please enter a valid description...");
            int priority = ReadValidPriority();

            Todo newTodo = new Todo(title, description, false, priority, todos.Count + 1);
            todos.Add(newTodo);
            Console.Clear();
            Console.WriteLine("New todo added.\n");
            Console.WriteLine("Want to add another TODO?");
            Console.Write("Write \"y\" for yes or \"n\" for no: ");
        } while (string.Equals(Console.ReadLine(), "y", StringComparison.OrdinalIgnoreCase));
    }

    private string ReadNonEmptyInput(string prompt, string errorMessage)
    {
        string input;
        do
        {
            Console.WriteLine(prompt);
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input)) break;
            Console.WriteLine(errorMessage);
        } while (true);

        return input;
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
            Console.WriteLine("Invalid priority. Please enter a number between 1 and 5: ");
        }
    }

    public void CheckIfTodoFileExist(string pathToFile)
    {
        if (!File.Exists(pathToFile))
        {
            File.Create(pathToFile).Close();
        }
    }

    public bool CheckIfAnyTodoIsInFile(List<Todo> todo)
    {
        bool answer = true;
        if (todo.Count < 1)
        {
            Console.Clear();
            Console.WriteLine("Any todo was found...Press any key to go back to menu.");
            Console.ReadKey();
            answer = false;
        }
        return answer;
    }
}