using System;

namespace SubsidyCalculation
{
    class Program
    {
        static void Main(string[] args)
        {

            Tariff tariff = new Tariff
            {
                HouseId = 1,
                PeriodBegin = new DateTime(2021, 1, 1),
                PeriodEnd = new DateTime(2022, 1, 1),
                ServiceId = 5,
                Value = 10
            };
            Volume volume = new Volume
            {
                HouseId = 1,
                Month = new DateTime(2020, 4, 1),
                ServiceId = 5,
                Value = 100
            };

            SubsidyCalculation sub = new SubsidyCalculation();
            sub.OnException += Handler.Exception;
            sub.OnNotify += Handler.Notify;
            Charge charge = sub.CalculateSubsidy(volume, tariff);

            Console.WriteLine(charge.Value);
        }
    }
}
