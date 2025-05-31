n, m = map(int, input().split(" "))
matrix = [[0] * m for _ in range(n)]

val = 0
i, j = 0, -1

while val < n * m:
    j += 1

    if j >= m:
        j -= 1
        i += 1

    matrix[i][j] = val
    val += 1
    di, dj = i + 1, j - 1

    while di < n and dj > -1:
        matrix[di][dj] = val
        val += 1

        di += 1
        dj -= 1

for i in range(n):
    for j in range(m):
        print(f"{matrix[i][j]:^3}", end='')
    print()