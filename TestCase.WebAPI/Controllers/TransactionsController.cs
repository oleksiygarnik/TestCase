using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using TestCase.Application.Transactions.Commands.DeleteTransaction;
using TestCase.Application.Transactions.Commands.ImportTransactions;
using TestCase.Application.Transactions.Commands.UpdateTransaction;
using TestCase.Application.Transactions.Queries;
using TestCase.WebAPI.Controllers.Abstract;

namespace TestCase.WebAPI.Controllers
{

    [Route("api/transactions")]
    public class TransactionsController : MediatorController
    {
        public TransactionsController(IMediator mediator) : base(mediator)
        {

        }

        /// <summary>
        /// Gets collection of transactions from database
        /// </summary>
        /// <param name="query"></param>
        /// <returns>Collection of transaction</returns>
        /// <response code="200">Returns collection of transactions from database</response>
        /// <response code="400">If the query is null</response>
        /// <response code="404">If server can't find neccessary resource</response>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [Produces(typeof(IEnumerable<TransactionDto>))]
        public Task<IActionResult> GetTransactions(TransactionsQuery query) => ExecuteQuery(query);

        /// <summary>
        /// Export transactions in .xlsx
        /// </summary>
        /// <param name="query"></param>
        /// <returns>File with extension .xlsx with transactions</returns>
        /// <response code="200">Returns collection of transactions in .xlsx file</response>
        /// <response code="400">If the query is null</response>
        /// <response code="404">If server can't find neccessary resource</response>
        [HttpGet("export")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [Produces(typeof(FileResult))]
        public async Task<IActionResult> ExportTransactions(ExportTransactionsQuery query)
        {
            if (query is null)
                return BadRequest(); 

            var workbook = await _mediator.Send(query);

            if (workbook is null)
                return NotFound();

            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                var content = stream.ToArray();
                return File(content, MIMEType.Types[".xlsx"], "transactions.xlsx");
            }
        }

        /// <summary>
        /// Import transactions
        /// </summary>
        /// <param name="file"></param>
        /// <returns>Dictionary with ids which were added and updated</returns>
        /// <response code="200">Returns dictionary with ids which were added and updated</response>
        /// <response code="400">If the file is null</response>
        /// <response code="404">If server can't find neccessary resource</response>
        [HttpPost("import")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [Produces(typeof(Dictionary<string, List<int>>))]
        public async Task<IActionResult> ImportTransactions(IFormFile file)
        {
            var command = new ImportTransactionsCommand()
            {
                File = file
            };

            var statesIds = await _mediator.Send(command);

            return Ok(statesIds);
        }


        /// <summary>
        /// Delete transaction by id from database
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status code 204 - NoContent</returns>
        /// <response code="204">Returns status code 204 if everything okey</response>
        /// <response code="400">If the command is null</response>
        /// <response code="404">If server can't find neccessary transaction with same id</response>
        [HttpDelete("{Id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteTransaction(DeleteTransactionCommand command)
        {
            if (command is null)
                return BadRequest();

            await _mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Update transaction status in database
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Transaction that was updated with new transaction status</returns>
        /// <response code="200">Returns transaction that was updated with new value</response>
        /// <response code="400">If the command is null</response>
        /// <response code="404">If server can't find neccessary transaction with same id</response>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [Produces(typeof(TransactionDto))]
        public async Task<IActionResult> UpdateTransaction([FromBody]  UpdateTransactionStatusCommand command)
        {
            if (command is null)
                return BadRequest();

            return Json(await _mediator.Send(command));
        }
    }

    public static class MIMEType
    {
        public static Dictionary<string, string> Types =>
            new Dictionary<string, string>()
            {
                [".xlsx"] = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            };
    }
}
