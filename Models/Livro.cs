using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_Livros.Models
{
    public class Livro
    {
    [Key]
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Autor { get; set; }

        public EnumStatusLivro Status { get; set; }
    }
}