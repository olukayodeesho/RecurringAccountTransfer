using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using RecurringAccountTransfer.Core.BusinessObject.DTO;

namespace RecurringAccountTransfer.Core.DataAccess
{
    public class RecurringAccountRepository
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        public static bool Create(RecurringAccountRequest req)
        {
            var result = false; 
            try
            {
                using (var con = new RecurringAccountTransfersEntities())
                {
                    var obj = new RecurringSetup();
                    obj.Amount = req.Amount;
                    obj.DateCreated = DateTime.Now;
                    obj.DestinationAccountNumber = req.DestinationAccountNumber;
                    obj.DestinationBankCode = req.DestinationBankCode;
                    obj.Enable = true;
                    obj.Purpose = req.Purpose;
                    obj.RecurringAlias = req.RecurringAlias;
                    obj.SourceAccountNumber = req.SourceAccountNumber;
                    obj.SourceBankCode = req.SourceBankCode;
                    obj.RecurringFrequency = req.Frequency.ToString();
                    con.RecurringSetups.Add(obj);
                    con.SaveChanges();
                    result = true; 
                }


            }
            catch (Exception e) { Logger.Error(e); }
            return result; 

        }

        public static bool CreateRecurringSetupAttemptLog(RecurringSetupAttemptLog req)
        {
            var result = false;
            try
            {
                using (var con = new RecurringAccountTransfersEntities())
                {
                    var obj = new RecurringSetupAttemptLog();
                    obj.DateAttempted = req.DateAttempted;
                    obj.DebitServiceResponse = req.DebitServiceResponse;
                    obj.IsSuccessful = req.IsSuccessful;
                    obj.RecurringSetupId = req.RecurringSetupId;
                    con.RecurringSetupAttemptLogs.Add(obj);
                    con.SaveChanges();
                    result = true;
                }


            }
            catch (Exception e) { Logger.Error(e); }
            return result;

        }





        public static List<RecurringSetup> Search(string alias, Int64 profileId)
        {
            var result = new List<RecurringSetup>();
            try
            {
                using (var dc = new RecurringAccountTransfersEntities())
                {
                    result = dc.RecurringSetups.Where(f => f.ProfileId == profileId && f.Enable  && f.RecurringAlias.ToLower().Contains(alias.Trim().ToLower())).ToList<RecurringSetup>();
                }

            }
            catch (Exception e) { Logger.Error(e); }
            return result;

        }
        public static RecurringSetup GetById(Int64 recurringId)
        {
            var result = new RecurringSetup();
            try
            {
                using (var dc = new RecurringAccountTransfersEntities())
                {
                    result = dc.RecurringSetups.FirstOrDefault(f => f.Id == recurringId && f.Enable);
                }

            }
            catch (Exception e) { Logger.Error(e); }
            return result;

        }
        public static List<RecurringSetup> GetAllByProfileId(Int64 profileId)
        {
            var result = new List<RecurringSetup>();
            try
            {
                using (var dc = new RecurringAccountTransfersEntities())
                {
                    result = dc.RecurringSetups.Where(f => f.ProfileId == profileId && f.Enable).ToList<RecurringSetup>();
                }

            }
            catch (Exception e) { Logger.Error(e); }
            return result;
        }

        public static List<RecurringSetup> GetSemiFilteredRecurringDuePerDay( DateTime dueDate )
        {
            var result = new List<RecurringSetup>();
            try
            {
                using (var dc = new RecurringAccountTransfersEntities())
                {
                    result = dc.RecurringSetups.Where(f =>  f.Enable && !f.IsCompleted  && (DbFunctions.TruncateTime(f.EndDate) <= DbFunctions.TruncateTime(f.EndDate)) ).ToList<RecurringSetup>();
                }

            }
            catch (Exception e) { Logger.Error(e); }
            return result;
        }



    }
}
