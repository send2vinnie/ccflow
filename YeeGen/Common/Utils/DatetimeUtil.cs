using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Tax666.Common
{
    public class DatetimeUtil
    {
        ///   <summary> 
        ///  获取某一日期是该年中的第几周
        ///   </summary> 
        ///   <param name="dt"> 日期 </param> 
        ///   <returns> 该日期在该年中的周数 </returns> 
        public static int GetWeekOfYear(DateTime dt)
        {
            GregorianCalendar gc = new GregorianCalendar();
            return gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

        ///   <summary> 
        ///  获取某一年有多少周
        ///   </summary> 
        ///   <param name="year"> 年份 </param> 
        ///   <returns> 该年周数 </returns> 
        public static int GetWeekAmount(int year)
        {
            DateTime end = new DateTime(year, 12, 31);   // 该年最后一天 
            System.Globalization.GregorianCalendar gc = new GregorianCalendar();
            return gc.GetWeekOfYear(end, CalendarWeekRule.FirstDay, DayOfWeek.Monday);   // 该年星期数 
        }
    }
}
