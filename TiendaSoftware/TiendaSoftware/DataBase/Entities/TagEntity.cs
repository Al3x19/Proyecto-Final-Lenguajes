using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaSoftware.DataBase.Entities;
[Table("tags", Schema = "dbo")]
public class TagEntity : BaseEntity
{
    [StringLength(50)]
    [Required]
    [Column("name")]
    public string Name { get; set; }

    public virtual IEnumerable<SoftwareTagsEntity> Softwares { get; set; }

    public virtual UserEntity CreatedByUser { get; set; }
    public virtual UserEntity UpdatedByUser { get; set; }

}
