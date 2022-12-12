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
        IEnumerable<Port> GetAllPorts(bool trackChanges);
        Port GetPort(Guid portsId, bool trackChanges);
        void CreatePort(Port port);
        IEnumerable<Port> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
        void DeletePort(Port port);
    }
}
