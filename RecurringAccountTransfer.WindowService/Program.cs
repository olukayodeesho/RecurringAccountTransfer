using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecurringAccountTransfer.Core.DataAccess;

namespace RecurringAccountTransfer.WindowService
{
    class Program
    {
        static void Main(string[] args)
        {

            var dueRecurring = getAllRecuringDueForToday();
            ProcessEachDueRecuring(dueRecurring);


        }
        protected static List<RecurringSetup> getAllRecuringDueForToday()
        {
            var result = new List<RecurringSetup>();
            //recurring 
            return result;
        }
        protected static void ProcessEachDueRecuring(List<RecurringSetup> dueRecurringSetups)
        {
            foreach (var recurring in dueRecurringSetups)
            {
                //check  balance, is it necessary?

                //do debit...call the debit service
                var debitServiceResponse = "";
                var isSuccessful = DoIntraInterBankTransfer(recurring.ProfileId, recurring.SourceAccountNumber, recurring.SourceBankCode, recurring.Amount, recurring.RecurringAlias, recurring.DestinationAccountNumber, recurring.DestinationBankCode, out debitServiceResponse);
                //log the attempt in the database
                LogServiceActivity(debitServiceResponse, isSuccessful, recurring.Id);
                if (!isSuccessful)
                {
                    //send or log notification to customer on why it was not succesful...this will depend on the service the bank makes available
                }


            }
        }
        protected static bool DoIntraInterBankTransfer(Int64 profileId, string sourceAccount, string sourceBankCode, decimal amount, string narration, string destinationAccount, string destinationBankCode, out string debitServiceResponse)
        {
            //expecting the debit service of the bank 
            debitServiceResponse = "Awaiting Bank Webservice ";
            return true;
        }
        protected static bool LogServiceActivity(string debitServiceResponse, bool isSuccessful, Int64 recurringSetupId)
        {
            RecurringAccountRepository.CreateRecurringSetupAttemptLog(new RecurringSetupAttemptLog
            {
                DateAttempted = DateTime.Now,
                DebitServiceResponse = debitServiceResponse,
                IsSuccessful = isSuccessful,
                RecurringSetupId = recurringSetupId
            });
            return true;
        }



    }
}
