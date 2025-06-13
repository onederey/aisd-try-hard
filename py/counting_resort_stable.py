"""
# Не оптимальное решение, каждый раз пересчитывается массив префиксов, сложность n * k
import sys

n = int(sys.stdin.readline())
queries = list(map(int, sys.stdin.readline().split()))
max_value = 101

C = [0] * max_value
B = [0] * max_value

for i in range(len(queries)):
    C[queries[i]] += 1
    
    for j in range(1, queries[i] + 1):
        B[j] = B[j - 1] + C[j]
    print(B[queries[i]] - 1, end=' ')
"""

# оптимальное решение
import sys

n = int(sys.stdin.readline())
queries = list(map(int, sys.stdin.readline().split()))

C = [0] * 101

for i in range(len(queries)):
    p = sum(C[1:queries[i]]) + C[queries[i]]
    print(p, end=' ')

    C[queries[i]] += 1