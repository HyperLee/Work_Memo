# C# .NET 8.0 物件導向開發規格書

## 專案概述
- **專案名稱**：物件導向程式設計示範
- **開發語言**：C# 
- **平台**：.NET 8.0
- **專案類型**：Console Application

## 物件導向程式設計（OOP）基礎概念

物件導向程式設計是一種程式設計典範，它使用「物件」的概念來表示數據和方法。在C#中，物件導向程式設計基於以下四個核心概念：

### 1. 封裝 (Encapsulation)
封裝是將數據和操作數據的方法綁定在一起，並隱藏對象的內部狀態和實現細節。

### 2. 繼承 (Inheritance)
繼承允許一個類（子類）獲取另一個類（父類）的屬性和方法，促進代碼重用。

### 3. 多型 (Polymorphism)
多型使不同的類可以實現相同的接口或繼承相同的基類，但以不同方式響應相同的方法。

### 4. 抽象 (Abstraction)
抽象是通過隱藏複雜性和只顯示必要特性來簡化系統的過程。

## C# 中的物件導向實作

### 類與物件

```csharp
// 定義一個類
public class Person
{
    // 屬性
    public string Name { get; set; }
    public int Age { get; set; }
    
    // 建構函數
    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
    
    // 方法
    public void Introduce()
    {
        Console.WriteLine($"你好，我是 {Name}，今年 {Age} 歲。");
    }
}

// 建立物件
Person person = new Person("張三", 30);
person.Introduce();
```

### 封裝示例

```csharp
public class BankAccount
{
    // 私有欄位
    private decimal _balance;
    private string _accountNumber;
    
    // 公開屬性
    public string AccountHolder { get; set; }
    
    // 唯讀屬性
    public decimal Balance 
    { 
        get { return _balance; } 
    }
    
    // 建構函數
    public BankAccount(string accountHolder, string accountNumber)
    {
        AccountHolder = accountHolder;
        _accountNumber = accountNumber;
        _balance = 0;
    }
    
    // 公開方法
    public void Deposit(decimal amount)
    {
        if (amount > 0)
        {
            _balance += amount;
            Console.WriteLine($"存款 {amount:C} 成功。當前餘額: {_balance:C}");
        }
    }
    
    public bool Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("提款金額必須大於零。");
            return false;
        }
        
        if (amount > _balance)
        {
            Console.WriteLine("餘額不足。");
            return false;
        }
        
        _balance -= amount;
        Console.WriteLine($"提款 {amount:C} 成功。當前餘額: {_balance:C}");
        return true;
    }
}
```

### 繼承示例

```csharp
// 基礎類別
public class Vehicle
{
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    
    public Vehicle(string make, string model, int year)
    {
        Make = make;
        Model = model;
        Year = year;
    }
    
    public virtual void DisplayInfo()
    {
        Console.WriteLine($"車輛: {Year} {Make} {Model}");
    }
}

// 派生類別
public class Car : Vehicle
{
    public int Doors { get; set; }
    
    public Car(string make, string model, int year, int doors)
        : base(make, model, year)
    {
        Doors = doors;
    }
    
    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"門數: {Doors}");
    }
}
```

### 多型示例

```csharp
// 基礎類別
public abstract class Shape
{
    // 抽象方法
    public abstract double CalculateArea();
    
    // 虛擬方法
    public virtual void Display()
    {
        Console.WriteLine($"面積: {CalculateArea()}");
    }
}

// 派生類別 - 圓形
public class Circle : Shape
{
    public double Radius { get; set; }
    
    public Circle(double radius)
    {
        Radius = radius;
    }
    
    public override double CalculateArea()
    {
        return Math.PI * Radius * Radius;
    }
    
    public override void Display()
    {
        Console.WriteLine($"圓形，半徑: {Radius}");
        base.Display();
    }
}

// 派生類別 - 矩形
public class Rectangle : Shape
{
    public double Width { get; set; }
    public double Height { get; set; }
    
    public Rectangle(double width, double height)
    {
        Width = width;
        Height = height;
    }
    
    public override double CalculateArea()
    {
        return Width * Height;
    }
    
    public override void Display()
    {
        Console.WriteLine($"矩形，寬: {Width}，高: {Height}");
        base.Display();
    }
}
```

### 接口（Interface）實作

```csharp
// 定義接口
public interface IPayable
{
    decimal CalculatePayment();
}

// 實作接口的類別
public class Employee : IPayable
{
    public string Name { get; set; }
    public decimal HourlyRate { get; set; }
    public int HoursWorked { get; set; }
    
    public decimal CalculatePayment()
    {
        return HourlyRate * HoursWorked;
    }
}

public class Invoice : IPayable
{
    public string PartNumber { get; set; }
    public int Quantity { get; set; }
    public decimal PricePerItem { get; set; }
    
    public decimal CalculatePayment()
    {
        return Quantity * PricePerItem;
    }
}
```

## 物件導向設計原則

### 1. 單一責任原則 (SRP)
一個類應該只有一個改變的原因，即一個類應該只負責一項職責。

### 2. 開放/封閉原則 (OCP)
軟體實體應該對擴展開放，對修改封閉。

### 3. 里氏替換原則 (LSP)
子類型必須能夠替換其基類型而不影響程式的正確性。

### 4. 介面隔離原則 (ISP)
客戶端不應該被迫依賴於它們不使用的方法。

### 5. 依賴反轉原則 (DIP)
高層模組不應該依賴於低層模組，兩者都應該依賴於抽象。

## 專案結構

```
ProjectRoot/
│
├── Program.cs             # 主程式進入點
├── Models/                # 實體類別
│   ├── Person.cs
│   ├── BankAccount.cs
│   └── ...
│
├── Interfaces/            # 介面定義
│   ├── IPayable.cs
│   └── ...
│
└── Services/              # 服務類別
    ├── PaymentService.cs
    └── ...
```

## 實作注意事項

1. **命名規範**
   - 類名和接口名使用 PascalCase
   - 方法和屬性使用 PascalCase
   - 變數和參數使用 camelCase

2. **代碼品質**
   - 遵循物件導向設計原則
   - 適當使用註解
   - 實作單元測試

3. **效能考量**
   - 避免過度使用繼承
   - 適當使用集合類型
   - 注意記憶體使用和釋放
