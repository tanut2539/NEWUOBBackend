
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UOB.PVD.Helper;
using UOB.PVD.Model;
using UOB.PVD.Repository.Account;

namespace UOB.PVD.API.Controllers.CMS
{
    public class AccountController : RootController
    {
        private readonly IConfiguration _configuration;
        string connectionString = "";
        public AccountController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            _configuration = configuration;
        }

        private account _account;
        private account account
        {
            set
            {
                _account = value;
            }
            get
            {
                if (_account == null)
                {
                    _account = new account();
                }
                return _account;
            }
        }

        [HttpPost]
        [Route("authentication")]
        public ActionResult<Response<LoginResponseDTO>> Post(LoginDTO param)
        {
            var response = new Response<LoginResponseDTO>();
            try
            {
                DataRow user = account.Login(connectionString, param.username, param.password);
                if (user is not null)
                {
                    var User = HelperFunctions.ToObject<AccountModel>(user);  
                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, User.user_seq),
                        new Claim("username", User.username),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };
                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("StrONGKAutHENTICATIONKEy"));
                    var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        expires: DateTime.Now.AddHours(12),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));
                    response.Data.token = new JwtSecurityTokenHandler().WriteToken(token);
                    response.Data.expire_time = token.ValidTo;
                    response.Data.name = $"{User.user_fname} {User.user_lname}";
                    return Ok(response);
                }
                else
                {
                    response.result = ConstantData.Fail;
                    response.message = "ชื่อผู้ใช้งาน หรือรหัสผ่านไม่ถูกต้อง";
                    return response;
                }
            }
            catch (Exception e)
            {
                response.result = ConstantData.Fail;
                response.message = e.Message;
                return response;
            }
        }

        [HttpGet]
        [Authorize]
        [Route("getall")]
        public IActionResult getall()
        {
            var email = User.FindFirst("Create")?.Value;
            return Ok(new
            {
                token = email,
                expiration = User.Identity.Name
            }); ;
        }
    }
}
