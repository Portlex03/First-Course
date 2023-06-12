namespace MySpace;

internal class Task1
{
    public static void GroupFinder(in List<StudentInfo> Students)
    {
        Console.Clear();
        StudentInfo.Message("Студенты, которые учатся в заданной группе");

        if (Students.Count != 0)
        {
            
            Console.Write("\nВведите группу: ");
            string thisGroup = Console.ReadLine();

            StudentInfo.Message("\nСтуденты с данной группой:");
            int count = 0;
            for (int i = 0; i < Students.Count; i++)
            {
                if (Students[i].group == thisGroup)
                {
                    Console.WriteLine($"{Students[i].surname} {Students[i].name} {Students[i].group}");
                    count++;
                }
            }
            if (count == 0) Console.WriteLine("Не найдено");
        }
        else Console.WriteLine("\nНет данных, нужно создать список студентов");

        StudentInfo.Message("\nДля выхода в меню нажмите *Enter* ");
        Console.ReadKey();
        Console.Clear();
        Project.Menu(Students);
    }
}
