namespace LibMember.Repository
{
    public interface IJwtAuthenticationManager
    {
       string authenticateAsync(string username, string password);
    }
}
