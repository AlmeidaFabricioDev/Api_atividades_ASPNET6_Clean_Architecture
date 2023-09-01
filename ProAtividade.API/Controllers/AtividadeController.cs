using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAtividade.Data.Context;
using ProAtividade.Domain.Entities;
using ProAtividade.Domain.Interfaces.Services;

namespace ProAtividade.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AtividadeController : ControllerBase
    {
        private readonly IAtividadeService _atividadeService;


        public AtividadeController(IAtividadeService atividadeService)
        {
            _atividadeService = atividadeService;

        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                // Chamando o serviço para recuperar todas as atividades.
                var atividades = await _atividadeService.PegarTodasAtividadesAsync();

                // Verificando se não há atividades retornadas.
                if (atividades == null)
                    return NoContent(); // Retorna um resultado indicando que não há conteúdo para mostrar.

                return Ok(atividades); // Retorna um resultado 200 OK com a lista de atividades.
            }
            catch (System.Exception ex)
            {
                // Se ocorrer uma exceção durante o processo, retorna um resultado de erro interno do servidor (HTTP 500)
                // com uma mensagem de erro detalhada.
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar atividades. Erro: {ex.Message}");
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                // Chamando o serviço para recuperar uma atividade pelo seu ID.
                var atividade = await _atividadeService.PegarAtividadesPorIdAsync(id);

                // Verificando se a atividade é nula.
                if (atividade == null)
                    return NoContent(); // Retorna um resultado indicando que não há conteúdo para mostrar.

                return Ok(atividade); // Retorna um resultado 200 OK com a atividade recuperada.
            }
            catch (System.Exception ex)
            {
                // Se ocorrer uma exceção durante o processo, retorna um resultado de erro interno do servidor (HTTP 500)
                // com uma mensagem de erro detalhada, incluindo o ID da atividade que estava sendo buscada.
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar atividade com id: {id}. Erro: {ex.Message}");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post(Atividade model)
        {
            try
            {
                // Tenta adicionar a atividade usando o serviço de atividade
                var atividade = await _atividadeService.AdicionarAtividade(model);

                if (atividade == null)
                {
                    return NoContent(); // Retorna código 204 se a atividade já existir ou ocorrer um problema ao salvar
                }

                return Ok(atividade); // Retorna código 200 OK se a atividade for adicionada com sucesso
            }
            catch (System.Exception ex)
            {
                // Retorna código 500 Internal Server Error e a mensagem de erro se ocorrer uma exceção
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar Adicionar atividades. Erro: {ex.Message}");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Atividade model)
        {
            try
            {
                if (model.Id != id)
                {
                    // Retorna código 409 Conflict se o ID no corpo da requisição não corresponder ao ID no parâmetro da rota
                    return this.StatusCode(StatusCodes.Status409Conflict,
                        "Você está tentando atualizar a atividade errada");
                }

                // Tenta atualizar a atividade usando o serviço de atividade
                var atividade = await _atividadeService.AtualizarAtividade(model);

                if (atividade == null)
                {
                    return NoContent(); // Retorna código 204 se a atividade não for encontrada ou ocorrer um problema ao salvar
                }

                return Ok(atividade); // Retorna código 200 OK se a atividade for atualizada com sucesso
            }
            catch (System.Exception ex)
            {
                // Retorna código 500 Internal Server Error e a mensagem de erro se ocorrer uma exceção
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar Atualizar atividade com id: {id}. Erro: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // Busca a atividade pelo ID usando o serviço de atividade
                var atividade = await _atividadeService.PegarAtividadesPorIdAsync(id);

                if (atividade == null)
                {
                    // Retorna código 409 Conflict se a atividade não for encontrada
                    return this.StatusCode(StatusCodes.Status409Conflict,
                        "Você está tentando deletar a atividade que não existe");
                }

                // Tenta deletar a atividade usando o serviço de atividade
                if (await _atividadeService.DeletarAtividade(id))
                {
                    // Retorna código 200 OK se a atividade for deletada com sucesso
                    return Ok(new { message = "Deletado" });
                }
                else
                {
                    // Retorna código 400 Bad Request se ocorrer um problema ao deletar a atividade
                    return BadRequest("Ocorreu um problema não específico ao tentar deletar a atividade");
                }
            }
            catch (System.Exception ex)
            {
                // Retorna código 500 Internal Server Error e a mensagem de erro se ocorrer uma exceção
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar deletar atividade com id: {id}. Erro: {ex.Message}");
            }
        }

    }
}