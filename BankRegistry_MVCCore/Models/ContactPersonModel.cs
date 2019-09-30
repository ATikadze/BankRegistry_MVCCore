using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankRegistry_MVCCore.Models
{
    public class ContactPersonModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int PositionID { get; set; }

        public static explicit operator ContactPersonModel(Domain.ContactPerson contactPerson)
        {
            return new ContactPersonModel()
            {
                ID = contactPerson.ID,
                FirstName = contactPerson.FirstName,
                LastName = contactPerson.LastName,
                DateOfBirth = contactPerson.DateOfBirth,
                PositionID = contactPerson.PositionID
            };
        }
    }
}
