namespace TooDoo;

public class Menu
{
    public string pathToFile;
    public string projectDirectory;
    public string fileName;
    public Writer ConsoleOutput;
    public Reader ConsoleInput;
    public Editor ConsoleEditor;
    public void ShowMenu()
    {
        ConsoleInput = new Reader();
        ConsoleOutput = new Writer();
        ConsoleEditor = new Editor();
        projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        fileName = "Todos.txt";
        pathToFile = Path.Combine(projectDirectory, fileName);

        bool flag = true;
        while (flag)
        {
            ConsoleInput.todosCounter(pathToFile);
            List<Todo> todosFromFile = ConsoleInput.ReadTodosFromFile(pathToFile);
            ConsoleOutput.WriteUpdatedIndexes(todosFromFile, pathToFile);

            Console.Clear();
            Console.WriteLine("1. List tasks");
            Console.WriteLine("2. Add tasks");
            Console.WriteLine("3. Edit tasks");
            Console.WriteLine("4. Delete tasks");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");
            string answer = Console.ReadLine();
            
            switch (answer)
            {
                case "1":
                    ConsoleOutput.WriteUpdatedIndexes(todosFromFile, pathToFile);
                    Console.Clear();
                    ConsoleOutput.WriteReadedTodos(todosFromFile);
                    Console.Write("Press any key to continue...");
                    Console.ReadKey();
                    break;
                case "2":
                    ConsoleInput.ReadTodosFromConsole(pathToFile);
                    break;
                case "3":
                    ConsoleEditor.EditTodo(todosFromFile, pathToFile);
                    break;
                case "4":
                    break;
                case "5":
                    flag = false;
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid option");
                    break;
            }
        }
    }
}