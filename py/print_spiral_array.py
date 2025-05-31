n = int(input())
matrix = [[0] * n for _ in range (n)]

for i in range(n):
    matrix[0][i] = 1

directions = [ (0, 1), (1, 0), (0, -1), (-1, 0) ]
direction = 0
steps = n - 1
i, j = 0, n - 1

while steps > 0:
    direction += 1
    current_direction = direction % 4

    for _ in range(steps):
        i += directions[current_direction][0]
        j += directions[current_direction][1]
        matrix[i][j] = 1

    if direction % 2 == 0:
        steps -= 2

for i in range (n):
    for j in range (n):
        print(matrix[i][j], end='')
    print()