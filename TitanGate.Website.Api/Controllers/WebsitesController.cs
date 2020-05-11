using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TitanGate.Website.Api.Contracts.Request;
using TitanGate.Website.Api.Contracts.Requests;
using TitanGate.Website.Api.Handlers.Contracts;

namespace TitanGate.Website.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WebsitesController : ControllerBase
    {
        private readonly IPaginationWebsiteHandler _paginationWebsiteHandler;
        private readonly IWebsiteDeleteHandler _websiteByIdHandler;
        private readonly IWebsiteCreateOrUpdateHandler _websiteHandler;
        private readonly IWebsiteGetHandler _websiteGetHandler;

        public WebsitesController(IPaginationWebsiteHandler paginationWebsiteHandler,
                                  IWebsiteDeleteHandler websiteByIdHandler,
                                  IWebsiteCreateOrUpdateHandler websiteHandler,
                                  IWebsiteGetHandler websiteGetHandler)
        {
            _paginationWebsiteHandler = paginationWebsiteHandler;
            _websiteByIdHandler = websiteByIdHandler;
            _websiteHandler = websiteHandler;
            _websiteGetHandler = websiteGetHandler;
        }

        [HttpGet]
        public Task<IActionResult> Get(int id) => id == 0 ? GetAllWebsites() : GetWebsiteById(id);

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateWebsite([FromBody] WebsiteRequest command)
        {
            await _websiteHandler.HandleCreateRequest(command);

            return Ok();
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateWebsite([FromQuery] int id, [FromBody] WebsiteRequest command)
        {
            var result =  await _websiteHandler.HandleUpdateRequest(id,command);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Fail);
            }

            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteWebsite([FromQuery] int id)
        {
            var result = await _websiteByIdHandler.HandleDeleteRequest(id);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Fail);
            }

            return Ok();
        }

        [HttpGet]
        [Route("paged")]
        public async Task<IActionResult> GetPaginationWebsites([FromBody] PaginationWebsiteRequest command)
        {
            var result = await _paginationWebsiteHandler.HandleRequest(command);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        private async Task<IActionResult> GetAllWebsites()
        {
            var result = await _websiteGetHandler.HandleGetAllRequest();
            return Ok(result.Success);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private async Task<IActionResult> GetWebsiteById([FromQuery] int id)
        {
            var result = await _websiteGetHandler.HandleGetRequest(id);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Fail);
            }

            return Ok(result.Success);
        }
    }
}
