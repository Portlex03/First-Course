//Необходимо разработать программу, которая включает 
//класс автомобиль, гараж и мойка. Гараж будет 
//является коллекцией машин. Мойка может только 
//мыть машину. Необходимо делегировать мытью всех машин.

class Car
{
    public string name;
    public bool isWashed = false;

    public Car(string name)
    {
        this.name = name;
    }
}

class Garage
{
    List<Car> cars;

    public Garage(List<Car> cars)
    {
        this.cars = cars;
    }
    public delegate void CarDelegate(Car car);

    public void DoSomething(CarDelegate Action)
    {
        foreach (var car in cars)
        {
            Action(car);
        }
    }
}

class Washing
{
    public void WashCar(Car car)
    {
        car.isWashed = true;
        Console.WriteLine($"Машина {car.name} помыта");
    }
}

class Start
{
    static void Main()
    {
        Car car1 = new Car("Toyota Mark II");
        Car car2 = new Car("BMW X6");
        Car car3 = new Car("Lada Priora");
        Car car4 = new Car("Tesla Model X");

        Garage garage = new Garage(new List<Car> { car1, car2, car3, car4 });

        Washing washObject = new Washing();

        garage.DoSomething(washObject.WashCar);
    }
}
