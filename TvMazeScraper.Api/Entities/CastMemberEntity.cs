namespace TvMazeScraper.Api.Entities
{
    public class CastMemberEntity : BaseEntity
    {
        public int CastMemberId { get; set; }
        public string Name { get; set; }
        public string? Birthday { get; set; }
        public int ShowEntityId { get; set; }
    }
}