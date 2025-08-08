using System.Net;
namespace ConsoleApp1;

class Program
{
    static void Main()
    {
        string? ans = "";
        LoanProgram customer1 = new();
        do
        {
            customer1 = CustomerInput();

            Console.Write("Continue? (was all data entered correctly) (Y/N): ");
            ans = Console.ReadLine();
            if (ans != null)
            {
                ans = ans.ToLower();
            }
            AnswerCheckYN(ans);
        } while (ans != "y");


        Console.WriteLine($"Thank you!  Here is what we got!");
        Console.WriteLine($"Purchase Price: ${customer1.Purchase:F2}\tDown Pay: ${customer1.DownPayment:F2}\tAnnual Interest Rate: {100 * customer1.AnnualInterestRate:F2}%\tHOA: {customer1.HOAEnable}:${customer1.HOAYearly:F2}/Yr\tMonthly Income: ${customer1.MonthlyIncome:F2}\n\n");


        for (int i = 15; i <= 30; i += 15)
        {
            customer1.LoanTermYear = i;
            Console.WriteLine($"----------THIS IS DATA FOR {customer1.LoanTermYear} YEAR LOANS----------");
            Console.WriteLine($"Your Total Loan amount (Purchase price - Down Pay + Originator Fee (1%) + Closing ($2500)): ${customer1.TotalLoanValue():F2}");
            Console.WriteLine($"Your loan with interest is: ${customer1.CalcBaseMonthly() * 12 * customer1.LoanTermYear:F2} with a monthly payment of ${customer1.CalcBaseMonthly():F2} for the {customer1.LoanTermYear} year loan.");
            Console.WriteLine($"Your Equity percentage is: {customer1.EquityP():F2}%");
            customer1.HomeLoanNeeded();
            Console.WriteLine($"Home Insurance required due to low Equity Percentage?: {customer1.NeedInsurance}");
            if (customer1.NeedInsurance == true)
            {
                Console.WriteLine($"Loan Insurance per month is: ${customer1.LoanInsurance():F2}");
            }
            Console.WriteLine($"You Escrow (insurance and taxes at 1.25% and 0.75% respectively) per month is : {customer1.EscrowMonthly():F2}");
            Console.WriteLine($"Your total monthly payments would be ${customer1.TotalMonthly():F2} with all applicable fees");
            Console.WriteLine($"With your ${customer1.MonthlyIncome} per month income.....");
            customer1.ApDnDecision();
            Console.WriteLine("-------------------------------------------------\n");
        }
    }

    public static double ErrorCheck(string? answer)
    {
        double num = 0;
        if (double.TryParse(answer, out num))
        {
            if (num > 0)
            {
                return num;
            }

            else
            {
                Console.WriteLine("Number cannot be equal to or less than 0");
                return -1;
            }
        }
        else
        {
            return -1;
        }

    }

    public static LoanProgram CustomerInput()
    {
        LoanProgram customer = new();
        string? answer = "";

        Console.Write("Please enter your Purchase (Purchase before down payment) amount: ");
        answer = Console.ReadLine();
        if (ErrorCheck(answer) != -1)
        {
            customer.Purchase = ErrorCheck(answer);
        }
        else
        {
            Console.WriteLine("Invalid number entered, please retry this selection.");
        }
        Console.Write("Please enter your annual interest rate(ex 0.01 = 1%): ");
        answer = Console.ReadLine();
        if (ErrorCheck(answer) != -1)
        {
            customer.AnnualInterestRate = ErrorCheck(answer);
        }
        else
        {
            Console.WriteLine("Invalid number entered, please retry this selection.");
        }
        Console.Write("How much are you putting as a down payment?: ");
        answer = Console.ReadLine();
        if (answer is not null)
        {
            if (ErrorCheck(answer) != -1)
            {
                customer.DownPayment = ErrorCheck(answer);
            }
            else
            {
                Console.WriteLine("Invalid number entered, please retry this selection.");
            }
        }
        Console.Write("Will the home be a part of a HOA, enter Y for yes or N for no: ");
        answer = Console.ReadLine();
        if (answer is not null)
        {
            switch (answer.ToLower())
            {
                case "y":
                    {
                        customer.HOAEnable = true;
                        Console.Write("Please enter the YEARLY HOA fee: ");
                        answer = Console.ReadLine();
                        if (ErrorCheck(answer) != -1)
                        {
                            customer.HOAYearly = ErrorCheck(answer);
                        }
                        break;
                    }
                case "n":
                    {
                        customer.HOAEnable = false;
                        customer.HOAYearly = 0;
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Invalid input, please try this option again.");
                        break;
                    }
            }
        }
        Console.Write("Please enter your MONTHLY income: ");
        answer = Console.ReadLine();
        if (answer is not null)
        {
            if (ErrorCheck(answer) != -1)
            {
                customer.MonthlyIncome = ErrorCheck(answer);
            }
            else
            {
                Console.WriteLine("Invalid number entered, please retry this selection.");
            }
        }
        


        return customer;
    }

    public static void AnswerCheckYN(string? answer)
    {
        if (answer != "y" && answer != "n")
        {
            Console.WriteLine("Invalid input");
        }
    }

}
