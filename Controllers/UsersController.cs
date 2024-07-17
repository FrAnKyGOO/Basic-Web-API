using Basic_Web_API.Data;
using Basic_Web_API.Dtos;
using Basic_Web_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Basic_Web_API.Controllers
{
    [Route("[controller]")]
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

        [HttpPut("EditDataUser")]
        public IActionResult EditDataUser(UsersModels user)
        {
            string sql = @"
                        UPDATE TutorialAppSchema.Users
                            SET [FirstName] = '" + user.FirstName +
                                "', [LastName] = '" + user.LastName +
                                "', [Email] = '" + user.Email +
                                "', [Gender] = '" + user.Gender +
                                "', [Active] = '" + user.Active +
                            "' WHERE UserId = " + user.UserId;
            if (_dapper.ExecuteSql(sql))
            {
                return Ok();
            }

            throw new Exception("Failed to Update User");
        }

        [HttpPost("AddUsers")]
        public IActionResult AddDataUser(UsersDto user)
        {
            string sql = @"
                        INSERT INTO TutorialAppSchema.Users (
                            [FirstName],
                            [LastName],
                            [Email],
                            [Gender],
                            [Active] )
                        VALUES
                            ('" + user.FirstName +
                            "', '" + user.LastName +
                            "', '" + user.Email +
                            "', '" + user.Gender +
                            "', '" + user.Active +
                         "')";

            if (_dapper.ExecuteSql(sql))
            {
                return Ok();
            }

            throw new Exception("Failed to Update User");
        }

        [HttpDelete("DeleteUserByID/{UserId}")]
        public IActionResult DeleteUserByID(int UserId)
        {
            string sql_Update_Active = @"
                        UPDATE TutorialAppSchema.Users
                        SET [Active] = 'False' 
                        WHERE UserId = " + UserId.ToString();

            string sql_Delete = @"
                        DELETE FROM TutorialAppSchema.Users
                        WHERE UserId = " + UserId.ToString();

            if (_dapper.ExecuteSql(sql_Update_Active))
            {
                return Ok();
            }

            throw new Exception("Failed to Delete User");
        }
    }
}
