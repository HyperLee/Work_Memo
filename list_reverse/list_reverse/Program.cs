using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace list_reverse
{
    class Program
    {
        /// <summary>
        /// https://docs.microsoft.com/zh-tw/dotnet/api/system.collections.generic.list-1.reverse?view=net-6.0
        /// list reverse
        /// list 反轉
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            List<string> dinosaurs = new List<string>();

            
            dinosaurs.Add("Pachycephalosaurus");
            dinosaurs.Add("Parasauralophus");
            dinosaurs.Add("Mamenchisaurus");
            dinosaurs.Add("Amargasaurus");
            dinosaurs.Add("Coelophysis");
            dinosaurs.Add("Oviraptor");
            

            /*
            dinosaurs.Add("1");
            dinosaurs.Add("2");
            dinosaurs.Add("4");
            dinosaurs.Add("3");
            */

            Console.WriteLine();
            foreach (string dinosaur in dinosaurs)
            {
                Console.WriteLine("Before: " + dinosaur);
            }

            dinosaurs.Reverse();

            Console.WriteLine();
            foreach (string dinosaur in dinosaurs)
            {
                Console.WriteLine("After: " + dinosaur);
            }

            /*
            dinosaurs.Reverse(1, 4);

            Console.WriteLine();
            foreach (string dinosaur in dinosaurs)
            {
                Console.WriteLine(dinosaur);
            }
            */
            

            Console.ReadKey();

        }
    }
}
