using TiendaSoftware.DTOS.Common;
using TiendaSoftware.DTOS.Softwares;
using TiendaSoftware.DTOS.Tags;

namespace TiendaSoftware.Services.Interfaces
{
    public interface ITagService
    {
        Task<ResponseDto<TagDto>> DeleteAsync(Guid id);
        Task<ResponseDto<PaginationDto<List<TagDto>>>> GetListAsync(string searchTerm = "", int page = 1, string Filter = "DateDes");
    }
}