using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProAtividade.Domain.Entities;
using ProAtividade.Domain.Interfaces.Repositories;
using ProAtividade.Domain.Interfaces.Services;

namespace ProAtividade.Domain.Services
{
    // Namespace onde a classe AtividadeService está definida
    public class AtividadeService : IAtividadeService // Implementa a interface IAtividadeService
    {
        // Campos privados para armazenar as instâncias das interfaces de repositório
        private readonly IAtividadeRepo _atividadeRepo;


        // Construtor da classe, recebe as instâncias das interfaces por injeção de dependência
        public AtividadeService(IAtividadeRepo atividadeRepo)
        {
            _atividadeRepo = atividadeRepo; // Atribui a instância do repositório de atividade ao campo correspondente
        }



        public async Task<Atividade> AdicionarAtividade(Atividade model)
        {
            // Verifica se já existe uma atividade com o mesmo título
            if (await _atividadeRepo.PegaPorTituloAsync(model.Titulo) != null)
            {
                throw new Exception("Já existe uma atividade com esse título");
            }

            // Verifica se a atividade não existe pelo ID
            if (await _atividadeRepo.PegaPorIdAsync(model.Id) == null)
            {
                // Adiciona a atividade usando o repositório de atividade
                _atividadeRepo.Adicionar(model);

                // Tenta salvar as mudanças no repositório de atividade
                if (await _atividadeRepo.SalvarMudancasAsync())
                {
                    return model; // Retorna a atividade adicionada se as mudanças forem salvas com sucesso
                }
            }

            return null; // Retorna null se a atividade já existir pelo ID ou se ocorrer algum problema ao salvar
        }


        public async Task<Atividade> AtualizarAtividade(Atividade model)
        {
            // Verifica se a atividade já está concluída (tem data de conclusão)
            if (model.DataConclusao != null)
            {
                throw new Exception("Não se pode alterar atividade já concluída");
            }

            // Verifica se a atividade existe pelo ID
            if (await _atividadeRepo.PegaPorIdAsync(model.Id) != null)
            {
                // Atualiza a atividade usando o repositório de atividade
                _atividadeRepo.Atualizar(model);

                // Tenta salvar as mudanças no repositório de atividade
                if (await _atividadeRepo.SalvarMudancasAsync())
                {
                    return model; // Retorna a atividade atualizada se as mudanças forem salvas com sucesso
                }
            }

            return null; // Retorna null se a atividade não existir pelo ID ou se ocorrer algum problema ao salvar
        }


        public async Task<bool> ConcluirAtividade(Atividade model)
        {
            // Verifica se o modelo de atividade não é nulo
            if (model != null)
            {
                model.Concluir(); // Chama o método Concluir na instância de atividade para atualizar a data de conclusão

                // Atualiza a atividade no repositório de atividade
                _atividadeRepo.Atualizar<Atividade>(model);

                // Tenta salvar as mudanças no repositório de atividade
                return await _atividadeRepo.SalvarMudancasAsync(); // Retorna true se as mudanças forem salvas com sucesso, 
                //caso contrário, retorna false
            }

            return false; // Retorna false se o modelo de atividade for nulo
        }



        public async Task<bool> DeletarAtividade(int atividadeId)
        {
            // Busca a atividade pelo ID usando o repositório de atividade
            var atividade = await _atividadeRepo.PegaPorIdAsync(atividadeId);

            // Verifica se a atividade foi encontrada
            if (atividade == null)
            {
                throw new Exception("A atividade que tentou deletar não existe");
            }

            // Deleta a atividade usando o repositório de atividade
            _atividadeRepo.Deletar(atividade);

            // Tenta salvar as mudanças no repositório de atividade
            return await _atividadeRepo.SalvarMudancasAsync(); // Retorna true se as mudanças forem salvas 
            //com sucesso, caso contrário, retorna false
        }



        public async Task<Atividade> PegarAtividadesPorIdAsync(int atividadeId)
        {
            try
            {
                // Busca a atividade pelo ID usando o repositório de atividade
                var atividade = await _atividadeRepo.PegaPorIdAsync(atividadeId);

                // Se a atividade não for encontrada, retorna null
                if (atividade == null)
                {
                    return null;
                }

                return atividade; // Retorna a atividade encontrada
            }
            catch (System.Exception ex)
            {
                // Captura e relança a exceção, propagando-a para cima
                throw new Exception(ex.Message);
            }
        }


        public async Task<Atividade[]> PegarTodasAtividadesAsync()
        {
            try
            {
                // Busca todas as atividades usando o método PegaTodasAsync do repositório de atividade
                var atividades = await _atividadeRepo.PegaTodasAsync();

                // Se não houver atividades encontradas, retorna null
                if (atividades == null)
                {
                    return null;
                }

                return atividades; // Retorna o array de atividades encontradas
            }
            catch (System.Exception ex)
            {
                // Captura e relança a exceção, propagando-a para cima
                throw new Exception(ex.Message);
            }
        }



    }
}
