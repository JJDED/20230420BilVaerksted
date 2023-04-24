using _20230420BilVaerksted;
using _20230420BilVaerksted.Code;
using System;

namespace _20230420BilVaerksted.Code;

public class Cars
{
    static readonly CarInfo[] RecalledCars =
    {
        new CarInfo { Brand = "Fiat", Model = "Punto", Regdate = 2010, LastCheckDate = new DateTime(2018, 1, 1)},
        new CarInfo { Brand = "Alfa Romeo", Model = "Giulia", Regdate = 2019, LastCheckDate = new DateTime(2021, 8, 1)}
    };
       
    public static void Main(string[] args)
    {
        Console.WriteLine("Velkommen til Pavels Autoværksted!");
        Console.WriteLine("Indtast info omkring bilen:");

        Console.Write("Mærke: ");
        string brand = Console.ReadLine();

        Console.Write("Model: ");
        string model = Console.ReadLine();

        Console.Write("Registreringsår (yyyy): ");
        int regdate = int.Parse(Console.ReadLine());

        Console.Write("Sidste syn (dd-mm-yyyy): ");
        DateTime lastCheckDate;
        if (!DateTime.TryParseExact(Console.ReadLine(), "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out lastCheckDate))
        {
            Console.WriteLine("Ugyldigt datoformat! Prøv igen.");
            return;
        }

        bool needsCheck = NeedsCheck(regdate, lastCheckDate);
        Console.WriteLine(needsCheck ? "Bilen skal til syn" : "Bilen skal ikke synes");

        CarInfo recalledCar = FindRecalledCars(brand, model, regdate);
        if (recalledCar.Brand != null)
        {
            Console.WriteLine($"Bilen har følgende fabriksfejl: {recalledCar.Model} {recalledCar.Brand} ældre end {recalledCar.LastCheckDate.ToString("dd-MM-yyyy")}");
        }

        Console.WriteLine("For at afslutte, tryk på vilkårlig tast: ");
        Console.ReadKey();
    }

    static bool NeedsCheck(int regdate, DateTime lastCheckDate)
    {
        const int CheckIntervalYr = 2;
        DateTime nextCheckDate = lastCheckDate.AddYears(CheckIntervalYr);
        return DateTime.Now >= nextCheckDate && DateTime.Now.Year == regdate;
    }
    public static CarInfo FindRecalledCars(string brand, string model, int regdate)
    {
        foreach (CarInfo recalledCar in RecalledCars)
        {
            if (recalledCar.Brand == brand && recalledCar.Model == model && recalledCar.Regdate == regdate)
            {
                return recalledCar;
            }
        }

        return new CarInfo();
    }
}
