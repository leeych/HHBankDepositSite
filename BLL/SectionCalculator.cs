using BLL;
using Common;
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
        private SectionInfo sectionInfo = new SectionInfo();

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
                    sectionInfo.M03 = 1;
                    break;
                case Period.M06:
                    months = 6;
                    sectionInfo.M06 = 1;
                    break;
                case Period.Y01:
                    months = 12;
                    sectionInfo.Y01 = 1;
                    break;
                case Period.Y02:
                    months = 24;
                    sectionInfo.Y02 = 1;
                    break;
                case Period.Y03:
                    months = 36;
                    sectionInfo.Y03 = 1;
                    break;
                case Period.Y05:
                    months = 60;
                    sectionInfo.Y05 = 1;
                    break;
                default:
                    break;
            }
            return start.AddMonths(months);
        }

        /// <summary>
        /// 根据存入日期、支取日期、约定存期计算靠档方案
        /// </summary>
        /// <param name="start">存入日期</param>
        /// <param name="end">支取日期</param>
        /// <param name="period">约定存期</param>
        /// <returns>靠档方案</returns>
        public SectionInfo GetSectionPlan(DateTime start, DateTime end, Period period)
        {
            sectionInfo.Reset();
            DateTime dueDate = GetDueDate(start, period);
            if (dueDate.Date <= end.Date)
            {
                TimeSpan ts = end.Date - dueDate.Date;
                sectionInfo.D01 = ts.Days;
            }
            else
            {
                sectionInfo.Reset();
                TimeSpan ts = end.Date - start.Date;
                int years = ts.Days / 365;
                DateTime mid = start.AddYears(years);
                if (mid.Date == end.Date)
                {
                    sectionInfo.Y01 = years;
                }
                else if (mid.Date < end.Date)
                {
                    CalcDueDateInYear(mid, end);
                    sectionInfo.Y01 = years;
                }
                else if (mid.Date > end.Date)
                {
                    CalcDueDateInYear(mid.AddYears(-1), end);
                    sectionInfo.Y01 = years - 1;
                }
            }
            SectionInfo info = sectionInfo;
            return info;
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
        /// 支取日是否到期
        /// </summary>
        /// <param name="start">存入日期</param>
        /// <param name="end">支取日期</param>
        /// <param name="period">约定存期</param>
        /// <returns>是否属于到期支取</returns>
        private bool IsDueDateDraw(DateTime start, DateTime end, Period period)
        {
            DateTime mid = GetDueDateByPeriod(start, period);
            if (end.Date > mid.Date)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 计算刚好到期支取利息
        /// </summary>
        /// <param name="money"></param>
        /// <param name="period"></param>
        /// <param name="bankRate"></param>
        /// <returns></returns>
        public static decimal CalcDueDrawInterest(decimal money, Period period, BankRate bankRate)
        {
            decimal lv = 0;
            switch (period)
            {
                case Period.M03:
                    lv = money * bankRate.M03 / 4;
                    break;
                case Period.M06:
                    lv = money * bankRate.M06 / 2;
                    break;
                case Period.Y01:
                    lv = money * bankRate.Y01;
                    break;
                case Period.Y02:
                    lv = money * bankRate.Y02 * 2;
                    break;
                case Period.Y03:
                    lv = money * bankRate.Y03 * 3;
                    break;
                case Period.Y05:
                    lv = money * bankRate.Y05 * 5;
                    break;
                default:
                    break;
            }
            return lv;
        }

        /// <summary>
        /// 按照约定存期计算到期日期
        /// </summary>
        /// <param name="start"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        public static DateTime GetDueDateByPeriod(DateTime start, Period period)
        {
            DateTime mid = start;
            switch (period)
            {
                case Period.M03:
                    mid = start.AddMonths(3);
                    break;
                case Period.M06:
                    mid = start.AddMonths(6);
                    break;
                case Period.Y01:
                    mid = start.AddYears(1);
                    break;
                case Period.Y02:
                    mid = start.AddYears(2);
                    break;
                case Period.Y03:
                    mid = start.AddYears(3);
                    break;
                case Period.Y05:
                    mid = start.AddYears(5);
                    break;
                default:
                    break;
            }
            return mid;
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
                sectionInfo.M06 = 1;
                sectionInfo.M03 = 1;
                sectionInfo.D01 = d09;
            }
            else if (IsMonthsLater(start, end, 6))
            {
                DateTime date06 = start.AddMonths(6);
                TimeSpan ts = end.Date - date06.Date;
                int d06 = ts.Days;
                sectionInfo.M06 = 1;
                sectionInfo.D01 = d06;
            }
            else if (IsMonthsLater(start, end, 3))
            {
                DateTime date03 = start.AddMonths(3);
                TimeSpan ts = end.Date - date03.Date;
                int d03 = ts.Days;
                sectionInfo.M03 = 1;
                sectionInfo.D01 = d03;
            }
            else
            {
                TimeSpan ts = end.Date - start.Date;
                int d00 = ts.Days;
                sectionInfo.D01 = d00;
            }
        }

        /// <summary>
        /// 计算一年内的靠档方案
        /// </summary>
        /// <param name="start">存入日期</param>
        /// <param name="end">支取日期</param>
        /// <returns>靠档方案</returns>
        private SectionInfo CalcPeriodInYear(DateTime start, DateTime end)
        {
            SectionInfo sects = new SectionInfo();
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
                sectionInfo.D01 = ts.Days;
            }
            else
            {
                sectionInfo.Reset();
                TimeSpan ts = end.Date - start.Date;
                int years = ts.Days / 365;
                DateTime mid = start.AddYears(years);
                if (mid.Date == end.Date)
                {
                    sectionInfo.Y01 = years;
                }
                else if (mid.Date < end.Date)
                {
                    CalcDueDateInYear(mid, end);
                    sectionInfo.Y01 = years;
                }
                else if (mid.Date > end.Date)
                {
                    CalcDueDateInYear(mid.AddYears(-1), end);
                    sectionInfo.Y01 = years - 1;
                }
            }
            string desc = sectionInfo.ToString();
            sectionInfo.Reset();
            return desc;
        }

        /// <summary>
        /// 计算利息集合
        /// </summary>
        /// <param name="depositInfo"></param>
        /// <param name="bankRate"></param>
        /// <returns></returns>
        public CalcResult CalcTotalResult(CalcInfo depositInfo, BankRate bankRate)
        {
            CalcResult result = new CalcResult();
            SectionInfo info = GetSectionPlan(depositInfo.StartDate, depositInfo.EndDate, depositInfo.DepositPeriod);
            if (IsDueDateDraw(depositInfo.StartDate, depositInfo.EndDate, depositInfo.DepositPeriod))
            {
                // TODO: 系统利息
                result.DueDate = GetDueDateByPeriod(depositInfo.StartDate, depositInfo.DepositPeriod);
                result.SectionDesc = info.ToString();
                result.SystemInterest = depositInfo.CapitalMoney * (info.Y01 * bankRate.Y01 + info.M06 * bankRate.M06 / 2 + info.M03 * bankRate.M03 / 4 + info.D01 * bankRate.CurrRate / 360);
                result.SectionInterest = result.SystemInterest;
                result.MarginInterest = result.SectionInterest - result.SystemInterest;
            }
            else
            {
                // TODO: 靠档利息
                result.DueDate = GetDueDateByPeriod(depositInfo.StartDate, depositInfo.DepositPeriod);
                result.SectionDesc = info.ToString();
                // 计算两个日期间的天数
                TimeSpan ts = depositInfo.EndDate.Date - depositInfo.StartDate.Date;
                result.SystemInterest = depositInfo.CapitalMoney * (ts.Days * bankRate.CurrRate / 360);
                result.SectionInterest = depositInfo.CapitalMoney * (info.Y01 * bankRate.Y01 + info.M06 * bankRate.M06 / 2 + info.M03 * bankRate.M03 / 4 + info.D01 * bankRate.CurrRate / 360);
                result.MarginInterest = result.SectionInterest - result.SystemInterest;
            }
            return result;
        }
    }
}
