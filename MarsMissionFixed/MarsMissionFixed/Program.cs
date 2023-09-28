using System.Runtime.InteropServices;

static class Program
{
    private static SemaphoreSlim scan = new SemaphoreSlim(0);
    private static SemaphoreSlim ack = new SemaphoreSlim(0);
    private static SemaphoreSlim store = new SemaphoreSlim(2);
    private static object auflock = new object();

    static Queue<string> goofy = new Queue<string>();

    public static void Main()
    {
        Sentinel sen1 = new Sentinel("s1");
        Sentinel sen2 = new Sentinel("s2");
        Harvester har1 = new Harvester("h1");
        Harvester har2 = new Harvester("h2");
        Harvester har3 = new Harvester("h3");
        Harvester har4 = new Harvester("h4");

        new Thread(sen1.Run).Start();
        new Thread(sen2.Run).Start();
        new Thread(har1.Run).Start();
        new Thread(har2.Run).Start();
        new Thread(har3.Run).Start();
        new Thread(har4.Run).Start();   
    }

    public class Sentinel
    {
        public string Code { get; set; }

        public Sentinel(string code)
        {
            Code = code;
        }

        public void Run()
        {
            while (true)
            {
                ScanningSurface();
                Signal();
                lock (auflock)
                {
                    goofy.Enqueue(Code);
                    ack.Release();
                }
                scan.Wait();
            }
        }

        private void ScanningSurface()
        {
            Console.WriteLine($"{Code}: Scanning Surface");
            Thread.Sleep(500);
        }

        private void Signal()
        {
            Thread.Sleep(800);
            Console.WriteLine($"{Code}: Found raw material");
        }
    }

    private class Harvester
    {
        private string Code { get; set; }

        public Harvester(string code)
        {
            Code = code;
        }

        public void Run()
        {
            while (true)
            {
                lock (auflock)
                {
                    ack.Wait();
                    Acknowledge();
                }
                scan.Release();
                Harvest();
                store.Wait();
                Store();
                store.Release(); 
            }
        }

        private void Acknowledge()
        {
            lock (auflock)
            {
                Console.WriteLine($"{Code}: Acknowledging signal + {{0}}", goofy.Dequeue());
                Thread.Sleep(100);
            }
        }
        
        private void Harvest()
        {
            Console.WriteLine($"{Code}: Harvesting resources");
            Thread.Sleep(100);
        }

        private void Store()
        {
            Console.WriteLine($"{Code}: Storing resources");
            Thread.Sleep(200);
        }
    }
}

