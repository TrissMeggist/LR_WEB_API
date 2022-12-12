using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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
        [HttpGet("{id}", Name = "GetShipForPort")]
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
        [HttpPost]
        public IActionResult CreateShipForPort(Guid portsId, [FromBody] ShipForCreationDto ship)
        {
            if (ship == null)
            {
                _logger.LogError("ShipForCreationDto object sent from client is null.");
            return BadRequest("ShipForCreationDto object is null");
            }
            var port = _repository.Ports.GetPort(portsId, trackChanges: false);
            if (port == null)
            {
                _logger.LogInfo($"Port with id: {portsId} doesn't exist in the database.");
            return NotFound();
            }
            var shipEntity = _mapper.Map<Ship>(ship);
            _repository.Ships.CreateShipForPort(portsId, shipEntity);
            _repository.Save();
            var shipToReturn = _mapper.Map<ShipDTO>(shipEntity);
            return CreatedAtRoute("GetShipForPort", new
            {
                portsId,
                id = shipToReturn.Id
            }, shipToReturn);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteShipForPort(Guid portsId, Guid id)
        {
            var port = _repository.Ports.GetPort(portsId, trackChanges: false);
            if (port == null)
            {
                _logger.LogInfo($"Port with id: {portsId} doesn't exist in the database.");
                return NotFound();
            }
            var shipForPort = _repository.Ships.GetShip(portsId, id, trackChanges: false);
            if (shipForPort == null)
            {
                _logger.LogInfo($"Ship with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _repository.Ships.DeleteShip(shipForPort);
            _repository.Save();
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateShipForPort(Guid portsId, Guid id, [FromBody] ShipForUpdateDto ship)
        {
            if (ship == null)
            {
                _logger.LogError("ShipForUpdateDto object sent from client is null.");
                return BadRequest("ShipForUpdateDto object is null");
            }
            var port = _repository.Ports.GetPort(portsId, trackChanges: false);
            if (port == null)
            {
                _logger.LogInfo($"Port with id: {portsId} doesn't exist in the database.");
                return NotFound();
            }
            var shipEntity = _repository.Ships.GetShip(portsId, id, trackChanges:true);
            if (shipEntity == null)
            {
                _logger.LogInfo($"Ship with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _mapper.Map(ship, shipEntity);
            _repository.Save();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateShipForPort(Guid portsId, Guid id, [FromBody] JsonPatchDocument<ShipForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }
            var port = _repository.Ports.GetPort(portsId, trackChanges: false);
            if (port == null)
            {
                _logger.LogInfo($"Port with id: {portsId} doesn't exist in the database.");
                return NotFound();
            }
            var shipEntity = _repository.Ships.GetShip(portsId, id,trackChanges: true);
            if (shipEntity == null)
            {
                _logger.LogInfo($"Ship with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var shipToPatch = _mapper.Map<ShipForUpdateDto>(shipEntity);
            patchDoc.ApplyTo(shipToPatch);

            _mapper.Map(shipToPatch, shipEntity);
            _repository.Save();
            return NoContent();
        }
    }
}
