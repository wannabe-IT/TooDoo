using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;


namespace TooDoo
{
    public class MenuArrowControl
    {
        public string pathToFile;
        public string projectDirectory;
        public string fileName;
        public Writer ConsoleOutput;
        public Reader ConsoleInput;
        public Editor ConsoleEditor;
        public Remover TodoRemover;

        private List<string> mainOptions = new List<string>
        {
            "List tasks",
            "Add tasks",
            "Edit tasks",
            "Delete tasks",
            "Exit"
        };

        private List<string> editOptions = new List<string>
        {
            "Edit Title",
            "Edit Description",
            "Edit Priority",
            "Mark as done",
            "Back"
        };

        private int mainSelectedIndex = 0;
        private int editSelectedIndex = 0;
        private bool isInEditMenu = false;

        public void ShowMenu()
        {
            ConsoleInput = new Reader();
            ConsoleOutput = new Writer();
            ConsoleEditor = new Editor();
            TodoRemover = new Remover();
            projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            fileName = "todos.txt";
            pathToFile = Path.Combine(projectDirectory, fileName);
            List<Todo> todosFromFile = ConsoleInput.ReadTodosFromFile(pathToFile);

            bool flag = true;
            Console.Clear();
            DisplayMenu();
            while (flag)
            {
                var key = Console.ReadKey(true).Key;
                bool updateMenu = false;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (isInEditMenu)
                        {
                            editSelectedIndex = (editSelectedIndex == 0) ? editOptions.Count - 1 : editSelectedIndex - 1;
                        }
                        else
                        {
                            mainSelectedIndex = (mainSelectedIndex == 0) ? mainOptions.Count - 1 : mainSelectedIndex - 1;
                        }
                        updateMenu = true;
                        break;
                    case ConsoleKey.DownArrow:
                        if (isInEditMenu)
                        {
                            editSelectedIndex = (editSelectedIndex == editOptions.Count - 1) ? 0 : editSelectedIndex + 1;
                        }
                        else
                        {
                            mainSelectedIndex = (mainSelectedIndex == mainOptions.Count - 1) ? 0 : mainSelectedIndex + 1;
                        }
                        updateMenu = true;
                        break;
                    case ConsoleKey.Enter:
                        if (isInEditMenu)
                        {
                            ExecuteEditOption(todosFromFile);
                        }
                        else
                        {
                            ExecuteMainOption(todosFromFile, ref flag);
                        }
                        Console.Clear();
                        DisplayMenu();
                        break;
                }

                if (updateMenu)
                {
                    Console.Clear();
                    DisplayMenu();
                }
            }
        }

        private void DisplayMenu()
        {
            List<string> options = isInEditMenu ? editOptions : mainOptions;
            int selectedIndex = isInEditMenu ? editSelectedIndex : mainSelectedIndex;

            for (int i = 0; i < options.Count; i++)
            {
                if (i == selectedIndex)
                {
                    Console.Write("--> ");
                }
                else
                {
                    Console.Write("    ");
                }
                Console.WriteLine(options[i]);
            }
        }

        private void ExecuteMainOption(List<Todo> todosFromFile, ref bool flag)
        {
            switch (mainSelectedIndex)
            {
                case 0:
                    ConsoleOutput.WriteReadedTodos(todosFromFile);
                    Pause();
                    break;
                case 1:
                    ConsoleInput.ReadTodosFromConsole(todosFromFile);
                    break;
                case 2:
                    isInEditMenu = true;
                    break;
                case 3:
                    DeleteTask(todosFromFile);
                    break;
                case 4:
                    ConsoleOutput.WriteTodosToFile(todosFromFile, pathToFile);
                    Console.WriteLine("Changes saved. Exiting...");
                    Pause();
                    flag = false;
                    break;
            }
        }

        private void ExecuteEditOption(List<Todo> todosFromFile)
        {
            if (editSelectedIndex == editOptions.Count - 1) // Back
            {
                isInEditMenu = false;
                return;
            }

            ConsoleOutput.WriteReadedTodos(todosFromFile);

            if (todosFromFile.Count > 0)
            {
                Console.Write("Choose a todo to edit (enter the index): ");
                int indexToEdit = ConsoleInput.TodoLineToEdit(pathToFile);
                if (indexToEdit > 0 && indexToEdit <= todosFromFile.Count)
                {
                    Todo todoToEdit = todosFromFile[indexToEdit - 1];
                    switch (editSelectedIndex)
                    {
                        case 0:
                            ConsoleEditor.EditTodoTitle(todoToEdit);
                            break;
                        case 1:
                            ConsoleEditor.EditTodoDescription(todoToEdit);
                            break;
                        case 2:
                            ConsoleEditor.EditTodoPriority(todoToEdit);
                            break;
                        case 3:
                            ConsoleEditor.EditTodoIsDone(todoToEdit);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid index.");
                    Pause();
                }
            }
            else
            {
                Console.WriteLine("No tasks to edit.");
                Pause();
            }
        }

        private void DeleteTask(List<Todo> todosFromFile)
        {
            ConsoleOutput.WriteReadedTodos(todosFromFile);

            if (todosFromFile.Count > 0)
            {
                Console.Write("Choose a todo to remove (enter the index): ");
                int indexToRemove = ConsoleInput.TodoLineToEdit(pathToFile);
                if (indexToRemove > 0 && indexToRemove <= todosFromFile.Count)
                {
                    Todo todoToRemove = todosFromFile[indexToRemove - 1];
                    TodoRemover.RemoveTodoByIndex(todosFromFile, todoToRemove);
                }
                else
                {
                    Console.WriteLine("Invalid index.");
                    Pause();
                }
            }
            else
            {
                Console.WriteLine("No tasks to delete.");
                Pause();
            }
        }

        private void Pause()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
