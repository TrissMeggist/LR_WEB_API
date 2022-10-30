using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LR_WEB_API.Controllers
{
    [Route("api/ports/{portsId}/ships")]
    [ApiController]
    public class ShipsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public ShipsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetShipsForPorts(Guid portsId)
        {
            var port = _repository.Ports.GetPort(portsId, trackChanges: false);
            if (port == null)
            {
                _logger.LogInfo($"Company with id: {portsId} doesn't exist in the database.");
            return NotFound();
            }
            var shipFromDb = _repository.Ships.GetShips(portsId, trackChanges: false);
            var shipDTO = _mapper.Map<IEnumerable<ShipDTO>>(shipFromDb);
            return Ok(shipDTO);
        }
        [HttpGet("{id}")]
        public IActionResult GetShipForPorts(Guid portsId, Guid id)
        {
            var port = _repository.Ports.GetPort(portsId, trackChanges: false);
            if (port == null)
            {
                _logger.LogInfo($"Port with id: {portsId} doesn't exist in the database.");
            return NotFound();
            }
            var shipDb = _repository.Ships.GetShip(portsId, id, trackChanges: false);
            if (shipDb == null)
            {
                _logger.LogInfo($"Ship with id: {id} doesn't exist in the database.");
            return NotFound();
            }
            var ship = _mapper.Map<ShipDTO>(shipDb);
            return Ok(ship);
        }
    }
}
