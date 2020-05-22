using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecurringAccountTransfer.Core.BusinessObject.DTO;
using RecurringAccountTransfer.Core.DataAccess;

namespace RecurringAccountTransfer.Core.Logic
{
  public  class RecurringAccountLogic
    {

        public static bool Create(RecurringAccountRequest req)
        {
            return RecurringAccountRepository.Create(req);
        }
        public static List<RecurringSetup> Search(SearchRequest req)
        {
            return RecurringAccountRepository.Search(req.alias, req.ProfileId);
        }
        public static RecurringSetup GetById(Int64 recurringId)
        {
            return RecurringAccountRepository.GetById(recurringId);
        }
        public static List<RecurringSetup> GetAllByProfileId(Int64 profileId)
        {
            return RecurringAccountRepository.GetAllByProfileId(profileId);
        }
       
    }
}
