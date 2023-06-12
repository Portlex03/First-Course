namespace Learning;
using PersonalMenu;

class Start
{
    delegate void Method();

    static void Main()
    {
        Project.Menu(menuText, ChouseCollection);
    }
    static readonly string[] menuText = new string[]
    {
        "1: Действия с коллекцией ArrayList",
        "2: Действия с коллекцией HashTable",
        "3: Выход"
    };
    static void ChouseCollection(int key)
    {
        Console.Clear();

        Project.MenuName = "Меню";
        Project.MenuText = menuText;
        Project.MenuChousing = ChouseCollection;

        switch (key)
        {
            case 0: Project.Menu(ArrayListActions.menuText, ArrayListActions.SelectAction, "Действия с коллекцией ArrayList"); break;
            case 1: Project.Menu(HashTableActions.menutext, HashTableActions.SelectAction, "Действия с коллекцией HashTable"); break;
            case 2: break;
        }
    }
}