using Azure.Identity;
using LibMember.Controllers;
using Microsoft.IdentityModel.Tokens;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using LibMember.Model;

namespace LibMember.Repository
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {

        private readonly IMemberRepository _memberRepo;
        private readonly string key;

        public JwtAuthenticationManager(
            IMemberRepository memberRepository,
            IConfiguration configuration
            )
        {
            //_connectionString = configuration.GetConnectionString("DefaultConnection");
            _memberRepo =memberRepository;
            key = configuration.GetSection("JwtSettings:key").ToString();
        }
        public string authenticateAsync(string userame, string password)
        {
            
            var userCred = _memberRepo.GetAllUserNameAsync().GetAwaiter().GetResult();
            Console.WriteLine(userCred.ToString());
            foreach (var user in userCred)
            {
                if (user.UserName == userame && user.Password == password)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenKey = Encoding.ASCII.GetBytes(key);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                           new Claim(ClaimTypes.Name, userame)
                        }),
                        Expires = DateTime.UtcNow.AddHours(1),
                        SigningCredentials = new SigningCredentials(
                            new SymmetricSecurityKey(tokenKey),
                        SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    return tokenHandler.WriteToken(token);
                }

            }
            return null;
        }
    }
}

