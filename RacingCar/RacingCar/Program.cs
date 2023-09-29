static class Program
{
    private static SemaphoreSlim box = new SemaphoreSlim(3);
    private static SemaphoreSlim start = new SemaphoreSlim(0);
    private static SemaphoreSlim end = new SemaphoreSlim(0);
    private static SemaphoreSlim race = new SemaphoreSlim(0);

    static void Main()
    {
        Car car1 = new Car("VER");
        new Thread(car1.Run).Start();
        Car car2 = new Car("LEC");
        new Thread(car2.Run).Start();
        Car car3 = new Car("NOR");
        new Thread(car3.Run).Start();
        Car car4 = new Car("HUL");
        new Thread(car4.Run).Start();
        Car car5 = new Car("HAM");
        new Thread(car5.Run).Start();

        static void main()
        {
            Car car1 = new Car("VER");
            Car car2 = new Car("LEC");
            Car car3 = new Car("NOR");
            Car car4 = new Car("HUL");
            Car car5 = new Car("HAM");
        }
    }

    public class Car {
    public string Racer { get; set; }
    public Car(string racer) {
        Racer = racer;
        }
    
    public void Run()
    {
        WaitForSignal();
        start.Release();
        race.Wait();
        Race();
        box.Wait();
        TakingPistop();
        box.Release();
        Race();
        end.Release();
    }

    private void WaitForSignal(){
        Console.WriteLine(
            $"{Racer}: Waiting for Start Signal");
        Thread.Sleep(200);
        }
    private void Race(){
        Console.WriteLine($"{Racer}: Racing");
        Thread.Sleep(1500);
        }
    private void TakingPistop(){
        Console.WriteLine(
            $"{Racer}: Taking Pit stop");
        Thread.Sleep(500);
        }
    }
    
    
    public class F1Race {
        public void Run()
        {
            start.Wait();
            Start();
            race.Release();
            end.Wait();
            End();
        }

        private void Start(){
        Console.WriteLine("Starting Race");
        Thread.Sleep(1000);
        }
    
    private void End(){
        Console.WriteLine("Race finished");
        }
    }
}