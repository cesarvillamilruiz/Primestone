using Logic_Layer.Bussiness;
using Logic_Layer.DTO;
using Logic_Layer.Helpers;
using Logic_Layer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Front.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class AuthUserController: ControllerBase
    {
        private readonly IAuthUser authUserRepository;
        private readonly IStudent studentRepository;
        private readonly AppSettings appSetings;
        public IConfiguration Configuration { get; }
        public AuthUserController(IAuthUser authUserRepository, 
                                  IStudent studentRepository, 
                                  IOptions<AppSettings> appSetings, 
                                  IConfiguration configuration)
        {
            this.authUserRepository = authUserRepository;
            this.studentRepository = studentRepository;
            this.appSetings = appSetings.Value;
            this.Configuration = configuration;
        }

        [HttpPost("Add_New_User")]
        public async Task<IActionResult> Add_New_User(CreateAuthUserDTO model)
        {
            if (ModelState.IsValid && model != null)
            {
                if (await AuthUserExecution.Insert_New_AuthUser(this.authUserRepository,
                                                                this.studentRepository,
                                                                model))
                {
                    return Ok(string.Format("{0} {1} {2}",
                            ResponseStrings.User,
                            ResponseStrings.Added,
                            ResponseStrings.Success));
                }
                return BadRequest(ResponseStrings.Error);
            }
            return BadRequest(ResponseStrings.NoValid);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginAuthUserDTO model)
        {
            if (ModelState.IsValid && model != null)
            {
                AuthUserExecution helper = new AuthUserExecution(this.appSetings);
                string encription = this.Configuration["AppSettings:Encryption"];

                LoggedUSerResponse objResponse = await helper.Validate_User(this.authUserRepository, encription, model);

                if (objResponse != null)
                {
                    return Ok(objResponse);
                }
                return BadRequest(string.Format("{0} {1}", ResponseStrings.Logged, ResponseStrings.Error));
            }
            return BadRequest(ResponseStrings.NoValid);
        }
    }
}
