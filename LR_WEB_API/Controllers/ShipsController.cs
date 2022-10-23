using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LR_WEB_API.Controllers
{
    [Route("api/ships")]
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
        public IActionResult GetShips()
        {
            var ship = _repository.Ships.GetAllShips(trackChanges: false);
            var shipDto = _mapper.Map<IEnumerable<ShipDTO>>(ship);
            return Ok(shipDto);
        }
    }
}
