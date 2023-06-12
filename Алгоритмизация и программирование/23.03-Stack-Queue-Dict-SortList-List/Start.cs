using PersonalMenu;

namespace Learning;

class Start
{
    static void Main()
    {
        Project.Menu(startText, StartChoising, menuName);
    }

    readonly static string menuName = "Функции Коллекций";
    readonly static string[] startText =
    {
        "1: Список List",
        "2: Сортированный список Sorted List",
        "3: Словарь Dictionary",
        "4: Очередь Queue",
        "5: Стек Stack",
        "6: Выход"
    };
    delegate void Functions(string[] menuText, Project.WhichMenu menuChousing, string menuName = "Меню");

    static void StartChoising(int number)
    {
        Project.MenuText = startText;
        Project.MenuChousing = StartChoising;
        Project.MenuName = menuName;
        Project.MenulastValue = number;
        switch (number)
        {
            case 0: Project.Menu(MyList.text, MyList.Choising, "Действия с коллекцией List"); break;
            case 1: Project.Menu(MySortedList.text, MySortedList.Choising, "Действия с коллекцией Sorted List"); break;
            case 2: Project.Menu(MyDictionary.text, MyDictionary.Choising, "Действия с коллекцией Dictionary"); break;
            case 3: Project.Menu(MyQueue.text, MyQueue.Choising, "Действия с коллекцией Queue"); break;
            case 4: Project.Menu(MyStack.text, MyStack.Choising, "Действия с коллекцией Stack"); break;
            case 5: break;
        }
    }
}
