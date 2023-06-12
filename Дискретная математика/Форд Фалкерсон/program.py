import numpy as np
def find_route(realMatrix: np.ndarray, p1, p2) -> list:
    """Нахождение маршрута по Дейкстре"""
    # копирование матрицы
    matrix = np.copy(realMatrix)
    # замена 0 на бесконечность
    matrix[matrix == 0] = float('inf')
    n = matrix.shape[0]
    # маршруты
    routes = [[] for i in range(n)]
    last_route = [p1]
    minColums = [float('inf')] * n
    len = 0
    numbers = [p1]
    # счётчик
    count = 0
    while (p1 != p2):
        line = [float('inf')] * n
        for i in range(n):
            if i not in numbers:
                if minColums[i] < matrix[p1][i] + len:
                    line[i] = minColums[i]
                elif matrix[p1][i] + len < minColums[i]:
                    line[i] = matrix[p1][i] + len
                    routes[i] = last_route + [i]
                minColums[i] = min(line[i], minColums[i])
        len = min(line)
        p1 = line.index(len)
        last_route = routes[p1]
        numbers.append(p1)

        count += 1
        # если попали в бесконечный цикл
        if count > n * 2:
            return None
    # возвращается маршрут от p1 до p2
    return routes[p2]

def find_edges(route) -> list:
    """Нахождение рёбер"""
    edges = []
    for i in range(len(route) - 1):
        edges.append((route[i],route[i + 1]))
    return edges

def find_values(matrix, edges):
    """Нахождение значений рёбер"""
    values = []
    for i in range(len(edges)):
        first_index = edges[i][0]
        second_index = edges[i][1]
        values.append(matrix[first_index][second_index])
    return values

def delete_min_value(matrix, edges, minValue):
    """Удаление минимального ребра из маршрута"""
    for i in range(len(edges)):
        first_index = edges[i][0]
        second_index = edges[i][1]
        matrix[first_index][second_index] -= minValue

def algorithm(matrix, source, stock):
    """Алгоритм Форда - Фалкерсона"""
    # максимальный поток
    maxFlow = 0

    while True:
        # находим произвольный маршрут
        route = find_route(matrix, source, stock)
        # если маршрута нет
        if not route: break
        # находим рёбра маршрута
        edges = find_edges(route)
        # находим значение рёбер
        values = find_values(matrix,edges)
        # минимальное ребро
        minValue = min(values)
        # отнимаем минимальное ребро от всех рёбер
        delete_min_value(matrix, edges, minValue)
        # прибавляем поток
        maxFlow += minValue
    return maxFlow

# начало программы
matrix = np.genfromtxt('форд фалкерсон\matrix')
print(algorithm(matrix, 0, 7))
