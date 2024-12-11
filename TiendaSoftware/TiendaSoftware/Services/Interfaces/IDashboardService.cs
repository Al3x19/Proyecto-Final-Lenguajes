using TiendaSoftware.DTOS.Common;
using TiendaSoftware.DTOS.Dashboard;

namespace TiendaSoftware.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<ResponseDto<DashboardDto>> GetDashboardAsync();
    }
}