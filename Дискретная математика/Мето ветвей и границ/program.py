import numpy as np

class Branch:

    def __init__(self, path: str) -> None:
        # чтение матрицы из файла (шаг 1)
        self.matrix = np.genfromtxt(path)
        # замена диагональных элементов на бесконечности
        np.fill_diagonal(self.matrix, float('inf'))
        # вершины по горизонтали
        self.line_numbers = np.arange(1,self.matrix.shape[0] + 1)#column_numbers
        # вершины по вертикали
        self.column_numbers = np.arange(1,self.matrix.shape[1] + 1)

    @staticmethod
    def give_max_mark(matrix: np.ndarray):
        # координаты элемента с максимальной оценкой
        coordinate1 = coordinate2 = 0
        # максимальная оценка
        max_mark = - float('inf')
        
        # проход по матрице
        for i in range(matrix.shape[0]):
            for j in range(matrix.shape[1]):
                if matrix[i][j] == 0:
                    # строка с нулевым элементом
                    min_of_line = np.sort(matrix[i, :])[1]
                    # столбец с нулевым элементом
                    min_of_column = np.sort(matrix[:,j])[1]
                    # оценка
                    mark = min_of_line + min_of_column
                    # проверка оценки на максимум
                    if mark > max_mark:
                        coordinate1 = i
                        coordinate2 = j
                        max_mark = mark
        return coordinate1,coordinate2,max_mark
             
    def reduxe_matrix(self,i,j):
        # замена элемента на бесконечность
        element1 = self.column_numbers[i]
        element2 = self.line_numbers[j]
        if element1 in self.line_numbers and element2 in self.column_numbers:
            # координаты элемента, которые нужно заменить на бесконечность
            index1 = np.where(self.column_numbers == element2)[0][0]
            index2 = np.where(self.line_numbers == element1)[0][0]
            self.matrix[index1, index2] = float('inf')
        # удалеине j столбца
        self.matrix = np.delete(self.matrix, j, axis=1)
        self.line_numbers = np.delete(self.line_numbers, j)
        # удаление i строки
        self.matrix = np.delete(self.matrix, i, axis=0)
        self.column_numbers = np.delete(self.column_numbers, i)
        # добавление удалённых вершин в путь
        

    def algorithm(self):
        # поиск минимума строк (шаг 2)
        line_mins = np.amin(self.matrix, axis=1)
        # редукция строк (шаг 3)
        self.matrix -= np.vstack(line_mins)
        # поиск минимума по столбцам (шаг 4)
        column_mins = np.amin(self.matrix, axis=0)
        # редукция столбцов (шаг 5)
        self.matrix -= column_mins
        # нахождение нижней границы (шаг 6)
        h = np.sum(line_mins) + np.sum(column_mins)

        while True:
            # вычисление максимальной оценки нулевой клетки (шаг 7,8)
            i,j,max_mark = self.give_max_mark(self.matrix)
            # редукция матрицы (шаг 9)
            self.reduxe_matrix(i,j)

            # вычисление нижней границы 1 ветки:

            # поиск минимума строк (шаг 2)
            line_mins = np.amin(self.matrix, axis=1)
            # редукция строк (шаг 3)
            self.matrix -= np.vstack(line_mins)
            # поиск минимума по столбцам (шаг 4)
            column_mins = np.amin(self.matrix, axis=0)
            # редукция столбцов (шаг 5)
            self.matrix -= column_mins
            # нахождение нижней границы (шаг 6)
            h1 = h + np.sum(line_mins) + np.sum(column_mins)

            # вычисление нижней границы 2 ветки
            h2 = h + max_mark

            if h1 > h2:
                print('Алгоритм не работает, так как считает только правую ветку')
                break

            if self.matrix.shape == (1,1):
                print(h1)
                break
        
# начало программы
start = Branch('метод ветвей и границ\matrix.txt')
start.algorithm()
