using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankRegistry_MVCCore.Models
{
    public class BankModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }

        public static explicit operator BankModel(Domain.Bank bank)
        {
            return new BankModel()
            {
                ID = bank.ID,
                Name = bank.Name,
                URL = bank.URL
            };
        }
    }
}
