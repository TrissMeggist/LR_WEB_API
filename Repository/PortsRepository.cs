using Contracts;
using Entities.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class PortsRepository : RepositoryBase<Port>, IPortsRepository
    {
        public PortsRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }
        public async Task<IEnumerable<Port>> GetAllPortsAsync(bool trackChanges) =>
        await FindAll(trackChanges)
         .OrderBy(c => c.Title)
         .ToListAsync();
        public async Task<Port> GetPortAsync(Guid portsId, bool trackChanges) => 
        await FindByCondition(c=> c.Id.Equals(portsId), trackChanges)
         .SingleOrDefaultAsync();
        public void CreatePort(Port port) => Create(port);
        public async Task<IEnumerable<Port>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
        await FindByCondition(x => ids.Contains(x.Id), trackChanges)
         .ToListAsync();
        public void DeletePort(Port port)
        {
            Delete(port);
        }
    }
}
