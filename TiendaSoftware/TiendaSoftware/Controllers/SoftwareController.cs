using TiendaSoftware.DTOS.Common;
using TiendaSoftware.DTOS.Softwares;
using TiendaSoftware.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TiendaSoftware.Constants;
using Microsoft.EntityFrameworkCore;
using TiendaSoftware.DataBase.Entities;
using TiendaSoftware.Services;
using TiendaSoftware.DataBase;

namespace TiendaSoftware.Controllers
{
    [Route("api/softwares")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class SoftwaresController : ControllerBase
    {
        private readonly TiendaSoftwareContext _context;
        private readonly IAuditService _auditService;
        private readonly ISoftwaresService _softwaresService;

        public SoftwaresController(TiendaSoftwareContext context, IAuditService auditService, ISoftwaresService softwaresService)
        {
            this._context = context;
            this._auditService = auditService;
            this._softwaresService = softwaresService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseDto<PaginationDto<List<SoftwareDto>>>>> PaginationList(
            string searchTerm, int page = 1, string Filter = "DateDes")
        {
            var response = await _softwaresService.GetListAsync(searchTerm, page, Filter);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseDto<SoftwareDto>>> GetOneById(Guid id)
        {
            var response = await _softwaresService.GetByIdAsync(id);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpGet("user/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseDto<SoftwareDto>>> GetByUserId(string id, int page = 1, string Filter = "DateDes")
        {
            var response = await _softwaresService.GetByUserIdAsync(id, page, Filter);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpGet("tag")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseDto<SoftwareDto>>> GetByTag(string name, int page = 1, string Filter = "DateDes")
        {
            var response = await _softwaresService.GetByTagAsync(name, page, Filter);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpGet("publisher/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseDto<SoftwareDto>>> GetByDevId(Guid id, int page = 1, string Filter = "DateDes")
        {
            var response = await _softwaresService.GetByDevIdAsync(id, page, Filter);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpGet("download/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseDto<SoftwareDto>>> Purchase(Guid id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    var softwareEntity = await _context.Software.FindAsync(id);


                    if (softwareEntity is null)
                    {
                        throw new Exception("No se encontro el software.");
                    }
                    var UserId = _auditService.GetUserId();


                    string parentPath = Path.Combine(Directory.GetCurrentDirectory(), "storedFiles");

                    string path = Path.Combine(parentPath, softwareEntity.Direction.ToString(), softwareEntity.FileName);



                    var fileBytes = System.IO.File.ReadAllBytes(path);
                    var contentType = "application/octet-stream";


                    var downloadEntity = new UserDownloadsEntity
                    {
                        SoftwareId = id
                    };


                    _context.UserDownloads.Add(downloadEntity);

                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return File(fileBytes, contentType, path);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al instalar...", ex);
                }
            }
        }

        [HttpPost]
        //[Authorize(Roles = $"{RolesConstant.ADMIN}, {RolesConstant.PUBLISHER}")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseDto<SoftwareDto>>> Create( IFormFile file, [FromForm] SoftwareCreateDto dto)
        {
            var response = await _softwaresService.CreateAsync(file, dto);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
            });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = $"{RolesConstant.ADMIN}, {RolesConstant.PUBLISHER}")]
        //[AllowAnonymous]
        public async Task<ActionResult<ResponseDto<SoftwareDto>>> Edit(SoftwareEditDto dto,
            Guid id)
        {
            var response = await _softwaresService.EditAsync(dto, id);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
            });
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = $"{RolesConstant.ADMIN}, {RolesConstant.PUBLISHER}")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseDto<SoftwareDto>>> Delete(Guid id)
        {
            var response = await _softwaresService.DeleteAsync(id);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
            });
        }
    }
}