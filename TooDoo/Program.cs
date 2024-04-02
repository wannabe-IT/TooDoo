using TooDoo;

class Program
{
    static void Main(string[] args)
    {
        Menu menu = new Menu();
        Reader reader = new Reader();
        Todo todo = new Todo();
        menu.ShowMenu();
    }
}