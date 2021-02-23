using System.ComponentModel.DataAnnotations;

namespace CursoWebApi.Entities
{
    public class Autor
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
    }
}