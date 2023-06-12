def func(path: str):
    f = open(path)
    # первая строка со старой валютой
    old_cource = [int(x) for x in f.readline().split()[1:]]

    # вторая строка с несчастливыми числам
    old_numbers = [int(x) for x in f.readline().split()[1:]]

    # третья строка с новой валютой
    new_cource = [int(x) for x in f.readline().split()[1:]]

    # четвёртая строка с несчастливыми чисами новой валюты
    new_numbers = [int(x) for x in f.readline().split()[1:]]

    # пятая строка с количеством старой валюты
    old_cource_value = [int(x) for x in f.readline().split()]

    # количество новой валюты
    new_cource_value = [0] * (len(new_cource) + 1)

    old_numbers.sort(reverse = 1)
    new_numbers.sort()

    # убирание несчастливых чисел
    for i in range(len(old_cource_value)):
        for j in range(len(old_numbers)):
            if old_numbers[j] <= old_cource_value[i]: 
                old_cource_value[i] -= 1

    # перевод из одной валюты в другую 1
    for i in range(len(old_cource_value) - 1):
        old_cource_value[i + 1] += old_cource_value[i]*old_cource[i]

    new_cource_value[-1] = old_cource_value[-1]

    # перевод из одной валюты в другую 2
    for i in range(len(new_cource_value) - 1, 0, -1):
        new_cource_value[i - 1] = new_cource_value[i] // new_cource[i - 1]
        new_cource_value[i] = new_cource_value[i] % new_cource[i - 1]

    # убирание несчастливых чисел
    for i in range(len(new_cource_value)):
        for j in range(len(new_numbers)):
            if new_numbers[j] <= new_cource_value[i]: 
                new_cource_value[i] += 1
    
    # вывод
    for value in new_cource_value:
        print(value, end = ' ')
    print()

for i in range(1,15):
    print(f'{i})')
    func(f'exchange money\Обмен денег\input{i}.txt')
    f = open(f'exchange money\Обмен денег\output{i}.txt')
    print(f.readline())
