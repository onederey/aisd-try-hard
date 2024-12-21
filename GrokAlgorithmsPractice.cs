public class GrokAlgorithmsPractice
{
    #region binary search
    public static void BinarySearchPlayground()
    {
        var nums = Enumerable.Range(0, 107).ToArray();

        nums[30] = 33;
        nums[31] = 33;
        nums[32] = 33;
        nums[33] = 33;
        nums[34] = 33;
        nums[35] = 33;
        nums[36] = 33;
        nums[37] = 33;

        BinarySearchFindLastOrFirstEntry(nums, 33, needToFindLastEntry: true);
        BinarySearchFindLastOrFirstEntry(nums, 33, needToFindLastEntry: false);
        BinarySearchFromBook(nums, 33);
    }

    public static int BinarySearchFromBook(int[] nums, int target)
    {
        var l = 0;
        var r = nums.Length - 1;

        while (l <= r)
        {
            var m = (l + r) / 2;
            var guess = nums[m];
            
            if (guess == target)
            {
                return m;
            }
            else if (guess > target)
            {
                r = m - 1;
            }
            else
            {
                l = m + 1;
            }
        }

        return -1;
    }

    public static int BinarySearchFindLastOrFirstEntry(int[] nums, int target, bool needToFindLastEntry)
    {
        var l = 0;
        var r = nums.Length - 1;
        var index = -1;

        while (l <= r)
        {
            var m = (l + r) / 2;
            var guess = nums[m];
            
            if (guess == target)
            {
                index = m;

                if (needToFindLastEntry)
                {
                    l = m + 1;
                }
                else
                {
                    r = m - 1;
                }
            }
            else if (guess > target)
            {
                r = m - 1;
            }
            else
            {
                l = m + 1;
            }
        }

        return index;
    }

    public static int BinarySearchRecursive(int[] nums, int target, int l, int r, int m)
    {
        if (l > r)
        {
            return -1;
        }

        if (target > nums[m])
        {
            l = m + 1;
            m = (l + r) / 2;
            return BinarySearchRecursive(nums, target, l, r, m);
        }
        else if (target < nums[m])
        {
            r = m - 1;
            m = (l + r) / 2;
            return BinarySearchRecursive(nums, target, l, r, m);
        }
        else
        {
            return m;
        }
    }
    #endregion

    #region quick sort
    public static void QuickSortPlayground()
    {
        var nums = Enumerable
            .Repeat(0, 10)
            .Select(num => Random.Shared.Next(10))
            .ToArray();

        var copyNums = nums.ToArray();

        Console.WriteLine("Before:" + string.Join(" ", nums));
        QuickSort(nums, 0, nums.Length - 1);
        Console.WriteLine("After:" + string.Join(" ", nums));

        Console.WriteLine("Before:" + string.Join(" ", copyNums));
        QuickSortDesc(copyNums, 0, copyNums.Length - 1);
        Console.WriteLine("After:" + string.Join(" ", copyNums));

    }
    
    /// <summary>
    /// Может я чего не понял, но из-за потребления доп. памяти это говно.
    /// </summary>
    public static int[] QuickSortFromBook(int[] nums)
    {
        if (nums.Length < 2)
        {
            return nums;
        }

        if (nums.Length == 2)
        {
            if (nums[0] > nums[1])
            {
                (nums[0], nums[1]) = (nums[1], nums[0]);
            }

            return nums;
        }

        var pivotIndex = Random.Shared.Next(nums.Length);
        var pivot = nums[pivotIndex];
        
        var less = nums.Where(x => x <= pivot);
        var greater = nums.Where(x => x > pivot);
        
        return [.. QuickSortFromBook([.. less]), .. QuickSortFromBook([.. greater])];
    }

    public static void QuickSort(int[] nums, int left, int right)
    {
        if (left >= right || nums.Length == 0)
        {
            return;
        }

        var result = Partition(nums, left, right);
        
        QuickSort(nums, left, result.Right);
        QuickSort(nums, result.Left, right);

        static (int Left, int Right) Partition(int[] nums, int left, int right)
        {
            var pivot = nums[(left + right) / 2];

            while(left <= right)
            {
                while(nums[left] < pivot) left++;
                while(nums[right] > pivot) right--;

                if (left <= right)
                {
                    (nums[left], nums[right]) = (nums[right], nums[left]);
                    
                    left++;
                    right--;
                }
            }

            return (left, right);
        }
    }

    public static void QuickSortDesc(int[] nums, int left, int right)
    {
        if (left >= right || nums.Length == 0)
        {
            return;
        }

        var result = Partition(nums, left, right);
        
        QuickSortDesc(nums, left, result.Right);
        QuickSortDesc(nums, result.Left, right);

        static (int Left, int Right) Partition(int[] nums, int left, int right)
        {
            var pivot = nums[(left + right) / 2];

            while(left <= right)
            {
                while(nums[left] > pivot) left++;
                while(nums[right] < pivot) right--;

                if (left <= right)
                {
                    (nums[left], nums[right]) = (nums[right], nums[left]);
                    
                    left++;
                    right--;
                }
            }

            return (left, right);
        }
    }
    #endregion
}