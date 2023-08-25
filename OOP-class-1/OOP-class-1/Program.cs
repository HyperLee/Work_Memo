using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_class_1
{
    internal class Program
    {
        /// <summary>
        /// OOP
        /// https://ithelp.ithome.com.tw/articles/10268380
        /// Class
        /// https://ithelp.ithome.com.tw/articles/10268481
        /// https://ithelp.ithome.com.tw/articles/10269967
        /// https://ithelp.ithome.com.tw/articles/10270718
        /// https://ithelp.ithome.com.tw/articles/10271607
        /// -2
        /// https://coreychen71.github.io/posts/2019-10/oop/
        /// 網路教學
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ///////////// cat ///////////////////
            
            Cat cat = new Cat();
            Cat cat2 = new Cat();

            //填寫變數成員
            Console.WriteLine("請輸入姓名:");
            cat.name = Console.ReadLine();

            Console.WriteLine("請輸入年齡:");
            cat.age = Convert.ToInt32(Console.ReadLine());

            //填寫變數成員2
            Console.WriteLine("請輸入姓名2:");
            cat2.name = Console.ReadLine();

            Console.WriteLine("請輸入年齡2:");
            cat2.age = Convert.ToInt32(Console.ReadLine());

            //呼叫方法成員
            //喵喵叫
            cat.Meow();

            //抓到老鼠
            cat.CaseMice();
            cat.CaseMice();

            //呼叫方法成員
            //喵喵叫
            cat2.Meow();

            //抓到老鼠
            cat2.CaseMice();

            Console.WriteLine("--END1--");
            //Console.ReadKey();
            

            //////////// duck  /////////////////////////
            Duck duck = new Duck();
            //設定為三歲
            duck.duckAge = 3;
            Console.WriteLine("duck.duckAge1: " + duck.duckAge);

            //設定為-1歲
            duck.duckAge = -1;
            Console.WriteLine("duck.duckAge2: " + duck.duckAge);

            Console.WriteLine("--END2--");
            Console.ReadKey();

        }
    }

    class Cat
    {
        //名稱
        public string name;
        //年齡
        public int age;
        //老鼠數量
        public int miceCount = 0; //記錄抓幾隻老鼠
        //方法成員
        //方法:打招呼
        public void Hello()
        {
            Console.WriteLine("嗨!我是{0} ", name);
        }
        //方法:喵喵叫
        public void Meow()
        {
            Hello();
            Console.WriteLine("喵~~~~~喵~~~");
        }
        //方法:捉老鼠
        public void CaseMice()
        {
            miceCount++;
            Hello();
            Console.WriteLine("我已經抓了 {0} 隻老鼠", miceCount);
            Console.ReadKey();
        }
    }


    class Duck
    {
        //名稱
        public string name;
        //年齡
        private int age;
        //屬性
        public int duckAge
        {
            get//回傳值
            {
                //回傳設定後的值
                return age;
            }
            set //設定值
            {
                if (value <= 0)
                {
                    age = 0;
                }
                else
                {
                    age = value;
                }
            }
        }

    }

}
