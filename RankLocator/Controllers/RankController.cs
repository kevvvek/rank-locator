using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RankLocator.Models;
using RankLocator.SearchEngines;

namespace RankLocator.Controllers
{
    [ApiController]
    [Route("api/rank")]
    public class RankController : ControllerBase
    {
        ///<summary>
        /// Locate the rank in search engine
        ///</summary>
        [HttpPost("search")]
        public RankSearchResponse Search([FromQuery] RankSearchRequest request)
        {
            // Create the search engine
            var searchEngine = SearchEngineFactory.Create(request.Engine);
            if (searchEngine == null)
            {
                return new RankSearchResponse() { Message = $"Search engine {request.Engine} not supported." };
            }

            // Search the keyword
            var response = searchEngine.Search(new SearchRequest()
            {
                Keyword = request.Keyword,
            });
            if (response == null || !response.IsSuccess)
            {
                return new RankSearchResponse() { Message = $"Search engine error. {response.Message}" };
            }

            // Match the result with user input URL
            var matched = response.Records.Where(a => a.Url.ToUpper().Contains(request.Url.ToUpper())).ToList();

            return new RankSearchResponse()
            {
                IsSuccess = true,
                Ranks = matched.Select(a => a.Rank).ToList()
            };
        }
    }
}