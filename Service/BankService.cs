using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    using Domain.ServiceInterfaces;
    using Domain;
    using Repository;
    public class BankService : ServiceBase<Bank>, IBankService
    {
        public BankService(BankRegistryDbContext context) : base(context)
        {
        }

        public override void Remove(Bank entity)
        {
            _unitOfWork.RepositoryBank.Value.Remove(entity);
        }

        public override void Save(Bank entity)
        {
            _unitOfWork.RepositoryBank.Value.Save(entity);
        }

        public override IEnumerable<Bank> Set()
        {
            return _unitOfWork.RepositoryBank.Value.Set();
        }
    }
}
