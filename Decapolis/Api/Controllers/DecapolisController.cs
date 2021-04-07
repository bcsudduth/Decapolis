using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Application.Cities.Queries.GetCitiesInDecapolis;
using AutoMapper;
using MediatR;

namespace Api.Controllers
{
    [Route("decapolis")]
    public class DecapolisController : ControllerBase
    {
        public string[] GREEK_DECAPOLIS =
        {
            "Abila",
            "Capitolis",
            "Gadara",
            "Gerasa",
            "Hippos",
            "Konata",
            "Pella",
            "Philadelphia",
            "Philoterio",
            "Scythopolis"
        };

        public string[] MOST_POPULATED_US_CITIES =
        {
            "New York",
            "Los Angeles",
            "Chicago",
            "Houston",
            "Phoenix",
            "Philadelphia",
            "San Antonio",
            "San Diego",
            "Dallas",
            "San Jose"
        };

        private IMediator _mediator;
        private IMapper  _mapper;
        
        public DecapolisController(IMediator mediator, IMapper mapper)
        {
            this._mediator = mediator;
            this._mapper = mapper;
        }
        
        
        private async Task<IActionResult> ControllerActionAsync<TRequest, TOutput>(object input, CancellationToken cancellationToken)
        {
            TRequest request = _mapper.Map<TRequest>(input);
            object source = await _mediator.Send((object) request, cancellationToken);
            IActionResult actionResult1;
            switch (source)
            {
                case null:
                    actionResult1 = (IActionResult) NotFound();
                    break;
                case Unit _:
                    actionResult1 = (IActionResult) NoContent();
                    break;
                case IActionResult actionResult2:
                    actionResult1 = actionResult2;
                    break;
                default:
                    actionResult1 = (IActionResult) Ok((object) _mapper.Map<TOutput>(source));
                    break;
            }
            return actionResult1;
        }
        
        
        [HttpGet]
        [Route("ancientgreece")]
        public async Task<ActionResult<string[]>> GetGreekDecapolis()
        {
            return Ok(GREEK_DECAPOLIS);
        }

        [HttpGet]
        [Route("usalargest")]
        public async Task<ActionResult<string[]>> GetUSALargest()
        {
            return Ok(MOST_POPULATED_US_CITIES);
        }

        [HttpGet]
        [Route("membercities")]
        [ProducesResponseType(typeof(IList<CityModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCitiesInDecapolis([FromQuery] GetCitiesInDecapolisQueryModel decapolisType, CancellationToken cancellationToken) =>
            await ControllerActionAsync<GetCitiesInDecapolisQuery, IList<CityModel>>(decapolisType, cancellationToken);

    }
    

}