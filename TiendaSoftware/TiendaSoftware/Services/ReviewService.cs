using AutoMapper;
using TiendaSoftware.DataBase;
using TiendaSoftware.DataBase.Entities;
using TiendaSoftware.DTOS.Reviews;
using TiendaSoftware.DTOS.Common;
using TiendaSoftware.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using TiendaSoftware.Constansts;
using TiendaSoftware.DTOS.Softwares;
using System.Data;
using Microsoft.AspNetCore.Identity;


namespace TiendaSoftware.Services
{
    public class ReviewService : IReviewService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IAuditService _auditService;
        private readonly TiendaSoftwareContext _context;
        private readonly IMapper _mapper;
        private readonly int PAGE_SIZE;

        public ReviewService(UserManager<UserEntity> userManager, IAuditService auditService, TiendaSoftwareContext context, IMapper mapper, IConfiguration configuration)
        {
            this._userManager = userManager;
            this._auditService = auditService;
            this._context = context;
            this._mapper = mapper;
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
        }

        public async Task<ResponseDto<PaginationDto<List<ReviewDto>>>> GetReviewsListAsync(string searchTerm = "", int page = 1)
        {
            int startIndex = (page - 1) * PAGE_SIZE;

            // Start with the base query
            var reviewEntityQuery = _context.Reviews
                .Include(x => x.CreatedByUser).AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                reviewEntityQuery = reviewEntityQuery
                    .Where(x => (x.CreatedByUser + " " + x.CreatedByUser.UserName + " " + x.Content)
                    .ToLower().Contains(searchTerm.ToLower()));
            }
            int totalReviews = await reviewEntityQuery.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalReviews / PAGE_SIZE);

            var reviewsEntity = await reviewEntityQuery
                .OrderByDescending(x => x.CreatedDate)
                .Skip(startIndex)
                .Take(PAGE_SIZE)
            .ToListAsync();

            var reviewDto = _mapper.Map<List<ReviewDto>>(reviewsEntity);

            return new ResponseDto<PaginationDto<List<ReviewDto>>>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstant.RECORDS_FOUND,
                Data = new PaginationDto<List<ReviewDto>>
                {
                    CurrentPage = page,
                    PageSize = PAGE_SIZE,
                    TotalItems = totalReviews,
                    TotalPages = totalPages,
                    Items = reviewDto,
                    HasPreviousPage = page > 1,
                    HasNextPage = page < totalPages,
                }
            };

        }
        public async Task<ResponseDto<ReviewDto>> GetByIdAsync(string id, Guid SoftwareId)
        {
            var reviewEntity = await _context.Reviews.FirstOrDefaultAsync(c => c.CreatedBy == id && c.SoftwareId == SoftwareId);
            if (reviewEntity == null)
            {
                return new ResponseDto<ReviewDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = "No se encontro el registro.",
                };
            }
            var reviewDto = _mapper.Map<ReviewDto>(reviewEntity);

            return new ResponseDto<ReviewDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "Registro encontrado.",
                Data = reviewDto
            };
        }

        public async Task<ResponseDto<ReviewDto>> CreateAsync(ReviewCreateDto dto)
        {

            var UserId = _auditService.GetUserId();
            
            var reviewEntity = _mapper.Map<ReviewEntity>(dto);
            //____________________________________________________________
            var userrev = await _context.Reviews.AnyAsync(x => x.CreatedBy == UserId && x.SoftwareId == reviewEntity.SoftwareId);


            if (userrev)
            {

                return new ResponseDto<ReviewDto>
                {
                    StatusCode = 405,
                    Status = false,
                    Message = "No se puede ingresar mas de un review.",
                };

            }
            //____________________________________________________________


            _context.Reviews.Add(reviewEntity);
            await _context.SaveChangesAsync();

            var Software = await _context.Software.FirstOrDefaultAsync(x => x.Id == dto.SoftwareId);
            var revs = _context.Reviews.Where(i => i.SoftwareId == Software.Id);
            Software.Score = await (revs.SumAsync(x => x.Score)) / revs.Count();

            _context.Software.Update(Software);
            await _context.SaveChangesAsync();

            return new ResponseDto<ReviewDto>
            {
                StatusCode = 201,
                Status = true,
                Message = MessagesConstant.CREATE_SUCCESS,
            };
        }

        public async Task<ResponseDto<ReviewDto>> EditAsync(ReviewEditDto dto, Guid id)
        {
            var reviewEntity = await _context.Reviews.FirstOrDefaultAsync(c => c.Id == id);
            if (reviewEntity == null)
            {
                return new ResponseDto<ReviewDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = "No se encontro el registro.",
                };
            }


            var UserId = _auditService.GetUserId();

            if (reviewEntity.CreatedBy != UserId)
            {
                return new ResponseDto<ReviewDto>
                {
                    StatusCode = 401,
                    Status = false,
                    Message = "no autorizado..."
                };
            }

            _mapper.Map<ReviewEditDto, ReviewEntity>(dto, reviewEntity);



            _context.Reviews.Update(reviewEntity);
            await _context.SaveChangesAsync();

            var Software = await _context.Software.FirstOrDefaultAsync(x => x.Id == dto.SoftwareId);
            var revs = _context.Reviews.Where(i => i.SoftwareId == Software.Id);
            Software.Score = await (revs.SumAsync(x => x.Score))/revs.Count();

            _context.Software.Update(Software);
            await _context.SaveChangesAsync();

            var reviewDto = _mapper.Map<ReviewDto>(reviewEntity);

            return new ResponseDto<ReviewDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "Registro modificado correctamente.",
                Data = reviewDto
            };
        }

        public async Task<ResponseDto<ReviewDto>> DeleteAsync(Guid id)
        {
            var reviewEntity = await _context.Reviews
                .FirstOrDefaultAsync(x => x.Id == id);
            var UserId = _auditService.GetUserId();
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == UserId);
            var roles = await _userManager.GetRolesAsync(user);


            if (reviewEntity.CreatedBy != UserId || !roles.Contains("ADMIN"))
            {
                return new ResponseDto<ReviewDto>
                {
                    StatusCode = 401,
                    Status = false,
                    Message = "no autorizado..."
                };
            }

            if (reviewEntity == null)
            {
                return new ResponseDto<ReviewDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = "No se encontro el registro.",
                };
            }

            _context.Reviews.Remove(reviewEntity);
            await _context.SaveChangesAsync();

            var Software = await _context.Software.FirstOrDefaultAsync(x => x.Id == id);
            var revs = _context.Reviews.Where(i => i.SoftwareId == Software.Id);
            Software.Score = await (revs.SumAsync(x => x.Score)) / revs.Count();

            _context.Software.Update(Software);
            await _context.SaveChangesAsync();

            return new ResponseDto<ReviewDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "Registro borrado correctamente"
            };
        }



        public async Task<ResponseDto<PaginationDto<List<ReviewDto>>>> GetBySoftwareIdAsync(Guid id, int page = 1)
        {

            int startIndex = (page - 1) * PAGE_SIZE;

            var softwareEntityQuery =  _context.Reviews
                                       .Include(x => x.CreatedByUser)
                                       .Include(x => x.Software)
                                       .AsQueryable();

            if (softwareEntityQuery is null)
            {
                return new ResponseDto<PaginationDto<List<ReviewDto>>>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = $"{MessagesConstant.RECORD_NOT_FOUND}"
                };
            }


                softwareEntityQuery = softwareEntityQuery.Where(x => x.SoftwareId == id);
  

            int totalReviews = await softwareEntityQuery.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalReviews / PAGE_SIZE);

            var softwaresEntity =  await softwareEntityQuery
                .OrderByDescending(x => x.CreatedDate)
                .Skip(startIndex)
                .Take(PAGE_SIZE)
            .ToListAsync();

            var reviewDto = _mapper.Map<List<ReviewDto>>(softwaresEntity);

            return new ResponseDto<PaginationDto<List<ReviewDto>>>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstant.RECORDS_FOUND,
                Data = new PaginationDto<List<ReviewDto>>
                {
                    CurrentPage = page,
                    PageSize = PAGE_SIZE,
                    TotalItems = totalReviews,
                    TotalPages = totalPages,
                    Items = reviewDto,
                    HasPreviousPage = page > 1,
                    HasNextPage = page < totalPages,
                }
            };
        }


    }
}