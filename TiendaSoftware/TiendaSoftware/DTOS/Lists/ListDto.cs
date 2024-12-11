using TiendaSoftware.DataBase.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using TiendaSoftware.DTOS.Users;
using TiendaSoftware.DTOS.Softwares;


namespace TiendaSoftware.DTOS.Lists
{
    public class ListDto
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public UserDto CreatedByUser { get; set; }

        public List<SoftwareDto> SoftwareList { get; set; }

    }
}
