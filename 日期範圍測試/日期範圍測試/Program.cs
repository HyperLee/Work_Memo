namespace 日期範圍測試
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DateTime LDs, LDe;
            int cycle7day = 7;
            try
            {
                LDe = DateTime.Today;

                // - 7 前七天時間
                LDs = LDe.AddDays(Convert.ToInt32('-' + cycle7day));

                Console.WriteLine("LDs: " + LDs);

                Console.WriteLine("LDe: " + LDe);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine("test: " + Convert.ToInt32(cycle7day));

            Console.ReadKey();
        }
    }
}
