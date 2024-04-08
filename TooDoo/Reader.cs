namespace TooDoo;

public class Reader
{
    Writer writer = new Writer();
    Menu menu = new Menu();
    Todo todo = new Todo();
    

    public List<Todo> ReadTodosFromFile(string pathToFile)
    {
        string? line;
        List<Todo> todosFromFile = new List<Todo>(); // list of objects
        using StreamReader reader = new StreamReader(Path.Combine(pathToFile));
        line = reader.ReadLine();
        while (line != null)
        {
            todo = new Todo(); //creating new object todo
            todo.FromString(line);
            todosFromFile.Add(todo); //object is added into list of objects
            line = reader.ReadLine();
        }
        return todosFromFile;
    }

    public void ReadTodosFromConsole()
    {
        int priority;
        string title, description, priorityInput = string.Empty;
        
        while (true)
        {
            while (true)
            {
                Console.WriteLine("Write a TODO title: ");
                title = Console.ReadLine();
                if (!string.IsNullOrEmpty(title))
                {
                    break;
                }
                Console.WriteLine("Title cannot be empty. Please enter a valid title.");
            }
            
            while (true)
            {
                Console.WriteLine("Write a TODO description: ");
                description = Console.ReadLine();
                if (!string.IsNullOrEmpty(description))
                {
                    break;
                }
                Console.WriteLine("Description cannot be empty. Please enter a valid description.");
            }
            
            while (true)
            {
                Console.WriteLine("Write a TODO priority (1-5): ");
                if (int.TryParse(Console.ReadLine(), out priority) && priority >= 1 && priority <= 5)
                {
                    break;
                }
                Console.WriteLine("Invalid priority. Please enter a number between 1 and 5.");
            }
            Console.WriteLine("Want to add another TODO? Y/N ");
            string answer = Console.ReadLine();
            if (string.Equals(answer, "n", StringComparison.OrdinalIgnoreCase))
            {
                Todo newTodo = new Todo(title, description, false, priority);
                List<Todo> todos = ReadTodosFromFile(menu.fullPath);
                todos.Add(newTodo);
                writer.WriteTodosToFile(todos, menu.fullPath);
                break;
            }
        }
    }
}