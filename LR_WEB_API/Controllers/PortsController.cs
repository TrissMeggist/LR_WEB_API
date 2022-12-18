using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using LR_WEB_API.ActionFilters;
using LR_WEB_API.ModelBinders;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet(Name = "GetPorts"), Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetPorts()
        {
            var ports = await _repository.Ports.GetAllPortsAsync(trackChanges: false);
            var portsDto = _mapper.Map<IEnumerable<PortDTO>>(ports);
            return Ok(portsDto);
        }

        [HttpGet("{id}", Name = "PortById")]
        public async Task<IActionResult> GetPort(Guid id)
        {
            var port = await _repository.Ports.GetPortAsync(id, trackChanges: false);
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
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreatePort([FromBody] PortForCreationDto port)
        {
            var portEntity = _mapper.Map<Port>(port);
            _repository.Ports.CreatePort(portEntity);
            await _repository.SaveAsync();
            var portToReturn = _mapper.Map<PortDTO>(portEntity);
            return CreatedAtRoute("PortById", new { id = portToReturn.Id }, portToReturn);
        }

        [HttpGet("collection/({ids})", Name = "PortCollection")]
        public async Task<IActionResult> GetPortCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)

        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }
            var portEntities = await _repository.Ports.GetByIdsAsync(ids, trackChanges: false);
            
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
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreatePortCollection([FromBody]
        IEnumerable<PortForCreationDto> portCollection)
        {
            var portEntities = _mapper.Map<IEnumerable<Port>>(portCollection);
            foreach (var port in portEntities)
            {
                _repository.Ports.CreatePort(port);
            }
            await _repository.SaveAsync();
            var portCollectionToReturn =
            _mapper.Map<IEnumerable<PortDTO>>(portEntities);
            var ids = string.Join(",", portCollectionToReturn.Select(c => c.Id));
            return CreatedAtRoute("PortCollection", new { ids },
            portCollectionToReturn);
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidatePortExistsAttribute))]
        public async Task<IActionResult> DeletePort(Guid id)
        {
            var port = HttpContext.Items["port"] as Port;
            _repository.Ports.DeletePort(port);
            await _repository.SaveAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidatePortExistsAttribute))]
        public async Task<IActionResult> UpdatePort(Guid id, [FromBody] PortForUpdateDto port)
        {
            var portEntity = HttpContext.Items["port"] as Port;
            _mapper.Map(port, portEntity);
            await _repository.SaveAsync();
            return NoContent();
        }

        [HttpOptions]
        public IActionResult GetCompaniesOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST");
            return Ok();
        }
    }
}
