using System.Collections.Generic;

namespace TvMazeScraper.Api.Models
{
    public class Show
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CastMember> Cast { get; set; }
    }
}