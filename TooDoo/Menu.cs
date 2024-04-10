namespace TooDoo;

public class Menu
{
    public string fullPath;
    public string projectDirectory;
    public string fileName;
    public Writer ConsoleOutput;
    public Reader ConsoleInput;
    public Editor ConsoleEditor;

    public Menu()
    {
        projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        fileName = "Todos.txt";
        fullPath = Path.Combine(projectDirectory, fileName);
    }
    public void ShowMenu()
    {
        ConsoleInput = new Reader();
        ConsoleOutput = new Writer();
        ConsoleEditor = new Editor();

        bool flag = true;
        while (flag)
        {
            ConsoleInput.todosCounter();
            List<Todo> todosFromFile = ConsoleInput.ReadTodosFromFile(fullPath);
            ConsoleOutput.WriteUpdatedIndexes(todosFromFile);

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
                    ConsoleOutput.WriteUpdatedIndexes(todosFromFile);
                    Console.Clear();
                    ConsoleOutput.WriteReadedTodos(todosFromFile);
                    Console.Write("Press any key to continue...");
                    Console.ReadKey();
                    
                    //TODO: Add a new menu to show tasks done and tasks to do
                    /*bool flag2 = true;
                    while (flag2)
                    {
                        todosFromFile = ConsoleInput.ReadTodosFromFile(fullPath);
                        Console.Clear();
                        Console.WriteLine("1. Tasks to do: ");
                        Console.WriteLine("2. Tasks done: ");
                        Console.WriteLine("3. Back");
                        Console.Write("Choose an option: ");
                        string answer2 = Console.ReadLine();
                        switch (answer2)
                        {
                            case "1":
                                Console.Clear();
                                ConsoleOutput.WriteReadedTodos(todosFromFile);
                                Console.Write("Press any key to continue...");
                                Console.ReadKey();
                                break;
                            case "2":
                                Console.Clear();
                                Console.Write("Press any key to continue...");
                                Console.ReadKey();
                                break;
                            case "3":
                                Console.Clear();
                                flag2 = false;
                                break;
                            default:
                                Console.Write("Invalid option! Press any key to continue...");
                                break;
                        }
                        ConsoleInput.todosCounter();
                    }*/
                    break;
                    
                case "2":
                    ConsoleInput.ReadTodosFromConsole();
                    break;
                case "3":
                    ConsoleEditor.EditTodo(todosFromFile);
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