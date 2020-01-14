using System;

namespace TvMazeScraper.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}