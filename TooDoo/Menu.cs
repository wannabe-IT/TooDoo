namespace TooDoo;

public class Menu
{
    public void ShowMenu()
    {
        Console.WriteLine("1. List tasks");
        Console.WriteLine("2. Add tasks");
        Console.WriteLine("3. Edit tasks");
        Console.WriteLine("4. Delete tasks");
        Console.WriteLine("5. Exit");
        Console.Write("Choose an option: ");
        string answer = Console.ReadLine();
        while (true)
        {
            {
                switch (answer)
                {
                    case "1":
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    case "4":
                        break;
                    case "5":
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }
    }
}