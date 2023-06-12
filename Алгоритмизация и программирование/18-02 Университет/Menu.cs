namespace Univercity;

class Project
{
    public static string[] menuText = new string[] 
    { 
        "\t\tМеню", 
        "1 : Список всех студентов университета", 
        "2 : Список всех учебных групп",
        "3 : Список всех преподавателей",
        "4 : Преподаватели, у которых имеются должники по их предмету",
        "5 : Список студентов должников",
        "6 : По заданному имени студента определить долги и какой преподаватель их принимает",
        "7 : Список приказов от управленцев",
        "8 : Выход"
    };

    public delegate void Action(int result);

    public static void Chousing1(int result)
    {
        switch (result)
        {
            case 1: Print.AllStudents(); break;
            case 2: Print.AllGroups(); break;
            case 3: Print.AllProfessors(); break;
            case 4: Solution.ProfessorsDebetors(); break;
            case 5: Solution.AllDebetors(); break;
            case 6: Solution.OneDebetor(); break;
            case 7: Solution.OrderList(); break;
            case 8: break;
        }
    }
    public static void Chousing2(int result)
    {
        switch (result)
        {
            case 1: Solution.ProfessorsOrderList(); break;
            case 2: Solution.StudentsOrderList(); break;
            case 3: Menu(menuText,Chousing1); break;
        }
    }
    internal static void Menu(string[] menuText,Action Switch)
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
    //Вывод текста
    static void PrintText(int button, string[] menuText)
    {
        Console.Clear();
        foreach (string item in menuText)
        {
            if (item == menuText[button] && button != 0) Color(item);
            else Console.WriteLine(item);
        }
    }
    //Покраска текста
    static void Color(string text)
    {
        //Console.BackgroundColor = ConsoleColor.Blue;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(text);
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Gray;
    }
    public static void Exit(int num = 0)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("\nНажмите любую кнопку для выхода: ");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.ReadKey();
        Console.Clear();
        if (num == 0) Menu(menuText, Chousing1);
        else if (num == 1) Menu(Solution.menuText, Chousing2);
    }
}