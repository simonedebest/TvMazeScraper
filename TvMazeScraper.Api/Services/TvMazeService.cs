using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TvMaze.Connector;
using TvMazeScraper.Api.Entities;
using TvMazeScraper.Api.Models;
using TvMazeScraper.Api.Pagination;

namespace TvMazeScraper.Api.Services
{
    public class TvMazeService : ITvMazeService
    {
        private readonly ITvMazeConnector _tvMazeConnector;
        private readonly IMapper _mapper;
        private readonly ApiDbContext _dbContext;

        public TvMazeService(ITvMazeConnector tvMazeConnector, IMapper mapper, ApiDbContext dbContext)
        {
            _tvMazeConnector = tvMazeConnector;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<List<Show>> GetAsync(PaginationParameters paginationParameters)
        {
            await UpdateAsync();
            var result =  GetTvShowsWithCast(paginationParameters);
            
            return result;
        }

        private List<Show> GetTvShowsWithCast(PaginationParameters paginationParameters)
        {
            var dbShows =  _dbContext.Shows.Include(show => show.Cast).ToList();
            var shows = _mapper.Map<List<Show>>(dbShows);
            
            var pagedShowsList =
                PagedList<Show>.ToPagedList(shows, paginationParameters.PageNumber, paginationParameters.PageSize);
            
            return pagedShowsList;
        }
        
        public async Task UpdateAsync()
        {
            var scrapedShows = await _tvMazeConnector.GetShowsAsync();
            var showList = _mapper.Map<List<Show>>(scrapedShows);

            var dbShowList = await SaveShowsAsync(showList);

            foreach (var show in dbShowList)
            {
                var castMembers = await GetTranslatedCastMembersForShowAsync(show.Id);
                await SaveCastMembersAsync(castMembers, show.Id);
            }
        }

        private async Task<List<CastMember>> GetTranslatedCastMembersForShowAsync(int showId)
        {
            var scrapedCastMembersForShow = await _tvMazeConnector.GetCastMembersForShowAsync(showId);
            var castMemberList = _mapper.Map<List<CastMember>>(scrapedCastMembersForShow);
            return castMemberList;
        }

        private async Task<List<ShowEntity>> SaveShowsAsync(List<Show> shows)
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
                    dbShow = new ShowEntity();
                    dbShow.ShowId = show.Id;
                    dbShow.Name = show.Name;
                    dbShow.CreatedAt = DateTime.UtcNow;
                    await _dbContext.AddAsync(dbShow);
                }
                dbShowList.Add(dbShow);
            }
            
            await _dbContext.SaveChangesAsync();
            return dbShowList;
        }

        private async Task SaveCastMembersAsync(List<CastMember> castMembers, int showId)
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
                    dbCastMember = new CastMemberEntity();
                    dbCastMember.CastMemberId = castMember.Id;
                    dbCastMember.ShowEntityId = showId;
                    dbCastMember.Name = castMember.Name;
                    dbCastMember.Birthday = castMember.Birthday;
                    dbCastMember.CreatedAt = DateTime.UtcNow;
                    await _dbContext.AddAsync(dbCastMember);
                }
            }
            await _dbContext.SaveChangesAsync();
        }
        
        private void UpdateCastMember(CastMemberEntity dbCastMemberEntity, CastMember castMember)
        {
            dbCastMemberEntity.Birthday = castMember.Birthday;
            dbCastMemberEntity.Name = castMember.Name;
            dbCastMemberEntity.ModifiedAt = DateTime.UtcNow;
            _dbContext.Update(dbCastMemberEntity);
        }

        private void UpdateShow(ShowEntity dbShow, Show show)
        {
            dbShow.Name = show.Name;
            dbShow.ModifiedAt = DateTime.UtcNow;
            _dbContext.Update(dbShow);
        }
    }
}