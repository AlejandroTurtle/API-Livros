using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Livros.Context;
using API_Livros.Interfaces;
using API_Livros.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Livros.Services
{
    public class LivroService : ILivroService
    {
        private readonly ApiLivrosContext _context;

        public LivroService(ApiLivrosContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<Livro>> Adicionar(Livro livro)
        {
            ServiceResponse<Livro> serviceResponse = new ServiceResponse<Livro>();

            try
            {
                if (livro == null) 
                {
                    serviceResponse.Sucesso = false;
                    serviceResponse.Mensagem = "Livro não informado";
                    serviceResponse.Dados = null;

                    return serviceResponse;
                }
                await _context.Livros.AddAsync(livro);
                await _context.SaveChangesAsync();
                serviceResponse.Dados = livro;
            }
            catch (Exception ex)
            {
                
                serviceResponse.Sucesso = false;
                serviceResponse.Mensagem = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<Livro>> Atualizar(int id, Livro livro)
        {
            ServiceResponse<Livro> serviceResponse = new ServiceResponse<Livro>();

            try
            {
                var atualizarLivro = _context.Livros.Find(id);

                if (atualizarLivro == null)
                {
                    serviceResponse.Sucesso = false;
                    serviceResponse.Mensagem = "Livro não encontrado";
                    serviceResponse.Dados = null;
                }

                atualizarLivro.Titulo = livro.Titulo;
                atualizarLivro.Autor = livro.Autor;
                atualizarLivro.Status = livro.Status;
                
                _context.Livros.Update(atualizarLivro);
                _context.SaveChanges();
                serviceResponse.Mensagem = "Livro atualizado com sucesso";
                serviceResponse.Dados = atualizarLivro;
                
            }
            catch (Exception ex)
            {
                
                serviceResponse.Sucesso = false;
                serviceResponse.Mensagem = ex.Message;
            }
            return await Task.FromResult(serviceResponse);
        }

        public async Task<ServiceResponse<Livro>> ObterPorId(int id)
        {
            ServiceResponse<Livro> serviceResponse = new ServiceResponse<Livro>();

            try 
            {
                var livro = _context.Livros.Find(id);

                if (livro == null)
                {
                    serviceResponse.Sucesso = false;
                    serviceResponse.Mensagem = "Livro não encontrado";
                    serviceResponse.Dados = null;
                }    

                serviceResponse.Dados = livro;

                return await Task.FromResult(serviceResponse);

            } catch (Exception ex)
            {
                serviceResponse.Sucesso = false;
                serviceResponse.Mensagem = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Livro>>> ObterTodos()
        {
            ServiceResponse<List<Livro>> serviceResponse = new ServiceResponse<List<Livro>>();
            try
            {
                serviceResponse.Dados = _context.Livros.ToList();

                if (serviceResponse.Dados == null)
                {
                    serviceResponse.Sucesso = false;
                    serviceResponse.Mensagem = "Nenhum Livro encontrado";

                    return await Task.FromResult(serviceResponse);
                }
            } catch (Exception ex)
            {
                serviceResponse.Sucesso = false;
                serviceResponse.Mensagem = ex.Message;
            }
            return await Task.FromResult(serviceResponse);
        }

        public async Task<ServiceResponse<Livro>> Remover(int id)
        {
            ServiceResponse<Livro> serviceResponse = new ServiceResponse<Livro>();
            try
            {
                var livro = _context.Livros.Find(id);

                if (livro == null)
                {
                    serviceResponse.Sucesso = false;
                    serviceResponse.Mensagem = "Livro não encontrado";
                    serviceResponse.Dados = null;

                    return await Task.FromResult(serviceResponse);
            }

               _context.Livros.Remove(livro);
                await _context.SaveChangesAsync();
            } catch (Exception ex)
            {
                serviceResponse.Sucesso = false;
                serviceResponse.Mensagem = ex.Message;
            }
            return await Task.FromResult(serviceResponse);
        }

        public async Task<ServiceResponse<List<Livro>>> VerificarConclusao(List<EnumStatusLivro> statusFiltro)
{
    ServiceResponse<List<Livro>> serviceResponse = new ServiceResponse<List<Livro>>();
    try
    {
        // Filtra os livros que possuem o status fornecido na lista de statusFiltro
        serviceResponse.Dados = await _context.Livros.Where(x => statusFiltro.Contains(x.Status)).ToListAsync();

        serviceResponse.Sucesso = true;
        serviceResponse.Mensagem = "Livros filtrados com sucesso";
    }
    catch (Exception ex)
    {
        serviceResponse.Sucesso = false;
        serviceResponse.Mensagem = ex.Message;
        serviceResponse.Dados = null; // Em caso de erro, retorna nulo
    }
    return serviceResponse;
}
         }
    }
