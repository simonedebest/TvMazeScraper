using System.Collections.Generic;
using System.Threading.Tasks;
using TvMazeScraper.Models;
using TvMazeScraper.Pagination;


namespace TvMazeScraper.Services
{
    public interface ITvMazeService
    {
        Task<List<Show>> GetAsync(PaginationParameters paginationParameters);
    }
}