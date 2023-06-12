//Необходимо реализовать обобщенный класс, 
//который позволяет хранить в массиве объекты
//любого типа. В данном классе определить методы 
//для добавления данных в массив, удаления из 
//массива, получения элемента из массива по индексу.

class MyClass<T>
{
    List<T> list = new List<T>();

    public void Add(T item)
    {
        list.Add(item);
    }

    public void Remove(T item)
    {
        list.Remove(item);
    }

    public T GetItem(int index)
    {
        if (index > list.Count || index < 0)
            throw new Exception("Ошибка ввода индекса");
        return list[index];
    }

    public void Print()
    {
        foreach (T item in list)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();
    }
}

class Start
{
    static void Main(string[] args)
    {
        MyClass<object> ex = new MyClass<object>();

        ex.Add("строка");
        ex.Add(true);
        ex.Add(54);
        ex.Add(0.74F);
        ex.Add(1.08);
        ex.Add(new int[] { 1, 2, 3 });

        Console.Write("Лист после добавления: ");
        ex.Print();

        ex.Remove(54);
        Console.Write("Лист после удаления элемента: ");
        ex.Print();

        Console.Write("Элемент с индексом 2: ");
        Console.WriteLine(ex.GetItem(2));
    }
}
