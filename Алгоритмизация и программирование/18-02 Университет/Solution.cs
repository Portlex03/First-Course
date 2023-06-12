using Univercity;

internal static class Solution
{
    // преподаватели, у которых имеются должники по их предмету
    public static void ProfessorsDebetors()
    {
        Console.Clear();

        // берём преподавателя
        foreach (var professor in Professors.List)
        {
            Print.Color($"\nПреподаватель: {professor.Name}, должники:");

            // смотрим список его предметов
            foreach (var subject in professor.Subjects)
            {
                int count = 0;

                // проходимся по ученикам с этим предметом
                foreach (var student in subject.Students)
                {
                    if (student.Value == 2)
                    {
                        Print.Color($"Студент: ",2);
                        Console.Write(student.Key.Name);
                        Print.Color(" Предмет: ",2);
                        Console.Write(subject.Name);
                        Print.Color(" Оценка: ", 2);
                        Console.WriteLine(student.Value);
                        count++;
                    }
                }
                if (count == 0)
                {
                    Print.Color($"Должников по предмету *{subject.Name}* нет",1);
                }
                Console.WriteLine();
            }
        }
        Project.Exit();
    }

    // список студентов должников
    public static void AllDebetors()
    {
        Console.Clear();
        int number = 1;
        foreach (var student in Students.List)
        {
            bool flag = true;
            foreach (var subject in student.Diary)
            {
                if (subject.Value == 2)
                {
                    if (flag)
                    {
                        Print.Color($"\n{number}) Студент {student.Name}");
                        Print.Color($"группа {student.Group.Name}, долги:");
                        flag = false;
                        number++;
                    }
                    Console.WriteLine($"{subject.Key.Name}: {subject.Value}");
                }
            }
        }
        Project.Exit();
    }

    // по заданному имени студента определить долги 
    // и какой преподаватель их принимает
    public static void OneDebetor()
    {
        Console.Clear();

        Console.WriteLine("Введите фамилию и имя ученика");
        string searchingName = Console.ReadLine();

        bool flag = true;

        // поиск в списке студентов
        foreach (var student in Students.List)
        {
            // если нашли студента из списка
            if (student.Name == searchingName)
            {
                flag = false;

                Print.Color($"\nУченик: {student.Name}, группа: {student.Group.Name}");
                Console.WriteLine("Задолженности:\n");

                int count = 0;

                // просмотр оценок данного ученика
                foreach (var subject in student.Diary)
                {
                    // если имеется задолженность
                    if (subject.Value == 2)
                    {
                        Console.WriteLine($"{subject.Key.Name}: {subject.Value}, преподаватель: {subject.Key.Professor.Name}");
                        count++;
                    }
                }
                // если нет задолженности
                if (count == 0)
                {
                    Console.WriteLine($"Студент {student.Name} не имеет долгов по предметам");
                }
            }
        }
        // если не нашёлся студент
        if (flag)
        {
            Console.WriteLine("\nОшибка: студент с таким именем не найден");
        }
        Project.Exit();
    }

    // список приказов от управленцев
    public static string[] menuText = new string[]
    {
        "\t\tМеню",
        "1 : Приказы для преподавателей",
        "2 : Приказы для студентов",
        "3 : Выход в меню",
    };
    public static void OrderList()
    {
        Console.Clear();

        Project.Menu(menuText, Project.Chousing2);
    }
    public static void ProfessorsOrderList()
    {
        Console.Clear();
        Print.Color("Список приказов для преподавателей:\n");

        for (int i = 0; i < Professors.OrderList.Count; i++)
        {
            Console.WriteLine($"{i + 1}) {Professors.OrderList[i]}");
        }
        Project.Exit(1);
    }
    public static void StudentsOrderList()
    {
        Console.Clear();
        Print.Color("Список приказов для студентов:\n");

        for (int i = 0; i < Students.OrderList.Count; i++)
        {
            Console.WriteLine($"{i + 1}) {Students.OrderList[i]}");
        }
        Project.Exit(1);
    }
}