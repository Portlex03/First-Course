namespace PersonalMenu;


public class Project
{
    public delegate void WhichMenu(int result);

    // для выхода в меню из метода
    static string lastMenuName = "Меню";
    static string[] lastMenuText;
    static WhichMenu lastMenuChousing;

    // для выхода в другое меню
    public static string MenuName;
    public static string[] MenuText;
    public static WhichMenu MenuChousing;
    public static int MenulastValue;

    /// <summary>
    /// Вызов меню. menuText - текст меню, menuChousing - делегат, отвечающий за то, 
    /// какие методы будут выполняться в меню, menuName - имя меню. 
    /// </summary>
    public static void Menu(string[] menuText, WhichMenu menuChousing, string menuName = "Меню",int lastValue = 0)
    {
        Console.Clear();
        lastMenuText = menuText;
        lastMenuChousing = menuChousing;
        lastMenuName = menuName;

        Paint.Title(menuName);
        for (int i = 0; i < menuText.Length; i++)
        {
            string item = menuText[i];
            if (i == lastValue) Paint.SubTitle(menuText[i]);
            else Console.WriteLine(item);
        }
        int result = Keys(menuText,lastValue);

        menuChousing(result);
    }
    static int Keys(string[] menuText, int lastValue)
    {
        int key = lastValue;
        bool flag = false;
        do
        {
            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.UpArrow: { key--; if (key != -1) PrintText(key, menuText); }; break;
                case ConsoleKey.DownArrow: { key++; if (key != menuText.Length) PrintText(key, menuText); }; break;
                case ConsoleKey.Enter: { flag = true; }; break;
            }

            if (key == menuText.Length)
            {
                key = 0;
                PrintText(key, menuText);
            }
            if (key == -1)
            {
                key = menuText.Length - 1;
                PrintText(key, menuText);
            }

        } while (!flag);
        return key;
    }

    static void PrintText(int button, string[] menuText)
    {
        Console.Clear();
        Paint.Title(lastMenuName);
        foreach (string item in menuText)
        {
            if (item == menuText[button]) Paint.SubTitle(item);
            else Console.WriteLine(item);
        }
    }

    /// <summary>
    /// Выход из метода в последнее использованное меню.
    /// </summary>
    public static void Exit(int lastValue)
    {
        Paint.Input("\nНажмите любую кнопку для выхода: ");
        Console.ReadKey();
        Menu(lastMenuText, lastMenuChousing, lastMenuName,lastValue);
    }

    /// <summary>
    /// Выход из одного меню в другое
    /// </summary>
    public static void MenuExit(int number)
    {
        Menu(MenuText, MenuChousing, MenuName,number);
    }
}

public class Paint
{
    public static void Write(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write(text);
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    public static void WriteLine(string text, ConsoleColor color) => Write(text + "\n", color);

    public static void Title(string text) => WriteLine(text, ConsoleColor.Yellow);

    public static void Error(string text) => WriteLine(text, ConsoleColor.Red);

    public static void Input(string text) => Write(text, ConsoleColor.Blue);

    public static void SubTitle(string text) => WriteLine(text, ConsoleColor.Cyan);

    public static void SubTitle2(string text) => Write(text, ConsoleColor.Cyan);
}
