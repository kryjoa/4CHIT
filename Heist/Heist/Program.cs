using System.Diagnostics;

public class Heist {
    /*
     * Implementieren Sie die EvaluateHeistAsync
     * Methode
     */  
    public async Task EvaluateHeist()
    {
        Stopwatch sw1 = new Stopwatch();
        sw1.Start();
        Task[] step1 = {
            Task.Run(()=>HireCrew()),
            Task.Run(()=>GetBankPlans()),
            Task.Run(()=>BribeBankEmployee()),
            Task.Run(()=>BuyGetawayCar())
        };
        Task.WaitAll(step1);
        Console.WriteLine(sw1.Elapsed);

        EnterBank();
        
        Task[] step3 = {
            Task.Run(()=>RobCounter1()),
            Task.Run(()=>RobCounter2()),
            Task.Run(()=>RobCounter3())
        };
        Task.WaitAll(step3);

        LeaveBank();

        LosePolice();
    }
	
    private void BribeBankEmployee()  
    {  
        Task.Delay(300);  
        Console.WriteLine(ELog.BRIBE_BANK_EMPLOYEE.ToString());
    }  
	  
    private void BuyGetawayCar() {  
        Task.Delay(200);  
        Console.WriteLine(ELog.BUY_GETAWAY_CAR.ToString());  
    }  
	  
    private void GetBankPlans() {  
        Task.Delay(200);  
        Console.WriteLine(ELog.GET_BANK_PLAN.ToString()); 
    }  
	  
    private void HireCrew() {  
        Task.Delay(400);  
        Console.WriteLine(ELog.HIRE_CREW.ToString()); 
    }  
	  
    private void EnterBank() {  
        Task.Delay(100);  
        Console.WriteLine(ELog.ENTER_BANK.ToString());  
    }  
	  
    private void RobCounter1() {  
        Task.Delay(300);  
        Console.WriteLine(ELog.ROB_COUNTER_1.ToString());  
    }  
	  
    private void RobCounter2() {  
        Task.Delay(300);  
        Console.WriteLine(ELog.ROB_COUNTER_2.ToString());  
    }  
	  
    private void RobCounter3() {  
        Task.Delay(300);  
        Console.WriteLine(ELog.ROB_COUNTER_3.ToString());  
    }  
	  
    private void LeaveBank() {  
        Task.Delay(120);  
        Console.WriteLine(ELog.LEAVE_BANK.ToString());  
    }  
	  
    private void LosePolice() {  
        Task.Delay(300);  
        Console.WriteLine(ELog.LOSE_POLICE.ToString());  
    }
}

public enum ELog {  
    BRIBE_BANK_EMPLOYEE,  
    BUY_GETAWAY_CAR,  
    GET_BANK_PLAN,  
    HIRE_CREW,  
    ENTER_BANK,  
    ROB_COUNTER_1,  
    ROB_COUNTER_2,  
    ROB_COUNTER_3,  
    LEAVE_BANK,  
    LOSE_POLICE  
}

public class Program
{
    public static async Task Main(string[] args)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        await new Heist().EvaluateHeist();
        Console.WriteLine();
        Console.WriteLine(sw.Elapsed);
    }
}