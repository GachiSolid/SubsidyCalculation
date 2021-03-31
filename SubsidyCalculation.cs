using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SubsidyCalculation
{
    class SubsidyCalculation : ISubsidyCalculation
    {
        public event EventHandler<string> OnNotify = Handler.Notify;
        public event EventHandler<Tuple<string, Exception>> OnException = Handler.Exception;

        void Check(Volume volume, Tariff tariff)
        {
            if (volume.HouseId != tariff.HouseId)
            {
                Exception e = new ArgumentException("Дом");
                OnException.Invoke(this, new Tuple<string, Exception>("Идентификаторы домов не совпадают", e));
                throw e;
            }
            if (volume.ServiceId != tariff.ServiceId)
            {
                Exception e = new ArgumentException("Услуга");
                OnException.Invoke(this, new Tuple<string, Exception>("Идентификаторы услуги не совпадают", e));
                throw e;
            }
            if(!(volume.Month >= tariff.PeriodBegin && volume.Month <= tariff.PeriodEnd))
            {
                Exception e = new ArgumentOutOfRangeException("Месяц");
                OnException.Invoke(this, new Tuple<string, Exception>("Месяц не входит в тариф", e));
                throw e;
            }
            if (tariff.Value <= 0)
            {
                Exception e = new ArgumentException("Тариф");
                OnException.Invoke(this, new Tuple<string, Exception>("Не допускаются нулевые или отрицательные значения тарифа", e));
                throw e;
            }
            if (volume.Value < 0)
            {
                Exception e = new ArgumentException("Объем");
                OnException.Invoke(this, new Tuple<string, Exception>("Не допускаются отрицательные значения объема", e));
                throw e;
            }
        }

        public Charge CalculateSubsidy(Volume volumes, Tariff tariff)
        {
            try
            {
                Check(volumes, tariff);
                OnNotify.Invoke(this, $"Расчет начат в {DateTime.Now:T}");
                Charge charge = new Charge
                {
                    HouseId = volumes.HouseId,
                    Month = volumes.Month,
                    ServiceId = volumes.ServiceId,
                    Value = volumes.Value * tariff.Value
                };
                OnNotify.Invoke(this, $"Расчет успешно завершен в {DateTime.Now:T}");
                return charge;
            }
            catch (Exception e)
            {
                OnException.Invoke(this, new Tuple<string, Exception>("Ошибка", e));
                throw;
            }
        }
    }
}
