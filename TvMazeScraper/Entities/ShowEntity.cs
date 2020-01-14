using System.Collections.Generic;

namespace TvMazeScraper.Entities
{
    public class ShowEntity : BaseEntity
    {
        public int ShowId { get; set; }
        public string Name { get; set; }
        public List<CastMemberEntity> Cast { get; set; }
    }
}