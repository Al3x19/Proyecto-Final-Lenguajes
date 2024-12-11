

namespace TiendaSoftware.DTOS.Dashboard
{
    public class DashboardDto
    {
        public int ListsCount { get; set; }
        public int TagsCount { get; set; }
        public int SoftwareCount { get; set; }
        public int ReviewsCount { get; set; }

        public List<DashboardSoftwareDto> Softwares { get; set; }
        public List<DashboardListDto> Users { get; set; }
        public List<DashboardTagDto> Tags { get; set; }
        public List<DashboardReviewDto> Reviews { get; set; }

    }
}
