// See https://aka.ms/new-console-template for more information

using RisksSystem.Domain.Interfaces;
using RisksSystem.Services;
using RisksSystem.Services.Interfaces;
using RisksSystem.Utils.Extensions;
using System.Runtime.CompilerServices;


// Program;


ITradeService tradeService = new TradeService();
List<ITrade> lstTrades;

StartMenu();

int StartMenu()
{
    string menuInput;
    int menuOpt;
    int[] check = new int[] { 1, 2, 3, 4, 5 };

    lstTrades = new List<ITrade>();

    Console.WriteLine("************* MENU ************");
    Console.WriteLine();
    Console.WriteLine("Make a option");
    Console.WriteLine("1) Responses in the order as entry");
    Console.WriteLine("2) Responses in Risk (increasing)");
    Console.WriteLine("3) Responses in Risk (decreasing)");
    Console.WriteLine("4) Clear Console");
    Console.WriteLine("5) Exit");
    Console.WriteLine();
    Console.WriteLine("*******************************");
    menuInput = Console.ReadLine();

    if (!int.TryParse(menuInput, out menuOpt) || !check.Contains(menuOpt))
    {
        Console.WriteLine("\nIncorrect choice, try again\n");
        StartMenu();
    }
    else if (menuOpt == 4)
    {
        Console.Clear();
        StartMenu();
    }
    else if (menuOpt == 5)
    {
        System.Environment.Exit(0);
    }
    else
        TradeRisks(menuOpt);

    return menuOpt;
}

void TradeRisks(int menuOpt)
{
    string refInput;

    DateTime refDate;
    int qtd;


    bool valid = true;
    string msgError = string.Empty;

    do
    {
        valid = true;
        msgError = string.Empty;
        Console.WriteLine("\nSet the reference date.");
        refInput = Console.ReadLine();

        refDate = tradeService.ValidateDateFormat(refInput, ref msgError);

        if (!string.IsNullOrWhiteSpace(msgError))
        {
            valid = false;
            Console.WriteLine(msgError + "\n");
        }
    } while (!valid);

    do
    {
        valid = true;
        Console.WriteLine("\nQuantit of trades?");
        refInput = Console.ReadLine();

        qtd = refInput.ConvertToInt(out valid);

        if (!valid)
        {
            Console.WriteLine("\nIncorrect Quantity, try again. \n");
        }
    } while (!valid);


    for (int i = 0; i < qtd; i++)
    {
        do
        {
            valid = false;
            Console.WriteLine($"\nTrade nº: {i + 1}");
            refInput = Console.ReadLine();
            ITrade objAdd = tradeService.GetTrade();

            if (!string.IsNullOrWhiteSpace(refInput) && refInput.Contains(" "))
            {
                string[] arrEntry = refInput.Split(" ");
                valid = (arrEntry.Length > 3);

                if (valid)
                    objAdd = tradeService.ConvertData(arrEntry[0], arrEntry[1], arrEntry[2]);
            }

            if (!valid)
            {
                Console.WriteLine("Incorrect format, Try Again.\n");
            }
            else if (!string.IsNullOrWhiteSpace(objAdd.MsgError))
            {
                valid = false;
                Console.WriteLine(objAdd.MsgError + "\n");
            }
            else
            {
                objAdd = tradeService.SetRisk(refDate, objAdd, lstTrades.Count + 1);
                lstTrades.Add(objAdd);
            }
        } while (!valid);
    }

    MsgOutput(menuOpt);
}

void MsgOutput(int menuOpt)
{
    switch (menuOpt)
    {
        case 2:
            lstTrades = lstTrades.OrderBy(o => o.Risk).ToList();
            break;
        case 3:
            lstTrades = lstTrades.OrderByDescending(o => o.Risk).ToList();
            break;
    }

    Console.WriteLine("\n\nAnswers: ");
    foreach (var item in lstTrades)
    {
        Console.WriteLine($"Line: {item.Line}. Risk: {item.Risk}");
    }

    Console.WriteLine("\n\nFinish.\nPress keyboard to start again.");
    Console.ReadLine();
    StartMenu();

}
