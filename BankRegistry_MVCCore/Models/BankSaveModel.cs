using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankRegistry_MVCCore.Models
{
    public class BankSaveModel
    {
        public BankModel BankModel { get; set; }
        public ContactPersonModel ContactPersonModel { get; set; }
        public List<ContactPersonModel> ContactPersonModels { get; set; }
        public List<PositionModel> Positions { get; set; }
    }
}
