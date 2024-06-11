using Basic_Web_API.Data;
using Microsoft.AspNetCore.Mvc;

namespace Basic_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        DataContextDapper _dapper;
        public UsersController(IConfiguration config)
        {
            //Console.WriteLine(config.GetConnectionString("DefaultConnection"));
            _dapper = new DataContextDapper(config);
        }

        [HttpGet("TestConnect")]

        public DateTime TestConnect()
        {
            return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
        }

        [HttpGet("GetUsers/{fistname}")]
        public string[] GetUsers(string fistname)
        {
            string[] responseArray = new string[]
            {
                fistname,
                "test user"
            };
            return responseArray;
        }
    }
}
