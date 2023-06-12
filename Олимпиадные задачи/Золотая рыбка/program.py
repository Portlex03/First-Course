def get_info(path : str):
    """Получение информации из файла"""
    f = open(path)
    # количество слов
    countWords = int(f.readline())
    # слова
    words = []
    for i in range(countWords):
        words.append(f.readline().rstrip())
    # начала слов
    countStarts = int(f.readline())
    starts = []
    for i in range(countStarts):
        letter,letterCount = f.readline().split()
        starts.append([letter,letterCount])
    # концы слов
    countEnds = int(f.readline())
    ends = []
    for i in range(countEnds):
        letter,letterCount = f.readline().split()
        ends.append([letter,letterCount])
    return countWords,words,starts,ends

def get_answer(path: str, answer_path: str):
    """Получение ответа"""
    # файл с ответом
    f = open(answer_path)
    n,N,F,L = get_info(path)
    way1 = []
    way2 = []
    for i in range (n):
        for c in range (len(F)):
            for t  in range (len(L)):
                if N[i][0]==F[c][0]:
                    if N[i][-1]==L[t][0]:
                        if int(F[c][1])!=0 and int(L[t][1])!=0:
                            way1.append(N[i])
                            F[c][1]=int(F[c][1])-1
                            L[t][1]=int(L[t][1])-1
    n,N,F,L = get_info(path)
    for t in range (len(F)):
        for c in range (len(F)-1):
            if F[c][1]<F[c+1][1]:
                o=F[c]
                F[c]=F[c+1]
                F[c+1]=o
    for t in range (len(L)):
        for c in range (len(L)-1):
            if L[c][1]<L[c+1][1]:
                o=L[c]
                L[c]=L[c+1]
                L[c+1]=o
    for t  in range (len(L)):
        for c in range (len(F)):
            for i in range (n):
                if N[i][0]==F[c][0]:
                    if N[i][-1]==L[t][0]:
                        if int(F[c][1])!=0 and int(L[t][1])!=0:
                            way2.append(N[i])
                            F[c][1]=int(F[c][1])-1
    my_answer = max(len(way1),len(way2))
    real_answer = f.readline()
    print(my_answer, real_answer)

# начало программы
for i in range(1, 14):
    print(i)
    get_answer(f'GoldenFishFile\input_s1_{i}.txt',
               f'GoldenFishFile\output_s1_{i}.txt')
