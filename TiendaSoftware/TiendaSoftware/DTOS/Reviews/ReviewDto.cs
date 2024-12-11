using TiendaSoftware.DataBase.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using TiendaSoftware.DTOS.Users;


namespace TiendaSoftware.DTOS.Reviews
{
    public class ReviewDto
    {
        public Guid Id { get; set; }

        public string Content { get; set; }


        public int Score { get; set; }


        public virtual SoftwareEntity Software { get; set; }


        public UserDto CreatedByUser { get; set; }





    }
}
