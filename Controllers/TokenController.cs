using System;
using System.Linq;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using DaData.Models;

namespace DaData
{
    [Route("/token")]
    public class TokenController : Controller
    {       
        private readonly DaDataContext _context;
        public TokenController(DaDataContext context)
        {
            _context = context;
        }
        
        /// <summary>
        /// Получение списка всех токенов в БД
        /// </summary>
        /// <remarks>
        /// Sample  request:
        ///     GET /token HTTP/1.1
        ///     Host: localhost:5000
        ///     Content-Type: application/json
        ///     Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoic3pkIiwibmJmIjoiMTUyODAzOTExOSIsImV4cCI6IjE1MjgxMjU1MTkifQ.6TFDPCafEv4IjYFyUoWd3BDzEfd-zvZPPUh5Xa1mXgk
        ///     Cache-Control: no-cache
        ///     Postman-Token: 9b314263-e694-42a1-af28-0fbd43d2fe2f
        /// </remarks>
        /// <returns>Список токенов</returns>
        [HttpGet, Authorize]
        public List<user> GetAll(){
            return _context.users.ToList();
        } 

        /// <summary>
        /// Получение токена для авторизации
        /// </summary>
        /// <returns>Ваш токен</returns>
        /// <response code="400">Bad Request</response>
        [HttpPost]
        public IActionResult Create(string username, string password)
        {
            if (IsValidUserAndPasswordCombination(username, password))
                return new ObjectResult(GenerateToken(username));
            return BadRequest();
        }

        private bool IsValidUserAndPasswordCombination(string username, string password)
        {
            return !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password);
        }

        private string GenerateToken(string username)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
            };

            var new_token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes("the secret that needs to be at least 16 characeters long for HmacSha256")), 
                                             SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));

            var temp = new JwtSecurityTokenHandler();
            var temp_user = new user {id = 0, token = temp.WriteToken(new_token)};

            _context.users.Add(temp_user);
            _context.SaveChanges();
            return new JwtSecurityTokenHandler().WriteToken(new_token);
        }
    }
}