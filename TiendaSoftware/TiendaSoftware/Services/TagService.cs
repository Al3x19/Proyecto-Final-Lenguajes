using AutoMapper;
using BlogUNAH.API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TiendaSoftware.Constansts;
using TiendaSoftware.DataBase;
using TiendaSoftware.DataBase.Entities;
using TiendaSoftware.DTOS.Common;
using TiendaSoftware.DTOS.Softwares;
using TiendaSoftware.DTOS.Tags;
using TiendaSoftware.DTOS.Users;
using TiendaSoftware.Services.Interfaces;

namespace TiendaSoftware.Services
{
    public class TagService : ITagService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IAuditService _auditService;
        private readonly TiendaSoftwareContext _context;
        private readonly ILogger<TagService> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private int PAGE_SIZE;

        public TagService(UserManager<UserEntity> userManager, IAuditService auditService, TiendaSoftwareContext context, ILogger<TagService> logger, IMapper mapper, IConfiguration configuration)
        {
            this._userManager = userManager;
            this._auditService = auditService;
            this._context = context;
            this._logger = logger;
            this._mapper = mapper;
            this._configuration = configuration;
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
        }


        public async Task<ResponseDto<PaginationDto<List<TagDto>>>> GetListAsync(string searchTerm = "", int page = 1, string Filter = "DateDes")
        {

            int startIndex = (page - 1) * PAGE_SIZE;


            var tagEntityQuery = _context.Tags
                .Include(x => x.CreatedByUser).AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                tagEntityQuery = tagEntityQuery
                    .Where(x => (x.Name + " " + x.CreatedByUser.UserName + " " )
                    .ToLower().Contains(searchTerm.ToLower()));
            }
            int totalSoftwares = await tagEntityQuery.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalSoftwares / PAGE_SIZE);

            var tagsEntity = new List<TagEntity>();

            switch (Filter)
            {
                case "DateDes":
                     tagsEntity = await tagEntityQuery
                    .OrderByDescending(x => x.CreatedDate)
                    .Skip(startIndex)
                    .Take(PAGE_SIZE)
                    .ToListAsync();
                    break;

                case "DateAsc":
                    tagsEntity = await tagEntityQuery
                    .OrderBy(x => x.CreatedDate)
                    .Skip(startIndex)
                    .Take(PAGE_SIZE)
                    .ToListAsync();
                    break;

                case "NameDes":
                    tagsEntity = await tagEntityQuery
                    .OrderByDescending(x => x.Name)
                    .Skip(startIndex)
                    .Take(PAGE_SIZE)
                    .ToListAsync();
                    break;

                case "NameAsc":
                    tagsEntity = await tagEntityQuery
                    .OrderBy(x => x.Name)
                    .Skip(startIndex)
                    .Take(PAGE_SIZE)
                    .ToListAsync();
                    break;


                default:
                    tagsEntity = await tagEntityQuery
                    .OrderByDescending(x => x.CreatedDate)
                    .Skip(startIndex)
                    .Take(PAGE_SIZE)
                    .ToListAsync();
                    break;
            }

            var tagDto = _mapper.Map<List<TagDto>>(tagsEntity);

            return new ResponseDto<PaginationDto<List<TagDto>>>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstant.RECORDS_FOUND,
                Data = new PaginationDto<List<TagDto>>
                {
                    CurrentPage = page,
                    PageSize = PAGE_SIZE,
                    TotalItems = totalSoftwares,
                    TotalPages = totalPages,
                    Items = tagDto,
                    HasPreviousPage = page > 1,
                    HasNextPage = page < totalPages,
                }
            };
        }

        public async Task<ResponseDto<TagDto>> DeleteAsync(Guid id)
        {

            var UserId = _auditService.GetUserId();
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == UserId);
            var roles = await _userManager.GetRolesAsync(user);

            if (!roles.Contains("ADMIN"))
            {
                return new ResponseDto<TagDto>
                {
                    StatusCode = 401,
                    Status = false,
                    Message = "no autorizado..."
                };
            }

            var TagEntity = await _context.Tags
                .FirstOrDefaultAsync(x => x.Id == id);

            if (TagEntity == null)
            {
                return new ResponseDto<TagDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = "No se encontro el registro.",
                };
            }

            _context.Tags.Remove(TagEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<TagDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "Registro borrado correctamente"
            };
        }
    }
}
