using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TvMaze.Connector.Configuration;
using TvMaze.Connector.Models.CastMember;
using TvMaze.Connector.Models.Show;
namespace TvMaze.Connector
{
    public class TvMazeConnector : ITvMazeConnector
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<TvMazeOptions> _options;

        public TvMazeConnector(HttpClient httpClient, IOptions<TvMazeOptions> options)
        {
            _httpClient = httpClient;
            _options = options;
        }

        public async Task<List<Show>> GetShowsAsync()
        {
            var pageNumber = 0;
            var showsPerPage = new List<Show>();
            while (pageNumber <= _options.Value.MaxPageAmount)
            {
                var shows = await ScrapeShowsPageAsync(pageNumber);
                showsPerPage.AddRange(shows);
                
                pageNumber += 1;
            }
            return showsPerPage;
        }

        public async Task<List<CastMember>> GetCastMembersForShowAsync(int showId)
        {
            var url = $"{_options.Value.BaseUri}/shows/{showId}/cast";
            var request = await _httpClient.GetAsync(url);
            var scrapedCastMembers = request.IsSuccessStatusCode ? await request.Content.ReadAsStringAsync() : null;
            
            var castMembers = JsonConvert.DeserializeObject<List<CastMember>>(scrapedCastMembers);
            
            return castMembers
                .OrderByDescending(c => c.Person.Birthday).ToList();
        }
        
        private async Task<IEnumerable<Show>> ScrapeShowsPageAsync(int pageNumber)
        {
            var url = $"{_options.Value.BaseUri}/shows?page={pageNumber}";
            var request = await _httpClient.GetAsync(url);
            var scrapedShows = request.IsSuccessStatusCode ? await request.Content.ReadAsStringAsync() : null;

            var shows = JsonConvert.DeserializeObject<List<Show>>(scrapedShows);
            
            return shows.Take(_options.Value.maxAmountOfShows);
        }
    }
}