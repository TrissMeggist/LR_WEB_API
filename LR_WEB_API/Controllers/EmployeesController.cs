﻿using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LR_WEB_API.Controllers
{
    [Route("api/employeesis")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public EmployeesController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetEmployeesis()
        {
            var employees = _repository.Employee.GetAllEmployees(trackChanges: false);
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDTO>>(employees);
            return Ok(employeesDto);
        }
    }
}
