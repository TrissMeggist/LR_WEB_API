using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IShipsRepository
    {

        Task<IEnumerable<Ship>> GetShipsAsync(Guid portsId, bool trackChanges);
        Task<Ship> GetShipAsync(Guid portsId, Guid id, bool trackChanges);
        void CreateShipForPort(Guid portsId, Ship ship);
        void DeleteShip(Ship ship);
    }
}
