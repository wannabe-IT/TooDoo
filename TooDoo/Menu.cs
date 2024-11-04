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
    public DateOperations DateOperations; 
    public void ShowMenu()
    {
        ConsoleInput = new Reader();
        ConsoleOutput = new Writer();
        ConsoleEditor = new Editor();
        TodoRemover = new Remover();
        DateOperations = new DateOperations();
        
        projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        fileName = "Todos.txt";
        pathToFile = Path.Combine(projectDirectory, fileName);
        List<Todo> listReadedTodosFromFile = ConsoleInput.ReadTodosFromFile(pathToFile);
        
        bool flag = true;
        while (flag)
        {
            //sorting by prio (1- highest, 5- lowest)
            IEnumerable<Todo> sortedTodos = listReadedTodosFromFile.OrderBy(todo => todo.Priority);
            // sorted todos put into list
            listReadedTodosFromFile = sortedTodos.ToList();
            ConsoleOutput.WriteUpdatedIndexes(listReadedTodosFromFile);     
            
            Console.Clear();
            //Console.WriteLine(DateTime.Now);
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
                    ConsoleOutput.WriteReadedTodos(listReadedTodosFromFile);
                    Console.Write("Press any key to continue...");
                    Console.ReadKey();
                    break;
                case "2":
                    Console.Clear();
                    ConsoleInput.ReadTodosFromConsole(listReadedTodosFromFile);
                    break;
                case "3":
                    Console.Clear();
                    ConsoleOutput.WriteReadedTodos(listReadedTodosFromFile);
                    
                    if (0 < listReadedTodosFromFile.Count)
                    {
                        Console.Write("Choose a todo to edit (enter the index): ");
                        int indexToEdit = ConsoleInput.todoLineToEdit(pathToFile);
                        if (indexToEdit == 0)
                        {
                            break;
                        }
                        Console.WriteLine(indexToEdit + "indexToEdit");
                        Todo todoToEdit = listReadedTodosFromFile[indexToEdit - 1];
                        
                        Console.Clear();
                        Console.WriteLine("Choose what to edit:");
                        Console.WriteLine("1. Title");
                        Console.WriteLine("2. Description");
                        Console.WriteLine("3. Priority");
                        Console.WriteLine("4. Date");
                        Console.WriteLine("5. Mark as done");
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
                                ConsoleEditor.EditDate(todoToEdit);
                                break;
                            case "5":
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
                    ConsoleOutput.WriteReadedTodos(listReadedTodosFromFile);
                    if (0 < listReadedTodosFromFile.Count)
                    {
                        Console.Write("Choose a todo to remove: ");
                        int indexToRemove = ConsoleInput.todoLineToEdit(pathToFile);
                        if (indexToRemove == 0)
                        {
                            break;
                        }
                        Todo todoToRemove = listReadedTodosFromFile[indexToRemove - 1];
                        TodoRemover.RemoveTodoByIndex(listReadedTodosFromFile, todoToRemove);
                    }   
                    else
                    {
                        Console.Write("Press any key to continue...");
                        Console.ReadKey();
                    }
                    break;
                case "5":
                    Console.Clear();
                    ConsoleOutput.WriteTodosToFile(listReadedTodosFromFile, pathToFile);
                    Console.WriteLine("Changes saved. Exiting...");
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