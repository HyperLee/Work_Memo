using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C__OOP_抽象
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
            Console.ReadKey();

        }
    }

    public class Car
    {
        public string Brand { get; set; }
        public string Color { get; set; }
        public int Speed { get; set; }

        public void Accelerate()
        {

        }

        public void Brake()
        {

        }
    }
}
