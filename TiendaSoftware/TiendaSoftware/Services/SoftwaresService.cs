using AutoMapper;
using TiendaSoftware.Constansts;
using TiendaSoftware.DataBase;
using TiendaSoftware.DataBase.Entities;
using TiendaSoftware.DTOS.Common;
using TiendaSoftware.DTOS.Softwares;
using TiendaSoftware.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace BlogUNAH.API.Services
{
    public class SoftwaresService : ISoftwaresService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IAuditService _auditService;
        private readonly TiendaSoftwareContext _context;
        private readonly ILogger<SoftwaresService> _logger;
        private readonly IMapper _mapper;
        private readonly int PAGE_SIZE;

        public SoftwaresService(UserManager<UserEntity> userManager, IAuditService auditService, TiendaSoftwareContext context, IAuthService authService, ILogger<SoftwaresService> logger, IMapper mapper, IConfiguration configuration)
        {
            this._userManager = userManager;
            this._auditService = auditService;
            this._context = context;
            this._logger = logger;
            this._mapper = mapper;
            PAGE_SIZE = configuration.GetValue<int>("PageSize");

        }

        public async Task<ResponseDto<PaginationDto<List<SoftwareDto>>>> GetListAsync(string searchTerm = "", int page = 1, string Filter = "DateDes")
        {

            int startIndex = (page - 1) * PAGE_SIZE;

         
            var softwareEntityQuery = _context.Software
                .Include(x=> x.Downloads)
                .Include(x => x.Publisher)
                .Include(x => x.Tags)
                .ThenInclude(x => x.tags).AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                softwareEntityQuery = softwareEntityQuery
                    .Where(x => (x.Name + " " + x.Description)
                    .ToLower().Contains(searchTerm.ToLower()));
            }
            int totalSoftwares = await softwareEntityQuery.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalSoftwares / PAGE_SIZE);

            var softwaresEntity = new List<SoftwareEntity>();

            switch (Filter)
            {
                case "PriceDes":
                    softwaresEntity = await softwareEntityQuery
                  .OrderByDescending(x => x.Price)
                   .Skip(startIndex)
                   .Take(PAGE_SIZE)
                   .ToListAsync();
                    break;

                case "PriceAsc":
                    softwaresEntity = await softwareEntityQuery
                   .OrderBy(x => x.Price)
                   .Skip(startIndex)
                   .Take(PAGE_SIZE)
                   .ToListAsync();
                    break;


                case "Score":
                    softwaresEntity = await softwareEntityQuery
                  .OrderByDescending(x =>  x.Score)
                   .Skip(startIndex)
                   .Take(PAGE_SIZE)
                   .ToListAsync();
                    break;

                case "Popularity":
                    softwaresEntity = await softwareEntityQuery
                  .OrderByDescending(x => x.Downloads.Count())
                   .Skip(startIndex)
                   .Take(PAGE_SIZE)
                   .ToListAsync();
                    break;

                case "DateDes":
                    softwaresEntity = await softwareEntityQuery
                  .OrderByDescending(x => x.CreatedDate)
                   .Skip(startIndex)
                   .Take(PAGE_SIZE)
                   .ToListAsync();
                    break;

                case "DateAsc":
                    softwaresEntity = await softwareEntityQuery
                    .OrderBy(x => x.CreatedDate)
                    .Skip(startIndex)
                    .Take(PAGE_SIZE)
                    .ToListAsync();
                    break;

                case "NameDes":
                    softwaresEntity = await softwareEntityQuery
                   .OrderByDescending(x => x.Name)
                    .Skip(startIndex)
                    .Take(PAGE_SIZE)
                    .ToListAsync();
                    break;

                case "NameAsc":
                    softwaresEntity = await softwareEntityQuery
                   .OrderBy(x => x.Name)
                    .Skip(startIndex)
                    .Take(PAGE_SIZE)
                    .ToListAsync();
                    break;


                default:
                     softwaresEntity = await softwareEntityQuery
                    .OrderByDescending(x => x.CreatedDate)
                    .Skip(startIndex)
                    .Take(PAGE_SIZE)
                    .ToListAsync();
                    break;
            }


            var softwareDto = _mapper.Map<List<SoftwareDto>>(softwaresEntity);

            return new ResponseDto<PaginationDto<List<SoftwareDto>>>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstant.RECORDS_FOUND,
                Data = new PaginationDto<List<SoftwareDto>>
                {
                    CurrentPage = page,
                    PageSize = PAGE_SIZE,
                    TotalItems = totalSoftwares,
                    TotalPages = totalPages,
                    Items = softwareDto,
                    HasPreviousPage = page > 1,
                    HasNextPage = page < totalPages,
                }
            };
        }

        public async Task<ResponseDto<SoftwareDto>> GetByIdAsync(Guid id)
        {
            var softwareEntity = await _context.Software.Include(x => x.CreatedByUser).Include(x=> x.Publisher).Include(x => x.Tags).ThenInclude(x => x.tags).FirstOrDefaultAsync(x => x.Id == id);

            if (softwareEntity is null)
            {
                return new ResponseDto<SoftwareDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = $"{MessagesConstant.RECORD_NOT_FOUND}"
                };
            }

            var softwareDto = _mapper.Map<SoftwareDto>(softwareEntity);

            return new ResponseDto<SoftwareDto>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstant.RECORD_FOUND,
                Data = softwareDto
            };
        }

        public async Task<ResponseDto<PaginationDto<List<SoftwareDto>>>>GetByTagAsync(string name, int page = 1, string Filter = "DateDes")
        {

            int startIndex = (page - 1) * PAGE_SIZE;

            var tagEntity = await _context.Tags.FirstOrDefaultAsync(x => x.Name == name);

            if (tagEntity is null)
            {
                return new ResponseDto<PaginationDto<List<SoftwareDto>>>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = $"{MessagesConstant.RECORD_NOT_FOUND}"
                };
            }


            var softwareEntityQuery = _context.Software
                .Include(x => x.Downloads)
                .Include(x => x.Publisher)
                .Include(x => x.Tags)
                .ThenInclude(x => x.tags).Where(x => x.Tags.Any(y => y.TagId ==tagEntity.Id)).AsQueryable();


            int totalSoftwares = await softwareEntityQuery.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalSoftwares / PAGE_SIZE);

            var softwaresEntity = new List<SoftwareEntity>();

            switch (Filter)
            {
                case "PriceDes":
                    softwaresEntity = await softwareEntityQuery
                  .OrderByDescending(x => x.Price)
                   .Skip(startIndex)
                   .Take(PAGE_SIZE)
                   .ToListAsync();
                    break;

                case "PriceAsc":
                    softwaresEntity = await softwareEntityQuery
                   .OrderBy(x => x.Price)
                   .Skip(startIndex)
                   .Take(PAGE_SIZE)
                   .ToListAsync();
                    break;


                case "Score":
                    softwaresEntity = await softwareEntityQuery
                  .OrderByDescending(x => (x.Downloads.Count()) *(1+ x.Score))
                   .Skip(startIndex)
                   .Take(PAGE_SIZE)
                   .ToListAsync();
                    break;

                case "Popularity":
                    softwaresEntity = await softwareEntityQuery
                  .OrderByDescending(x => x.Downloads.Count())
                   .Skip(startIndex)
                   .Take(PAGE_SIZE)
                   .ToListAsync();
                    break;

                case "DateDes":
                    softwaresEntity = await softwareEntityQuery
                  .OrderByDescending(x => x.CreatedDate)
                   .Skip(startIndex)
                   .Take(PAGE_SIZE)
                   .ToListAsync();
                    break;

                case "DateAsc":
                    softwaresEntity = await softwareEntityQuery
                    .OrderBy(x => x.CreatedDate)
                    .Skip(startIndex)
                    .Take(PAGE_SIZE)
                    .ToListAsync();
                    break;

                case "NameDes":
                    softwaresEntity = await softwareEntityQuery
                   .OrderByDescending(x => x.Name)
                    .Skip(startIndex)
                    .Take(PAGE_SIZE)
                    .ToListAsync();
                    break;

                case "NameAsc":
                    softwaresEntity = await softwareEntityQuery
                   .OrderBy(x => x.Name)
                    .Skip(startIndex)
                    .Take(PAGE_SIZE)
                    .ToListAsync();
                    break;


                default:
                    softwaresEntity = await softwareEntityQuery
                   .OrderByDescending(x => x.CreatedDate)
                   .Skip(startIndex)
                   .Take(PAGE_SIZE)
                   .ToListAsync();
                    break;
            }


            var softwareDto = _mapper.Map<List<SoftwareDto>>(softwaresEntity);

            return new ResponseDto<PaginationDto<List<SoftwareDto>>>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstant.RECORDS_FOUND,
                Data = new PaginationDto<List<SoftwareDto>>
                {
                    CurrentPage = page,
                    PageSize = PAGE_SIZE,
                    TotalItems = totalSoftwares,
                    TotalPages = totalPages,
                    Items = softwareDto,
                    HasPreviousPage = page > 1,
                    HasNextPage = page < totalPages,
                }
            };
        }

        public async Task<ResponseDto<PaginationDto<List<SoftwareDto>>>> GetByDevIdAsync(Guid id, int page = 1, string Filter = "DateDes")
        {

            int startIndex = (page - 1) * PAGE_SIZE;

            var devEntity = await _context.Publishers.FirstOrDefaultAsync(x => x.Id == id);

            if (devEntity is null)
            {
                return new ResponseDto<PaginationDto<List<SoftwareDto>>>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = $"{MessagesConstant.RECORD_NOT_FOUND}"
                };
            }


            var softwareEntityQuery = _context.Software
                .Include(x => x.Downloads)
                .Include(x => x.Publisher)
                .Include(x => x.Tags)
                .ThenInclude(x => x.tags).Where(x => x.PublisherId==id).AsQueryable();



            int totalSoftwares = await softwareEntityQuery.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalSoftwares / PAGE_SIZE);

            var softwaresEntity = new List<SoftwareEntity>();

            switch (Filter)
            {
                case "PriceDes":
                    softwaresEntity = await softwareEntityQuery
                  .OrderByDescending(x => x.Price)
                   .Skip(startIndex)
                   .Take(PAGE_SIZE)
                   .ToListAsync();
                    break;

                case "PriceAsc":
                    softwaresEntity = await softwareEntityQuery
                   .OrderBy(x => x.Price)
                   .Skip(startIndex)
                   .Take(PAGE_SIZE)
                   .ToListAsync();
                    break;

                case "Score":
                    softwaresEntity = await softwareEntityQuery
                  .OrderByDescending(x => (x.Downloads.Count()) * (1 + x.Score))
                   .Skip(startIndex)
                   .Take(PAGE_SIZE)
                   .ToListAsync();
                    break;

                case "Popularity":
                    softwaresEntity = await softwareEntityQuery
                  .OrderByDescending(x => x.Downloads.Count())
                   .Skip(startIndex)
                   .Take(PAGE_SIZE)
                   .ToListAsync();
                    break;

                case "DateDes":
                    softwaresEntity = await softwareEntityQuery
                  .OrderByDescending(x => x.CreatedDate)
                   .Skip(startIndex)
                   .Take(PAGE_SIZE)
                   .ToListAsync();
                    break;

                case "DateAsc":
                    softwaresEntity = await softwareEntityQuery
                    .OrderBy(x => x.CreatedDate)
                    .Skip(startIndex)
                    .Take(PAGE_SIZE)
                    .ToListAsync();
                    break;

                case "NameDes":
                    softwaresEntity = await softwareEntityQuery
                   .OrderByDescending(x => x.Name)
                    .Skip(startIndex)
                    .Take(PAGE_SIZE)
                    .ToListAsync();
                    break;

                case "NameAsc":
                    softwaresEntity = await softwareEntityQuery
                   .OrderBy(x => x.Name)
                    .Skip(startIndex)
                    .Take(PAGE_SIZE)
                    .ToListAsync();
                    break;


                default:
                    softwaresEntity = await softwareEntityQuery
                   .OrderByDescending(x => x.CreatedDate)
                   .Skip(startIndex)
                   .Take(PAGE_SIZE)
                   .ToListAsync();
                    break;
            }


            var softwareDto = _mapper.Map<List<SoftwareDto>>(softwaresEntity);

            return new ResponseDto<PaginationDto<List<SoftwareDto>>>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstant.RECORDS_FOUND,
                Data = new PaginationDto<List<SoftwareDto>>
                {
                    CurrentPage = page,
                    PageSize = PAGE_SIZE,
                    TotalItems = totalSoftwares,
                    TotalPages = totalPages,
                    Items = softwareDto,
                    HasPreviousPage = page > 1,
                    HasNextPage = page < totalPages,
                }
            };
        }

        public async Task<ResponseDto<PaginationDto<List<SoftwareDto>>>> GetByUserIdAsync(string id, int page = 1, string Filter = "DateDes")
        {

            int startIndex = (page - 1) * PAGE_SIZE;

            var userEntity = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (userEntity is null)
            {
                return new ResponseDto<PaginationDto<List<SoftwareDto>>>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = $"{MessagesConstant.RECORD_NOT_FOUND}"
                };
            }


            var softwareEntityQuery = _context.Software
                .Include(x => x.Downloads)
                .Include(x => x.Publisher)
                .Include(x => x.Tags)
                .ThenInclude(x => x.tags).Where(x => x.Downloads.Any(y => y.CreatedBy == id)).AsQueryable();



            int totalSoftwares = await softwareEntityQuery.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalSoftwares / PAGE_SIZE);

            var softwaresEntity = new List<SoftwareEntity>();

            switch (Filter)
            {

                case "PriceDes":
                    softwaresEntity = await softwareEntityQuery
                  .OrderByDescending(x => x.Price)
                   .Skip(startIndex)
                   .Take(PAGE_SIZE)
                   .ToListAsync();
                    break;

                case "PriceAsc":
                    softwaresEntity = await softwareEntityQuery
                   .OrderBy(x => x.Price)
                   .Skip(startIndex)
                   .Take(PAGE_SIZE)
                   .ToListAsync();
                    break;

                case "Score":
                    softwaresEntity = await softwareEntityQuery
                  .OrderByDescending(x => (x.Downloads.Count()) * (1 + x.Score))
                   .Skip(startIndex)
                   .Take(PAGE_SIZE)
                   .ToListAsync();
                    break;

                case "Popularity":
                    softwaresEntity = await softwareEntityQuery
                  .OrderByDescending(x => x.Downloads.Count())
                   .Skip(startIndex)
                   .Take(PAGE_SIZE)
                   .ToListAsync();
                    break;

                case "DateDes":
                    softwaresEntity = await softwareEntityQuery
                  .OrderByDescending(x => x.CreatedDate)
                   .Skip(startIndex)
                   .Take(PAGE_SIZE)
                   .ToListAsync();
                    break;

                case "DateAsc":
                    softwaresEntity = await softwareEntityQuery
                    .OrderBy(x => x.CreatedDate)
                    .Skip(startIndex)
                    .Take(PAGE_SIZE)
                    .ToListAsync();
                    break;

                case "NameDes":
                    softwaresEntity = await softwareEntityQuery
                   .OrderByDescending(x => x.Name)
                    .Skip(startIndex)
                    .Take(PAGE_SIZE)
                    .ToListAsync();
                    break;

                case "NameAsc":
                    softwaresEntity = await softwareEntityQuery
                   .OrderBy(x => x.Name)
                    .Skip(startIndex)
                    .Take(PAGE_SIZE)
                    .ToListAsync();
                    break;


                default:
                    softwaresEntity = await softwareEntityQuery
                   .OrderByDescending(x => x.CreatedDate)
                   .Skip(startIndex)
                   .Take(PAGE_SIZE)
                   .ToListAsync();
                    break;
            }


            var softwareDto = _mapper.Map<List<SoftwareDto>>(softwaresEntity);

            return new ResponseDto<PaginationDto<List<SoftwareDto>>>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstant.RECORDS_FOUND,
                Data = new PaginationDto<List<SoftwareDto>>
                {
                    CurrentPage = page,
                    PageSize = PAGE_SIZE,
                    TotalItems = totalSoftwares,
                    TotalPages = totalPages,
                    Items = softwareDto,
                    HasPreviousPage = page > 1,
                    HasNextPage = page < totalPages,
                }
            };
        }

        public async Task<ResponseDto<SoftwareDto>> CreateAsync(IFormFile file,SoftwareCreateDto dto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    List<string> validExtensions = new List<string>() { ".exe", ".iso", ".zip", ".rar" };
                    string extension = Path.GetExtension(file.FileName);

                    if (!validExtensions.Contains(extension))
                    {

                        return new ResponseDto<SoftwareDto>
                        {
                            StatusCode = 405,
                            Status = false,
                            Message = "Formato de archivo no aceptado..."
                        };
                    }

                    string parentPath = Path.Combine(Directory.GetCurrentDirectory(), "storedFiles");

                    Guid folderName = Guid.NewGuid();

                    string path = Path.Combine(parentPath, folderName.ToString());

                    Directory.CreateDirectory(path);

                    string filePath = Path.Combine(path, file.FileName);

                    using FileStream stream = new FileStream(filePath, FileMode.Create);

                    file.CopyTo(stream);
                    //-----------------------------------------------------------------------------------------

                    var softwareEntity = _mapper.Map<SoftwareEntity>(dto);
                    softwareEntity.Direction = folderName;

                    var UserId = _auditService.GetUserId();
                    var devEntity = await _context.Publishers.FirstOrDefaultAsync(x => x.CreatedBy == UserId);

                    if (devEntity is null)
                    {
                        return new ResponseDto<SoftwareDto>
                        {
                            StatusCode = 401,
                            Status = false,
                            Message = "no autorizado..."
                        };
                    }

                    softwareEntity.PublisherId = devEntity.Id;
                    softwareEntity.FileName = Path.GetFileName(file.FileName);

                    _context.Software.Add(softwareEntity);
                    await _context.SaveChangesAsync();
                    

                    //Buscar tags del dto en la tabla de tags 
                    var existingTags = await _context.Tags.Where(t => dto.TagList.Contains(t.Name)).ToListAsync();

                    // Identificar tags que no existen
                    var newTagNames = dto.TagList.Except(existingTags.Select(t => t.Name)).ToList();

                    // Crear los nuevos tags
                    var newTags = newTagNames.Select(name => new TagEntity
                    {
                        Name = name,
                    }).ToList();

                    _context.Tags.AddRange(newTags);
                    await _context.SaveChangesAsync();

                    var allTags = existingTags.Concat(newTags).ToList();

                    var softwareTagsEntity = allTags.Select(t => new SoftwareTagsEntity
                    {
                        SoftwareId = softwareEntity.Id,
                        TagId = t.Id,
                    }).ToList();

                    _context.SoftwareTags.AddRange(softwareTagsEntity);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();


                    return new ResponseDto<SoftwareDto>
                    {
                        StatusCode = 201,
                        Status = true,
                        Message = MessagesConstant.CREATE_SUCCESS,
                    };
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(e, MessagesConstant.CREATE_ERROR);
                    return new ResponseDto<SoftwareDto>
                    {
                        StatusCode = 500,
                        Status = false,
                        Message = MessagesConstant.CREATE_ERROR,
                    };
                }
            }
        }

        public async Task<ResponseDto<SoftwareDto>>  EditAsync(SoftwareEditDto dto, Guid id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                     var UserId = _auditService.GetUserId();
                    var softwareEntity = await _context.Software.FindAsync(id);
                    var devEntity = await _context.Publishers.FirstOrDefaultAsync(x => x.Id == softwareEntity.PublisherId);

                    if (devEntity.CreatedBy != UserId)
                    {
                        return new ResponseDto<SoftwareDto>
                        {
                            StatusCode = 401,
                            Status = false,
                            Message = "no autorizado..."
                        };
                    }

                    if (softwareEntity is null)
                    {
                        return new ResponseDto<SoftwareDto>
                        {
                            StatusCode = 404,
                            Status = false,
                            Message = "registro no encontrado"
                        };
                    }

                    _mapper.Map(dto, softwareEntity);

                    _context.Software.Update(softwareEntity);
                    await _context.SaveChangesAsync();

                    // Eliminar tags anteriores 
                    var existingSoftwareTags = await _context.SoftwareTags.Where(t => t.SoftwareId == id).ToListAsync();

                    _context.RemoveRange(existingSoftwareTags);
                    await _context.SaveChangesAsync();

                    // Buscar tags de dto en la tabla de tags 
                    var existingTags = await _context.Tags.Where(t => dto.TagList.Contains(t.Name)).ToListAsync();

                    // Identficar tags que no existen
                    var newTagsNames = dto.TagList.Except(existingTags.Select(t => t.Name)).ToList();

                    // Crear nuevos tags 
                    var newTags = newTagsNames.Select(name => new TagEntity
                    {
                        Name = name,
                    }).ToList();

                    _context.Tags.AddRange(newTags);

                    await _context.SaveChangesAsync();

                    // Combinar tags existentes y nuevas

                    var allTags = existingTags.Concat(newTags).ToList();

                    // Agregar tags a la tabla Software_tags
                    var softwareTagsNew = allTags.Select(t => new SoftwareTagsEntity
                    {
                        SoftwareId = softwareEntity.Id,
                        TagId = t.Id,
                    }).ToList();

                    _context.SoftwareTags.AddRange(softwareTagsNew);
                    await _context.SaveChangesAsync();

                    //throw new Exception("Error para validar el rollback");

                    await transaction.CommitAsync();

                    return new ResponseDto<SoftwareDto>
                    {
                        StatusCode = 200,
                        Status = true,
                        Message = MessagesConstant.UPDATE_SUCCESS
                    };

                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(e, MessagesConstant.UPDATE_ERROR);
                    return new ResponseDto<SoftwareDto>
                    {
                        StatusCode = 500,
                        Status = false,
                        Message = MessagesConstant.UPDATE_ERROR
                    };
                }
            }
        }



        public async Task<ResponseDto<SoftwareDto>> DeleteAsync(Guid id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                   
                    var softwareEntity = await _context.Software.FindAsync(id);
                    var devEntity = await _context.Publishers.FirstOrDefaultAsync(x => x.Id == softwareEntity.PublisherId);
                    var UserId = _auditService.GetUserId();
                    var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == UserId);
                    var roles = await _userManager.GetRolesAsync(user);

     
                    if (devEntity.CreatedBy != UserId || !roles.Contains("ADMIN"))
                    {
                        return new ResponseDto<SoftwareDto>
                        {
                            StatusCode = 401,
                            Status = false,
                            Message = "no autorizado..."
                        };
                    }

                    if (softwareEntity is null)
                    {
                        return new ResponseDto<SoftwareDto>
                        {
                            StatusCode = 404,
                            Status = false,
                            Message = $"{MessagesConstant.RECORD_NOT_FOUND}"
                        };
                    }


                    //____________________________________________________________

                    //____________________________________________________________

                    var listsRel =  _context.SoftwareLists.Where(x => x.SoftwareId == softwareEntity.Id);
                     _context.RemoveRange(listsRel);

                    var TagsRel = _context.SoftwareTags.Where(x => x.SoftwareId == softwareEntity.Id);
                    _context.RemoveRange(TagsRel);

                    var reviewRel = _context.Reviews.Where(x => x.SoftwareId == softwareEntity.Id);
                    _context.RemoveRange(reviewRel);

                    _context.Software.Remove(softwareEntity);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return new ResponseDto<SoftwareDto>
                    {
                        StatusCode = 200,
                        Status = true,
                        Message = MessagesConstant.DELETE_SUCCESS
                    };
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(e, MessagesConstant.DELETE_ERROR);
                    return new ResponseDto<SoftwareDto>
                    {
                        StatusCode = 500,
                        Status = false,
                        Message = MessagesConstant.DELETE_ERROR
                    };
                }
            }
        }
    }
}
