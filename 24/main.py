import sys
import itertools
import collections

dirmap = {
    '>': (0, 1),
    '<': (0, -1),
    '^': (-1, 0),
    'v': (1, 0),
}

movements = {
    (0, 0),
    (1, 0),
    (-1, 0),
    (0, -1),
    (0, 1),
}

def dfs_path(start_pos, end_pos, lines, blizzards):

    cur_posses = set([start_pos])

    for i in itertools.count(0):
        if end_pos in cur_posses:
            return i, blizzards

        board = [['.' for c in l] for l in lines]
        new_blizzards = []
        for b in blizzards:
            x, y, dir = b
            x, y = (x + dir[0]) % len(lines), (y + dir[1]) % len(lines[0])
            board[x][y] = 'x'
            new_blizzards.append((x, y, dir))

        new_possib_posses = set()

        for pos in cur_posses:
            for m in movements:
                x, y = pos[0] + m[0], pos[1] + m[1]
                if (x, y) != start_pos and (x, y) != end_pos:
                    if (0 <= x < len(lines) and
                        0 <= y < len(lines[0]) and
                        board[x][y] != 'x'):
                        new_possib_posses.add((x, y))
                else:
                    new_possib_posses.add((x, y))


        blizzards = new_blizzards
        cur_posses = new_possib_posses




def main():
    lines = list(map(lambda x: x[1:-1], map(str.strip, sys.stdin.readlines())))
    lines = lines[1:-1:]


    blizzards = []

    for i, l in enumerate(lines):
        for j, c in enumerate(l):
            if c in dirmap:
                blizzards.append((i, j, dirmap[c]))

    start_pos = (-1, 0)
    end_pos = (len(lines), len(lines[0])-1)

    s1, blizzards = dfs_path(start_pos, end_pos, lines, blizzards)
    s2, blizzards = dfs_path(end_pos, start_pos, lines, blizzards)
    s3, blizzards = dfs_path(start_pos, end_pos, lines, blizzards)

    print(s1)
    print(s2)
    print(s3)
    print(s1 + s2 + s3)

main()
