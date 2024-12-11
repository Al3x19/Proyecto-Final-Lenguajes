using AutoMapper;
using TiendaSoftware.DataBase;
using TiendaSoftware.DTOS.Common;


using Microsoft.EntityFrameworkCore;
using TiendaSoftware.Services.Interfaces;
using TiendaSoftware.DTOS.Dashboard;

namespace  TiendaSoftware.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly TiendaSoftwareContext _context;
        private readonly IMapper _mapper;

        public DashboardService(TiendaSoftwareContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<ResponseDto<DashboardDto>> GetDashboardAsync()
        {
            var softwares = await _context.Software.OrderByDescending(x => x.CreatedDate).Take(5).ToListAsync();
            var lists = await _context.Users.OrderByDescending(x => x.UserName).Take(5).ToListAsync();
            var tags = await _context.Tags.OrderByDescending(x => x.CreatedDate).Take(5).ToListAsync();
            var reviews = await _context.Reviews.OrderByDescending(x => x.CreatedDate).Take(5).ToListAsync();

            var dashboardDto = new DashboardDto
            {
                SoftwareCount = await _context.Software.CountAsync(),
                TagsCount = await _context.Tags.CountAsync(),
                ListsCount = await _context.Users.CountAsync(),
                ReviewsCount = await _context.Reviews.CountAsync(),
                Softwares = _mapper.Map<List<DashboardSoftwareDto>>(softwares),
                Tags = _mapper.Map<List<DashboardTagDto>>(tags),
                Reviews = _mapper.Map<List<DashboardReviewDto>>(reviews),
                Users = _mapper.Map<List<DashboardListDto>>(lists)

            };

            return new ResponseDto<DashboardDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "",
                Data = dashboardDto
            };
        }
    }
}
