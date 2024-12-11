using AutoMapper;
using TiendaSoftware.Constansts;
using TiendaSoftware.DataBase;
using TiendaSoftware.DataBase.Entities;
using TiendaSoftware.DTOS.Common;
using TiendaSoftware.DTOS.Lists;
using TiendaSoftware.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using TiendaSoftware.DTOS.Publishers;
using TiendaSoftware.DTOS.Softwares;
using TiendaSoftware.Services;
using Microsoft.AspNetCore.Identity;
using TiendaSoftware.DTOS.Reviews;

namespace BlogUNAH.API.Services
{
    public class ListsService : IListsService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IAuditService _auditService;
        private readonly TiendaSoftwareContext _context;
        private readonly ILogger<ListsService> _logger;
        private readonly IMapper _mapper;
        private readonly int PAGE_SIZE;

        public ListsService(UserManager<UserEntity> userManager, IAuditService auditService, TiendaSoftwareContext context, IAuthService authService, ILogger<ListsService> logger, IMapper mapper, IConfiguration configuration)
        {
            this._userManager = userManager;
            this._auditService = auditService;
            this._context = context;
            this._logger = logger;
            this._mapper = mapper;
            PAGE_SIZE = configuration.GetValue<int>("PageSize");

        }

        public async Task<ResponseDto<PaginationDto<List<ListDto>>>> GetListAsync(string searchTerm = "", int page = 1)
        {

            int startIndex = (page - 1) * PAGE_SIZE;


            var listEntityQuery = _context.Lists
                .Include(x => x.CreatedByUser)
                .Include(x => x.Softwares)
                .ThenInclude(x => x.Software)
                .ThenInclude(x => x.Downloads)
                .Include(x => x.Softwares)
                .ThenInclude(x => x.Software)
                .ThenInclude(x => x.Publisher)
                .Include(x => x.Softwares)
                .ThenInclude(x => x.Software)
                .ThenInclude(x => x.Tags)
                .ThenInclude(x => x.tags).AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                listEntityQuery = listEntityQuery
                    .Where(x => (x.Name + " " + x.CreatedByUser.UserName + " " + x.Description)
                    .ToLower().Contains(searchTerm.ToLower()));
            }
            int totalLists = await listEntityQuery.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalLists / PAGE_SIZE);

            var listsEntity = await listEntityQuery
                .OrderByDescending(x => x.CreatedDate)
                .Skip(startIndex)
                .Take(PAGE_SIZE)
            .ToListAsync();

            var listDto = _mapper.Map<List<ListDto>>(listsEntity);


            listDto.ForEach(list => {

                var listEntity = listsEntity.FirstOrDefault(x => x.Id == list.Id);
                list.SoftwareList = listEntity.Softwares.Select(ls =>

                _mapper.Map<SoftwareDto>(ls.Software)
             
                )
                .ToList();

            });
           

            return new ResponseDto<PaginationDto<List<ListDto>>>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstant.RECORDS_FOUND,
                Data = new PaginationDto<List<ListDto>>
                {
                    CurrentPage = page,
                    PageSize = PAGE_SIZE,
                    TotalItems = totalLists,
                    TotalPages = totalPages,
                    Items = listDto,
                    HasPreviousPage = page > 1,
                    HasNextPage = page < totalPages,
                }
            };

        }

        public async Task<ResponseDto<ListDto>> GetByIdAsync(Guid id)
        {
            var listEntity = await _context.Lists
                                         .Include(x => x.CreatedByUser)
                                         .Include(x => x.Softwares)
                                        .ThenInclude(x => x.Software)
                                        .ThenInclude(x => x.Downloads)
                                        .Include(x => x.Softwares)
                                        .ThenInclude(x => x.Software)
                                        .ThenInclude(x => x.Publisher)
                                        .Include(x => x.Softwares)
                                        .ThenInclude(x => x.Software)
                                        .ThenInclude(x => x.Tags)
                                        .ThenInclude(x => x.tags)
                                        .FirstOrDefaultAsync(x => x.Id == id);

            if (listEntity is null)
            {
                return new ResponseDto<ListDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstant.RECORD_NOT_FOUND
                };
            }


            var listDto = _mapper.Map<ListDto>(listEntity);

            listDto.SoftwareList = listEntity.Softwares.Select(ls =>

                _mapper.Map<SoftwareDto>(ls.Software)

                ).ToList();

            return new ResponseDto<ListDto>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstant.RECORD_FOUND,
                Data = listDto
            };
        }

        public async Task<ResponseDto<ListDto>> CreateAsync(ListCreateDto dto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {   var ListEntity = _mapper.Map<ListEntity>(dto);
                    var existingsoft = await _context.Software.Where(t => dto.SoftwaresList.Contains(t.Id)).ToListAsync();
                  



                    if (existingsoft.Count() != dto.SoftwaresList.Count())
                    {
                        return new ResponseDto<ListDto>
                        {
                            StatusCode = 400,
                            Status = false,
                            Message = "Se intento agregar un software no existente"
                        };
                    }


                    _context.Lists.Add(ListEntity);
                    await _context.SaveChangesAsync();

                    var listSoftwareEntity = existingsoft.Select(t => new ListSoftwareEntity
                    {
                        ListId = ListEntity.Id,
                        SoftwareId = t.Id,
                    }).ToList();

                
                    _context.SoftwareLists.AddRange(listSoftwareEntity);
                    await _context.SaveChangesAsync();





                    await transaction.CommitAsync();


                    return new ResponseDto<ListDto>
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
                    return new ResponseDto<ListDto>
                    {
                        StatusCode = 500,
                        Status = false,
                        Message = MessagesConstant.CREATE_ERROR,
                    };
                }
            }
        }


        public async Task<ResponseDto<PaginationDto<List<ListDto>>>> GetByUserIdAsync(string id, int page = 1)
        {

            int startIndex = (page - 1) * PAGE_SIZE;

            var userEntity = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (userEntity is null)
            {
                return new ResponseDto<PaginationDto<List<ListDto>>>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = $"{MessagesConstant.RECORD_NOT_FOUND}"
                };
            }


            var listEntityQuery = _context.Lists
                                         .Include(x => x.CreatedByUser)
                                         .Include(x => x.Softwares)
                                        .ThenInclude(x => x.Software)
                                        .ThenInclude(x => x.Downloads)
                                        .Include(x => x.Softwares)
                                        .ThenInclude(x => x.Software)
                                        .ThenInclude(x => x.Publisher)
                                        .Include(x => x.Softwares)
                                        .ThenInclude(x => x.Software)
                                        .ThenInclude(x => x.Tags)
                                        .ThenInclude(x => x.tags)
                .Where(x => x.CreatedBy == id).AsQueryable();



            int totalLists = await listEntityQuery.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalLists / PAGE_SIZE);

            var listsEntity = new List<ListEntity>();

            listsEntity = await listEntityQuery
            .OrderByDescending(x => x.CreatedDate)
            .Skip(startIndex)
            .Take(PAGE_SIZE)
             .ToListAsync();
                   

            var listDto = _mapper.Map<List<ListDto>>(listsEntity);

            listDto.ForEach(list => {

                var listEntity = listsEntity.FirstOrDefault(x => x.Id == list.Id);
                list.SoftwareList = listEntity.Softwares.Select(ls =>

                _mapper.Map<SoftwareDto>(ls.Software)

                )
                .ToList();

            });

            return new ResponseDto<PaginationDto<List<ListDto>>>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstant.RECORDS_FOUND,
                Data = new PaginationDto<List<ListDto>>
                {
                    CurrentPage = page,
                    PageSize = PAGE_SIZE,
                    TotalItems = totalLists,
                    TotalPages = totalPages,
                    Items = listDto,
                    HasPreviousPage = page > 1,
                    HasNextPage = page < totalPages,
                }
            };
        }


        public async Task<ResponseDto<ListDto>> EditAsync(ListEditDto dto, Guid id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {


                    var ListEntity = await _context.Lists
                                         .Include(x => x.CreatedByUser)
                                         .Include(x => x.Softwares)
                                        .ThenInclude(x => x.Software)
                                        .ThenInclude(x => x.Downloads)
                                        .Include(x => x.Softwares)
                                        .ThenInclude(x => x.Software)
                                        .ThenInclude(x => x.Publisher)
                                        .Include(x => x.Softwares)
                                        .ThenInclude(x => x.Software)
                                        .ThenInclude(x => x.Tags)
                                        .ThenInclude(x => x.tags)
                                        .FirstOrDefaultAsync(x => x.Id == id);
                
                    var existingsoft = await _context.Software.Where(t => dto.SoftwaresList.Contains(t.Id)).ToListAsync();
                    var UserId = _auditService.GetUserId();

                    if (ListEntity.CreatedBy != UserId)
                    {
                        return new ResponseDto<ListDto>
                        {
                            StatusCode = 401,
                            Status = false,
                            Message = "no autorizado..."
                        };
                    }



                    if (existingsoft.Count() != dto.SoftwaresList.Count())
                    {
                        return new ResponseDto<ListDto>
                        {
                            StatusCode = 400,
                            Status = false,
                            Message = "Se intento agregar un software no existente"
                        };
                    }

                    _context.Lists.Update(ListEntity);
                    await _context.SaveChangesAsync();





                    var listSoftwareEntity = existingsoft.Where(x=> ListEntity.Softwares.Any(u => u.SoftwareId==x.Id)==false).Select(t => new ListSoftwareEntity
                    {
                        ListId = ListEntity.Id,
                        SoftwareId = t.Id,
                   
                    }
                    ).ToList();


                    _context.SoftwareLists.AddRange(listSoftwareEntity);
                    await _context.SaveChangesAsync();



                    await transaction.CommitAsync();


                    return new ResponseDto<ListDto>
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
                    return new ResponseDto<ListDto>
                    {
                        StatusCode = 500,
                        Status = false,
                        Message = MessagesConstant.CREATE_ERROR,
                    };
                }
            }
        }

        public async Task<ResponseDto<ListDto>> DeleteAsync(Guid id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var ListEntity = await _context.Lists.FindAsync(id);
                    var UserId = _auditService.GetUserId();
                    var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == UserId);
                    var roles = await _userManager.GetRolesAsync(user);


                    if (ListEntity.CreatedBy != UserId || !roles.Contains("ADMIN"))
                    {
                        return new ResponseDto<ListDto>
                        {
                            StatusCode = 401,
                            Status = false,
                            Message = "no autorizado..."
                        };
                    }

                    if (ListEntity is null)
                    {
                        return new ResponseDto<ListDto>
                        {
                            StatusCode = 404,
                            Status = false,
                            Message = $"{MessagesConstant.RECORD_NOT_FOUND}"
                        };
                    }

                    _context.Lists.Remove(ListEntity);
                    await transaction.CommitAsync();

                    return new ResponseDto<ListDto>
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
                    return new ResponseDto<ListDto>
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
