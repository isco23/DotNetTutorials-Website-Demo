﻿using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RefreshTokenDemo.Models
{
    public class RefreshTokenProvider : IAuthenticationTokenProvider
    {
        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var clientid = context.Ticket.Properties.Dictionary["client_id"];
            if (string.IsNullOrEmpty(clientid))
            {
                return;
            }

            var refreshTokenId = Guid.NewGuid().ToString("n");
            using(AuthenticationRepository _repo = new AuthenticationRepository())
            {
                var refreshTokenLifeTime = context.OwinContext.Get<string>("ta:clientRefreshTokenLifeTime");

                var token = new RefreshToken()
                {
                    ID = Helper.GetHash(refreshTokenId),
                    ClientID = clientid,
                    UserName = context.Ticket.Identity.Name,
                    IssuedTime = DateTime.UtcNow,
                    ExpiredTime = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeTime))
                };
                context.Ticket.Properties.IssuedUtc = token.IssuedTime;
                context.Ticket.Properties.ExpiresUtc = token.ExpiredTime;
                token.ProtectedTicket = context.SerializeTicket();
                var result = await _repo.AddRefreshToken(token);
                if (result)
                {
                    context.SetToken(refreshTokenId);
                }
            }
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>("ta:clientAllowedOrigin");
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });
            string hashedTokenId = Helper.GetHash(context.Token);
            using (AuthenticationRepository _repo = new AuthenticationRepository())
            {
                var refreshToken = await _repo.FindRefreshToken(hashedTokenId);
                if(refreshToken != null)
                {
                    context.DeserializeTicket(refreshToken.ProtectedTicket);
                    var result = await _repo.RemoveRefreshTokenByID(hashedTokenId);
                }
            }
        }
    }
}