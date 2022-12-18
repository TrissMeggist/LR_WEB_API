using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using System.Reflection;

namespace Repository.Extensions
{
    public static class RepositoryShipExtensions
    {
        public static IQueryable<Ship> FilterShip(this IQueryable<Ship>
        ship ) => ship;
        public static IQueryable<Ship> Search(this IQueryable<Ship> ship, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return ship;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return ship.Where(e => e.Title.ToLower().Contains(lowerCaseTerm));
        }
        public static IQueryable<Ship> Sort(this IQueryable<Ship> ships, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return ships.OrderBy(e => e.Title);
            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Employee>(orderByQueryString);
            if (string.IsNullOrWhiteSpace(orderQuery))
                return ships.OrderBy(e => e.Title);
            return ships.OrderBy(orderQuery);
        }
    }
}
