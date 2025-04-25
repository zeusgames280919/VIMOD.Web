using System.ComponentModel.DataAnnotations;

namespace VIMOD.Domain.Models
{
    public class Certificado
    {
        [Key]
        public int IdCertificado { get; set; }
        public DateTime FechaEmision { get; set; }
        public string RutaArchivo { get; set; }

        public int IdCurso { get; set; }
        public Curso? Curso { get; set; }

        public string IdUsuario { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
