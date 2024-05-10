namespace TooDoo;

public class Menu
{
    public string pathToFile;
    public string projectDirectory;
    public string fileName;
    public Writer ConsoleOutput;
    public Reader ConsoleInput;
    public Editor ConsoleEditor;
    public Remover TodoRemover;
    public void ShowMenu()
    {
        ConsoleInput = new Reader();
        ConsoleOutput = new Writer();
        ConsoleEditor = new Editor();
        TodoRemover = new Remover();
        projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        fileName = "Todos.txt";
        pathToFile = Path.Combine(projectDirectory, fileName);
        List<Todo> todosFromFile = ConsoleInput.ReadTodosFromFile(pathToFile);
        
        bool flag = true;
        while (flag)
        {
            ConsoleInput.todosCounter(pathToFile);
            ConsoleOutput.WriteUpdatedIndexes(todosFromFile, pathToFile);
            
            Console.Clear();
            Console.WriteLine(DateTime.Now);
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
                    Console.Clear();
                    ConsoleOutput.WriteReadedTodos(todosFromFile);
                    Console.Write("Press any key to continue...");
                    Console.ReadKey();
                    break;
                case "2":
                    Console.Clear();
                    ConsoleInput.ReadTodosFromConsole(todosFromFile);
                    break;
                case "3":
                    Console.Clear();
                    ConsoleOutput.WriteReadedTodos(todosFromFile);
                    
                    if (0 < todosFromFile.Count)
                    {
                        Console.Write("Choose a todo to edit (enter the index): ");
                        int indexToEdit = ConsoleInput.todoLineToEdit(pathToFile);
                        Todo todoToEdit = todosFromFile[indexToEdit - 1];
                        
                        Console.Clear();
                        Console.WriteLine("Choose what to edit:");
                        Console.WriteLine("1. Title");
                        Console.WriteLine("2. Description");
                        Console.WriteLine("3. Priority");
                        Console.WriteLine("4. Mark as done");
                        Console.Write("Enter your choice: ");
                        string editChoice = Console.ReadLine();
                        switch (editChoice)
                        {
                            case "1":
                                ConsoleEditor.EditTodoTitle(todoToEdit);
                                break;
                            case "2":
                                ConsoleEditor.EditTodoDescription(todoToEdit);
                                break;
                            case "3":
                                ConsoleEditor.EditTodoPriority(todoToEdit);
                                break;
                            case "4":
                                ConsoleEditor.EditTodoIsDone(todoToEdit);
                                break;
                            default:
                                Console.WriteLine("Invalid choice.");
                                break;
                        }
                    }
                    else
                    {
                        Console.Write("Press any key to continue...");
                        Console.ReadKey();
                    }
                    break;
                case "4":
                    Console.Clear();
                    ConsoleOutput.WriteReadedTodos(todosFromFile);
                    if (0 < todosFromFile.Count)
                    {
                        Console.Write("Choose a todo to remove (enter the index): ");
                        int indexToRemove = ConsoleInput.todoLineToEdit(pathToFile);
                        Todo todoToRemove = todosFromFile[indexToRemove - 1];
                        TodoRemover.RemoveTodoByIndex(todosFromFile ,todoToRemove);
                    }
                    else
                    {
                        Console.Write("Press any key to continue...");
                        Console.ReadKey();
                    }
                    break;
                case "5":
                    Console.Clear();
                    ConsoleOutput.WriteTodosToFile(todosFromFile, pathToFile);
                    Console.WriteLine("Changes saved. Exiting...");
                    Console.ReadKey();
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