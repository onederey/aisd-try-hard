import time

saved = {}

def calculate_old(n):
    if n <= 1:
        return 1

    initial_value = (n * n) // 3 + calculate_old(n - 2)
    while initial_value % 2 == 0:
        initial_value = initial_value // 2

    initial_value += calculate_old(2 * n // 3)
    value = calculate_old(initial_value % n)

    return value + calculate_old(n // 2) + n


def calculate(n):
    if n <= 1:
        return 1
    elif n in saved:
        return saved[n]

    a = n - 2
    saved[a] = calculate(a)
    
    initial_value = (n * n) // 3 + saved[a]

    while initial_value % 2 == 0:
        initial_value = initial_value // 2

    b = 2 * n // 3
    saved[b] = calculate(b)
    initial_value += saved[b]
    
    c = initial_value % n
    saved[c] = calculate(c)
    
    d = n // 2
    saved[d] = calculate(d)

    return saved[c] + saved[d] + n

start = time.time()
print(calculate(1000))
end = time.time()
print(end - start)


start = time.time()
print(calculate_old(1000))
end = time.time()
print(end - start)