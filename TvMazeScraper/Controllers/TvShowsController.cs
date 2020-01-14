using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TvMazeScraper.Services;
using Show = TvMazeScraper.Models.Show;

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
        public async Task<List<Show>> GetShowsAsync()
        {
            var showsWithCast = await _tvMazeService.GetAsync();
            return showsWithCast;
        }
    }
}