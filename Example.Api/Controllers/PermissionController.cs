using AutoMapper;
using Example.Domain.Entities;
using Example.Domain.Service;
using Microsoft.AspNetCore.Mvc;
using Nest;
using System.Net;


namespace Example.Api.Controllers
{
    [Route("api/permission")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPermissionService _permissionService;
        private readonly IElasticClient _elasticClient;

        public PermissionController(IMapper mapper, IPermissionService permissionService, IElasticClient elasticClient)
        {
            _mapper = mapper;
            _permissionService = permissionService;
            _elasticClient = elasticClient;
        }

        [HttpGet(Name = "GetPermission")]
        public async Task<IActionResult> Get()
        {
            var results = await _permissionService.GetAllElasticAsync();
            _permissionService.SendMessageTopic(new OperationDto
            {
                Id = Guid.NewGuid(),
                OperationName = "get"
            });
            return Ok(results);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomResponse<PermissionDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Single(int id)
        {
            var results = await _elasticClient.GetAsync<PermissionDto>(id);
            _permissionService.SendMessageTopic(new OperationDto
            {
                Id = Guid.NewGuid(),
                OperationName = "get"
            });
            return Ok(results.Source);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomResponse<PermissionDto>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create(PermissionDto dto)
        {
            var permission = _mapper.Map<Permission>(dto);
            await _permissionService.CreateAsync(permission);
            var response = new CustomResponse<PermissionDto>("Success", null);
            await _permissionService.CreateElasticAsync(permission);
            _permissionService.SendMessageTopic(new OperationDto
            {
                Id = Guid.NewGuid(),
                OperationName = "request"
            });
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(CustomResponse<PermissionDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update(PermissionDto dto)
        {
            var permission = _mapper.Map<Permission>(dto);
            await _permissionService.UpdateAsync(permission);
            var response = new CustomResponse<PermissionDto>("Success", null);
            await _permissionService.UpdateElasticAsync(permission);
            _permissionService.SendMessageTopic(new OperationDto
            {
                Id = Guid.NewGuid(),
                OperationName = "modify"
            });
            return Ok(response);
        }
    }
}