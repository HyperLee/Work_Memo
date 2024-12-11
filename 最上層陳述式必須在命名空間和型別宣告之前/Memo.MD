#這是 c# .NetCore 專案程式碼

#Q: .netcode c# 最上層陳述式必須在命名空間和型別宣告之前。

在 C# 中，最上層陳述式（Top-level Statements）是一種簡化程式碼的方式，允許您在沒有 `Main` 方法和 `Program` 類別的情況下撰寫程式碼。這些陳述式必須位於檔案的最上層，並且在任何命名空間或型別宣告之前。

例如，以下是一個使用最上層陳述式的簡單 C# 程式：

```csharp
using System;

Console.WriteLine("Hello, World!");
```

在這個例子中，`Console.WriteLine("Hello, World!");` 是最上層陳述式，並且它位於任何命名空間或類別宣告之前。這樣的程式碼會自動被編譯器包裝在一個隱含的 `Main` 方法中[1](https://learn.microsoft.com/zh-tw/dotnet/csharp/fundamentals/program-structure/top-level-statements).

如果您需要在同一檔案中包含命名空間或類別宣告，它們必須位於最上層陳述式之後，例如：

```csharp
using System;

Console.WriteLine("Hello, World!");

namespace MyNamespace
{
    class MyClass
    {
        public static void MyMethod()
        {
            Console.WriteLine("Hello from MyNamespace.MyClass.MyMethod!");
        }
    }
}
```

這樣的設計使得撰寫簡單的程式更加方便，特別適合於小型公用程式或教學範例[1](https://learn.microsoft.com/zh-tw/dotnet/csharp/fundamentals/program-structure/top-level-statements).

希望這對您有幫助！如果您有其他問題，隨時告訴我哦。

[1](https://learn.microsoft.com/zh-tw/dotnet/csharp/fundamentals/program-structure/top-level-statements): [Microsoft Learn](https://learn.microsoft.com/zh-tw/dotnet/csharp/fundamentals/program-structure/top-level-statements)





**筆記
簡單說就是 main 維持在最上方
其餘 class 宣告 放下方即可

參考附件檔案
範例為 LeetCode 2. Add Two Numbers 程式碼
