using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HHBankDepositSite
{
    public class SectionCalculator
    {
        /// <summary>
        /// 存入日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 支取日期
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 靠档方案
        /// </summary>
        private PeriodDescriptor sections = new PeriodDescriptor();

        /// <summary>
        /// 根据存入日期和存期，计算应到期日期
        /// </summary>
        /// <param name="start">存入日期</param>
        /// <param name="period">存期</param>
        /// <returns>到期日期</returns>
        public DateTime GetDueDate(DateTime start, Period period)
        {
            int months = 0;
            switch (period)
            {
                case Period.M03:
                    months = 3;
                    sections.M03 = 1;
                    break;
                case Period.M06:
                    months = 6;
                    sections.M06 = 1;
                    break;
                case Period.Y01:
                    months = 12;
                    sections.Y01 = 1;
                    break;
                case Period.Y02:
                    months = 24;
                    sections.Y02 = 1;
                    break;
                case Period.Y03:
                    months = 36;
                    sections.Y03 = 1;
                    break;
                case Period.Y05:
                    months = 60;
                    sections.Y05 = 1;
                    break;
                default:
                    break;
            }
            return start.AddMonths(months);
        }

        /// <summary>
        /// 判断later是否在start之后的months个月
        /// </summary>
        /// <param name="start">存入日期</param>
        /// <param name="later">支取日期</param>
        /// <param name="months">间隔月数</param>
        /// <returns></returns>
        private bool IsMonthsLater(DateTime start, DateTime later, int months)
        {
            DateTime mid = start.AddMonths(months);
            return (mid.Date <= later.Date);
        }

        /// <summary>
        /// 计算一年之内的靠档方案
        /// </summary>
        /// <param name="start">存入日期</param>
        /// <param name="end">支取日期</param>
        private void CalcDueDateInYear(DateTime start, DateTime end)
        {
            if (IsMonthsLater(start, end, 9))
            {
                DateTime date09 = start.AddMonths(9);
                TimeSpan ts = end.Date - date09.Date;
                int d09 = ts.Days;
                sections.M06 = 1;
                sections.M03 = 1;
                sections.D01 = d09;
            }
            else if (IsMonthsLater(start, end, 6))
            {
                DateTime date06 = start.AddMonths(6);
                TimeSpan ts = end.Date - date06.Date;
                int d06 = ts.Days;
                sections.M06 = 1;
                sections.D01 = d06;
            }
            else if (IsMonthsLater(start, end, 3))
            {
                DateTime date03 = start.AddMonths(3);
                TimeSpan ts = end.Date - date03.Date;
                int d03 = ts.Days;
                sections.M03 = 1;
                sections.D01 = d03;
            }
            else
            {
                TimeSpan ts = end.Date - start.Date;
                int d00 = ts.Days;
                sections.D01 = d00;
            }
        }

        /// <summary>
        /// 计算一年内的靠档方案
        /// </summary>
        /// <param name="start">存入日期</param>
        /// <param name="end">支取日期</param>
        /// <returns>靠档方案</returns>
        private PeriodDescriptor CalcPeriodInYear(DateTime start, DateTime end)
        {
            PeriodDescriptor sects = new PeriodDescriptor();
            if (IsMonthsLater(start, end, 9))
            {
                DateTime date09 = start.AddMonths(9);
                TimeSpan ts = end.Date - date09.Date;
                int d09 = ts.Days;
                sects.M06 = 1;
                sects.M03 = 1;
                sects.D01 = d09;
            }
            else if (IsMonthsLater(start, end, 6))
            {
                DateTime date06 = start.AddMonths(6);
                TimeSpan ts = end.Date - date06.Date;
                int d06 = ts.Days;
                sects.M06 = 1;
                sects.D01 = d06;
            }
            else if (IsMonthsLater(start, end, 3))
            {
                DateTime date03 = start.AddMonths(3);
                TimeSpan ts = end.Date - date03.Date;
                int d03 = ts.Days;
                sects.M03 = 1;
                sects.D01 = d03;
            }
            else
            {
                TimeSpan ts = end.Date - start.Date;
                int d00 = ts.Days;
                sects.D01 = d00;
            }
            return sects;
        }

        /// <summary>
        /// 根据存入日期，支取日期计算靠档方案
        /// </summary>
        /// <param name="start">存入日期</param>
        /// <param name="end">支取日期</param>
        /// <param name="period">约定存期</param>
        /// <returns>靠档方案</returns>
        public string GetDueDateDesc(DateTime start, DateTime end, Period period)
        {
            DateTime dueDate = GetDueDate(start, period);
            if (dueDate.Date <= end.Date)
            {
                TimeSpan ts = end.Date - dueDate.Date;
                sections.D01 = ts.Days;
            }
            else
            {
                sections.Reset();
                TimeSpan ts = end.Date - start.Date;
                int years = ts.Days / 365;
                DateTime mid = start.AddYears(years);
                if (mid.Date == end.Date)
                {
                    sections.Y01 = years;
                }
                else if (mid.Date < end.Date)
                {
                    CalcDueDateInYear(mid, end);
                    sections.Y01 = years;
                }
                else if (mid.Date > end.Date)
                {
                    CalcDueDateInYear(mid.AddYears(-1), end);
                    sections.Y01 = years - 1;
                }
            }
            string desc = sections.ToString();
            sections.Reset();
            return desc;
        }
    }
}
