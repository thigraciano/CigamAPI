using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CigamAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefasController : ControllerBase
    {
        private static List<Tarefa> _tarefas = new List<Tarefa>();

        [HttpGet]
        [SwaggerOperation(Summary = "Retorna todas as Tarefas", Tags = new[] { "Tarefas" })]
        public ActionResult<IEnumerable<Tarefa>> GetTarefas()
        {
            return Ok(_tarefas);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Retorna uma Tarefa por ID", Tags = new[] { "Tarefas" })]
        [SwaggerResponse(200, "A tarefa com ID fornecido", typeof(Tarefa))]
        [SwaggerResponse(404, "Tarefa não encontrado")]
        public ActionResult<Tarefa> GetTarefas(int id)
        {
            var product = _tarefas.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria uma nova Tarefa", Tags = new[] { "Tarefas" })]
        [SwaggerResponse(201, "A Tarefa foi criada", typeof(Tarefa))]
        public ActionResult<Tarefa> CreateProduct(Tarefa tarefa)
        {
            if (tarefa.Descrição == null)
                return BadRequest("Necessário informar a Descrição!");

            tarefa.Id = _tarefas.Count + 1;
            _tarefas.Add(tarefa);

            return CreatedAtAction(nameof(GetTarefas), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualiza uma tarefa existente por ID", Tags = new[] { "Tarefas" })]
        [SwaggerResponse(200, "Uma Tarefa atualizada", typeof(Tarefa))]
        [SwaggerResponse(404, "Tarefa não encontrado")]
        public ActionResult<Tarefa> AtualizaTarefa(int id, Tarefa tarefa)
        {
            var existeTarefa = _tarefas.FirstOrDefault(p => p.Id == id);

            if (existeTarefa == null)
            {
                return NotFound();
            }
            if (id != tarefa.Id)
            {
                return BadRequest("O Id enviado não corresponde ao Id do recurso existente.");
            }
            existeTarefa.Nome = tarefa.Nome;
            existeTarefa.Descrição = tarefa.Descrição;
            existeTarefa.Status = tarefa.Status;

            return Ok(existeTarefa);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Exclui uma tarefa existente por ID", Tags = new[] { "Tarefas" })]
        [SwaggerResponse(204, "Tarefa excluída com sucesso")]
        [SwaggerResponse(404, "Tarefa não encontrado")]
        public ActionResult DeletarProduto(int id)
        {
            var existeTarefa = _tarefas.FirstOrDefault(p => p.Id == id);

            if (existeTarefa == null)
            {
                return NotFound();
            }

            _tarefas.Remove(existeTarefa);

            return NoContent();
        }
    }

}
