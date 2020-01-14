using System.Collections.Generic;
using System.Threading.Tasks;
using TvMaze.Connector.Models.CastMember;
using TvMaze.Connector.Models.Show;


namespace TvMaze.Connector
{
    public interface ITvMazeConnector
    {
        Task<List<Show>> GetShows();
        Task<List<CastMember>> GetCastMembersForShow(int showId);
    }
}