// владельцы машины
// владелец (его машины)
// водители с фамилией заданой буквы

class Car
{
    public string name;
    public int number;

    public Car(string name, int number)
    {
        this.name = name;
        this.number = number;
    }
}

class Driver
{
    public string name;
    public List<int> cars;

    public Driver(string name, List<int> cars)
    {
        this.name = name;
        this.cars = cars;
    }
}

class Program
{
    static void Main()
    {
        var driverList = new List<Driver>()
        {
            new Driver("Скворцов Пётр", new List<int>{1,2}),
            new Driver("Сидоров Евгений", new List<int>{2,3}),
            new Driver("Сыроежкин Семён", new List<int>{3,4}),
            new Driver("Петрова Валерия", new List<int>{1,4})
        };
        var carList = new List<Car>()
        {
            new Car("Toyota", 1),
            new Car("BMW", 2),
            new Car("Ferrary", 3),
            new Car("Ford", 4)
        };

        // владельцы машины
        var car_drivers = from car in carList
                          group car by car.name into g
                          select new
                          {
                              name = g.Key,
                              drivers = from driver in driverList
                                        where driver.cars.Contains(g.First().number)
                                        select driver.name
                          };
        foreach (var car in car_drivers)
        {
            Console.WriteLine($"Машина {car.name}, владельцы:");
            foreach (var driver in car.drivers)
            {
                Console.WriteLine(driver);
            }
            Console.WriteLine();
        }

        // машины владельцов
        var owner_cars = from driver in driverList
                         group driver by driver.name into g
                         select new
                         {
                             name = g.Key,
                             cars = from car in carList
                                    where g.First().cars.Contains(car.number)
                                    select car.name
                         };
        foreach(var owner in owner_cars)
        {
            Console.WriteLine($"Владелец {owner.name}, Машины:");
            foreach(var car in owner.cars)
            {
                Console.WriteLine(car);
            }
            Console.WriteLine();
        }

        // Водители с фамилией на определённую букву
        var driver_names = from driver in driverList
                           group driver by driver.name[0];
        foreach(var letter in driver_names)
        {
            Console.WriteLine($"Буква {letter.Key}:");
            foreach(var driver in letter)
            {
                Console.WriteLine(driver.name);
            }
            Console.WriteLine();
        }
    }
}
