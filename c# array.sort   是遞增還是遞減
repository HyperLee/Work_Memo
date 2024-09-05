在 C# 中，`Array.Sort` 默认情况下是**递增排序**。也就是说，`Array.Sort` 会将数组中的元素按照从小到大的顺序进行排序。

### 示例：
```csharp
int[] numbers = { 5, 3, 8, 1, 4 };
Array.Sort(numbers);

foreach (int number in numbers)
{
    Console.WriteLine(number);
}
```

#### 输出：
```
1
3
4
5
8
```

### 如果需要**递减排序**，可以使用 `Array.Sort` 方法结合 `Array.Reverse`，或者直接使用 `Array.Sort` 时提供一个自定义比较器。

#### 使用 `Array.Sort` 和 `Array.Reverse`：
```csharp
int[] numbers = { 5, 3, 8, 1, 4 };
Array.Sort(numbers);
Array.Reverse(numbers);

foreach (int number in numbers)
{
    Console.WriteLine(number);
}
```

#### 使用自定义比较器实现递减排序：
```csharp
int[] numbers = { 5, 3, 8, 1, 4 };
Array.Sort(numbers, (x, y) => y.CompareTo(x));  // 递减排序

foreach (int number in numbers)
{
    Console.WriteLine(number);
}
```

这样可以实现数组的递减排序。