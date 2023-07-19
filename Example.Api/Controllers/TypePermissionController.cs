using AutoMapper;
using Example.Domain.Service;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Example.Api.Controllers
{
    [Route("api/typePermission")]
    [ApiController]
    public class TypePermissionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITypePermissionService _typePermissionService;

        public TypePermissionController(IMapper mapper, ITypePermissionService typePermissionService)
        {
            _mapper = mapper;
            _typePermissionService = typePermissionService;
        }

        [HttpGet(Name = "GetTypePermission")]
        [ProducesResponseType(typeof(CustomResponse<IEnumerable<TypePermitDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _typePermissionService.GetAllAsync();
            var response =
                new CustomResponse<IEnumerable<TypePermitDto>>("Success", _mapper.Map<IEnumerable<TypePermitDto>>(result));
            return Ok(result);
        }
    }
}