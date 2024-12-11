using TiendaSoftware.DataBase.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace TiendaSoftware.DataBase.Entities
{
    [Table("users", Schema = "dbo")]
    public class UserEntity :  IdentityUser
    {

        [Display(Name = "Datos bancarios")]
        [Column("securitycode")]
        public string Securitycode { get; set; }

        [Column("resfesh_token")]
        [StringLength(450)]
        public string RefreshToken { get; set; }

        [Column("resfesh_token_expire")]
        public DateTime RefreshTokenExpire { get; set; }

        public virtual IEnumerable<ReviewEntity> Reviews { get; set; }

        public virtual IEnumerable<ListEntity> Lists { get; set; }
    }
}
