using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TvMaze.Connector;
using TvMazeScraper.Entities;
using TvMazeScraper.Models;

namespace TvMazeScraper.Services
{
    public class TvMazeService : ITvMazeService
    {
        private readonly ITvMazeConnector _tvMazeConnector;
        private readonly IMapper _mapper;
        private readonly ApiDbContext _dbContext;
        private ITvMazeService _tvMazeServiceImplementation;

        public TvMazeService(ITvMazeConnector tvMazeConnector, IMapper mapper, ApiDbContext dbContext)
        {
            _tvMazeConnector = tvMazeConnector;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<List<Models.Show>> Get()
        {
            await Update();  
            
            var result = GetTvShowsWithCast();

            if (result.Count < 1)
            {
                result = GetTvShowsWithCast();
            }
            
            return result;
        }

        private List<Models.Show> GetTvShowsWithCast()
        {
            var dbShows =  _dbContext.Shows.Include(show => show.Cast).ToList();
            var result = _mapper.Map<List<Models.Show>>(dbShows);
            return result;
        }
        public async Task Update()
        {
            var scrapedShows = await _tvMazeConnector.GetShows();
            var showList = _mapper.Map<List<Models.Show>>(scrapedShows);

            var dbShowList = await SaveShows(showList);

            foreach (var show in dbShowList)
            {
                var castMembers = await GetTranslatedCastMembersForShow(show.Id);
                await SaveCastMembers(castMembers, show.Id);
            }
        }

        private async Task<List<IEnumerable<Models.Show>>> AddCastToShow(List<IEnumerable<Models.Show>> showsList)
        {
            foreach (var shows in showsList)
            {
                shows.Select(async s => s.Cast.AddRange(
                    await GetTranslatedCastMembersForShow(s.Id))
                );
            }

            return showsList;
        }

        private async Task<List<CastMember>> GetTranslatedCastMembersForShow(int showId)
        {
            var scrapedCastMembersForShow = await _tvMazeConnector.GetCastMembersForShow(showId);
            var castMemberList = _mapper.Map<List<CastMember>>(scrapedCastMembersForShow);
            return castMemberList;
        }

        private async Task<List<ShowEntity>> SaveShows(List<Models.Show> shows)
        {
            var dbShowList = new List<ShowEntity>();
            foreach (var show in shows)
            {
                var dbShow = await _dbContext.Shows.FirstOrDefaultAsync(s => s.ShowId == show.Id);
                if (dbShow != null)
                {
                    UpdateShow(dbShow, show);
                }
                else
                {
                    var showToAdd = new ShowEntity();
                    showToAdd.ShowId = show.Id;
                    showToAdd.Name = show.Name;
                    showToAdd.CreatedAt = DateTime.UtcNow;
                    await _dbContext.AddAsync(showToAdd);
                }
                await _dbContext.SaveChangesAsync();
                
                dbShowList.Add(dbShow);
            }

            return dbShowList;
        }

        private async Task SaveCastMembers(List<CastMember> castMembers, int showId)
        {
            foreach (var castMember in castMembers)
            {
                var dbCastMember =
                    await _dbContext.CastMembers.FirstOrDefaultAsync(c => c.CastMemberId == castMember.Id);
                if (dbCastMember != null)
                { 
                    UpdateCastMember(dbCastMember, castMember);
                }
                else
                {
                    var castMemberToAdd = new CastMemberEntity();
                    castMemberToAdd.CastMemberId = castMember.Id;
                    castMemberToAdd.ShowEntityId = showId;
                    castMemberToAdd.Name = castMember.Name;
                    castMemberToAdd.Birthday = castMember.Birthday;
                    castMemberToAdd.CreatedAt = DateTime.UtcNow;
                    await _dbContext.AddAsync(castMemberToAdd);
                }
                await _dbContext.SaveChangesAsync();
            }
        }
        
        private void UpdateCastMember(CastMemberEntity dbCastMemberEntity, CastMember castMember)
        {
            dbCastMemberEntity.Birthday = castMember.Birthday;
            dbCastMemberEntity.Name = castMember.Name;
            dbCastMemberEntity.ModifiedAt = DateTime.UtcNow;
            _dbContext.Update(dbCastMemberEntity);
        }

        private void UpdateShow(ShowEntity dbShow, Models.Show show)
        {
            dbShow.Name = show.Name;
            dbShow.ModifiedAt = DateTime.UtcNow;
            _dbContext.Update(dbShow);
        }
        
    }
}