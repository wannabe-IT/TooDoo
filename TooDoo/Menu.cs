using System.Threading.Channels;

namespace TooDoo;

public class Menu
{
    private string PathToFile{get;}
    private Writer ConsoleOutput{get;} 
    private Reader ConsoleInput{get;} 
    private Editor ConsoleEditor{get;} 
    private Remover TodoRemover{get;} 
    public Menu()
    {
        ConsoleInput = new Reader();
        ConsoleOutput = new Writer();
        ConsoleEditor = new Editor();
        TodoRemover = new Remover();
        string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        string fileName = "todos.txt";
        PathToFile = Path.Combine(projectDirectory, fileName);
        ConsoleInput.CheckIfTodoFileExist(PathToFile);
    }
    
    public void ShowMenu()
    {
        List<Todo> todosFromFile = ConsoleInput.ReadTodosFromFile(PathToFile);
        ConsoleOutput.WriteUpdatedIndexes(todosFromFile, PathToFile);
        
        bool flag = true;
        while (flag)
        {
            ConsoleInput.todosCounter(PathToFile);
            ConsoleOutput.WriteUpdatedIndexes(todosFromFile, PathToFile);

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
                    bool stopEdit = ConsoleInput.CheckIfAnyTodoIsInFile(todosFromFile);
                    if (stopEdit)
                    {
                        ConsoleOutput.WriteReadedTodos(todosFromFile);
                        Console.Write("Choose a todo to edit (or press any key to go back to menu): ");
                        int indexToEdit = ConsoleInput.todoLineToEdit(PathToFile);
                        if (indexToEdit < 1 || indexToEdit > todosFromFile.Count)
                        {
                            break;
                        }
                        Todo todoToEdit = todosFromFile[indexToEdit - 1];

                        Console.WriteLine("***Choose what to edit***");
                        Console.WriteLine("1. Title");
                        Console.WriteLine("2. Description");
                        Console.WriteLine("3. Priority");
                        Console.WriteLine("4. Mark as done");
                        Console.WriteLine("Press any key to go back to menu...");
                        Console.Write("Enter your choice: ");
                        string editChoice = Console.ReadLine();
                        switch (editChoice)
                        {
                            case "1":
                                Console.Clear();
                                ConsoleEditor.EditTodoTitle(todoToEdit);
                                break;
                            case "2":
                                Console.Clear();
                                ConsoleEditor.EditTodoDescription(todoToEdit);
                                break;
                            case "3":
                                Console.Clear();
                                ConsoleEditor.EditTodoPriority(todoToEdit);
                                break;
                            case "4":
                                Console.Clear();
                                ConsoleEditor.EditTodoIsDone(todoToEdit);
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine("Invalid choice.");
                                break;
                        } 
                    }
                    break;
                case "4":
                    Console.Clear();
                    ConsoleOutput.WriteReadedTodos(todosFromFile);
                    Console.Write("Choose a todo to remove (or press any key to go back to menu): ");
                    int indexToRemove = ConsoleInput.todoLineToEdit(PathToFile);
                    if (indexToRemove < 1 || indexToRemove > todosFromFile.Count)
                    {
                        Console.WriteLine("Back to menu...");
                        Console.ReadKey();
                        break;
                    }
                    Todo todoToRemove = todosFromFile[indexToRemove - 1];
                    TodoRemover.RemoveTodoByIndex(todosFromFile, todoToRemove);
                    break;
                case "5":
                    Console.Clear();
                    todosFromFile = ConsoleInput.ReadTodosFromFile(PathToFile);
                    ConsoleOutput.WriteTodosToFile(todosFromFile, PathToFile);
                    Console.WriteLine("Changes saved. Exiting...");
                    flag = false;
                    break;
                default:
                    Console.Clear();
                    Console.Write("Invalid option!");
                    Console.ReadKey();
                    break;
            }
        }
    }
}