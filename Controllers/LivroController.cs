using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Livros.Context;
using API_Livros.Interfaces;
using API_Livros.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Livros.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivroController : ControllerBase
    {
        private readonly ILivroService _LivroService;

        public LivroController(ILivroService livroService)
        {
            _LivroService = livroService;
        }

        [HttpGet]
        public async Task<ServiceResponse<List<Livro>>> ObterTodos()
        {
            var livros = await _LivroService.ObterTodos();
            return livros;
        }

        [HttpPost]
        public async Task<ServiceResponse<Livro>> Adicionar(Livro livro)
        {
            var livros = await _LivroService.Adicionar(livro);
            return livros;
        }

        [HttpDelete("{id}")]
        public async Task<ServiceResponse<Livro>> Remover(int id)
        {
            var livros = await _LivroService.Remover(id);
            return livros;
        }

        [HttpGet("verificar-conclusao")]
        public async Task<IActionResult> VerificarConclusao([FromQuery] List<EnumStatusLivro> statusFiltro)
        {
            // Chama o método VerificarConclusao no serviço
            var response = await _LivroService.VerificarConclusao(statusFiltro);

            // Retorna a resposta de acordo com o resultado do serviço
            if (response.Sucesso)
            {
                return Ok(response); // Retorna 200 OK com os dados
            }
            else
            {
                return BadRequest(response); // Retorna 400 Bad Request com a mensagem de erro
            }
        }

        [HttpGet("{id}")]
        public async Task<ServiceResponse<Livro>> ObterPorId(int id)
        {
            var livros = await _LivroService.ObterPorId(id);
            return livros;
        }

        [HttpPut("{id}")]
        public async Task<ServiceResponse<Livro>> Atualizar(int id, Livro livro)
        {
            var livros = await _LivroService.Atualizar(id, livro);
            return livros;
        }
    }
}
