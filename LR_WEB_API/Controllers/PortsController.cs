using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
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

        [HttpGet("{id}")]
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
    }
}
