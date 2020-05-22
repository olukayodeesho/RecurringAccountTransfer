using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecurringAccountTransfer.Core.Enum;

namespace RecurringAccountTransfer.Core.BusinessObject.DTO
{
   public class RecurringAccountRequest : BaseRequest
    {
        public string SourceAccountNumber { get; set; }
        public string SourceBankCode { get; set; }
        public string DestinationBankCode { get; set; }
        public string DestinationAccountNumber { get; set; }
        public string RecurringAlias { get; set; }
        public decimal Amount { get; set; }
        public string Purpose { get; set; }
        public RecurringFrequency Frequency { get; set; }
        //alias, amount, destinationbankcode, destinationaccountnumber, frequency
        //channel , purpose , 
    }
}
