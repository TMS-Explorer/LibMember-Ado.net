using LibMember.Model;
using LibMember.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibMember.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberRepository _memberRepository;
        public MemberController(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
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
