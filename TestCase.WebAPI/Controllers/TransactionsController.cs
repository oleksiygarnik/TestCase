using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestCase.Application.Transactions.Commands.ExportTransactions;
using TestCase.Application.Transactions.Commands.ImportTransactions;
using TestCase.Application.Transactions.Queries;
using TestCase.Domain;
using TestCase.WebAPI.Controllers.Abstract;

namespace TestCase.WebAPI.Controllers
{
    [Route("api/transactions")]
    public class TransactionsController : MediatorController
    {
        public TransactionsController(IMediator mediator) : base(mediator)
        {

        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [Produces(typeof(IEnumerable<Transaction>))]
        public Task<IActionResult> GetTransactions(TransactionsQuery query) => ExecuteQuery(query);

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [Produces(typeof(string))]
        public async Task<IActionResult> ImportTransactions()
        {
            var command = new ImportTransactionsCommand()
            {
                File = Request.Form.Files[0]
            };

            var transactions = await _mediator.Send(command);

            return CreatedAtAction("Import Transactions", transactions);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [Produces(typeof(string))]
        public async Task<IActionResult> ExportTransactions(ExportTransactionsCommand command)
        {
            if (command is null)
                throw new ArgumentNullException(nameof(command));

            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "authors.xlsx";

            var workbook = await _mediator.Send(command);

            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                var content = stream.ToArray();
                return File(content, contentType, fileName);
            }
        }



        public async Task<IActionResult> Download(string filename)
        {
            if (filename == null)
                return Content("filename not present");

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},  
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
        //    [HttpPost, DisableRequestSizeLimit]
        //    public IActionResult Upload()
        //    {
        //        try
        //        {
        //            var file = Request.Form.Files[0];
        //            var folderName = Path.Combine("Resources", "Images");
        //            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

        //            if (file.Length > 0)
        //            {
        //                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        //                var fullPath = Path.Combine(pathToSave, fileName);
        //                var dbPath = Path.Combine(folderName, fileName);

        //                using (var stream = new FileStream(fullPath, FileMode.Create))
        //                {
        //                    file.CopyTo(stream);
        //                }

        //                return Ok(new { dbPath });
        //            }
        //            else
        //            {
        //                return BadRequest();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            return StatusCode(500, $"Internal server error: {ex}");
        //        }
        //    }
    }
}
