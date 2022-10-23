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
    public class PortsRepository : RepositoryBase<Port>, IPortsRepository
    {
        public PortsRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }
        public IEnumerable<Port> GetAllPorts(bool trackChanges) =>
        FindAll(trackChanges)
         .OrderBy(c => c.Title)
         .ToList();
    }
}
