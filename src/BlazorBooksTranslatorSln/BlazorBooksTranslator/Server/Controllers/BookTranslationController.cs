using BlazorBooksTranslator.Server.Services;
using BlazorBooksTranslator.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorBooksTranslator.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookTranslationController : ControllerBase
    {
        private ILogger<BookTranslationController> Logger { get; }
        private PTIBooksTranslationService PTIBooksTranslationService { get; }
        public BookTranslationController(ILogger<BookTranslationController> logger, 
            PTIBooksTranslationService pTIBooksTranslationService)
        {
            this.Logger = logger;
            this.PTIBooksTranslationService = pTIBooksTranslationService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Translate(TranslateModel model, CancellationToken cancellationToken=default)
        {
            await this.PTIBooksTranslationService.TranslateFromUrl(model, cancellationToken);
            return Ok();
        }
    }
}
