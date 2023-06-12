using System;
using System.Diagnostics.CodeAnalysis;

namespace Company;

class MainProg
{
    static void Main()
    {

        // открытие файлов
        string[] persons = File.ReadAllLines($"Company-XXX/input_s1_1.txt");
        string[] answers = File.ReadAllLines($"Company-XXX/output_s1_1.txt");
        // список всех сотрудников
        var workers = new List<Worker>();

        // проход по списку начальников и рабочих
        for (int i = 0; i < persons.Length - 2; i += 2)
        {
            // руководитель
            var chief = new Worker(persons[i].Split());
            // подчинённый
            var sub = new Worker(persons[i + 1].Split());
            // начальник рабочего
            sub.Chief = chief;

            // руководитель и подчинённый есть в списке сотрудников
            if (workers.Exists(x => x.Id == chief.Id) && workers.Exists(x => x.Id == sub.Id))
            {
                int index = workers.IndexOf(sub);
                if (sub.Name != "Unknown name") workers[index].Name = sub.Name;

                chief.Sublist.AddRange(workers[index].Sublist);

                index = workers.IndexOf(chief);
                if (chief.Name != "Unknown name") workers[index].Name = chief.Name;
                workers[index].Sublist.AddRange(chief.Sublist);

                while (index != -1)
                {
                    workers[index].Sublist.Add(sub);

                    if (workers[index].Chief != null) index = workers.IndexOf(workers[index].Chief);
                    else index = -1;
                }
            }

            // руководитель есть в списке сотрудников
            else if (workers.Contains(chief))
            {
                chief.Sublist.Add(sub);
                int index = workers.IndexOf(chief);
                if (chief.Name != "Unknown name") workers[index].Name = chief.Name;

                while (index != -1)
                {
                    workers[index].Sublist.Add(sub);

                    if (workers[index].Chief != null) index = workers.IndexOf(workers[index].Chief);
                    else index = -1;
                }
                workers.Add(sub);
            }

            // подчинённый есть в списке сотрудников
            else if (workers.Contains(sub))
            {
                chief.Sublist.Add(sub);
                int index = workers.IndexOf(sub);
                if (sub.Name != "Unknown name") workers[index].Name = sub.Name;

                chief.Sublist.AddRange(workers[index].Sublist);

                workers.Add(chief);
                workers[index].Chief = chief;
            }

            // ни подчинённого, ни руководителя нет в списке сотрудников
            else
            {
                chief.Sublist.Add(sub);
                workers.Add(chief); workers.Add(sub);
            }
        }
        // работник, у которого надо найти подчинённых
        string needWorker = persons[^1];

        // поиск подчинённых нужного работника
        Worker.PrintSub(workers, needWorker);

        // ответ
        Console.WriteLine("________________________________________");
        foreach (var answer in answers)
        {
            Console.WriteLine(answer);
        }
    }
}

class Worker : IComparable<Worker>
{
    // Id человека
    public string Id { get; set; } = "0000";
    // Имя человека
    public string Name { get; set; } = "Unknown name";
    // список подчинённых
    public List<Worker> Sublist { get; set; } = new();
    // руководитель
    public Worker? Chief { get; set; }
    //конструктор
    public Worker(string[] workerInfo)
    {
        switch(workerInfo.Length)
        {
            case 1:
                Id = workerInfo[0];
                Name = "Unknown name";
                break;
            case 2:
                Id = workerInfo[0];
                if (workerInfo[1] != "") Name = workerInfo[1];
                else Name = "Unknown name";
                break;
            case 3:
                Id = workerInfo[0];
                Name = workerInfo[1] + " " + workerInfo[2];
                break;
        }
    }

    // нахождение подчинённых у работника
    public static void PrintSub(List<Worker> workers, string personInfo)
    {
        foreach(var worker in workers)
        {
            if (worker.Id == personInfo || worker.Name == personInfo)
            {
                worker.Sublist.Sort();
                foreach(var sub in worker.Sublist)
                {
                    Console.WriteLine($"{sub.Id} {sub.Name}");
                }
                if (worker.Sublist.Count == 0) Console.WriteLine("NO");
                break;
            }
        }
    }
    public override int GetHashCode()
    {
        return int.Parse(Id);
    }
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is Worker objectType) return objectType.Id == Id;
        else return false;
    }
    public int CompareTo(Worker person)
    {
        if (person == null)
            return 1;

        else
            return Id.CompareTo(person.Id);
    }
}