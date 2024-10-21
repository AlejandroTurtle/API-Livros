using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Livros.Models;

namespace API_Livros.Interfaces
{
    public interface ILivroService
    {
        Task<ServiceResponse<List<Livro>>> ObterTodos();

        Task<ServiceResponse<Livro>> ObterPorId(int id);

        Task<ServiceResponse<Livro>> Adicionar(Livro livro);

        Task<ServiceResponse<Livro>> Atualizar(int id, Livro livro);

        Task<ServiceResponse<Livro>> Remover(int id);

        Task<ServiceResponse<List<Livro>>> VerificarConclusao(List<EnumStatusLivro> statusFiltro);
    }
}