using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.ContentModerator;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using webapi.event_.Domains;
using webapi.event_.Interfaces;
using webapi.event_.Repositories;

namespace webapi.event_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ComentariosEventoController : ControllerBase
    {
        ComentariosEventoRepository _comentariosEventoRepository = new ComentariosEventoRepository();

        private readonly ContentModeratorClient _contentModeratorClient;

        /// <summary>
        /// Construtor que recebe os dados necessários para acesso ao serviço extreno
        /// </summary>
        /// <param name="contentModeratorClient">objeto do tipoContentModerator</param>
        public ComentariosEventoController(ContentModeratorClient contentModeratorClient)
        {
            _contentModeratorClient= contentModeratorClient;
        }

        [HttpPost("ComentarioIA")]
        public async Task<IActionResult> PostIA(ComentariosEvento comentario)
        {
            try
            {
                if (comentario.Descricao.IsNullOrEmpty())
                {
                   return BadRequest("A descrição do mentário não pode estar vazio ou nulo!");
                }
                
                using var stream = new MemoryStream(Encoding.UTF8.GetBytes(comentario.Descricao));

                var moderationResult = await _contentModeratorClient.TextModeration
                    .ScreenTextAsync("text/plain", stream, "por", false, false, null, true);

                if(moderationResult.Terms != null)
                {
                    comentario.Exibe = false;

                    _comentariosEventoRepository.Cadastrar(comentario);
                }
                else
                {
                    comentario.Exibe= true;

                    _comentariosEventoRepository.Cadastrar(comentario) ;
                }

                return StatusCode(201, comentario);
            }
            catch(Exception e) 
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_comentariosEventoRepository.Listar());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("ListarSomenteExibe/{id}")]
        public IActionResult GetShow(Guid id)
        {
            try
            {
                return Ok(_comentariosEventoRepository.ListarSomenteExibe(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("Evento/{id}")]
        public IActionResult GetComentariosPorEvento(Guid id)
        {
            try
            {
                var comentariosEvento = _comentariosEventoRepository.ListarPeloIdEvento(id);

                if (comentariosEvento == null || comentariosEvento.Count == 0)
                {
                    return NotFound("Nenhum comentário encontrado para o evento com o ID fornecido.");
                }

                return Ok(comentariosEvento);
            }
            catch (Exception ex)
            {
                // Log de erro, retorno de status 500 ou outra lógica de tratamento de erro, conforme necessário.
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet("BuscarPorIdUsuario/{id}")]
        public IActionResult GetByIdUser(Guid id)
        {
            try
            {
                return Ok(_comentariosEventoRepository.BuscarPorIdUsuario(id)); 
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult Post(ComentariosEvento novoComentario)
        {
            try
            {
                _comentariosEventoRepository.Cadastrar(novoComentario);

                return StatusCode(201, novoComentario);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _comentariosEventoRepository.Deletar(id);

                return NoContent();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
