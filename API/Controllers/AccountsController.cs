using API.Base;
using API.Models;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {

        private readonly AccountRepository accountRepository;
        public IConfiguration _configuration;

        public AccountsController(AccountRepository accountRepository, IConfiguration configuration) : base(accountRepository)
        {
            this.accountRepository = accountRepository;
            this._configuration = configuration;
        }


        [HttpPost("register")]
        public ActionResult Register(RegisterVM registerVM)
        {
            try
            {
                var dataRegister = accountRepository.Register(registerVM);
                if (dataRegister == 0)
                {
                    return BadRequest("Gak masuk");
                }
                else if (dataRegister > 0)
                {
                    return Ok("Akun berhasil ditambahkan");
                }
                else if (dataRegister == -1)
                {
                    return BadRequest("Email sudah terdaftar");
                }
                else if (dataRegister == -2)
                {
                    return BadRequest("HP sudah terdaftar");
                }
                else if (dataRegister == -3)
                {
                    return BadRequest("Email dan HP sudah terdaftar");
                }
                else
                {
                    return BadRequest("Gagal melakukan Register");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "REGISTER Server Error");
            }
        }

        [Authorize (Roles = "Director")]
        [HttpGet("TestJWT")]
        public ActionResult TestJWT()
        {
            return Ok("Test JWT Berhasil");        
        }


        [HttpPost("login")]
        public ActionResult Login(LoginVM loginVM)
        {
            try
            {
                var dataLogin = accountRepository.Login(loginVM);
                if (dataLogin == 1)
                {
                    var User = accountRepository.GetAccountRole(loginVM.Email);

                    //PAYLOAD
                    var claims = new List<Claim>
                {
                    new Claim("Email",User.Email)
                };

                    foreach (string r in User.Roles)
                    {
                        claims.Add(new Claim("roles", r));
                    }

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn
                        );
                    var idtoken = new JwtSecurityTokenHandler().WriteToken(token);
                    claims.Add(new Claim("TokenSecurity", idtoken.ToString()));

                    return Ok(new { status = HttpStatusCode.OK, idtoken, message = "Login Succes!!" });
                }
                else if (dataLogin == 2 )
                {
                    return NotFound(new { status = HttpStatusCode.NotFound,idtoken = (object)null,message = "Password Salah" });
                }
                else
                {
                    return NotFound(new { status = HttpStatusCode.NotFound, idtoken = (object)null, message = "Akun tidak ditemukan" });
                }
                


            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Login Server Error");
            }
        }

        [HttpPost("ForgotPassword")]
        public ActionResult ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            try
            {
                var dataForgotPassword = accountRepository.ForgotPassword(forgotPasswordVM);
                if (dataForgotPassword > 0)
                {
                    return Ok("Silahkan cek emailmu ya");

                }
                else
                {
                    return BadRequest("Email yang kamu masukan tidak terdaftar");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Forgot Password Server Error");
            }
        }
    
        [HttpPost("ChangePassword")]
        public ActionResult ChangePassword(ChangePasswordVM changePasswordVM)
        {
            try
            {
                var dataChangePassword = accountRepository.ChangePassword(changePasswordVM);
                if (dataChangePassword > 0 )
                {
                    return Ok("Berhasil merubah passwordmu");
                }
                else if (dataChangePassword == -1)
                {
                    return BadRequest("Password baru dan konfirmasimu tidak sama");
                }
                else if(dataChangePassword == -2)
                {
                    return BadRequest("Kode OTP sudah kadaluarsa");
                }
                else if (dataChangePassword == -3)
                {
                    return BadRequest("Kode OTP sudah digunakan");
                }
                else
                {
                    return BadRequest("Kode OTP tidak sama");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Change Passwrod Server Error");
            }
        }
    
        
    }
}
