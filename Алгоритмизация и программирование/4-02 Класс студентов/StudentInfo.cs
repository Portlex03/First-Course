namespace MySpace;
internal class StudentInfo // Заполнение
{
    public string name = "None";
    public string surname = "None";
    public string patronymic = "";

    public int[] date = new int[3];
    public string group = "";
    public static string[] Subjects;
    public int[] score;

    public static void RegisterStudents(ref List<StudentInfo> Students)
    {
        ConsoleKeyInfo consoleKey = new();
        do
        {
            StudentInfo person = new StudentInfo();
            Console.Clear();
            Message("Заполнение информации о студентах");

            // ФИО студента
            Console.Write("\nВведите ФИО студента: ");
            string?[] studentFIO = Console.ReadLine().Split();
            person.surname = studentFIO?[0] ?? "None";
            if (studentFIO?.Length > 1) person.name = studentFIO[1];
            if (studentFIO?.Length > 2) person.patronymic = studentFIO[2];

            //Дата рождения 
            Console.Write("Введите дату рождения студента: ");

            string[] strDate = Console.ReadLine().Split(".");
            if (strDate?.Length > 2)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (!Int32.TryParse(strDate[i], out person.date[i])) person.date[i] = 0;
                }
                if (person.date[0] < 0 || person.date[0] > 31) person.date[0] = 0;
                if (person.date[1] < 0 || person.date[1] > 12) person.date[1] = 0;
                if (person.date[2] < 1923 || person.date[2] > 2023) person.date[2] = 0;

            }

            // Группа
            Console.Write("Введите учебную группу: ");
            string? group = Console.ReadLine();
            person.group = group;

            // Если предметы не введены, то вводятся предметы
            if (Subjects is null && person.group != "")
            {
                int subjectCount;
                Console.Write("Введите количество предметов: ");
                if (!Int32.TryParse(Console.ReadLine(), out subjectCount)) subjectCount = 0;
                Subjects = new string[subjectCount];
                person.score = new int[subjectCount];

                Message("Введите предмет и оценку за него:");
                for (int i = 0; i < subjectCount; i++)
                {
                    string[] input = Console.ReadLine().Split();
                    if (input.Length < 1) break;
                    else
                    {
                        Subjects[i] = input[0];
                        if (input.Length >= 2 && 0 <= Int32.Parse(input[1]) && Int32.Parse(input[1]) <= 5)
                            person.score[i] = Int32.Parse(input[1]);
                        else person.score[i] = 0;
                    }
                }
            }
            else if (person.group != "")
            {
                person.score = new int[Subjects.Length];
                Message("Введите оценку за предмет");
                for (int i = 0; i < Subjects.Length; i++)
                {
                    Console.Write(Subjects[i] + ": ");
                    person.score[i] = Int32.Parse(Console.ReadLine());
                }
            }
            if (person.name != "None") Students.Add(person);
            Message("Для продолжения нажмие *Enter*");
            Message("Для выхода в меню нажмите любую клавишу: ");
            consoleKey = Console.ReadKey();

        } while (consoleKey.Key == ConsoleKey.Enter);
        Console.Clear();
        Project.Menu(Students);

    }
    public static void Message(string mes)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"{mes}");
        Console.ForegroundColor = ConsoleColor.Gray;
    }
}