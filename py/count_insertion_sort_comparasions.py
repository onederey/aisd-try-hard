_ = input()
a = list(map(int, input().split()))
n = len(a)
ans = 0

for i in range(1, n):
    cur = a[i]
    j = i - 1
    
    while j >= 0:
        if a[j] > cur:
            ans += 1
            a[j + 1] = a[j]
            j -= 1
        else:
            ans += 1
            break

    a[j + 1] = cur
    
print(ans)