using LibMember.Model;

namespace LibMember.Repository
{
    public interface IMemberRepository
    {
       
            Task<List<Member>> GetAllAsync();
            Task<Member> GetByIdAsync(int id);
            Task<int> AddAsync(Member newMember);
            Task UpdateAsync(Member updatedMember);
            Task DeleteAsync(int id);
       

    }
}
