namespace TooDoo;

public class Editor
{
    Writer writer = new();
    public void EditTodo(List<Todo> todo, string pathToFile)
    {
        bool flag = true;
        while (flag)
        {
            Console.Clear();
            int index;
            Console.WriteLine("1. Edit todos");
            Console.WriteLine("2. Back");
            Console.Write("Choose an option: ");
            string menuOption = Console.ReadLine();
            Console.Clear();
            switch (menuOption)
            {
                case "1":
                    while (true)
                    {
                        writer.WriteReadedTodos(todo);
                        Console.WriteLine("Choose a todo to edit: ");
                        string answer = Console.ReadLine();

                        if (int.TryParse(answer, out index) && index > 0 && index <= todo.Count)
                        {
                            break;
                        }
                    }
                    Console.Clear();
                    Console.WriteLine("What do you want to edit?");
                    Console.WriteLine("1. Title");
                    Console.WriteLine("2. Description");
                    Console.WriteLine("3. Priority");
                    Console.WriteLine("4. Mark as done");
                    Console.WriteLine("5. Back");
                    Console.Write("Choose an option: ");

                    string answer2 = Console.ReadLine();
                    bool flag2 = true;
                        
                    while (flag2)
                    {
                        Console.Clear();
                        switch (answer2)
                        {
                            case "1":
                                while (true)
                                {
                                    Console.Clear();
                                    string oldTitle = todo[index - 1].Title;
                                    Console.WriteLine($"Old title: {oldTitle}");
                                    Console.Write("Write a new TODO title: ");
                                    string title = Console.ReadLine();
                                    if (!string.IsNullOrEmpty(title))
                                    {
                                        todo[index - 1].Title = title;
                                        writer.WriteTodosToFile(todo, pathToFile);
                                        break;
                                    }
                                    Console.Clear();
                                }
                                break;

                            case "2":
                                while (true)
                                {
                                    Console.Clear();
                                    string oldDescription = todo[index - 1].Description;
                                    Console.WriteLine($"Old description: {oldDescription}");
                                    Console.Write("Write a TODO description: ");
                                    string description = Console.ReadLine();
                                    if (!string.IsNullOrEmpty(description))
                                    {
                                        todo[index - 1].Description = description;
                                        writer.WriteTodosToFile(todo, pathToFile);
                                        break;
                                    }
                                    Console.Clear();
                                }
                                break;

                            case "3":
                                while (true)
                                {
                                    Console.Clear();
                                    int oldPriority = todo[index - 1].Priority;
                                    Console.WriteLine("Please enter a number between 1 and 5.");
                                    Console.WriteLine($"Old priority: {oldPriority}");
                                    Console.Write("Write a new priority: ");
                                    string answerPriority = Console.ReadLine();
                                    Console.Clear();
                                    bool isPriority = int.TryParse(answerPriority, out int priority);
                                    if (isPriority && priority >= 1 && priority <= 5)
                                    {
                                        todo[index - 1].Priority = priority;
                                        break;
                                    }
                                    Console.Clear();
                                }
                                break;

                            case "4":
                                Console.Clear();
                                Console.WriteLine("Mark as done? (y/n)");
                                todo[index - 1].IsDone = Console.ReadLine().ToLower() == "y";
                                writer.WriteTodosToFile(todo, pathToFile);
                                Console.Clear();
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                                break;

                            case "5":
                                Console.Clear();
                                Console.WriteLine("Back to edit menu.");
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                                flag2 = false;
                                break;

                            default:
                                Console.Clear();
                                Console.WriteLine("Invalid option! Press any key to continue...");
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                                break;
                        }
                        break;
                    }
                    break;

                case "2":
                    Console.WriteLine("Back to main menu.");
                    flag = false;
                    break;
                default:
                    Console.WriteLine("Invalid option! Press any key to continue...");
                    break;        
            }
        }
    }
}