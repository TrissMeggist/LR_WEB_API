using Contracts;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LR_WEB_API.ActionFilters
{
    public class ValidateShipForPortExistsAttribute : IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        public ValidateShipForPortExistsAttribute(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;
            var trackChanges = (method.Equals("PUT") || method.Equals("PATCH")) ?
           true : false;
            var portId = (Guid)context.ActionArguments["portId"];
            var port = await _repository.Ports.GetPortAsync(portId, false);
            if (port == null)
            {
                _logger.LogInfo($"Port with id: {portId} doesn't exist in the database.");
                return;
                context.Result = new NotFoundResult();
            }
            var id = (Guid)context.ActionArguments["id"];
            var ship = await _repository.Ships.GetShipAsync(portId, id, trackChanges);
            if (ship == null)
            {
                _logger.LogInfo($"Ship with id: {id} doesn't exist in the database.");

                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("ship", ship);
                await next();
            }
        }
    }
}
