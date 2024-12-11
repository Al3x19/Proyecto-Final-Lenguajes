using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TiendaSoftware.DataBase.Entities;

namespace TiendaSoftware.DataBase.Entities
{
    [Table("publishers", Schema = "dbo")]
    public class PublisherEntity : BaseEntity
    {

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El {0} de la categoría es requerido.")]
        [StringLength(50)]
        [Column("name")]
        public string Name { get; set; }

        [Display(Name = "Descripción")]
        [MinLength(10, ErrorMessage = "La {0} debe tener al menos {1} caracteres.")]
        [StringLength(500)]
        [Column("description")]
        public string Description { get; set; }

        [Display(Name = "Imagen")]
        [Column("icono")]
        public string Icon { get; set; }

        public virtual IEnumerable<SoftwareEntity> Software { get; set; }

        public virtual UserEntity CreatedByUser { get; set; }
        public virtual UserEntity UpdatedByUser { get; set; }
    }
}
