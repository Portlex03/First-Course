//запрос на выборку положительных
//сумму отриц
//колво кратных 5
//запрос будет обрабатывать массив
//после конца запросов удалить все чётные элементы
//и прогнать ещё раз запросы
void query(List<int> numbers)
{
    Console.WriteLine("Положительные числа:");
    foreach (var i in from number in numbers
                      where number > 0
                      select number)
        Console.Write(i + " ");
    Console.WriteLine();

    int summNegatives = 0;
    foreach (var i in from number in numbers
                      where number < 0
                      select number)
        summNegatives += i;

    int countMultiply5 = 0;
    foreach (var i in from number in numbers
                      where number % 5 == 0
                      select number)
        countMultiply5++;

    Console.WriteLine($"{summNegatives}, {countMultiply5}");
}

var numbers = new List<int>() { -2, -1, 4, -1, 5, 10, 15, 6, -1, 1 };

query(numbers);

for(int i = 0; i < numbers.Count; i++)
{
    if (numbers[i] % 2 == 0)
    {
        numbers.RemoveAt(i);
    }
}

query(numbers);


