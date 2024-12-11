using TiendaSoftware.DTOS.Common;
using TiendaSoftware.DTOS.Softwares;

namespace TiendaSoftware.Services.Interfaces
{
    public interface ISoftwaresService
    {
        Task<ResponseDto<SoftwareDto>> CreateAsync(IFormFile file,  SoftwareCreateDto dto);
        Task<ResponseDto<SoftwareDto>> DeleteAsync(Guid id);
        Task<ResponseDto<SoftwareDto>> EditAsync(SoftwareEditDto dto, Guid id);
        Task<ResponseDto<PaginationDto<List<SoftwareDto>>>> GetByDevIdAsync(Guid id, int page = 1, string Filter = "DateDes");
        Task<ResponseDto<SoftwareDto>> GetByIdAsync(Guid id);
        Task<ResponseDto<PaginationDto<List<SoftwareDto>>>> GetByTagAsync(string name, int page = 1, string Filter = "DateDes");
        Task<ResponseDto<PaginationDto<List<SoftwareDto>>>> GetByUserIdAsync(string id, int page = 1, string Filter = "DateDes");
        Task<ResponseDto<PaginationDto<List<SoftwareDto>>>> GetListAsync(string searchTerm = "", int page = 1, string Filter = "DateDes");
    }
}
