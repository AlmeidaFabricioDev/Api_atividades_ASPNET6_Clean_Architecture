using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProAtividade.Domain.Entities;

namespace ProAtividade.Domain.Interfaces.Services
{
    public interface IAtividadeService
    {
        // Adiciona uma nova atividade assíncronamente.
        Task<Atividade> AdicionarAtividade(Atividade model);

        // Atualiza uma atividade existente assíncronamente.
        Task<Atividade> AtualizarAtividade(Atividade model);

        // Deleta uma atividade por ID assíncronamente e retorna se foi deletada com sucesso.
        Task<bool> DeletarAtividade(int atividadeId);

        // Marca uma atividade como concluída assíncronamente e retorna se foi concluída com sucesso.
        Task<bool> ConcluirAtividade(Atividade model);

        // Pega todas as atividades assíncronamente.
        Task<Atividade[]> PegarTodasAtividadesAsync();

        // Pega uma atividade por ID assíncronamente.
        Task<Atividade> PegarAtividadesPorIdAsync(int atividadeId);
    }
}
