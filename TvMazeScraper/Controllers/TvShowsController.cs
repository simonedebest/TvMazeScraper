using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TvMazeScraper.Models;
using TvMazeScraper.Pagination;
using TvMazeScraper.Services;

namespace TvMazeScraper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TvShowsController : ControllerBase
    {
        private readonly ITvMazeService _tvMazeService;

        public TvShowsController(ITvMazeService tvMazeService)
        {
            _tvMazeService = tvMazeService;
        }

        [HttpGet]
        public async Task<List<Models.Show>> GetShowsAsync([FromQuery] PaginationParameters paginationParameters)
        {
            var showsWithCast = await _tvMazeService.GetAsync(paginationParameters);
            return showsWithCast;
        }
    }
}