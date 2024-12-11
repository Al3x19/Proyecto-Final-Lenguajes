using TiendaSoftware.DTOS.Common;
using TiendaSoftware.DTOS.Lists;
using TiendaSoftware.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlogUNAH.API.Services;
using Microsoft.AspNetCore.Authorization;
using TiendaSoftware.DTOS.Softwares;

namespace TiendaSoftware.Controllers
{
    [Route("api/lists")]
    [ApiController]
    public class ListsController : ControllerBase
    {
        private readonly IListsService _listsService;

        public ListsController(IListsService listsService)
        {
            this._listsService = listsService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<PaginationDto<List<ListDto>>>>> PaginationList(
            string searchTerm, int page = 1)
        {
            var response = await _listsService.GetListAsync(searchTerm, page);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<ListDto>>> GetOneById(Guid id)
        {
            var response = await _listsService.GetByIdAsync(id);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<ListDto>>> Create(ListCreateDto dto)
        {
            var response = await _listsService.CreateAsync(dto);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<ListDto>>> Edit(ListEditDto dto,
            Guid id)
        {
            var response = await _listsService.EditAsync(dto, id);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
            });
        }

        [HttpGet("user/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseDto<SoftwareDto>>> GetByUserId(string id, int page = 1)
        {
            var response = await _listsService.GetByUserIdAsync(id, page);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<ListDto>>> Delete(Guid id)
        {
            var response = await _listsService.DeleteAsync(id);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
            });
        }
    }
}