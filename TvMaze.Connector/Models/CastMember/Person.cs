namespace TvMaze.Connector.Models.CastMember
{
    public class Person
    { 
        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public Country Country { get; set; }
        public string Birthday { get; set; }
        public string DeathDay { get; set; }
        public string Gender { get; set; }
        public Image Image { get; set; } 
    }
}