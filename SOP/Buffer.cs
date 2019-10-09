using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SOP
{
    class Buffer
    {
        public static List<int> buffer = new List<int>();
        public static int producersCount = 0;
        public static int customersCount = 0;

        public static void ProducersStop(string név)
        {
            lock (typeof(Buffer))
            {
                producersCount--;

                lock (typeof(Console))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Leállt " + név);
                    Console.ResetColor();
                }

                if (producersCount == 0)
                {
                    lock (buffer)
                    {
                        Monitor.PulseAll(buffer);
                    }
                }
            }
        }

        public static void CustomersStop()
        {
            customersCount--;

            lock (typeof(Console))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Leállt egy fogyasztó!");
                Console.ResetColor();
            }

            if (customersCount == 0)
            {
                lock (buffer)
                {
                    Monitor.PulseAll(buffer);
                }
            }
        }

        public static void WriteBufferState()
        {
            lock (typeof(Console))
            {
                int left = Console.CursorLeft;
                int top = Console.CursorTop;

                Console.SetCursorPosition(0, 0);
                Console.ForegroundColor = ConsoleColor.Green;

                lock (buffer)
                {
                    Console.WriteLine("Buffer mennyiség: " + buffer.Count);
                    Monitor.PulseAll(buffer);
                }

                Console.ResetColor();
                Console.SetCursorPosition(left, top);
            }
        }
    }
}
