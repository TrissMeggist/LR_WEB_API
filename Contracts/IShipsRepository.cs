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

        IEnumerable<Ship> GetShips(Guid portsId, bool trackChanges);
        Ship GetShip(Guid portsId, Guid id, bool trackChanges);
        void CreateShipForPort(Guid portsId, Ship ship);

    }
}
