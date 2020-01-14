using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace TvMazeScraper.Entities
{
    public class CastMemberEntity : BaseEntity
    {
        public int Id { get; set; }
        public int CastMemberId { get; set; }
        public string Name { get; set; }
        public string? Birthday { get; set; }
        public int ShowEntityId { get; set; }
    }
}