using System.Collections.Concurrent;
using System.Runtime.InteropServices;

static class Program
{
    private static SemaphoreSlim found = new SemaphoreSlim(0);
    private static SemaphoreSlim store = new SemaphoreSlim(2);
    static BlockingCollection<ResourceFound> rf_queue = new();
    private static object auflock = new object();

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
        private SemaphoreSlim harvesterack = new SemaphoreSlim(0);
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
                var coordinates = new Random().Next(1000);
                Signal(coordinates);
                rf_queue.Add(new ResourceFound()
                {
                    Coordinates = coordinates,
                    Ack = harvesterack
                });
                harvesterack.Wait();
            }
        }

        private void ScanningSurface()
        {
            Console.WriteLine($"{Code}: Scanning Surface");
            Thread.Sleep(500);
        }

        private void Signal(int coord)
        {
            Thread.Sleep(800);
            Console.WriteLine($"{Code}: Found raw material at {coord}");
        }
    }

    public class Harvester
    {
        public string Code { get; set; }

        public Harvester(string code)
        {
            Code = code;
        }

        public void Run()
        {
            while (true)
            {
                var rf = rf_queue.Take();
                Acknowledge(rf.Coordinates);
                rf.Ack.Release();
                Harvest();
                store.Wait();
                Store();
                store.Release();
            }
        }

        private void Acknowledge(int coords)
        {
            Console.WriteLine($"{Code}: Acknowledging signal {coords}");
            Thread.Sleep(100);
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

    public class ResourceFound
    {
        public int Coordinates { get; set; }
        public SemaphoreSlim Ack { get; set; }
    }
}    