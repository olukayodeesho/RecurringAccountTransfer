using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RecurringAccountTransfer.Core.Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RecurringFrequency
    {
        Daily = 1 , 
        Weekly = 2, 
        Monthly = 3, 
        Quaterly = 4
    }
}
