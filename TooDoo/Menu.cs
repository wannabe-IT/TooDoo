namespace TooDoo;

public class Menu
{
    private Writer _consoleOutput = new();
    private Reader _consoleInput = new();
    private Editor _consoleEditor = new();
    private Remover _todoRemover = new();
    string path;
    string pathToJson;
    public List<Todo> listReadTodosFromFile;
    
    public string checkDependecies()
    {
        string subPath = "Todo";
        string pathToAppdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string fileName =  "todos.json";
        path = Path.Combine(pathToAppdata, subPath);
        pathToJson = Path.Combine(path, fileName);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        if (!File.Exists(pathToJson))
        {
            File.Create(pathToJson).Close();
        }
        return pathToJson;
    }
    
    public void ShowMenu()
    {
        Console.Clear();
        listReadTodosFromFile = _consoleInput.ReadTodosFromFile(pathToJson);
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
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");
            
            string? answer = Console.ReadLine();
            
            switch (answer)
            {
                case "1":
                    Console.Clear();
                    _consoleOutput.WriteReadTodos(listReadTodosFromFile);
                    Console.Write("Press any key to continue...");
                    Console.ReadKey();
                    break;
                case "2":
                    Console.Clear();
                    _consoleInput.ReadTodosFromConsole(listReadTodosFromFile);
                    _consoleOutput.WriteTodosToFile(listReadTodosFromFile, pathToJson);
                    break;
                case "3":
                    Console.Clear();
                    _consoleOutput.WriteReadTodos(listReadTodosFromFile);
                    
                    if (0 < listReadTodosFromFile.Count)
                    {
                        Console.Write("Choose a todo to edit (enter the index): ");
                        int indexToEdit = _consoleInput.TodoLineToEdit(pathToJson);
                        if (indexToEdit == 0)
                        {
                            break;
                        }
                        Todo todoToEdit = listReadTodosFromFile[indexToEdit - 1];
                        
                        Console.Clear();
                        Console.WriteLine("Choose what to edit:");
                        Console.WriteLine("1. Title");
                        Console.WriteLine("2. Description");
                        Console.WriteLine("3. Priority");
                        Console.WriteLine("4. Date & time");
                        Console.WriteLine("5. Mark as done");
                        Console.WriteLine("6. Delete task");
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
                            case "6":
                                _todoRemover.RemoveTodoByIndex(listReadTodosFromFile, todoToEdit);
                                break;
                            default:
                                Console.Write("Invalid choice.");
                                break;
                        }
                        _consoleOutput.WriteTodosToFile(listReadTodosFromFile, pathToJson);
                    }
                    else
                    {
                        Console.Write("Press any key to continue...");
                        Console.ReadKey();
                    }
                    break;
                case "4":
                    Console.Clear();
                    _consoleOutput.WriteTodosToFile(listReadTodosFromFile, pathToJson);
                    Console.Write("Changes saved. Exiting...");
                    flag = false;
                    break;
                default:
                    Console.Clear();
                    Console.Write("Invalid option");
                    Console.ReadKey();
                    break;
            }
        }
    }
}