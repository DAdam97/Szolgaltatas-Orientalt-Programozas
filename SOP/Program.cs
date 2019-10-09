using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SOP
{
    class Program
    {
        static void Main(string[] args)
        {
            Producer p1 = new Producer("Micsu", 3, 20);
            Producer p2 = new Producer("Ede", 5, 20);
            Producer p3 = new Producer("Kanturo", 7, 30);
            Thread tp1 = new Thread(p1.Termel);
            Thread tp2 = new Thread(p2.Termel);
            Thread tp3 = new Thread(p3.Termel);

            Customer c1 = new Customer("Qio", ConsoleColor.Cyan);
            Customer c2 = new Customer("Kőtulok", ConsoleColor.Blue);
            Customer c3 = new Customer("Kancig", ConsoleColor.Yellow);
            Thread tc1 = new Thread(c1.Fogyaszt);
            Thread tc2 = new Thread(c2.Fogyaszt);
            Thread tc3 = new Thread(c3.Fogyaszt);

            tp1.Start();
            tc1.Start();

            tp2.Start();
            tc2.Start();

            tp3.Start();
            tc3.Start();

            tp1.Join();
            tp2.Join();
            tp3.Join();

            Console.WriteLine(Buffer.buffer.Count);
            Console.ReadLine();
        }
    }
}
