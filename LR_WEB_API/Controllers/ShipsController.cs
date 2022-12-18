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
        public async Task<IActionResult> GetShipsForPorts(Guid portsId)
        {
            var port = await _repository.Ports.GetPortAsync(portsId, trackChanges: false);
            if (port == null)
            {
                _logger.LogInfo($"Company with id: {portsId} doesn't exist in the database.");
            return NotFound();
            }
            var shipFromDb = await _repository.Ships.GetShipsAsync(portsId, trackChanges: false);
            var shipDTO = _mapper.Map<IEnumerable<ShipDTO>>(shipFromDb);
            return Ok(shipDTO);
        }

        [HttpGet("{id}", Name = "GetShipForPort")]
        public async Task<IActionResult> GetShipForPorts(Guid portsId, Guid id)
        {
            var port = await _repository.Ports.GetPortAsync(portsId, trackChanges: false);
            if (port == null)
            {
                _logger.LogInfo($"Port with id: {portsId} doesn't exist in the database.");
            return NotFound();
            }
            var shipDb = await _repository.Ships.GetShipAsync(portsId, id, trackChanges: false);
            if (shipDb == null)
            {
                _logger.LogInfo($"Ship with id: {id} doesn't exist in the database.");
            return NotFound();
            }
            var ship = _mapper.Map<ShipDTO>(shipDb);
            return Ok(ship);
        }
        [HttpPost]
        public async Task<IActionResult> CreateShipForPort(Guid portsId, [FromBody] ShipForCreationDto ship)
        {
            if (ship == null)
            {
                _logger.LogError("ShipForCreationDto object sent from client is null.");
            return BadRequest("ShipForCreationDto object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the ShipForCreationDto object");
                return UnprocessableEntity(ModelState);
            }
            var port = await _repository.Ports.GetPortAsync(portsId, trackChanges: false);
            if (port == null)
            {
                _logger.LogInfo($"Port with id: {portsId} doesn't exist in the database.");
            return NotFound();
            }
            var shipEntity = _mapper.Map<Ship>(ship);
            _repository.Ships.CreateShipForPort(portsId, shipEntity);
            await _repository.SaveAsync();
            var shipToReturn = _mapper.Map<ShipDTO>(shipEntity);
            return CreatedAtRoute("GetShipForPort", new
            {
                portsId,
                id = shipToReturn.Id
            }, shipToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShipForPort(Guid portsId, Guid id)
        {
            var port = await _repository.Ports.GetPortAsync(portsId, trackChanges: false);
            if (port == null)
            {
                _logger.LogInfo($"Port with id: {portsId} doesn't exist in the database.");
                return NotFound();
            }
            var shipForPort = await _repository.Ships.GetShipAsync(portsId, id, trackChanges: false);
            if (shipForPort == null)
            {
                _logger.LogInfo($"Ship with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _repository.Ships.DeleteShip(shipForPort);
            await _repository.SaveAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShipForPort(Guid portsId, Guid id, [FromBody] ShipForUpdateDto ship)
        {
            if (ship == null)
            {
                _logger.LogError("ShipForUpdateDto object sent from client is null.");
                return BadRequest("ShipForUpdateDto object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the ShipForUpdateDto object");
                return UnprocessableEntity(ModelState);
            }

            var port = await _repository.Ports.GetPortAsync(portsId, trackChanges: false);
            if (port == null)
            {
                _logger.LogInfo($"Port with id: {portsId} doesn't exist in the database.");
                return NotFound();
            }
            var shipEntity = await _repository.Ships.GetShipAsync(portsId, id, trackChanges:true);
            if (shipEntity == null)
            {
                _logger.LogInfo($"Ship with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _mapper.Map(ship, shipEntity);
            await _repository.SaveAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateShipForPort(Guid portsId, Guid id, [FromBody] JsonPatchDocument<ShipForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }
            var port = await _repository.Ports.GetPortAsync(portsId, trackChanges: false);
            if (port == null)
            {
                _logger.LogInfo($"Port with id: {portsId} doesn't exist in the database.");
                return NotFound();
            }
            var shipEntity = await _repository.Ships.GetShipAsync(portsId, id,trackChanges: true);
            if (shipEntity == null)
            {
                _logger.LogInfo($"Ship with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var shipToPatch = _mapper.Map<ShipForUpdateDto>(shipEntity);
            patchDoc.ApplyTo(shipToPatch, ModelState);
            TryValidateModel(shipToPatch);

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the patch document");
                return UnprocessableEntity(ModelState);
            }

            _mapper.Map(shipToPatch, shipEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
    }
}
