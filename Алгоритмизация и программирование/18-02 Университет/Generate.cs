namespace Univercity;

static class Generate
{
    static Random index = new Random();

    // присваивание имени
    public static string Name()
    {
        int n = index.Next(Info.fileNames.Count);

        string name = Info.fileNames[n];

        Info.fileNames.RemoveAt(n);

        return name;
    }

    // присваивание имени группы
    public static string GroupName()
    {
        int n = index.Next(Info.fileGroups.Count);

        string groupName = Info.fileGroups[n];

        Info.fileGroups.RemoveAt(n);

        return groupName;
    }

    // присваивание предметов для группы
    public static Subjects[] GroupSubjects(Groups group)
    {
        string[] subjectNames = new string[10];

        for (int i = 0; i < group.Subjects.Length; i++)
        {
            Subjects subject = new();
            
            // присвоение уникального премета 
            int n;
            do
            {
                n = index.Next(Info.fileSubjects.Length);
                subject.Name = Info.fileSubjects[n];

            } while (subjectNames.Contains(subject.Name));

            subjectNames[i] = subject.Name;

            subject.Group = group;

            group.Subjects[i] = subject;
        }

        return group.Subjects;
    }

    // присваивание преподавателя для предмета группы
    public static Professors Professor(Subjects subject)
    {
        int n;
        Professors professor;
        do
        {
            n = index.Next(Professors.List.Length);
            professor = Professors.List[n];

        } while (Professors.List[n].CountSubjects == 5);

        Professors.List[n].CountSubjects++;

        Professors.List[n].Subjects.Add(subject);

        return professor;
    }

    // присваивание группы студенту
    public static Groups StudentGroup(Students student)
    {
        int n;

        do
        {
            n = index.Next(Groups.List.Length);
            student.Group = Groups.List[n];

        } while (Groups.List[n].CountPeople == 10);

        Groups.List[n].CountPeople++;
        Groups.List[n].Students.Add(student);

        return student.Group;
    }

    // присваивание оценки
    public static int Mark()
    {
        int n = index.Next(2,6);
        return n;
    }

    // генерация приказов
    public static string Order()
    {
        // приказы: Р - преподавателям, S - судентам, E - всем
        string[] sign = { "P","S","E" };

        int n = index.Next(sign.Length);
        string order = sign[n] + Convert.ToString(index.Next(3000));

        return order;
    }
}
