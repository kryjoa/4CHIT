using System.Diagnostics;

public class Heist {
    /*
     * Implementieren Sie die EvaluateHeistAsync
     * Methode
     */  
    public async Task EvaluateHeistAsync()  
    {  
        await Task.WhenAll(
            HireCrew(),
            GetBankPlans(),
            BribeBankEmployee(),
            BuyGetawayCar()
        );

        await EnterBank();
        
        await Task.WhenAll(
            RobCounter1(),
            RobCounter2(),
            RobCounter3()
        );

        await LeaveBank();

        await LosePolice();
    }
	
    private async Task BribeBankEmployee()  
    {  
        await Task.Delay(300);  
        Console.WriteLine(ELog.BRIBE_BANK_EMPLOYEE.ToString());  
    }  
	  
    private async Task BuyGetawayCar() {  
        await Task.Delay(200);  
        Console.WriteLine(ELog.BUY_GETAWAY_CAR.ToString());  
    }  
	  
    private async Task GetBankPlans() {  
        await Task.Delay(200);  
        Console.WriteLine(ELog.GET_BANK_PLAN.ToString());  
    }  
	  
    private async Task HireCrew() {  
        await Task.Delay(400);  
        Console.WriteLine(ELog.HIRE_CREW.ToString());  
    }  
	  
    private async Task EnterBank() {  
        await Task.Delay(100);  
        Console.WriteLine(ELog.ENTER_BANK.ToString());  
    }  
	  
    private async Task RobCounter1() {  
        await Task.Delay(300);  
        Console.WriteLine(ELog.ROB_COUNTER_1.ToString());  
    }  
	  
    private async Task RobCounter2() {  
        await Task.Delay(300);  
        Console.WriteLine(ELog.ROB_COUNTER_2.ToString());  
    }  
	  
    private async Task RobCounter3() {  
        await Task.Delay(300);  
        Console.WriteLine(ELog.ROB_COUNTER_3.ToString());  
    }  
	  
    private async Task LeaveBank() {  
        await Task.Delay(120);  
        Console.WriteLine(ELog.LEAVE_BANK.ToString());  
    }  
	  
    private async Task LosePolice() {  
        await Task.Delay(300);  
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
        await new Heist().EvaluateHeistAsync();
        Console.WriteLine();
        Console.WriteLine(sw.Elapsed);
    }
}