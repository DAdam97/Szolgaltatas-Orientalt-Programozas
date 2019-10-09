using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SOP
{
    class Producer
    {
        private int osztó;
        private int mennyiség;
        private string név;
        private Random rnd = new Random();
        private int eddigElőállitott = 0;

        public void Termel()
        {
            lock (typeof(Buffer))
            {
                Buffer.producersCount++;
                lock (typeof(Console))
                {
                   // Buffer.WriteBufferState();
                    Console.WriteLine(név + " elindult");
                }
            }

            while (eddigElőállitott != mennyiség)
            {
                int termék = rnd.Next(10000, 90000);

                if (termék % osztó == 0)
                {
                    eddigElőállitott++;

                    lock (Buffer.buffer)
                    {
                        while (Buffer.buffer.Count >= 100)
                        {
                            lock (typeof(Console))
                                Console.WriteLine("Várok egy fogyasztóra. ({0})", this.név);
                            Monitor.Wait(Buffer.buffer);
                        }

                        Buffer.buffer.Add(termék);
                        lock (typeof(Console))
                        {
                           // Buffer.WriteBufferState();
                            Console.WriteLine("{0} Berakta: " + termék, this.név);
                           // Buffer.WriteBufferState();
                        }
                        Monitor.PulseAll(Buffer.buffer);
                    }
                }
            }

            Buffer.ProducersStop(név);

        }

        public Producer(string név, int osztható, int mennyiség)
        {
            this.név = név;
            this.osztó = osztható;
            this.mennyiség = mennyiség;
        }
    }
}
