using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaSoftware.DataBase.Entities
{
    [Table("software", Schema = "dbo")]
    public class SoftwareEntity : BaseEntity
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El {0}  es requerido.")]
        [StringLength(50)]
        [Column("name")]
        public string Name { get; set; }

        [Display(Name = "Imagen")]

        [Column("icono")]
        public string Icon { get; set; }

        [Display(Name = "Descripción")]
        [MinLength(10, ErrorMessage = "La {0} debe tener al menos {1} caracteres.")]
        [StringLength(500)]
        [Column("description")]
        public string Description { get; set; }


        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El {0}  es requerido.")]
        [Column("price")]
        public float Price { get; set; }

        [Display(Name = "valoracion")]
        [Required(ErrorMessage = "La {0}  es requerida.")]
        [Column("score")]
        public float Score { get; set; }

        [Column("direction")]
        public Guid Direction { get; set; }

        [Column("file_name")]
        public string FileName { get; set; }

        [Display(Name = "version")]
        [Column("version")]

        public string Version { get; set; }

        [Column("Publisher_id")]
        public Guid PublisherId { get; set; }
        public virtual PublisherEntity Publisher { get; set; }

        public virtual IEnumerable<SoftwareTagsEntity> Tags { get; set; }
        public virtual IEnumerable<UserDownloadsEntity> Downloads{ get; set; }
        public virtual IEnumerable<ListSoftwareEntity> Lists  { get; set; }
        public virtual UserEntity UpdatedByUser { get; set; }
        public virtual UserEntity CreatedByUser { get; set; }

    }
}
