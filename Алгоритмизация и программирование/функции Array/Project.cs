namespace ArrayFunctions;

class Project
{

    public delegate void WhichMenu(int result);

    internal static void Menu(string[] menuText, WhichMenu Switch)
    {
        Console.Clear();
        for (int i = 0; i < menuText.Length; i++)
        {
            string? item = menuText[i];
            Console.WriteLine(item);
        }
        int result = Keys(menuText);

        Switch(result);
    }
    public static int Keys(string[] menuText)
    {
        int num = 0;
        bool flag = false;
        do
        {
            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.UpArrow: { num--; if (num != -1) PrintText(num, menuText); }; break;
                case ConsoleKey.DownArrow: { num++; if (num != menuText.Length) PrintText(num, menuText); }; break;
                case ConsoleKey.Enter: { flag = true; }; break;
            }

            if (num == menuText.Length)
            {
                num = 1;
                PrintText(num, menuText);
            }
            if (num == 0 || num == -1)
            {
                num = menuText.Length - 1;
                PrintText(num, menuText);
            }

        } while (!flag);
        return num;
    }

    static void PrintText(int button, string[] menuText)
    {
        Console.Clear();
        foreach (string item in menuText)
        {
            if (item == menuText[button] && button != 0) Color(item);
            else Console.WriteLine(item);
        }
    }

    public static void Color(string text, bool flag = true)
    {
        Console.ForegroundColor = ConsoleColor.Red;

        if (flag) Console.WriteLine(text);
        else Console.Write(text);

        Console.ForegroundColor = ConsoleColor.Gray;
    }

    public static void Exit()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("\nНажмите любую кнопку для выхода: ");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.ReadKey();
        Console.Clear();
        Menu(MainProg.menuText,ArrayActions.SelectOperation);
    }
}