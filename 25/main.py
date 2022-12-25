
import sys
import itertools

char_conf = {
    '=': -2,
    '-': -1,
    '0': 0,
    '1': 1,
    '2': 2,
}

def snafu_to_decimal(snafu):
    c = 0
    for i, s in enumerate(snafu[::-1]):
        c += char_conf[s] * (5** i)

    return c

def decimal_to_snafu(decimal):
    ls = [0] * 25
    print(decimal)

    for i in itertools.count():
        r = decimal % 5
        ls[i] = r
        decimal //= 5
        if not decimal:
            break

    print(ls)

    for i in range(len(ls)-1):
        if ls[i] > 2:
            ls[i] -= 5
            ls[i+1] += 1

    ls = list(map(lambda x: '=' if x == -2 else x, ls))
    ls = list(map(lambda x: '-' if x == -1 else x, ls))

    return "".join(map(str, ls)).rstrip('0')[::-1]

def main():

    lines = map(str.strip, sys.stdin.readlines())

    print(decimal_to_snafu( sum(map(snafu_to_decimal, lines))))

main()
