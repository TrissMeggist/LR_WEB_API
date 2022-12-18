using Contracts;
using Entities.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Entities.RequestFeatures;

namespace Repository
{
    public class ShipsRepository : RepositoryBase<Ship>, IShipsRepository
    {
        public ShipsRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }
        public async Task<PagedList<Ship>> GetShipsAsync(Guid portsId, ShipParameters shipParameters, bool trackChanges)
        {
            var ships = await FindByCondition(e => e.PortsId.Equals(portsId),
            trackChanges)
            .OrderBy(e => e.Title)
            .ToListAsync();
            return PagedList<Ship>
            .ToPagedList(ships, shipParameters.PageNumber,
            shipParameters.PageSize);
        }
        public async Task<Ship> GetShipAsync(Guid portsId, Guid id, bool trackChanges) =>
        await FindByCondition(e => e.PortsId.Equals(portsId) && e.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();
        public void CreateShipForPort(Guid portsId, Ship ship)
        {
            ship.PortsId = portsId;
            Create(ship);
        }
        public void DeleteShip(Ship ship)
        {
            Delete(ship);
        }
    }
}
