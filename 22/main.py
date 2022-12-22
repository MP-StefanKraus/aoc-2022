import sys
import re

dirs = [(0, 1), (1, 0), (0, -1), (-1, 0)]

sign = lambda x: x and (1, -1)[x<0]

# This is a edge mapping of my input, done by hand and enpoints
# I did this not for all possible inputs, as my own sanity
# was more important than this task
test_map_edge_mapping = [
    (((50, 50), (99, 50), 2), ((100, 0), (100, 49), 1)),
    (((0, 50), (49, 50), 2), ((149, 0), (100, 0), 0)),
    (((0, 50), (0, 99), 3), ((150, 0), (199, 0), 0)),
    (((50, 99), (99, 99), 0), ((49, 100), (49, 149), 3)),
    (((100, 99), (149, 99), 0), ((49, 149), (0, 149), 2)),
    (((150, 49), (199, 49), 0), ((149, 50), (149, 99), 3)),
    (((0, 100), (0, 149), 3), ((199, 0), (199, 49), 3)),
]

def add_tup(p1, p2):
    return p1[0] + p2[0], p1[1] + p2[1]

def mul_tup(t, s):
    return t[0] * s, t[1] * s

def mod_board(t, board):
    return t[0] % len(board), t[1] % len(board[0])

def generate_portal():
    portal = {}
    for edge_map in test_map_edge_mapping:
        a, b = edge_map
        a_start, a_end, a_dir = a
        b_start, b_end, b_dir = b
        a_vec = sign(a_end[0] - a_start[0]), sign(a_end[1] - a_start[1])
        b_vec = sign(b_end[0] - b_start[0]), sign(b_end[1] - b_start[1])
        print('-')
        print(a)
        print(b)
        print(a_vec)
        print(b_vec)
        print('-')
        for i in range(50):
            f = add_tup(a_start, mul_tup(a_vec, i))
            t = add_tup(b_start, mul_tup(b_vec, i))
            portal[(f, a_dir)] = (t, b_dir)
            portal[(t, (2+b_dir) % 4)] = (f, (a_dir + 2) % 4)

    return portal

tel_portal = generate_portal()

def satisfy(pos, facing, board):

    d = dirs[facing]

    np = mod_board(add_tup(pos, d), board)
    if board[np[0]][np[1]] != ' ':
        return np, facing
    else:
        return tel_portal[(pos, facing)]

def main():
    lines = list(map(lambda x: x.rstrip('\n'), sys.stdin.readlines()))

    line_length = max(map(len, lines))
    lines = [l + ' ' * (line_length - len(l)) for l in lines]

    *board, _, move = lines

    pos = (0, board[0].find('.'))
    facing = 0

    instructions = re.findall('(\d+|R|L)', move)

    for instr in instructions:
        if instr in 'RL':
            facing += 1 if instr == 'R' else -1
            facing %= 4
            continue

        num = int(instr)

        for i in range(num):
            maybe_new_pos, f = satisfy(pos, facing, board)
            nx, ny = maybe_new_pos
            if board[nx][ny] == '.':
                pos = maybe_new_pos
                facing = f
            #print(pos)

    print((pos[0] + 1) * 1000 + (pos[1] + 1) * 4 + facing)

main()
