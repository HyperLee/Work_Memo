using System;

namespace OOPDemo
{
    // 主程式類別
    internal class Program
    {
        /// <summary>
        /// 以固定資料驗證封裝、繼承、多型、介面與抽象類別示範。
        /// </summary>
        /// <param name="args">命令列參數；本示範不使用外部輸入。</param>
        static void Main(string[] args)
        {
            int total = 0;
            int passed = 0;

            RunCase(
                "封裝-銀行帳戶",
                "balance=1200;withdraw=true;rejected=true",
                EvaluateBankAccount,
                ref total,
                ref passed);

            RunCase(
                "繼承-多型",
                "Vehicle|Car:4|Motorcycle:跑車",
                EvaluateVehicles,
                ref total,
                ref passed);

            RunCase(
                "介面-付款",
                "employee=8000;invoice=1000",
                EvaluatePayments,
                ref total,
                ref passed);

            RunCase(
                "抽象類別-形狀",
                "circle=78.539816;rectangle=24",
                EvaluateShapes,
                ref total,
                ref passed);

            Console.WriteLine($"Summary: {passed}/{total} checks passed.");
            if (passed != total)
            {
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// 驗證銀行帳戶的存款、成功提款、餘額不足拒絕與餘額封裝。
        /// </summary>
        /// <returns>固定格式的銀行帳戶結果。</returns>
        private static string EvaluateBankAccount()
        {
            TextWriter originalOutput = Console.Out;
            try
            {
                using StringWriter suppressedOutput = new StringWriter();
                Console.SetOut(suppressedOutput);

                BankAccount account = new BankAccount("王小明", "TW-123456789");
                account.Deposit(1000);
                account.Deposit(500);
                bool withdrawSucceeded = account.Withdraw(300);
                bool insufficientFundsRejected = !account.Withdraw(2000);
                return $"balance={account.Balance};withdraw={withdrawSucceeded.ToString().ToLowerInvariant()};" +
                       $"rejected={insufficientFundsRejected.ToString().ToLowerInvariant()}";
            }
            finally
            {
                Console.SetOut(originalOutput);
            }
        }

        /// <summary>
        /// 驗證 Vehicle 陣列透過虛擬方法保存不同衍生型別的資料。
        /// </summary>
        /// <returns>固定格式的型別與專屬屬性結果。</returns>
        private static string EvaluateVehicles()
        {
            Vehicle[] vehicles =
            {
                new Vehicle("Toyota", "Corolla", 2020),
                new Car("Honda", "Civic", 2021, 4),
                new Motorcycle("Yamaha", "MT-07", 2022, "跑車")
            };

            return string.Join(
                "|",
                vehicles.Select(vehicle => vehicle switch
                {
                    Car car => $"Car:{car.Doors}",
                    Motorcycle motorcycle => $"Motorcycle:{motorcycle.Type}",
                    _ => "Vehicle"
                }));
        }

        /// <summary>
        /// 驗證 Employee 與 Invoice 透過 IPayable 介面提供不同付款計算。
        /// </summary>
        /// <returns>固定格式的兩種付款結果。</returns>
        private static string EvaluatePayments()
        {
            IPayable employee = new Employee { Name = "李小華", HourlyRate = 200, HoursWorked = 40 };
            IPayable invoice = new Invoice { PartNumber = "A-123", Quantity = 10, PricePerItem = 100 };
            return $"employee={employee.CalculatePayment()};invoice={invoice.CalculatePayment()}";
        }

        /// <summary>
        /// 驗證 Circle 與 Rectangle 透過抽象 Shape 計算面積。
        /// </summary>
        /// <returns>固定格式的兩種形狀面積。</returns>
        private static string EvaluateShapes()
        {
            Shape[] shapes = { new Circle(5), new Rectangle(4, 6) };
            double circleArea = Math.Round(shapes[0].CalculateArea(), 6);
            double rectangleArea = Math.Round(shapes[1].CalculateArea(), 6);
            return $"circle={circleArea.ToString(System.Globalization.CultureInfo.InvariantCulture)};" +
                   $"rectangle={rectangleArea.ToString(System.Globalization.CultureInfo.InvariantCulture)}";
        }

        /// <summary>
        /// 執行一個物件導向示範案例並輸出統一的驗證結果。
        /// </summary>
        /// <param name="name">案例名稱。</param>
        /// <param name="expected">預期結果。</param>
        /// <param name="actualFactory">產生實際結果的函式。</param>
        /// <param name="total">累積案例數。</param>
        /// <param name="passed">累積通過數。</param>
        private static void RunCase(string name, string expected, Func<string> actualFactory, ref int total, ref int passed)
        {
            total++;
            string actual;
            try
            {
                actual = actualFactory();
            }
            catch (Exception exception)
            {
                actual = $"EXCEPTION: {exception.GetType().Name}: {exception.Message}";
            }

            bool isPassed = actual == expected;
            if (isPassed)
            {
                passed++;
            }

            Console.WriteLine($"[{name}]");
            Console.WriteLine($"Expected: {expected}");
            Console.WriteLine($"Actual: {actual}");
            Console.WriteLine($"PASS-FAIL: {(isPassed ? "PASS" : "FAIL")}");
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
        private readonly string _accountNumber;

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