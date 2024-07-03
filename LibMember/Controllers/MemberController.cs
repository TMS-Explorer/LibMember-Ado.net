using LibMember.Model;
using LibMember.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibMember.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;
        public MemberController(IMemberRepository memberRepository, IJwtAuthenticationManager jwtAuthenticationManager)
        {
            _memberRepository = memberRepository;
            _jwtAuthenticationManager = jwtAuthenticationManager;


        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserCred userCred)
        {
            var token = _jwtAuthenticationManager.authenticateAsync(userCred.UserName, userCred.Password);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);

        }

        [HttpGet]
        public async Task<IActionResult> GetMembers()
        {
            var members = await _memberRepository.GetAllAsync();
            return Ok(members);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMemberById(int id)
        {
            var members = await _memberRepository.GetByIdAsync(id);
            return Ok(members);
        }

        [HttpPost]
        public async Task<IActionResult> AddMember(Member newMember)
        {
            await _memberRepository.AddAsync(newMember);
            return Ok("Member added successfully");
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember(int id, Member updatedMember)
        {
            await _memberRepository.UpdateAsync(updatedMember);
            return Ok("Member updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            await _memberRepository.DeleteAsync(id);
            return Ok("Member deleted successfully");
        }


    }
}
