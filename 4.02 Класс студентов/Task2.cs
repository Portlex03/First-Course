namespace MySpace;

internal class Task2
{
    public static void Debtors(in List<StudentInfo> Students) //Должники
    {
        List<int> debtors = new();
        int count = 0;

        Console.Clear();
        StudentInfo.Message("Студенты - должники");
        if (StudentInfo.Subjects != null)
        {
            Console.WriteLine("\nСписок студентов должников:");
            for(int i = 0; i < Students.Count; i++)
            {
                for(int j = 0; j < StudentInfo.Subjects.Length; j++)
                {
                    if (Students[i].score[j] < 3 && !debtors.Contains(i))
                    {
                        Console.WriteLine($"{Students[i].surname} {Students[i].name} {Students[i].group} " +
                            $"\n{StudentInfo.Subjects[j]}: {Students[i].score[j]}\n");
                        debtors.Add(i);
                        count++;
                    }
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
