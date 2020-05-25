using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using RecurringAccountTransfer.Core;
using RecurringAccountTransfer.Core.DataAccess;

namespace RecurringAccountTransfer.WindowService
{
    class Program
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            try
            {
                Logger.Info("....account recurring service  started");
                var now = DateTime.Now;
                var aboutToBeDueDate = now.AddDays(getNotificationDaysBeforeActualDebit());
                Logger.Info("....done with notifying those to be debitted next run date");
                var aboutDueRecurringList = getAllRecuringDueForDate(aboutToBeDueDate);

                var dueRecurring = getAllRecuringDueForDate(now);
                ProcessEachDueRecuring(dueRecurring);
                Logger.Info("....account recurring service  ended");
            }
            catch (Exception e) 
            {
                Logger.Error("....account recurring service  exception : " + e);
            }


        }

        protected static int getNotificationDaysBeforeActualDebit() 
        {
            int days = 0; 
            try { days = Convert.ToInt32(ConfigurationManager.AppSettings["NotificationDaysBeforeActualDebit"]);  } catch { days = 1; }
            return days; 
         }

        protected static List<RecurringSetup> getAllRecuringDueForDate(DateTime dt)
        {
            var result = new List<RecurringSetup>();
            //recurring 
            var unfilteredRecurring = RecurringAccountRepository.GetSemiFilteredRecurringDuePerDay(dt);
            foreach (var rec in unfilteredRecurring)
            {
                int dayIncrementValue = 0;
                DateTime startDate = rec.StartDate;
                switch (rec.RecurringFrequency.Trim().ToLower())
                {
                    case "daily":
                        dayIncrementValue = 1;
                        break;
                    case "weekly":
                        dayIncrementValue = 7;
                        break;
                    case "monthly":
                        dayIncrementValue = 30;
                        break;
                    case "quarterly":
                        dayIncrementValue = 120;
                        break;
                    default:
                        dayIncrementValue = 30;
                        break;
                }

                while (Util.IsDateALessOrEqualB(startDate, rec.EndDate))
                {
                    if (Util.DatesEqual(startDate, DateTime.Today))
                    {
                        result.Add(rec);
                        continue;
                    }
                    else { }


                    startDate = startDate.AddDays(dayIncrementValue);
                }

            }

            return result;
        }
        protected static void ProcessEachDueRecuring(List<RecurringSetup> dueRecurringSetups)
        {
            Logger.Info("about processing " + dueRecurringSetups.Count + "  due for today " );
            foreach (var recurring in dueRecurringSetups)
            {
                try
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
                } catch (Exception e) { Logger.Error(e); }

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

        protected static void NotifyAllCustomersAboutPendingDebitForDueRecurring(List<RecurringSetup> aboutTodueRecurringSetups)
        {
            foreach (var obj in aboutTodueRecurringSetups)
            {
                NotificationService(obj);
            }

        }
        protected static void NotificationService(RecurringSetup recurring)
        {

        }


    }
}
