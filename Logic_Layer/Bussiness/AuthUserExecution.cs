using Data_Access.Data.Entities;
using Logic_Layer.DTO;
using Logic_Layer.Helpers;
using Logic_Layer.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Bussiness
{
    public class AuthUserExecution
    {
        private readonly AppSettings appSetings;
        public AuthUserExecution(AppSettings appSetings)
        {
            this.appSetings = appSetings;
        }
        
        public static async Task<bool> Insert_New_AuthUser(IAuthUser AuthUserRepository,
                                                           IStudent studentRepository,
                                                           CreateAuthUserDTO model)
        {
            try
            {
                var student = await studentRepository.GetByIdAsync(model.StudentId);

                if (student != null)
                {
                    await AuthUserRepository.CreateAsync(new AuthUser
                    {
                        User = model.User,
                        Password = model.Password,
                        StudentId = model.StudentId
                    });
                    return true;
                }                
            }
            catch (Exception ex)
            {
                Log.Instance.Add(string.Format("Logic_Layer.Bussiness.AuthUserExecution.Insert_New_AuthUser {0} {1}",
                                 ResponseStrings.Error, ex.Message));
            }
            return false;
        }

        public  async Task<LoggedUSerResponse> Validate_User(IAuthUser AuthUserRepository,
                                         string encription,
                                         LoginAuthUserDTO model)
        {
            try
            {
                var authUser = await AuthUserRepository.Validate_Login(model);

                if (authUser != null)
                {
                    string token = Get_Token(encription, authUser);
                    LoggedUSerResponse helper = new LoggedUSerResponse
                    {
                        User = authUser.User,
                        Token = token
                    };
                    return helper;
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Add(string.Format("Logic_Layer.Bussiness.AuthUserExecution.Validate_User {0} {1}",
                                 ResponseStrings.Error, ex.Message));
            }
            return null;
        }

        public   string Get_Token(string encription, AuthUser model)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(encription);

            var tkenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                        {
                            new Claim (ClaimTypes.NameIdentifier, model.StudentId.ToString()),
                            new Claim (ClaimTypes.Email, model.User.ToString())
                        }
                    ),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature) 
            };

            var token = tokenHandler.CreateToken(tkenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
