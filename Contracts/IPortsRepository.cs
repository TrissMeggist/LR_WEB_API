using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IPortsRepository
    {
        Task<IEnumerable<Port>> GetAllPortsAsync(bool trackChanges);
        Task<Port> GetPortAsync(Guid portsId, bool trackChanges);
        void CreatePort(Port port);
        Task<IEnumerable<Port>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void DeletePort(Port port);
    }
}
