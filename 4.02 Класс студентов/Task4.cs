namespace MySpace;

internal class Task4
{
    public static void YoungStudents(in List<StudentInfo> Students)
    {
        Console.Clear();
        StudentInfo.Message("Студенты, которые моложе 20 лет\n");

        if (Students.Count != 0)
        {
            int count = 0;
            for (int i = 0; i < Students.Count; i++)
            {
                if (2023 - Students[i].date[2] < 20 || 23 - Students[i].date[2] < 20)
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
