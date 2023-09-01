using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProAtividade.Domain.Entities;

namespace ProAtividade.Domain.Interfaces.Repositories
{
    public interface IAtividadeRepo : IGeralRepo
    {
        // trabalharemos com essas 3 assinaturas,
        //a classe que implementar a interface
        //terá que implementar estes métodos.

        // Retorna uma lista de todas as atividades assíncronamente.
        Task<Atividade[]> PegaTodasAsync();

        // Retorna uma atividade por seu ID assíncronamente.
        Task<Atividade> PegaPorIdAsync(int id);

        // Retorna uma atividade por seu título assíncronamente.
        Task<Atividade> PegaPorTituloAsync(string titulo);
    }
}
