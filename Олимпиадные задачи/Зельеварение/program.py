f = open('input10.txt')
things = [x.split() for x in f]
answer = ''

def turning(method):
    method = method.replace('MIX','MX')
    method = method.replace('WATER','WT')
    method = method.replace('DUST','DT')
    method = method.replace('FIRE','FR')
    return method

# рекурсивная функция    
def f(index):
    global things,answer

    # заклинание 
    spell = things[index]
    #метод 
    method = turning(spell[0])
    answer += method
    for i in range(1,len(spell)):
        if spell[i].isdigit():
            f(int(spell[i]) - 1)
        else:
            answer += spell[i]
    answer += method[::-1]

f(-1)
print(answer)
