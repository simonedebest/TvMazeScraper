using System.Collections.Generic;
using System.Threading.Tasks;
using TvMazeScraper.Pagination;
using Show = TvMazeScraper.Models.Show;

namespace TvMazeScraper.Services
{
    public interface ITvMazeService
    {
        Task<List<Show>> GetAsync(PaginationParameters paginationParameters);
    }
}