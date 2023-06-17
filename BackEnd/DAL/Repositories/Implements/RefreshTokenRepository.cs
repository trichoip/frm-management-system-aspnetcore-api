using DAL.Entities;
using DAL.Infrastructure;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implements
{
    public class RefreshTokenRepository : RepositoryBase<RefreshToken>, IRefreshTokenRepository
    {
        private readonly FRMDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        public RefreshTokenRepository(IDbFactory dbFactory, IUnitOfWork unitOfWork) : base(dbFactory)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbFactory.Init();
        }

        public async Task<bool> Add(RefreshToken refreshToken)
        {
            if (refreshToken != null)
            {
                await _dbContext.AddAsync(refreshToken);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<RefreshToken> FindToken(string refreshToken)
        {
            var result = await _dbSet.Where(t => t.Token == refreshToken).FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> ChangeStatus(RefreshToken refreshToken)
        {
            if (refreshToken != null)
            {
                refreshToken.IsUsed = true;
                refreshToken.IsRevoked = true;
                _dbContext.Update(refreshToken);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task DeleteAll(long userId)
        {
            IEnumerable<RefreshToken> refreshTokens = await _dbSet
                .Where(t => t.UserId == userId)
                .ToListAsync();
            _dbSet.RemoveRange(refreshTokens);
            await _dbContext.SaveChangesAsync();            
        }
    }
}
