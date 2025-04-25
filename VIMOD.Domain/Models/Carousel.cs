using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIMOD.Domain.Models
{
    public class Carousel
    {
        [Key]
        public int Id { get; set; }

        public string Titulo { get; set; }
        public string? Descripcion { get; set; }
        public string? ImagenUrl { get; set; }

    }
}
