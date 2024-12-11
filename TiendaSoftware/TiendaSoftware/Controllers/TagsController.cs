using BlogUNAH.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TiendaSoftware.DTOS.Common;
using TiendaSoftware.DTOS.Reviews;
using TiendaSoftware.DTOS.Softwares;
using TiendaSoftware.Services;
using TiendaSoftware.Services.Interfaces;

namespace TiendaSoftware.Controllers
{
    [Route("api/tags")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagsController(ITagService tagService)
        {
            this._tagService = tagService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<PaginationDto<List<ReviewDto>>>>> PaginationList(string searchTerm, int page = 1, string Filter = "DateDes")
        {
            var response = await _tagService.GetListAsync(searchTerm, page, Filter);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = $"{RolesConstant.ADMIN}, {RolesConstant.PUBLISHER}")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseDto<SoftwareDto>>> Delete(Guid id)
        {
            var response = await _tagService.DeleteAsync(id);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
            });
        }
    }
}
