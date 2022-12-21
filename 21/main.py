import sys
import pprint

from sympy.parsing.sympy_parser import parse_expr
from sympy.solvers import solve
from sympy import Symbol

def call_1(expressions):
    while not expressions['root'].isdigit():
        pprint.pprint(expressions)
        digit_strings  = list(filter(lambda x: x[1].isdigit(), expressions.items()))
        pprint.pprint(digit_strings)
        for digit_str, val in digit_strings:
            for r in expressions:
                expressions[r] = expressions[r].replace(digit_str, val)
            del expressions[digit_str]
        pprint.pprint(expressions)
        for r, v in expressions.items():
            try:
                expressions[r] = str(int(eval(v, {}, {})))
            except NameError:
                pass # not valid

    print(expressions['root'])

def call_2(expressions):

    for k in list(expressions.keys()):
        v = expressions[k]
        if k == 'root':
            continue

        for j in expressions:
            expressions[j] = expressions[j].replace(k, f"({v})")
        del expressions[k]

    exp = expressions['root']
    l, r = exp.split('==')
    eq = f'{r} - {l}'
    print(eq)

    x = Symbol('x')
    symbolic = parse_expr(eq)
    print(symbolic)
    res = solve(symbolic, x)
    print(res)

def main():
    lines = sys.stdin.readlines()

    expressions = dict(map(lambda x: (x[0], x[1].strip()), (line.split(':') for line in lines)))
    #call_1(expressions)

    expressions['root'] = expressions['root'].replace('+', '==')
    expressions['humn'] = 'x'

    call_2(expressions)


if __name__ == '__main__':
    main()
