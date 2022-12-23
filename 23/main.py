import sys
from collections import Counter

turns = [
    ((-1, 0), (-1, -1), (-1, 1)),
    ((1, 0), (1, -1), (1, 1)),
    ((0, -1), (-1, -1), (1, -1)),
    ((0, 1), (-1, 1), (1, 1)),
]

def tuple_add(p1, p2):
    return p1[0] + p2[0], p1[1] + p2[1]

def main():
    lines = map(str.strip, sys.stdin.readlines())
    elves = set()
    global turns
    for i, l in enumerate(lines):
        for j, c in enumerate(l):
            if c == '#':
                elves.add((i, j))

    #for k in range(10):
    k = 1
    while True:
        #print('-')
        #print(k)
        #print(turns[0])
        new_posses = []
        for elf in elves:
            c = 0
            for i in range(-1, 2):
                for j in range(-1, 2):
                    np = tuple_add(elf, (i, j))
                    if np in elves:
                        c += 1

            #print(elf)
            #print(c)
            if c == 1:
                new_posses.append((elf, elf))
                #print("ja nichts")
                continue

            for t in turns:
                for dir in t:
                    if tuple_add(elf, dir) in elves:
                        break
                else:
                    new_posses.append((tuple_add(elf, t[0]), elf))
                    break
            else:
                new_posses.append((elf, elf))


        counter = Counter(map(lambda x: x[0], new_posses))

        next_elves = set()
        for new_pos in new_posses:
            next_elves.add(new_pos[0] if counter[new_pos[0]] == 1 else new_pos[1])

        if elves == next_elves:
            break

        elves = next_elves
        turns = turns[1::] + [turns[0]]
        k+=1


    print(k)
    #xs = list(map(lambda x: x[0], elves))
    #ys = list(map(lambda x: x[1], elves))
    #x0, x1 = min(xs), max(xs)
    #y0, y1 = min(ys), max(ys)

    #print((x1 - x0 + 1) * (y1 - y0 + 1) - len(next_elves))

main()
