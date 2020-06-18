using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RefreshTokenDemo.Models
{
    public class AuthenticationRepository : IDisposable
    {
        UsersDBEntities context = new UsersDBEntities();
        public void Dispose()
        {
            context.Dispose();
        }


        // Add the Refresh Token
        public async Task<bool> AddRefreshToken(RefreshToken token)
        {
            var existingToken = context.RefreshTokens.FirstOrDefault(r => r.UserName == token.UserName && r.ClientID == token.ClientID);
            if(existingToken != null)
            {
                var result = await RemoveRefreshToken(existingToken);
            }
            context.RefreshTokens.Add(token);
            return await context.SaveChangesAsync() > 0 ? true: false;
        }

        // Remove the Refresh Token by Id
        public async Task<bool> RemoveRefreshTokenByID(string refreshTokenId)
        {
            var refreshToken = await context.RefreshTokens.FindAsync(refreshTokenId);
            if(refreshToken != null)
            {
                context.RefreshTokens.Remove(refreshToken);
                return await context.SaveChangesAsync() > 0;
            }
            return false;
        }

        // Remove the Refresh Token
        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            context.RefreshTokens.Remove(refreshToken);
            return await context.SaveChangesAsync() > 0;
        }

        // Find the Refresh Token By ID
        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = await context.RefreshTokens.FindAsync(refreshTokenId);
            return refreshToken;
        }

        // Get All ReFresh Tokens
        public List<RefreshToken> GetAllRefreshTokens()
        {
            return context.RefreshTokens.ToList();
        }
    }
}