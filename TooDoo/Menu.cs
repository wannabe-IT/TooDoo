namespace TooDoo;

public class Menu
{
    private string pathToFile = string.Empty;
    private string projectDirectory = string.Empty;
    private string fileName = string.Empty;
    private Writer _consoleOutput = new();
    private Reader _consoleInput = new();
    private Editor _consoleEditor = new();
    private Remover _todoRemover = new();
    
    public void ShowMenu()
    {
        projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        fileName = "todos.json";
        pathToFile = Path.Combine(projectDirectory, fileName);
        if (!_consoleInput.IsFile(pathToFile))
        {
            _consoleOutput.CreateTodoFile(pathToFile);
            Console.ReadKey();
        }
        List<Todo> listReadTodosFromFile = _consoleInput.ReadTodosFromFile(pathToFile);
        bool flag = true;
        while (flag)
        {
            IEnumerable<Todo> sortedTodos = listReadTodosFromFile.OrderBy(todo => todo.Priority);
            listReadTodosFromFile = sortedTodos.ToList();
            _consoleOutput.WriteUpdatedIndexes(listReadTodosFromFile);
            
            Console.Clear();
            Console.WriteLine(DateTime.Now);
            Console.WriteLine("--------MENU--------");
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
                    _consoleOutput.WriteReadTodos(listReadTodosFromFile);
                    //_consoleOutput.WriteTodosIntoDB(listReadTodosFromFile);
                    Console.Write("Press any key to continue...");
                    Console.ReadKey();
                    break;
                case "2":
                    Console.Clear();
                    _consoleInput.ReadTodosFromConsole(listReadTodosFromFile);
                    break;
                case "3":
                    Console.Clear();
                    _consoleOutput.WriteReadTodos(listReadTodosFromFile);
                    
                    if (0 < listReadTodosFromFile.Count)
                    {
                        Console.Write("Choose a todo to edit (enter the index): ");
                        int indexToEdit = _consoleInput.TodoLineToEdit(pathToFile);
                        if (indexToEdit == 0)
                        {
                            break;
                        }
                        Console.Write(indexToEdit + "indexToEdit");
                        Todo todoToEdit = listReadTodosFromFile[indexToEdit - 1];
                        
                        Console.Clear();
                        Console.WriteLine("Choose what to edit:");
                        Console.WriteLine("1. Title");
                        Console.WriteLine("2. Description");
                        Console.WriteLine("3. Priority");
                        Console.WriteLine("4. Date & time");
                        Console.WriteLine("5. Mark as done");
                        Console.Write("Enter your choice: ");
                        string editChoice = Console.ReadLine();
                        switch (editChoice)
                        {
                            case "1":
                                _consoleEditor.EditTodoTitle(todoToEdit);
                                break;
                            case "2":
                                _consoleEditor.EditTodoDescription(todoToEdit);
                                break;
                            case "3":
                                _consoleEditor.EditTodoPriority(todoToEdit);
                                break;
                            case "4":
                                _consoleEditor.EditDate(todoToEdit);
                                break;
                            case "5":
                                _consoleEditor.EditTodoIsDone(todoToEdit);
                                break;
                            default:
                                Console.Write("Invalid choice.");
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
                    _consoleOutput.WriteReadTodos(listReadTodosFromFile);
                    if (0 < listReadTodosFromFile.Count)
                    {
                        Console.Write("Choose a todo to remove: ");
                        int indexToRemove = _consoleInput.TodoLineToEdit(pathToFile);
                        if (indexToRemove == 0)
                        {
                            break;
                        }
                        Todo todoToRemove = listReadTodosFromFile[indexToRemove - 1];
                        _todoRemover.RemoveTodoByIndex(listReadTodosFromFile, todoToRemove);
                    }   
                    else
                    {
                        Console.Write("Press any key to continue...");
                        Console.ReadKey();
                    }
                    break;
                case "5":
                    Console.Clear();
                    _consoleOutput.WriteTodosToFile(listReadTodosFromFile, pathToFile);
                    Console.Write("Changes saved. Exiting...");
                    flag = false;
                    break;
                default:
                    Console.Clear();
                    Console.Write("Invalid option");
                    break;
            }
        }
    }
}