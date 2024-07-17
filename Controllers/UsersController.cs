using Basic_Web_API.Data;
using Basic_Web_API.Models;
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

        [HttpGet("GetUsers")]
        public IEnumerable<UsersModels> GetUsers()
        {
            string sql = @"
                SELECT [UserId]
                      ,[FirstName]
                      ,[LastName]
                      ,[Email]
                      ,[Gender]
                      ,[Active]
                  FROM TutorialAppSchema.Users";
            IEnumerable<UsersModels> Users = _dapper.LoadData<UsersModels>(sql);
            return Users;

        }

        [HttpGet("GetSingleUsers/{UserId}")]
        public UsersModels GetSingleUsers(int UserId)
        {
            string sql = @"
                SELECT [UserId]
                    ,[FirstName]
                    ,[LastName]
                    ,[Email]
                    ,[Gender]
                    ,[Active]
                FROM TutorialAppSchema.Users 
                WHERE UserId = " + UserId.ToString();

            UsersModels Users = _dapper.LoadDataSingle<UsersModels>(sql);
            return Users;
        }

        [HttpPut]
        public IActionResult EditDataUser()
        {
            string sql = @"";
            return Ok();
        }

        [HttpPost]
        public IActionResult AddDataUser()
        {
            return Ok();
        }
    }
}
