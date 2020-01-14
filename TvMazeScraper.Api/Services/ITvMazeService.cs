using System.Collections.Generic;
using System.Threading.Tasks;
using TvMazeScraper.Api.Models;
using TvMazeScraper.Api.Pagination;


namespace TvMazeScraper.Api.Services
{
    public interface ITvMazeService
    {
        Task<List<Show>> GetAsync(PaginationParameters paginationParameters);
    }
}