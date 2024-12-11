using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TiendaSoftware.DTOS.Reviews;

public class ReviewCreateDto
{


    [Display(Name = "Contenido")]
    [MinLength(10, ErrorMessage = "el {0} debe tener al menos {1} caracteres.")]
    [StringLength(500)]
    public string Content { get; set; }

    [Display(Name = "Puntuacion")]
    [Required(ErrorMessage = "La {0} de la categoría es requerido.")]
    [Range(0, 100, ErrorMessage = "El valor debe estar entre 0 y 100.")]
    public int Score { get; set; }


    public Guid SoftwareId { get; set; }









}
