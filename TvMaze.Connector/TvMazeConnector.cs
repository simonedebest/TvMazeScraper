using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        private readonly TvMazeOptions _options;

        public TvMazeConnector(HttpClient httpClient, IOptions<TvMazeOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }

        public async Task<List<Show>> GetShowsAsync()
        {
            var pageNumber = 0;
            var showsPerPage = new List<Show>();
            while (pageNumber <= _options.MaxPageAmount)
            {
                var shows = await ScrapeShowsPageAsync(pageNumber);
                showsPerPage.AddRange(shows);
                
                pageNumber += 1;
            }
            return showsPerPage;
        }

        public async Task<List<CastMember>> GetCastMembersForShowAsync(int showId)
        {
            var castMembers = default(List<CastMember>);
            
            var url = $"{_options.BaseUri}/shows/{showId}/cast";
            var request = await _httpClient.GetAsync(url);
            var scrapedCastMembers = request.StatusCode == HttpStatusCode.NotFound ? null : await request.Content.ReadAsStringAsync();
            
            if (scrapedCastMembers != null)
            {
                castMembers = JsonConvert.DeserializeObject<List<CastMember>>(scrapedCastMembers)
                    .OrderByDescending(c => c.Person.Birthday).ToList();
            }

            return castMembers;
        }
        
        private async Task<IEnumerable<Show>> ScrapeShowsPageAsync(int pageNumber)
        {
            var url = $"{_options.BaseUri}/shows?page={pageNumber}";
            var request = await _httpClient.GetAsync(url);
            var scrapedShows = request.IsSuccessStatusCode ? await request.Content.ReadAsStringAsync() : null;

            var shows = JsonConvert.DeserializeObject<List<Show>>(scrapedShows);
            
            return shows.Take(_options.MaxAmountOfShows);
        }
    }
}