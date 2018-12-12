using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Infrastructure.UnitOfWork;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("[action]/{email}")]
        public async Task<User> GetUserByEmail(string email)
        {
            return await _unitOfWork.UserRepository.Get(x=>x.Email==email).FirstOrDefaultAsync();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddUser([FromBody]User user)
        {
            if (_unitOfWork.UserRepository.Get().Any(x => x.Name == user.Name))
                return BadRequest(user);

            _unitOfWork.UserRepository.Insert(user);

            await _unitOfWork.Save();

            return Ok(_unitOfWork.UserRepository.Get(x=>x.Id == user.Id).FirstOrDefault());
        }
    }
}
