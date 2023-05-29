using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_test
{
    class Animal
    {
        protected string name;

        public Animal()
        {
            name = "動物";
        }

        public Animal(string s)
        {
            name = s;
        }

        public void speak()
        {
            Console.WriteLine("哈囉，我是{0}．．．．", name);
            //Console.WriteLine("~~~~");
        }


    }


    class Elephant : Animal
    {
        public Elephant()
        {
            name = "大象";
        }
    }


    class Mouse : Animal
    {
        public Mouse()
        {
            name = "老鼠";
        }
    }


    ///////////////////
    class People
    {
        public double Height;

        public double weight;

        public bool gender;

        public double BMI()
        {
            return weight / (Height * Height / 1000);
        }

    }
    class Woman : People
    {
        public int Hips;//臀圍
        public int Waistline;//腰圍
        public int bust;//胸圍(cup問題在此略過)
        public int age;//年齡
    }

    class Man
    {
        public double Property;//財產單位(萬)(不動產與動產合計之)
        public int Educational_Level;//學歷:1~5(小學~博士)
        public int OL;//職等1~10(二等專員、高等專員、副科長、科長、副理、經理、副總經理、總經理、副董、董事長)
        public int age;//年齡
    }
    ////////////////////


    /// <summary>
    /// http://kaiching.org/pydoing/cs-guide/unit-12-object-oriented-programming.html
    /// https://dotblogs.com.tw/pharaohs_treasure/2016/05/21/oop
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            /* // oop 1
            var animals = new List<Animal>
            {
                new Animal("ddd"),
                new Animal(),
                new Elephant(),
                new Mouse()
            };
            foreach (var animal in animals)
            {
                animal.speak();
                //Console.WriteLine("output: " + animal.speak);
            }
            Console.ReadKey();
            */

            Animal a1 = new Animal();
            a1.speak();
            Animal a2 = new Animal("1213");
            a2.speak();
            Elephant e1 = new Elephant();
            e1.speak();
            Mouse m1 = new Mouse();
            m1.speak();
            Console.ReadKey();

            


            /* // oop 2 
            People p1 = new People();
            p1.weight = 70;
            p1.Height = 180;
            double aa = 0.0;
            aa = p1.BMI();

            Woman w1 = new Woman();
            w1.Hips = 30;

            Man m1 = new Man();
            m1.age = 44;

            Console.WriteLine("BMI = " + aa);
            Console.ReadKey();
            */

        }
    }
}
