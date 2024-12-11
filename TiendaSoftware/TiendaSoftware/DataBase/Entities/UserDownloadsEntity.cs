using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaSoftware.DataBase.Entities;
[Table("user_downloads", Schema = "dbo")]
public class UserDownloadsEntity : BaseEntity
{         

        [Column("Software_id")]
        public Guid SoftwareId { get; set; }
        
        [ForeignKey(nameof(SoftwareId))]
        public virtual SoftwareEntity Software { get; set; }

    public virtual UserEntity CreatedByUser { get; set; }
    public virtual UserEntity UpdatedByUser { get; set; }

}
