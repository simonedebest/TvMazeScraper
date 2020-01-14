namespace TvMaze.Connector.Models.CastMember
{
    public class CastMember
    {
        public Person Person { get; set; } 
        public Character Character { get; set; } 
        public bool Self { get; set; } 
        public bool Voice { get; set; }
    }
}