using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankRegistry_MVCCore.Models
{
    public class PositionModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public static explicit operator PositionModel(Domain.Position position)
        {
            return new PositionModel()
            {
                ID = position.ID,
                Name = position.Name
            };
        }
    }
}
