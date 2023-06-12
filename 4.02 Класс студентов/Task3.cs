namespace MySpace;

internal class Task3
{
    public static void HonorStudents(in List<StudentInfo> Students)
    {
        Console.Clear();
        StudentInfo.Message("Студенты - отличники");
        int count = 0;
        
        if (StudentInfo.Subjects != null)
        {
            Console.WriteLine("\nСписок студентов отличников:");
            for (int i = 0; i < Students.Count; i++)
            {
                int summ = 0;
                for (int j = 0; j < StudentInfo.Subjects.Length; j++)
                {
                    if (Students[i].score[j] == 5)
                    {
                        summ ++; 
                        count++;
                    }
                }
                if (summ == StudentInfo.Subjects.Length)
                {
                    Console.WriteLine($"{Students[i].surname} {Students[i].name} {Students[i].group}, средний балл - 5");
                }
            }
            if (count == 0) Console.WriteLine("Не найдено");
        }
        else if (Students.Count != 0) Console.WriteLine("\nНет данных, нужно создать список предметов");
        else Console.WriteLine("\nНет данных, нужно создать список студентов");

        StudentInfo.Message("\nДля выхода в меню нажмите *Enter* ");
        Console.ReadKey();
        Console.Clear();
        Project.Menu(Students);
    }
}
