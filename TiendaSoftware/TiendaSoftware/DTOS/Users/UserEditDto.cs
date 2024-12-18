﻿using TiendaSoftware.DataBase.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace TiendaSoftware.DTOS.Users
{
    public class UserEditDto
    {


        [Display(Name = "Datos bancarios")]
        [Required(ErrorMessage = "Los {0} de la categoría es requerido.")]
        [StringLength(16)]
        [Column("securitycode")]
        public string Securitycode { get; set; }

    }
}
