using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using LR_WEB_API.ModelBinders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LR_WEB_API.Controllers
{
    [Route("api/ports")]
    [ApiController]
    public class PortsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public PortsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetPorts()
        {
            var ports = _repository.Ports.GetAllPorts(trackChanges: false);
            var portsDto = _mapper.Map<IEnumerable<PortDTO>>(ports);
            return Ok(portsDto);
        }

        [HttpGet("{id}", Name = "PortById")]
        public IActionResult GetPort(Guid id)
        {
            var port = _repository.Ports.GetPort(id, trackChanges: false);
            if (port == null)
            {
                _logger.LogInfo($"Port with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var portDto = _mapper.Map<PortDTO>(port);
                return Ok(portDto);
            }
        }
        [HttpPost]
        public IActionResult CreatePort([FromBody] PortForCreationDto port)
        {
            if (port == null)
            {
                _logger.LogError("PortForCreationDto object sent from client is null.");
            return BadRequest("PortForCreationDto object is null");
            }
            var portEntity = _mapper.Map<Port>(port);
            _repository.Ports.CreatePort(portEntity);
            _repository.Save();
            var portToReturn = _mapper.Map<PortDTO>(portEntity);
            return CreatedAtRoute("PortById", new { id = portToReturn.Id }, portToReturn);
        }

        [HttpGet("collection/({ids})", Name = "PortCollection")]
        public IActionResult GetPortCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)

        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }
            var portEntities = _repository.Ports.GetByIds(ids, trackChanges: false);
            
            if (ids.Count() != portEntities.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }
            var portToReturn =
           _mapper.Map<IEnumerable<PortDTO>>(portEntities);
            return Ok(portToReturn);
        }
        [HttpPost("collection")]
        public IActionResult CreatePortCollection([FromBody]
        IEnumerable<PortForCreationDto> portCollection)
        {
            if (portCollection == null)
            {
                _logger.LogError("Port collection sent from client is null.");
                return BadRequest("Port collection is null");
            }
            var portEntities = _mapper.Map<IEnumerable<Port>>(portCollection);
            foreach (var port in portEntities)
            {
                _repository.Ports.CreatePort(port);
            }
            _repository.Save();
            var portCollectionToReturn =
            _mapper.Map<IEnumerable<PortDTO>>(portEntities);
            var ids = string.Join(",", portCollectionToReturn.Select(c => c.Id));
            return CreatedAtRoute("PortCollection", new { ids },
            portCollectionToReturn);
        }
    }
}
