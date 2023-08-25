using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C__OOP_封裝
{
    internal class Program
    {
        /// <summary>
        /// 來源:
        /// https://coreychen71.github.io/posts/2019-10/oop/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Car car = new Car();
            car.Brand = "Honda";
            car.Color = "Blue";
            car.Speed = 60;

            Console.WriteLine($"建立一台汽車，廠牌：{car.Brand}，顏色：{car.Color}，目前速度：{car.Speed}。");
            Console.WriteLine();
            Console.WriteLine($"進行加速，輸入要加速的速度？");
            var speed = Convert.ToInt32(Console.ReadLine());
            car.Accelerate(speed);
            Console.WriteLine($"汽車加速至{car.Speed}");
            Console.WriteLine();
            Console.WriteLine($"進行減速，輸入要減速的速度？");
            speed = Convert.ToInt32(Console.ReadLine());
            car.Brake(speed);
            Console.WriteLine($"汽車減速至{car.Speed}");
            Console.ReadKey();

        }
    }


    public class Car
    {
        public string Brand { get; set; }
        public string Color { get; set; }
        public int Speed { get; set; }

        public void Accelerate(int speed)
        {
            Speed += speed;
            if (Speed > 200)
            {
                Speed = 200;
            }
        }

        public void Brake(int speed)
        {
            Speed -= speed;
            if (Speed < 0)
            {
                Speed = 0;
            }
        }
    }

}
