using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Domain
{
    public struct Date
    {
        public Date(int ticks)
        {
            Ticks = ticks;
        }

        public Date()
        {
            Ticks = 0;
        }

        public int Ticks { get; set; }
        public int Hour => Ticks % 24;
        public int Year => (Ticks / (360 * 24));
        public int Month => ((Ticks % (360 * 24)) / (30 * 24)) + 1;
        public int Day => ((Ticks % (30 * 24)) / 24) + 1;
        public int TotalMonth => Ticks / (30 * 24);
        public int TotalDay => Ticks / 24;

        public static Date operator +(Date date, int ticks)
        {
            return new Date(date.Ticks + ticks);
        }

        public static Date operator -(Date date, int ticks)
        {
            return new Date(date.Ticks - ticks);
        }

        public static Date operator ++(Date date)
        {
            return new Date(date.Ticks + 1);
        }

        public static Date operator --(Date date)
        {
            return new Date(date.Ticks - 1);
        }

        public static Date operator -(Date date1, Date date2)
        {
            return new Date(date1.Ticks - date2.Ticks);
        }

        public static Date operator +(Date date1, Date date2)
        {
            return new Date(date1.Ticks + date2.Ticks);
        }

        public static bool operator <(Date date1, Date date2)
        {
            return date1.Ticks < date2.Ticks;
        }
        public static bool operator >(Date date1, Date date2)
        {
            return date1.Ticks > date2.Ticks;
        }

        public static bool operator <=(Date date1, Date date2)
        {
            return date1.Ticks <= date2.Ticks;
        }

        public static bool operator >=(Date date1, Date date2)
        {
            return date1.Ticks >= date2.Ticks;
        }

        public Date AddDays(int days)
        {
            return new Date(Ticks + (days * 24));
        }

        public Date AddMonths(int months)
        {
            return new Date(Ticks + (months * 30 * 24));
        }

        public Date AddYears(int years)
        {
            return new Date(Ticks + (years * 360 * 24));
        }

        public Date AddHours(int hours)
        {
            return new Date(Ticks + hours);
        }

        public string GetMonthName()
        {
            return System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Month);
        }

        public override string ToString()
        {
            return $"{Day} {GetMonthName()} {Year}";
        }

        public Date OnlyDate
        {
            get
            {
                return new Date(Ticks - (Ticks % 24));
            }
        }
    }
}
