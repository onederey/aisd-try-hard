import sys

input = sys.stdin.readline

n = int(input())
a = list(map(int, input().split()))

def merge(l, r):
    merged = []
    i, j = 0, 0
    
    while i < len(l) and j < len(r):
        if l[i] < r[j]:
            merged.append(l[i])
            i += 1
        else:
            merged.append(r[j])
            j += 1
    
    if i < len(l):
        merged += l[i:]
    else:
        merged += r[j:]
    return merged

def merge_sort(array):
    if len(array) <= 1:
        return array
    
    h = len(array) // 2
    l = merge_sort(array[:h])
    r = merge_sort(array[h:])    
    
    merged = merge(l, r)
    return merged

print(merge_sort(a))