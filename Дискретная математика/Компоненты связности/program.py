import random as rn

def find_components(matrix) -> list:

    # алгоритм поиска в глубину
    def dfs(pick):
        used[pick] = True
        component.append(pick)
        for i in range(len(matrix[pick])):
            if matrix[pick][i] and not used[i]:
                dfs(i)
    
    components = []
    n = len(matrix)
    used = [False] * n
    for i in range(n):
        if not used[i]:
            component = []
            dfs(i)
            components.append(component)
    return components
            
matrix = [
    [0,1,1,0,0],
    [1,0,1,0,0],
    [1,1,0,0,0],
    [0,0,0,0,1],
    [0,0,0,1,0]
]
for line in matrix:
    print(line)
print(find_components(matrix))
