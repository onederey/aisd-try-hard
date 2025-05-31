n, m = map(int, input().split(" "))
arr = []
val = 0

for i in range(n):
    arr.append([0] * m)
    for j in range(m):
        k = j if i % 2 == 0 else m - j - 1
        arr[i][k] = val
        val += 1

for i in range(n):
    for j in range(m):
        print(f"{arr[i][j]:>3}", end='')
    print()