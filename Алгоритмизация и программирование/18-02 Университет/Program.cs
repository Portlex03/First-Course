namespace Univercity;

class Program
{
    static void Main()
    {
        Groups.Create(out Groups.List);

        Professors.Create(out Professors.List);

        Professors.GiveWork(out Professors.List);

        Students.Create(out Students.List);

        Managers.CreateOrderList(ref Managers.OrderList);

        Students.ReadOrders(ref Students.OrderList);

        Professors.ReadOrders(ref Professors.OrderList);

        Project.Menu(Project.menuText, Project.Chousing1);
    }
}

class Managers
{
    public static string[] OrderList = new string[30];
    
    // создание списка приказов
    public static void CreateOrderList(ref string[] managers)
    {
        for (int i = 0; i < OrderList.Length; i++)
        {
            string order = Generate.Order();

            OrderList[i] = order;
        }
    }
}
class Students
{
    public string Name { get; set; }
    public Groups Group { get; set; }
    public Dictionary<Subjects, int> Diary { get; set; } = new();

    public static Students[] List = new Students[100];

    // создание списка студентов
    public static void Create(out Students[] students)
    {
        students = List;

        // Заполнение списка студентов
        for (int i = 0; i < students.Length; i++)
        {
            Students student = new();

            student.Name = Generate.Name();
            student.Group = Generate.StudentGroup(student);

            // присваивание оценки и добавление предмета с оценкой в дневник
            for (int j = 0; j < student.Group.Subjects.Length; j++)
            {
                int mark = Generate.Mark();
                student.Diary.Add(student.Group.Subjects[j], mark);
                student.Group.Subjects[j].Students.Add(student,mark);
            }

            students[i] = student;
        }
    }
    public static char Identificator { get; set; } = 'S';

    public static List<string> OrderList = new();

    // чтение приказов
    public static void ReadOrders(ref List<string> orders)
    {
        for (int i = 0; i < Managers.OrderList.Length; i++)
        {
            string order = Managers.OrderList[i];
            if (Identificator == order[0] || order[0] == 'E') orders.Add(order);
        }
    }

}
class Professors
{
    public string Name { get; set; }
    public int CountSubjects { get; set; } = 0;
    public List<Subjects> Subjects { get; set; } = new();

    public static Professors[] List = new Professors[20];

    // заполнение списка преподавателей
    public static void Create(out Professors[] professors)
    {
        professors = List;

        for (int i = 0; i < professors.Length; i++)
        {
            Professors professor = new();
            professor.Name = Generate.Name();

            professors[i] = professor;
        }
    }
    // связь преподавателей с предметами групп
    public static void GiveWork(out Professors[] professors)
    {
        professors = List;

        // проходимся о всем группам
        for (int i = 0; i < Groups.List.Length; i++)
        {
            // проходимся по всем предметам группы
            for (int j = 0; j < Groups.List[i].Subjects.Length; j++)
            {
                Groups.List[i].Subjects[j].Professor = Generate.Professor(Groups.List[i].Subjects[j]);
            }
        }
    }

    public static char Identificator { get; set; } = 'P';

    public static List<string> OrderList = new();

    // чтение приказов
    public static void ReadOrders(ref List<string> orders)
    {
        for (int i = 0; i < Managers.OrderList.Length; i++)
        {
            string order = Managers.OrderList[i];
            if (Identificator == order[0] || order[0] == 'E') orders.Add(order);
        }
    }
}
class Groups
{
    public string Name { get; set; }
    public int CountPeople { get; set; } = 0;
    public List<Students> Students { get; set; } = new();
    public Subjects[] Subjects { get; set; } = new Subjects[10];

    public static Groups[] List = new Groups[10];

    // создание списка групп
    public static void Create(out Groups[] groups)
    {
        groups = List;

        // заполнение списка групп
        for (int i = 0; i < groups.Length; i++)
        {
            Groups group = new();

            group.Name = Generate.GroupName();
            group.Subjects = Generate.GroupSubjects(group);

            groups[i] = group;
        }
    }
}
class Subjects
{
    public string Name { get; set; }
    public Groups Group { get; set; }
    public Professors Professor { get; set; }
    public Dictionary<Students, int> Students { get; set; } = new();
}

static class Print
{
    public static void AllStudents()
    {
        Console.Clear();
        foreach (var student in Students.List)
        {
            Color($"{student.Name}, {student.Group.Name}");
            foreach (var subject in student.Diary)
            {
                Console.WriteLine($"{subject.Key.Name}: {subject.Value}");
            }
            Console.WriteLine();
        }
        Project.Exit();
    }

    public static void AllProfessors()
    {
        Console.Clear();
        foreach (var professor in Professors.List)
        {
            Color(professor.Name);
            foreach (var subject in professor.Subjects)
            {
                Console.WriteLine($"Предмет: {subject.Name} - группа {subject.Group.Name}");
            }
            Console.WriteLine();
        }
        Project.Exit();
    }

    public static void AllGroups()
    {
        Console.Clear();
        foreach (var group in Groups.List)
        {
            Color(group.Name,1);
            foreach (var subject in group.Subjects)
            {
                Color(subject.Name);
                foreach (var subjectMark in subject.Students)
                {
                    Console.WriteLine($"{subjectMark.Key.Name}: {subjectMark.Value}");
                }
                Console.WriteLine();
            }
        }
        Project.Exit();
    }
    public static void Color(string text, int count = 0)
    {
        if (count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        else if (count == 1)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        else if (count == 2)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
class Info
{
    public static List<string> fileNames = File.ReadAllLines("files/Names.txt").ToList();
    public static string[] fileSubjects = File.ReadAllLines("files/Subjects.txt");
    public static List<string> fileGroups = File.ReadAllLines("files/Groups.txt").ToList();
}
