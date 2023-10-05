namespace RacingCar;

static class Program
{
    private static SemaphoreSlim _box = new SemaphoreSlim(3);
    private static SemaphoreSlim _start = new SemaphoreSlim(0);
    private static SemaphoreSlim _end = new SemaphoreSlim(0);
    private static SemaphoreSlim _race = new SemaphoreSlim(0);

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
        
        F1Race austria = new F1Race(5);
        new Thread(austria.Run).Start();
        
    }

    private class Car { 
        private string Racer { get; set; }
        public Car(string racer) {
            Racer = racer;
        }
    
        public void Run()
        {
            WaitForSignal();
            _start.Release();
            _race.Wait();
            Race();
            _box.Wait();
            TakingPitstop();
            _box.Release();
            Race();
            _end.Release();
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
        private void TakingPitstop(){
            Console.WriteLine(
                $"{Racer}: Taking Pit stop");
            Thread.Sleep(500);
        }
    }


    private class F1Race {
        private int Racer { get; set; }

        public F1Race(int racer)
        {
            Racer = racer;
        }
        
        public void Run()
        {
            for (int i = 0; i < 5; i++)
            {
                _start.Wait();
            }
            Start();
            _race.Release(Racer);
            for (int y = 0; y < 5; y++)
            {
                _end.Wait();
            }
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