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
        public Port GetPort(Guid portsId, bool trackChanges) => FindByCondition(c=> c.Id.Equals(portsId), trackChanges).SingleOrDefault();
        public void CreatePort(Port port) => Create(port);
        public IEnumerable<Port> GetByIds(IEnumerable<Guid> ids, bool trackChanges) =>FindByCondition(x => ids.Contains(x.Id), trackChanges).ToList();
        public void DeletePort(Port port)
        {
            Delete(port);
        }
    }
}
