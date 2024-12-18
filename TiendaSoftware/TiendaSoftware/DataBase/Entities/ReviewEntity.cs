﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaSoftware.DataBase.Entities
{
    [Table("reviews", Schema = "dbo")]
    public class ReviewEntity : BaseEntity
    {

        [Display(Name = "Contenido")]
        [MinLength(10, ErrorMessage = "el {0} debe tener al menos {1} caracteres.")]
        [StringLength(500)]
        [Column("content")]
        public string Content { get; set; }

        [Display(Name = "Puntuacion")]
        [Required(ErrorMessage = "La {0} de la categoría es requerido.")]
        [Range(0, 100, ErrorMessage = "El valor debe estar entre 0 y 100.")]
        [Column("score")]

        public int Score { get; set; }

        [Column("software_id")]
        public Guid SoftwareId { get; set; }


        [ForeignKey(nameof(SoftwareId))]
        public virtual SoftwareEntity Software { get; set; }


        public virtual UserEntity CreatedByUser { get; set; }
        public virtual UserEntity UpdatedByUser { get; set; }

    }
}
