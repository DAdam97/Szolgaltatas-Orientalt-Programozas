using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SOP
{
    class Customer
    {
        private string név;
        private ConsoleColor szin;
        private int termék;

        public void Fogyaszt()
        {
           // Buffer.WriteBufferState();
            Kiir(név + " elindult");
            lock (Buffer.buffer)
            {
                while (Buffer.producersCount != 0)
                {
                    if (Buffer.buffer.Count == 0)
                    {
                        Kiir(név + " vár egy számra");
                        Monitor.Wait(Buffer.buffer);
                        Kiir(név + " nem vár már");
                    }

                    if (Buffer.buffer.Count != 0)
                    {
                      // Buffer.WriteBufferState();

                        termék = Buffer.buffer[0];
                        Buffer.buffer.RemoveAt(0);

                      // Buffer.WriteBufferState();

                        Monitor.PulseAll(Buffer.buffer);
                        Kiir(név + " kivette: " + termék);
                    }
                }

                Kiir(név + " a termelők hiánya miatt leállt!");
            }
        }


        private void Kiir(string szöveg)
        {
            lock (typeof(Console))
            {
                Console.ForegroundColor = szin;
                Console.WriteLine(szöveg);
                Console.ResetColor();
            }
        }

        public Customer(string név, ConsoleColor szin)
        {
            this.név = név;
            this.szin = szin;
        }
    }
}
