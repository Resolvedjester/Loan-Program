class LoanProgram
{
    public double DownPayment { get; set; }
    public double Purchase { get; set; }
    public double AnnualInterestRate { get; set; }
    public int LoanTermYear { get; set; }
    public bool NeedInsurance = false;
    public bool HOAEnable { get; set; }
    public double HOAYearly { get; set; }
    public double PropertyTax = 0.0125d;
    public double PropertyTaxMonthly { get; set; }
    public double HomeInsurance = 0.0075d;
    public double HomeInsuranceMonthly { get; set; }
    public double MonthlyIncome { get; set; }
    public double TotalMonthlyPay { get; set; }
    public static int ClosingFee = 2500;
    public static int PaymentsPerYear = 12;




    public double CalcBaseMonthly()
    {
        double r = AnnualInterestRate / PaymentsPerYear;
        int n = PaymentsPerYear * LoanTermYear;
        double m = Math.Pow(1 + r, n);
        return Purchase * r * (m / (m - 1));
    }

    public double TotalLoanValue()
    {
        return (Purchase - DownPayment) * 1.01 + ClosingFee;
    }

    public double EquityP()
    {
        return 100 - (((Purchase - DownPayment) * 100) / Purchase);
    }

    public void HomeLoanNeeded()
    {
        if (EquityP() < 10)
        {
            NeedInsurance = true;
        }
    }

    public double LoanInsurance()
    {
        return TotalLoanValue() * 0.01 * LoanTermYear / (LoanTermYear * 12);
    }

    public double HOAFee()
    {
        return (HOAYearly / 12);
    }

    public double EscrowMonthly()
    {
        HomeInsuranceMonthly = Purchase * HomeInsurance / 12;
        PropertyTaxMonthly = Purchase * PropertyTax / 12;
        return HomeInsuranceMonthly + PropertyTaxMonthly;
    }


    public double TotalMonthly()
    {
        TotalMonthlyPay = CalcBaseMonthly() + EscrowMonthly();

        if (NeedInsurance)
        {
            TotalMonthlyPay += LoanInsurance();
        }

        if (HOAEnable)
        {
            TotalMonthlyPay += HOAFee();
        }
        return TotalMonthlyPay;

    }

    public void ApDnDecision()
    {
        if (TotalMonthly() >= (MonthlyIncome * 0.25))
        {
            Console.WriteLine("Customer is denied for the current loan.  Please either increase your down payment or find a more afforadable home.");
        }

        else
        {
            Console.WriteLine("Customer is approved for the current loan!");
        }

    }

}