﻿using TiendaSoftware.DataBase.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace TiendaSoftware.DTOS.Softwares
{
    public class SoftwareCreateDto
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El {0} de la categoría es requerido.")]
        [StringLength(50)]

        public string Name { get; set; }

        [Display(Name = "Descripción")]
        [MinLength(10, ErrorMessage = "La {0} debe tener al menos {1} caracteres.")]
        [StringLength(500)]

        public string Description { get; set; }

        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El {0} de la categoría es requerido.")]

        public float Price { get; set; }

        [Display(Name = "Icono")]
        public string icon { get; set; }

        [Display(Name = "version")]
        [RegularExpression(@"^\d+\.\d+\.\d+$", ErrorMessage = "formato de version no valido...")]
        public string Version { get; set; }


        public virtual List<string> TagList { get; set; }
    }
}
