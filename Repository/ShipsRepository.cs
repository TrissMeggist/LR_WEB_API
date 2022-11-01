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
    public class ShipsRepository : RepositoryBase<Ship>, IShipsRepository
    {
        public ShipsRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }
        public IEnumerable<Ship> GetShips(Guid portsId, bool trackChanges) =>
        FindByCondition(e => e.PortsId.Equals(portsId), trackChanges).OrderBy(e => e.Title);
        public Ship GetShip(Guid portsId, Guid id, bool trackChanges) =>
        FindByCondition(e => e.PortsId.Equals(portsId) && e.Id.Equals(id), trackChanges).SingleOrDefault();
        public void CreateShipForPort(Guid portsId, Ship ship)
        {
            ship.PortsId = portsId;
            Create(ship);
        }
    }
}
