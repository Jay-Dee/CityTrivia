using Asp.Versioning;
using AutoMapper;
using CityTrivia.DataAccess.Entities;
using CityTrivia.WebApi.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using CitiesTrivia.UseCases.Queries;
using CitiesTrivia.UseCases.Commands;

namespace TriviaTrivia.WebApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("/api/v{version:apiVersion}/Trivias")]
    public class TriviasController : ControllerBase {
        private const int MaxNumberOfEntitiesAllowed = 10;

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TriviasController(IMapper mapper, IMediator mediator) {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mapper));
        }

        [ApiVersion("1.0")]
        [RequiredScope("Countries.Read.All")]
        [HttpGet(Name = nameof(GetTrivias))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TriviaGetModel>>> GetTrivias([FromQuery] string? nameToFilter, int pageNumber = 1, int pageSize = 10) {
            if (pageSize > MaxNumberOfEntitiesAllowed) {
                pageSize = MaxNumberOfEntitiesAllowed;
            }

            var trivias = await _mediator.Send(new GetTriviasQuery(nameToFilter??string.Empty, pageSize * (pageNumber - 1), pageSize));
            return Ok(_mapper.Map<IEnumerable<TriviaGetModel>>(trivias));
        }

        [ApiVersion("1.0")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> CreateTrivia([FromBody]TriviaPostModel triviaToCreate) {
            var triviaToAdd = _mapper.Map<TriviaPostModel, Trivia>(triviaToCreate);
            var TriviaCreatedSuccesfully = await _mediator.Send(new CreateTriviaCommand(triviaToAdd));
            return TriviaCreatedSuccesfully
                ? CreatedAtRoute(nameof(GetTrivias), new { TriviaId = triviaToAdd.Id }, _mapper.Map<TriviaGetModel>(triviaToAdd))
                : StatusCode(StatusCodes.Status500InternalServerError, "Failed to create Trivia");
        }

        [ApiVersion("1.0")]
        [HttpDelete("{TriviaId:int}", Name = nameof(DeleteTrivia))]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteTrivia(int TriviaId) {
            var triviaToDelete = await _mediator.Send(new GetTriviaQuery(TriviaId));
            if (triviaToDelete != null) {
                var triviaDeletedSuccessfully = await _mediator.Send(new DeleteTriviaCommand(triviaToDelete));
                return triviaDeletedSuccessfully
                    ? Accepted(nameof(DeleteTrivia), TriviaId)
                    : StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete Trivia");
            } else {
                return NotFound($"No Trivia exists with Id = {TriviaId}");
            }
        }
    }
}
