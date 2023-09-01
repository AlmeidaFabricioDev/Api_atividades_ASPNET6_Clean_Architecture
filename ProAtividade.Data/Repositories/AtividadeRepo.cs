using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAtividade.Data.Context;
using ProAtividade.Domain.Entities;
using ProAtividade.Domain.Interfaces.Repositories;

namespace ProAtividade.Data.Repositories
{
    public class AtividadeRepo : GeralRepo, IAtividadeRepo
    //AtividadeRepo herda de GeralRepo, e implementa IAtividadeRepo
    {
        private readonly DataContext _context;

        //Porque toda vez que você implementar o AtividadeRepo
        // você vai receber um contexto e esse contexto
        public AtividadeRepo(DataContext context) : base(context)
        {
            _context = context;

        }

        public async Task<Atividade> PegaPorIdAsync(int id)
        {
            //_context Vem do nosso Contexto
            //.Atividades é table do Dbset
            // Criando uma consulta IQueryable para a tabela de atividades no contexto.
            IQueryable<Atividade> query = _context.Atividades;

            // Configurando a consulta:
            // - AsNoTracking(): Indica que os resultados não serão rastreados pelo contexto, melhorando o desempenho para consultas de leitura.
            // - OrderBy(ativ => ativ.Id): Ordena os resultados com base no ID da atividade.
            // - Where(a => a.Id == id): Filtra as atividades com base no ID fornecido.
            query = query.AsNoTracking()
                         .OrderBy(ativ => ativ.Id)
                         .Where(a => a.Id == id);

            // Executa a consulta e retorna a primeira atividade correspondente ou null se não houver nenhuma correspondência.
            return await query.FirstOrDefaultAsync();
        }

        //em PegaPorTituloAsync vamos fazer um pouco diferente, para termos noção das possibilidades
        public async Task<Atividade> PegaPorTituloAsync(string titulo)
        {
            IQueryable<Atividade> query = _context.Atividades;

            query = query.AsNoTracking()
                         .OrderBy(ativ => ativ.Id);
                        

            //Percebe que no método anterior usamos o where, aqui usamos no FirstOrDefault
            return await query.FirstOrDefaultAsync(a => a.Titulo == titulo);
        }

        public async Task<Atividade[]> PegaTodasAsync()
        {
            IQueryable<Atividade> query = _context.Atividades;

            query = query.AsNoTracking()
                         .OrderBy(ativ => ativ.Id);
                        
            //Ao inves de retornarmos FirstOrDefault, usamos ToArrayAsync, 
            //já que retornamos um array com todos
            return await query.ToArrayAsync();
        }


    }
}