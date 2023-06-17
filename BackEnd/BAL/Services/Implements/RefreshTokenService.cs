using BAL.Services.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Implements
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<bool> Add(RefreshToken refreshToken)
        {
            return await _refreshTokenRepository.Add(refreshToken);
        }

        public async Task<RefreshToken> FindToken(string refreshToken)
        {
            return await _refreshTokenRepository.FindToken(refreshToken);
        }

        public async Task<bool> ChangeStatus(RefreshToken refreshToken)
        {
            return await _refreshTokenRepository.ChangeStatus(refreshToken);
        }

        public async Task DeleteAll(long userId)
        {
            await _refreshTokenRepository.DeleteAll(userId);
        }
    }
}
