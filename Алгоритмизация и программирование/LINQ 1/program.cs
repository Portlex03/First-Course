//необходимо реализовать для массива(списка) 
//подсчёт количества отрицательных, сумму положительных
//произведение чётных чисел с помощью библиотеки LINQ

var numbers = new List<int>() { -1, 4, -15, 80, 17, -3, -50, 12 };

int countNegatives = 0;
foreach (var i in from number in numbers
                     where number < 0
                     select number)
    countNegatives++;

int summPositives = 0;
foreach (var i in from number in numbers
                  where number > 0
                  select number)
    summPositives+=i;

var multiply4etn = 1;
foreach (var i in from number in numbers
                  where number % 2 == 0
                  select number)
    multiply4etn *= i;

Console.WriteLine($"{countNegatives}, {summPositives}, {multiply4etn}");
