using System;

namespace OOPDemo
{
    // 主程式類別
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("C# .NET 8.0 物件導向程式設計示範");
            Console.WriteLine("================================");

            // 封裝示範
            DemonstrateBankAccount();
            
            Console.WriteLine();
            
            // 繼承與多型示範
            DemonstrateVehicles();
            
            Console.WriteLine();
            
            // 多型與接口示範
            DemonstratePaymentSystem();
            
            Console.WriteLine();
            
            // 形狀多型示範
            DemonstrateShapes();

            Console.WriteLine("\n按任意鍵結束...");
            //Console.ReadKey();
        }

        // 封裝示範
        static void DemonstrateBankAccount()
        {
            Console.WriteLine("===== 封裝示範 =====");
            var account = new BankAccount("王小明", "TW-123456789");
            
            // 測試存款方法
            account.Deposit(1000);
            account.Deposit(500);
            
            // 測試提款方法
            account.Withdraw(300);
            account.Withdraw(2000); // 餘額不足
            
            // 無法直接修改餘額，只能透過方法
            // account.Balance = 5000; // 編譯錯誤，Balance是唯讀的
            
            Console.WriteLine($"帳戶持有人: {account.AccountHolder}");
            Console.WriteLine($"當前餘額: {account.Balance:C}");
        }

        // 繼承與多型示範
        static void DemonstrateVehicles()
        {
            Console.WriteLine("===== 繼承與多型示範 =====");
            
            Vehicle[] vehicles = new Vehicle[]
            {
                new Vehicle("Toyota", "Corolla", 2020),
                new Car("Honda", "Civic", 2021, 4),
                new Motorcycle("Yamaha", "MT-07", 2022, "跑車")
            };
            
            foreach (var vehicle in vehicles)
            {
                vehicle.DisplayInfo(); // 多型行為
                Console.WriteLine();
            }
        }

        // 多型與接口示範
        static void DemonstratePaymentSystem()
        {
            Console.WriteLine("===== 多型與接口示範 =====");
            
            // 建立員工和帳單物件
            Employee employee = new Employee
            {
                Name = "李小華",
                HourlyRate = 200,
                HoursWorked = 40
            };
            
            Invoice invoice = new Invoice
            {
                PartNumber = "A-123",
                Quantity = 10,
                PricePerItem = 100
            };
            
            // 使用接口多型處理付款
            IPayable[] payables = { employee, invoice };
            
            foreach (var payable in payables)
            {
                string type = payable.GetType().Name;
                decimal payment = payable.CalculatePayment();
                Console.WriteLine($"{type} 支付金額: {payment:C}");
            }
        }

        // 形狀多型示範
        static void DemonstrateShapes()
        {
            Console.WriteLine("===== 形狀多型示範 =====");
            
            Shape[] shapes = new Shape[]
            {
                new Circle(5),
                new Rectangle(4, 6)
            };
            
            foreach (var shape in shapes)
            {
                shape.Display();
                Console.WriteLine();
            }
        }
    }

    // 封裝示例 - 銀行帳戶
    public class BankAccount
    {
        // 私有欄位
        private decimal _balance;
        private string _accountNumber;
        
        // 公開屬性
        public string AccountHolder { get; set; }
        
        // 唯讀屬性
        public decimal Balance { get { return _balance; } }
        
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

    // 繼承示例 - 交通工具類別
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

    // 繼承自Vehicle的汽車類別
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
            Console.WriteLine($"類型: 汽車");
            Console.WriteLine($"門數: {Doors}");
        }
    }

    // 繼承自Vehicle的摩托車類別
    public class Motorcycle : Vehicle
    {
        public string Type { get; set; }
        
        public Motorcycle(string make, string model, int year, string type)
            : base(make, model, year)
        {
            Type = type;
        }
        
        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"類型: 摩托車");
            Console.WriteLine($"款式: {Type}");
        }
    }

    // 抽象類別與多型示例
    public abstract class Shape
    {
        public abstract double CalculateArea();
        
        public virtual void Display()
        {
            Console.WriteLine($"面積: {CalculateArea()}");
        }
    }

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

    // 接口示例
    public interface IPayable
    {
        decimal CalculatePayment();
    }

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
}
