using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Livros.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Livros.Context
{
    public class ApiLivrosContext : DbContext
    {
        public ApiLivrosContext(DbContextOptions<ApiLivrosContext> options)
            : base(options)
        {
        }

        public DbSet<Livro> Livros { get; set; }
    }
}