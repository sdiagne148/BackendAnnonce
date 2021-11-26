using BackendAnnonce.Service.Features.AnnonceFeatures.Commands;
using BackendAnnonce.Service.Features.AnnonceFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using BackendAnnonce.Infrastructure.ViewModel;

namespace BackendAnnonce.Controllers
{
   // [Authorize]
    [ApiController]
    [Route("api/v{version:apiVersion}/Annonce")]
    [ApiVersion("1.0")]
    public class AnnonceController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        [HttpPost]
        public async Task<IActionResult> Create(CreateAnnonceCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("Upload/{id}"), DisableRequestSizeLimit]
        [SwaggerOperation(
            Summary = "Uploader un ficheir",
            Description = "Uploader un ficheir avec les informations de l'annonce"
        )]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Upload(int id, [FromForm] AnnonceModel form)
        {
           
            // Validate FormFile
            if (form.AnnonceFile == null || form.AnnonceFile.Length < 1)
            {
                return BadRequest("The uploaded file is empty.");
            }
            
            try
            {
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (form.AnnonceFile.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(form.AnnonceFile.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await form.AnnonceFile.CopyToAsync(stream);
                    }

                    UploadImageAnnonceCommand command = new UploadImageAnnonceCommand();
                    command.Id = id;
                    command.Image = fileName;
                    return Ok(await Mediator.Send(command));
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet("ImageByAnnonceId/{id}")]
        [SwaggerOperation(
            Summary = "Rechercher une image de par son nom",
            Description = "Rechercher une image en passant en paramètre son nom"
        )]
        public async Task<byte[]> GetImageByAnnonceId(int id)
        {
            var annonce=await Mediator.Send(new GetAnnonceByIdQuery { Id = id });
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Images", annonce.Image);

            var img = System.IO.File.ReadAllBytes(filePath);
            return img;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllAnnonceQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetAnnonceByIdQuery { Id = id }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteAnnonceByIdCommand { Id = id }));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateAnnonceCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }
    }
}
