using System;

namespace TvMaze.Connector.Models.Show
{
    public class Show
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Language { get; set; }
        public string[] Genres { get; set; }
        public string Status { get; set; }
        public int Runtime { get; set; }
        public DateTime? Premiered { get; set; }
        public string OfficialSite { get; set; }
        public Rating Rating { get; set; }
        public int Weight { get; set; }
        public Network Network { get; set; }
        public Image Image { get; set; }
        public string Summary { get; set; }
    }
}