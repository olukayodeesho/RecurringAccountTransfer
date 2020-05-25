using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecurringAccountTransfer.Core
{
   public class Util
    {
        public static bool IsDateALessOrEqualB(DateTime a, DateTime b)
        {
            if (DbFunctions.TruncateTime(a) <= DbFunctions.TruncateTime(b))
                return true;
            else
                return false; 
        }
        public static bool DatesEqual(DateTime a, DateTime b)
        {
            if (DbFunctions.TruncateTime(a) == DbFunctions.TruncateTime(b))
                return true;
            else
                return false;
        }
    }
}
