using Contracts;
using Entities.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PortsRepository : RepositoryBase<Ports>, IPortsRepository
    {
        public PortsRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }
    }
}
