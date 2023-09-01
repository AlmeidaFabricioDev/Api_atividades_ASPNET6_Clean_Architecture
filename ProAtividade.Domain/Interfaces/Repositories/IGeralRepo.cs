using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProAtividade.Domain.Interfaces.Repositories
{
    public interface IGeralRepo
    {
        //Aqui os metodos criados não será assincronos e será genericos
        void Adicionar<T>(T entity) where T : class; //Generico, onde tiver t será um class
        void Atualizar<T>(T entity) where T : class;
        void Deletar<T>(T entity) where T : class;
        void DeletarVarias<T>(T[] entity) where T : class;

        Task<bool> SalvarMudancasAsync(); // Assinatura para salvar mudanças
    }
}