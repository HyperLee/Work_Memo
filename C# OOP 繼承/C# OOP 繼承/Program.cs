using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C__OOP_繼承
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
            var speed = 0;

            Honda honda = new Honda();
            honda.Color = "Red";
            honda.Speed = 60;

            Nissan nissan = new Nissan();
            nissan.Color = "Blue";
            nissan.Speed = 80;

            Console.WriteLine($"建立一台汽車，廠牌：{honda.GetType().Name}，顏色：{honda.Color}，目前速度：{honda.Speed}。");
            Console.WriteLine();
            Console.WriteLine($"建立一台汽車，廠牌：{nissan.GetType().Name}，顏色：{nissan.Color}，目前速度：{nissan.Speed}。");
            Console.WriteLine();

            Console.WriteLine($"Honda進行加速，輸入要加速的速度？");
            speed = Convert.ToInt32(Console.ReadLine());
            honda.Accelerate(speed);
            Console.WriteLine($"Honda汽車加速至{honda.Speed}");
            Console.WriteLine();

            Console.WriteLine($"Nissan進行加速，輸入要加速的速度？");
            speed = Convert.ToInt32(Console.ReadLine());
            nissan.Accelerate(speed);
            Console.WriteLine($"Nissan汽車加速至{nissan.Speed}");
            Console.WriteLine();

            Console.WriteLine($"Honda進行減速，輸入要減速的速度？");
            speed = Convert.ToInt32(Console.ReadLine());
            honda.Brake(speed);
            Console.WriteLine($"Honda汽車減速至{honda.Speed}");
            Console.WriteLine();

            Console.WriteLine($"Nissan進行減速，輸入要減速的速度？");
            speed = Convert.ToInt32(Console.ReadLine());
            nissan.Brake(speed);
            Console.WriteLine($"Nissan汽車減速至{nissan.Speed}");
            Console.ReadKey();
        }
    }


    /// <summary>
    /// class car 基本資訊
    /// </summary>
    public class Car
    {
        public string Color { get; set; }
        public int Speed { get; set; }


        /// <summary>
        /// 加速
        /// </summary>
        /// <param name="speed"></param>
        public virtual void Accelerate(int speed)
        {
            Speed += speed;
            if (Speed > 200)
            {
                Speed = 200;
            }
        }


        /// <summary>
        /// 煞車
        /// </summary>
        /// <param name="speed"></param>
        public void Brake(int speed)
        {
            Speed -= speed;
            if (Speed < 0)
            {
                Speed = 0;
            }
        }
    }


    /// <summary>
    /// 繼承 car
    /// </summary>
    public class Honda : Car
    {

    }


    /// <summary>
    /// 繼承 car
    /// </summary>
    public class Nissan : Car
    {

    }


}
