using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UOB.PVD.Helper;
using UOB.PVD.Model;
 

namespace UOB.PVD.CMS.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;
 
        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IActionResult> LoginAsync(LoginDTO dto)
        {
            if (!JwtManager.IsEmptyOrInvalid((ConvertTo.String(Request.Cookies["token"]))))
            {
                return RedirectToAction("Index", "Investment");
            }

            try
            {
                            //Send To API
                if (string.IsNullOrEmpty(dto.username) && string.IsNullOrEmpty(dto.password))
                {
                    return View(dto);
                }

                var service = new HTTPService(_configuration["Uri:WebAPI"]);
                service.Path = "authentication";
                var response = service.PostAsync(dto);
                if (response.StatusCode is HttpStatusCode.OK)
                {
                    string contents = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<Response<LoginResponseDTO>>(contents);
                    if (data.result.Equals(ConstantData.Success))
                    {
                        var option = new CookieOptions();
                        option.Expires = DateTime.Now.AddHours(ConvertTo.Int(_configuration["JWT:Expire"]));
                        Response.Cookies.Append("token", data.Data.token, option);
                        Response.Cookies.Append("name", data.Data.name, option);
                        return RedirectToAction("InCountry", "InvestorChoice");
                    }
                    ViewBag.error = data.message;
                }
                else
                {
                    ViewBag.error = $"เกิดข้อผิดพลาด {(int)response.StatusCode} : {response.StatusCode} ";
                }    
                return View(dto);
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
                return View(dto);
            }
        }

        public IActionResult Logout(LoginDTO dto)
        {
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
            return RedirectToAction("Login", "Account");
        }
    }
}
